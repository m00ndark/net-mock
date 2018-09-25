using System;
using System.Collections.Generic;
using RestSharp;

namespace NetMock.Tests.Utils
{
	public class Client
	{
		public Client(string scheme, string baseUri, int port)
		{
			RestClient = new RestClient(new UriBuilder(scheme, "localhost", port, baseUri).Uri);
		}

		public RestClient RestClient { get; }

		public IRestResponse MakeRequest(
			Method method,
			string path,
			IDictionary<string, string> parameters = null,
			IDictionary<string, string> headers = null,
			string body = null)
		{
			RestRequest request = new RestRequest(path, method) { RequestFormat = DataFormat.Json };
			if (parameters != null)
			{
				foreach (var parameter in parameters)
				{
					request.AddParameter(parameter.Key, parameter.Value, path.Contains($"{{{parameter.Key}}}")
						? ParameterType.UrlSegment
						: ParameterType.QueryString);
				}
			}

			if (headers != null)
			{
				foreach (var header in headers)
				{
					request.AddHeader(header.Key, header.Value);
				}
			}

			if (body != null)
			{
				request.AddParameter(request.JsonSerializer.ContentType, body, ParameterType.RequestBody);
				//request.AddBody(body);
			}

			return RestClient.Execute(request);
		}

		public IRestResponse Get(string path, IDictionary<string, string> parameters = null, IDictionary<string, string> headers = null)
			=> MakeRequest(Method.GET, path, parameters, headers);

		public IRestResponse Post(string path, IDictionary<string, string> parameters = null, IDictionary<string, string> headers = null, string body = null)
			=> MakeRequest(Method.POST, path, parameters, headers, body);

		public IRestResponse Put(string path, IDictionary<string, string> parameters = null, IDictionary<string, string> headers = null, string body = null)
			=> MakeRequest(Method.PUT, path, parameters, headers, body);

		public IRestResponse Delete(string path, IDictionary<string, string> parameters = null, IDictionary<string, string> headers = null, string body = null)
			=> MakeRequest(Method.DELETE, path, parameters, headers, body);

		public IRestResponse Head(string path, IDictionary<string, string> parameters = null, IDictionary<string, string> headers = null)
			=> MakeRequest(Method.HEAD, path, parameters, headers);
	}
}
