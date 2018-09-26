using System;

namespace NetMock.Exceptions
{
	public class NetMockException : Exception
	{
		public NetMockException(string message) : base(message) { }
		public NetMockException(string message, Exception innerException) : base(message, innerException) { }
	}
}
