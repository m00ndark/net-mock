using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetMock.Utils;

namespace NetMock.Rest
{
	internal class ReceivedRequest
	{
		public ReceivedRequest(HttpListenerRequest request)
		{
			Uri = request.Url;
			Body = request.GetBody();
			Headers = request.Headers
				.Cast<string>()
				.ToDictionary(name => name, name => request.Headers[name]);
		}

		public Uri Uri { get; set; }
		public string Body { get; set; }
		public IDictionary<string, string> Headers { get; set; }
	}
}
