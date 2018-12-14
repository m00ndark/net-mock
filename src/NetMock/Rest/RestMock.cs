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
			public static HttpStatusCode DefaultResponseStatusCode { get; set; } = HttpStatusCode.NotImplemented;
			public static UndefinedHandling UndefinedQueryParameterHandling { get; set; } = UndefinedHandling.Fail;
			public static UndefinedHandling UndefinedHeaderHandling { get; set; } = UndefinedHandling.Ignore;
			public static MockBehavior? MockBehavior { get; set; } = null;
			public static bool InterpretBodyAsJson { get; set; } = true;
		}

		private readonly HttpListenerController _httpListener;
		private readonly List<RestRequestSetup> _requestDefinitions;
		private readonly List<IReceivedRequest> _receivedRequests;
		private readonly List<Exception> _unexpectedExceptions;
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
			_unexpectedExceptions = new List<Exception>();
			_mockBehavior = mockBehavior;
			_isActivated = false;

			ServiceMock = serviceMock;
			BasePath = basePath;
			Port = port;
			Scheme = scheme;
			Certificate = certificate;
		}

		public static IEnumerable<Func<IReceivedRequest, string>> DefaultRequestPrinterSelectors { get; } = new Func<IReceivedRequest, string>[]
			{
				x => x.Method,
				x => x.Uri.ToString(),
				x => $"(bodyLength: {x.Body.Length}, headers: [{string.Join(" | ", x.Headers.Select(y => $"{y.Key}={y.Value}"))}])"
			};

		internal ServiceMock ServiceMock { get; }
		public string BasePath { get; }
		public int Port { get; }
		public Scheme Scheme { get; }
		public X509Certificate2 Certificate { get; }
		public StaticHeaders StaticHeaders { get; } = new StaticHeaders();

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

		internal Uri BaseUri => new UriBuilder(Scheme == Scheme.Http ? Uri.UriSchemeHttp : Uri.UriSchemeHttps, "localhost", Port, BasePath).Uri;

		public void Activate()
		{
			if (_isActivated)
				throw new InvalidOperationException("Already activated");

			if (ServiceMock.ActivationStrategy == ActivationStrategy.Manual)
				ParseRequestDefinitions();

			_httpListener.StartListening(BaseUri, useWildcardHost: true);
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

				if (_unexpectedExceptions.Any())
				{
					string exceptionOutput = _unexpectedExceptions.Aggregate(Environment.NewLine, (output, ex) => output + $"[{ex.GetType().Name}] {ex.Message.Replace(Environment.NewLine, " ")}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}");
					throw new InternalNetMockException($"{Environment.NewLine}Unexpected exceptions caught:{exceptionOutput}", _unexpectedExceptions);
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
				_unexpectedExceptions.Clear();
			}
		}

		public void PrintReceivedRequests()
			=> _receivedRequests.Print(DefaultRequestPrinterSelectors);

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
			HttpResponse response = new HttpResponse { Headers = StaticHeaders };

			try
			{
				ReceivedRequest request = _receivedRequests.AddAndReturn(new ReceivedRequest(httpRequest));

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
				_unexpectedExceptions.Add(ex);
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
