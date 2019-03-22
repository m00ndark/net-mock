using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using NetMock.Exceptions;
using NetMock.Utils;

namespace NetMock.Server
{
	internal class HttpListenerController
	{
		public const string LOCALHOST = "localhost";
		public const string WILDCARD_HOST = "://+";
		public const string WILDCARD_HOST_REPLACEMENT = "://" + LOCALHOST;

		private readonly Func<HttpListenerRequest, HttpResponse> _requestCallback;
		private readonly X509Certificate2 _certificate;
		private HttpListener _httpListener;
		private int _prefixPort;

		static HttpListenerController()
		{
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
		}

		public HttpListenerController(Func<HttpListenerRequest, HttpResponse> requestCallback, X509Certificate2 certificate = null)
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
						throw new CertificateException("Certificate not provided. Unable to listen to https without binding certificate.");

					_prefixPort = uriPrefix.Port;
					CertificateUtil.BindCertificate(_certificate, _prefixPort);
				}

				string prefix = useWildcardHost
					? uriPrefix.ToString().Replace(WILDCARD_HOST_REPLACEMENT, WILDCARD_HOST)
					: uriPrefix.ToString();

				_httpListener = new HttpListener();
				_httpListener.Prefixes.Add(prefix.TrimEnd('/') + '/');
				_httpListener.Start();

				ThreadPool.QueueUserWorkItem(_ =>
					{
						try
						{
							while (_httpListener?.IsListening ?? false)
							{
								ThreadPool.QueueUserWorkItem(obj =>
									{
										if (!(obj is HttpListenerContext context))
											return;

										try
										{
											HttpResponse response;

											try
											{
												response = _requestCallback(context.Request);
											}
											catch (StatusCodeException ex)
											{
												response = new HttpResponse { Body = ex.ToString(), StatusCode = ex.StatusCode };
											}
											catch (Exception ex) when (IsListening || !(ex is HttpListenerException))
											{
												response = new HttpResponse { Body = ex.ToString(), StatusCode = (int) HttpStatusCode.InternalServerError };
											}

											WriteResponse(context, response);
										}
										catch (HttpListenerException) when (!IsListening)
										{
											// .. tearing down
										}
										catch (Exception ex)
										{
											Console.WriteLine($"Unhandled exception in {nameof(HttpListenerController)}: {ex}");
										}
										finally
										{
											try
											{
												context.Response.OutputStream.Close();
											}
											catch
											{
												// likely due to tear down.. suppress
											}
										}
									}, _httpListener?.GetContext());
							}
						}
						catch
						{
							// likely due to tear down.. suppress
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

		private static void WriteResponse(HttpListenerContext context, HttpResponse response)
		{
			context.Response.StatusCode = response.StatusCode;

			if (response.Headers != null)
			{
				foreach (KeyValuePair<string, string> header in response.Headers)
				{
					context.Response.Headers.Add(header.Key, header.Value);
				}
			}

			if (string.IsNullOrEmpty(response.Body))
				return;

			byte[] bodyBytes = Encoding.UTF8.GetBytes(response.Body);
			context.Response.ContentLength64 = bodyBytes.Length;
			context.Response.OutputStream.Write(bodyBytes, 0, bodyBytes.Length);
		}

		public void StopListening()
		{
			if (!IsListening)
				return;

			try
			{
				HttpListener httpListener = _httpListener;
				_httpListener = null;
				httpListener?.Stop();
				httpListener?.Close();

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
