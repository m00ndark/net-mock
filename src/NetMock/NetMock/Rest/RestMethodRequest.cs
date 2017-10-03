using System;
using System.Collections.Generic;
using System.Text;

namespace NetMock.Rest
{
	public class RestMethodRequest
	{
		private RestMethodResponse _response;

		public RestMethodRequest(Method method, string path, string body)
			: this(method, path)
		{
			Body = body;
		}

		public RestMethodRequest(Method method, string path)
		{
			Method = method;
			Path = path;
		}

		public Method Method { get; }
		public string Path { get; }
		public string Body { get; }

		public RestMethodResponse Returns(object body)
		{
			return _response = new RestMethodResponse(body);
		}
	}
}
