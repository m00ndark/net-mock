using System;
using System.Net;

namespace NetMock.Exceptions
{
    public class StatusCodeException : NetMockException
    {
	    public StatusCodeException(HttpStatusCode statusCode) : this((int) statusCode) { }
	    public StatusCodeException(HttpStatusCode statusCode, string message) : this((int) statusCode, message) { }
	    public StatusCodeException(HttpStatusCode statusCode, string message, Exception innerException) : this((int) statusCode, message, innerException) { }

	    public StatusCodeException(int statusCode) : base(null)
	    {
		    StatusCode = statusCode;
	    }

	    public StatusCodeException(int statusCode, string message) : base(message)
	    {
		    StatusCode = statusCode;
	    }

	    public StatusCodeException(int statusCode, string message, Exception innerException) : base(message, innerException)
	    {
		    StatusCode = statusCode;
	    }

		public int StatusCode { get; }
    }
}
