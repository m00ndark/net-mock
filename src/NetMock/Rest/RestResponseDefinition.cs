using System;
using System.Collections.Generic;

namespace NetMock.Rest
{
	public class RestResponseDefinition
	{
		internal RestResponseDefinition(int statusCode, IEnumerable<AttachedHeader> headers = null)
		{
			StatusCode = statusCode;
			Headers = headers;
		}

		internal RestResponseDefinition(object body, IEnumerable<AttachedHeader> headers = null)
		{
			Body = body;
			Headers = headers;
		}

		internal RestResponseDefinition(int statusCode, object body, IEnumerable<AttachedHeader> headers = null)
		{
			StatusCode = statusCode;
			Body = body;
			Headers = headers;
		}

		internal RestResponseDefinition(Delegate bodyProvider, IEnumerable<AttachedHeader> headers = null)
		{
			BodyProvider = bodyProvider;
			Headers = headers;
		}

		internal RestResponseDefinition(int statusCode, Delegate bodyProvider, IEnumerable<AttachedHeader> headers = null)
		{
			StatusCode = statusCode;
			BodyProvider = bodyProvider;
			Headers = headers;
		}

		internal RestResponseDefinition(Func<(int, object, IEnumerable<AttachedHeader>)> responseProvider)
		{
			ResponseProvider = responseProvider;
		}

		public int StatusCode { get; } = -1;
		public IEnumerable<AttachedHeader> Headers { get; }
		public object Body { get; }
		public Delegate BodyProvider { get; }
		public Func<(int, object, IEnumerable<AttachedHeader>)> ResponseProvider { get; }
	}
}
