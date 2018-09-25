using System;

namespace NetMock.Exceptions
{
	public class MockSetupException : NetMockException
	{
		public MockSetupException(string message) : base(message) { }
		public MockSetupException(string message, Exception innerException) : base(message, innerException) { }
	}
}
