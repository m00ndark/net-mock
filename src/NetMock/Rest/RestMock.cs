using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using NetMock.Exceptions;
using NetMock.Server;
using NetMock.Utils;
using Newtonsoft.Json;

namespace NetMock.Rest
{
	public partial class RestMock : INetMock
	{
		private readonly HttpListenerController _httpListener;
		private readonly List<RestRequestSetup> _requestDefinitions;
		private readonly List<IReceivedRequest> _receivedRequests;
		private readonly List<Exception> _unexpectedExceptions;
		private bool _isActivated;

		internal RestMock(string basePath, int port, Scheme scheme, X509Certificate2 certificate = null, MockBehavior mockBehavior = MockBehavior.Loose)
		{
			_httpListener = new HttpListenerController(HandleRequest, certificate);
			_requestDefinitions = new List<RestRequestSetup>();
			_receivedRequests = new List<IReceivedRequest>();
			_unexpectedExceptions = new List<Exception>();
			_isActivated = false;

			BasePath = basePath;
			Port = port;
			Scheme = scheme;
			Certificate = certificate;
			MockBehavior = mockBehavior;
		}

		public static IEnumerable<Func<IReceivedRequest, string>> DefaultRequestPrinterSelectors { get; } = new Func<IReceivedRequest, string>[]
			{
				x => x.Method,
				x => x.Uri.ToString(),
				x => $"(bodyLength: {x.Body.Length}, headers: [{string.Join(" | ", x.Headers.Select(y => $"{y.Key}={y.Value}"))}])"
			};

		public string BasePath { get; }
		public int Port { get; }
		public Scheme Scheme { get; }
		public X509Certificate2 Certificate { get; }
		public StaticHeaders StaticHeaders { get; } = new StaticHeaders();
		public HttpStatusCode DefaultResponseStatusCode { get; set; } = HttpStatusCode.NotImplemented;
		public UndefinedHandling UndefinedQueryParameterHandling { get; set; } = UndefinedHandling.Fail;
		public UndefinedHandling UndefinedHeaderHandling { get; set; } = UndefinedHandling.Ignore;
		public MockBehavior MockBehavior { get; set; }
		public bool InterpretBodyAsJson { get; set; } = true;

		internal Uri BaseUri => new UriBuilder(Scheme == Scheme.Http ? Uri.UriSchemeHttp : Uri.UriSchemeHttps, "localhost", Port, BasePath).Uri;

		public void Activate()
		{
			if (_isActivated)
				throw new InvalidOperationException("Already activated");

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
					throw new InternalNetMockException("Unexpected exceptions caught", _unexpectedExceptions);

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

				var matchedRequestDefinition = _requestDefinitions
					.Select(requestDefinition => new
						{
							RequestDefinition = requestDefinition,
							IsMatch = requestDefinition.Match(request, out IList<MatchResult> matchResult),
							MatchResult = matchResult
						})
					.LastOrDefault(x => x.IsMatch);

				if (matchedRequestDefinition?.RequestDefinition.Response != null)
				{
					object body = matchedRequestDefinition.RequestDefinition.Response.Body;
					response.Body = body == null ? null : body is string bodyStr ? bodyStr : JsonConvert.SerializeObject(body);

					response.StatusCode = matchedRequestDefinition.RequestDefinition.Response.StatusCode;

					response.Headers.Apply(matchedRequestDefinition.RequestDefinition.Response.Headers
						.Select(header => (KeyValuePair<string, string>) header));
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
