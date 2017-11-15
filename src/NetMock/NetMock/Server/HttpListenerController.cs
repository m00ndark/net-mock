using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using NetMock.Exceptions;
using NetMock.Utils;

namespace NetMock.Server
{
	public class HttpListenerController
	{
		public const string WILDCARD_HOST = "://+";
		public const string WILDCARD_HOST_REPLACEMENT = "://localhost";

		private readonly Func<HttpListenerRequest, string> _requestCallback;
		private readonly X509Certificate2 _certificate;
		private HttpListener _httpListener;
		private int _prefixPort;

		static HttpListenerController()
		{
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
		}

		public HttpListenerController(Func<HttpListenerRequest, string> requestCallback, X509Certificate2 certificate = null)
		{
			if (!HttpListener.IsSupported)
				throw new NotSupportedException("Needs Windows XP SP2, Server 2003 or later");

			_requestCallback = requestCallback;
			_certificate = certificate;
			_httpListener = null;
			_prefixPort = -1;
		}

		public bool IsListening => _httpListener != null;

		public void StartListening(Uri uriPrefix, bool useWildcardHost)
		{
			if (IsListening)
				throw new InvalidOperationException();

			try
			{
				if (uriPrefix.Scheme == Uri.UriSchemeHttps)
				{
					if (_certificate == null)
						throw new CertificateException("Certifiace not provided. Unable to listen to https without binding certificate.");

					_prefixPort = uriPrefix.Port;
					CertificateUtil.BindCertificate(_certificate, _prefixPort);
				}

				string prefix = useWildcardHost
					? uriPrefix.ToString().Replace(WILDCARD_HOST_REPLACEMENT, WILDCARD_HOST)
					: uriPrefix.ToString();

				_httpListener = new HttpListener();
				_httpListener.Prefixes.Add(prefix + '/');
				_httpListener.Start();

				ThreadPool.QueueUserWorkItem(_ =>
					{
						try
						{
							while (_httpListener.IsListening)
							{
								ThreadPool.QueueUserWorkItem(obj =>
									{
										HttpListenerContext context = obj as HttpListenerContext;
										try
										{
											if (context == null)
												return;

											string response = _requestCallback(context.Request);

											WriteResponse(context, response, 200);
										}
										catch (StatusCodeException ex)
										{
											WriteResponse(context, ex.ToString(), ex.StatusCode);
										}
										catch (Exception ex)
										{
											WriteResponse(context, ex.ToString(), 500);
										}
										finally
										{
											context?.Response.OutputStream.Close();
										}
									}, _httpListener.GetContext());
							}
						}
						catch
						{
							// suppress any exceptions
						}
					});

			}
			catch
			{
				_httpListener?.Close();
				_httpListener = null;
				throw;
			}
		}

		private static void WriteResponse(HttpListenerContext context, string response, int statusCode)
		{
			context.Response.StatusCode = statusCode;

			if (response == null)
				return;

			byte[] responseBytes = Encoding.UTF8.GetBytes(response);
			context.Response.ContentLength64 = responseBytes.Length;
			context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
		}

		public void StopListening()
		{
			if (!IsListening)
				return;

			try
			{
				_httpListener?.Stop();
				_httpListener?.Close();
				_httpListener = null;

				if (_prefixPort > -1)
				{
					CertificateUtil.UnbindCertificate(_prefixPort);
					_prefixPort = -1;
				}
			}
			catch
			{
				// suppress any exceptions
			}
		}
	}
}
