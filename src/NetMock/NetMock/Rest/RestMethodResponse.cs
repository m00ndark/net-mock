using System;
using System.Collections.Generic;
using System.Text;

namespace NetMock.Rest
{
	public class RestMethodResponse
	{
		public RestMethodResponse(object body)
		{
			Body = body;
		}

		public object Body { get; }
	}
}
