using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using NetMock.Exceptions;
using NetMock.Server;
using NetMock.Utils;

namespace NetMock.Rest
{
	public partial class RestMock : INetMock
	{
		public static class GlobalConfig
		{
			public static StaticHeaders StaticResponseHeaders { get; } = new StaticHeaders();
			public static HttpStatusCode DefaultResponseStatusCode { get; set; } = HttpStatusCode.NotImplemented;
			public static UndefinedHandling UndefinedQueryParameterHandling { get; set; } = UndefinedHandling.Fail;
			public static UndefinedHandling UndefinedHeaderHandling { get; set; } = UndefinedHandling.Ignore;
			public static MockBehavior? MockBehavior { get; set; } = null;
			public static bool InterpretBodyAsJson { get; set; } = true;
			public static bool UseWildcardHostWhenListening { get; set; } = true;
		}

		private class UnhandledRequestExceptionData
		{
			public UnhandledRequestExceptionData(Exception exception, ReceivedRequest request)
			{
				Exception = exception;
				Request = request;
			}

			public Exception Exception { get; }
			public ReceivedRequest Request { get; }

			public override string ToString()
			{
				string printedRequest = null;
				Request.Print(request => printedRequest = request, String.NL, DefaultRequestPrinterSelectors_WithBody);

				return string.Join(String.NL + String.LINE + String.NL, Exception.ToStringEx().Select(x => $"[{x.ExceptionType.Name}] {x.Message}" + String.NL + x.StackTrace))
					+ (printedRequest != null
						? String.NL + String.LINE + String.NL + $"[{Request.GetType().Name}]" + (String.NL + printedRequest).Replace(String.NL, String.NL + ">  ")
						: string.Empty);
			}
		}

		private readonly HttpListenerController _httpListener;
		private readonly List<RestRequestSetup> _requestDefinitions;
		private readonly List<IReceivedRequest> _receivedRequests;
		private readonly List<UnhandledRequestExceptionData> _unhandledExceptions;
		private HttpStatusCode? _defaultResponseStatusCode;
		private UndefinedHandling? _undefinedQueryParameterHandling;
		private UndefinedHandling? _undefinedHeaderHandling;
		private MockBehavior? _mockBehavior;
		private bool? _interpretBodyAsJson;
		private bool _isActivated;

		internal RestMock(ServiceMock serviceMock, string basePath, int port, Scheme scheme, X509Certificate2 certificate = null, MockBehavior? mockBehavior = null)
		{
			_httpListener = new HttpListenerController(HandleRequest, certificate);
			_requestDefinitions = new List<RestRequestSetup>();
			_receivedRequests = new List<IReceivedRequest>();
			_unhandledExceptions = new List<UnhandledRequestExceptionData>();
			_mockBehavior = mockBehavior;
			_isActivated = false;

			ServiceMock = serviceMock;
			BasePath = basePath;
			Port = port;
			Scheme = scheme;
			Certificate = certificate;
		}

		public static IEnumerable<Func<IReceivedRequest, string>> DefaultRequestPrinterSelectors_WithoutBody { get; } = new Func<IReceivedRequest, string>[]
			{
				x => x.Method,
				x => x.Uri.ToString(),
				x => $"(bodyLength: {x.Body.Length}, headers: [{string.Join(" | ", x.Headers.Select(y => $"{y.Key}={y.Value}"))}])"
			};

		public static IEnumerable<Func<IReceivedRequest, string>> DefaultRequestPrinterSelectors_WithBody { get; } = new Func<IReceivedRequest, string>[]
			{
				x => $"{x.Method} {x.Uri}",
				x => string.Join(String.NL, x.Headers.Select(y => $"{y.Key}={y.Value}")),
				x => String.NL + x.Body
			};

		internal ServiceMock ServiceMock { get; }
		public string BasePath { get; }
		public int Port { get; }
		public Scheme Scheme { get; }
		public X509Certificate2 Certificate { get; }
		public StaticHeaders StaticResponseHeaders { get; } = new StaticHeaders();

		public HttpStatusCode DefaultResponseStatusCode
		{
			get => _defaultResponseStatusCode ?? GlobalConfig.DefaultResponseStatusCode;
			set => _defaultResponseStatusCode = value;
		}

		public UndefinedHandling UndefinedQueryParameterHandling
		{
			get => _undefinedQueryParameterHandling ?? GlobalConfig.UndefinedQueryParameterHandling;
			set => _undefinedQueryParameterHandling = value;
		}

