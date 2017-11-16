using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetMock.Utils;

namespace NetMock.Rest
{
	public interface IReceivedRequest
	{
		string Method { get; }
		Uri Uri { get; }
		string Body { get; }
		IDictionary<string, string> Headers { get; }
	}

	internal class ReceivedRequest : IReceivedRequest
	{
		public ReceivedRequest(HttpListenerRequest httpRequest)
		{
			Method = httpRequest.HttpMethod;
			Uri = httpRequest.Url;
			Body = httpRequest.GetBody();
			Headers = httpRequest.Headers
				.Cast<string>()
				.ToDictionary(name => name, name => httpRequest.Headers[name]);
		}

		public string Method { get; }
		public Uri Uri { get; }
		public string Body { get; }
		public IDictionary<string, string> Headers { get; }
	}
}
