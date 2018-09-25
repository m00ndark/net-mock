using System.Collections.Generic;

namespace NetMock.Server
{
	internal class HttpResponse
	{
		public int StatusCode { get; set; } = -1;
		public string Body { get; set; }
		public IDictionary<string, string> Headers { get; set; }
	}
}