		public UndefinedHandling UndefinedHeaderHandling
		{
			get => _undefinedHeaderHandling ?? GlobalConfig.UndefinedHeaderHandling;
			set => _undefinedHeaderHandling = value;
		}

		public MockBehavior MockBehavior
		{
			get => _mockBehavior ?? GlobalConfig.MockBehavior ?? ServiceMock.MockBehavior;
			set => _mockBehavior = value;
		}

		public bool InterpretBodyAsJson
		{
			get => _interpretBodyAsJson ?? GlobalConfig.InterpretBodyAsJson;
			set => _interpretBodyAsJson = value;
		}

		internal Uri BaseUri => new UriBuilder(Scheme == Scheme.Http ? Uri.UriSchemeHttp : Uri.UriSchemeHttps, HttpListenerController.LOCALHOST, Port, BasePath).Uri;

		public void Activate()
		{
			if (_isActivated)
				throw new InvalidOperationException("Already activated");

			if (ServiceMock.ActivationStrategy == ActivationStrategy.Manual)
				ParseRequestDefinitions();

			_httpListener.StartListening(BaseUri, GlobalConfig.UseWildcardHostWhenListening);
			_isActivated = true;
		}

		public void TearDown()
		{
			if (!_isActivated)
				return;

			try
			{
				_isActivated = false;
				_httpListener.StopListening();

				if (_unhandledExceptions.Any())
				{
					string exceptionOutput = string.Join(String.NL + String.NL, _unhandledExceptions.Select((x, i) => $"(#{i})" + String.NL + x.ToString()));
					throw new InternalNetMockException("Unhandled exceptions caught while handling requests" + String.NL + exceptionOutput);
				}

				if (MockBehavior == MockBehavior.Strict)
				{
					IEnumerable<IReceivedRequest> unmatchedRequests = _receivedRequests
						.Where(request => _requestDefinitions.All(requestDefinition => !requestDefinition.Match(request, out IList<MatchResult> matchResult)))
						.ToArray();

					if (unmatchedRequests.Any())
						throw new StrictMockException("With mock behavior Strict, all requests must have a corresponding setup", unmatchedRequests);
				}
			}
			finally
			{
				_receivedRequests.Clear();
				_requestDefinitions.Clear();
				_unhandledExceptions.Clear();
			}
		}

		public void PrintReceivedRequests()
			=> _receivedRequests.Print(DefaultRequestPrinterSelectors_WithoutBody);

		public void PrintReceivedRequests(params Func<IReceivedRequest, string>[] selectors)
			=> _receivedRequests.Print(selectors);

		public void PrintReceivedRequests(string separator, params Func<IReceivedRequest, string>[] selectors)
			=> _receivedRequests.Print(separator, selectors);

		public void PrintReceivedRequests(Action<string> printer, string separator, params Func<IReceivedRequest, string>[] selectors)
			=> _receivedRequests.Print(printer, separator, selectors);

		private void ParseRequestDefinitions()
		{
			_requestDefinitions.ForEach(requestDefinition => requestDefinition.Parse());
		}

		private HttpResponse HandleRequest(HttpListenerRequest httpRequest)
		{
			ReceivedRequest request = null;
			HttpResponse response = new HttpResponse { Headers = StaticResponseHeaders.MergeWith(GlobalConfig.StaticResponseHeaders) };

			try
			{
				request = _receivedRequests.AddAndReturn(new ReceivedRequest(httpRequest));

				var match = _requestDefinitions
					.Select(requestDefinition => new
						{
							RequestDefinition = requestDefinition,
							IsMatch = requestDefinition.Match(request, out IList<MatchResult> matchResult),
							MatchResult = matchResult
						})
					.LastOrDefault(x => x.IsMatch);

				if (match?.RequestDefinition.Response != null)
				{
					(int StatusCode, string Body, IEnumerable<AttachedHeader> Headers) providedResponse =
						match.RequestDefinition.Response.Provide(request, match.MatchResult);

					response.StatusCode = providedResponse.StatusCode;
					response.Headers.Apply(providedResponse.Headers?.Select(header => (KeyValuePair<string, string>) header));
					response.Body = providedResponse.Body;
				}
				else
				{
					match?.RequestDefinition.TryCallback(request, match.MatchResult);
				}
			}
			catch (Exception ex)
			{
				_unhandledExceptions.Add(new UnhandledRequestExceptionData(ex, request));
				throw new StatusCodeException(HttpStatusCode.InternalServerError, "Unhandled exception", ex);
			}

			if (response.StatusCode == -1)
			{
				response.StatusCode = (int) DefaultResponseStatusCode;
			}

			return response;
		}
	}
}
