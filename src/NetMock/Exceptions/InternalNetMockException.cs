using System;
using System.Collections.Generic;
using System.Linq;

namespace NetMock.Exceptions
{
	public class InternalNetMockException : NetMockException
	{
		public InternalNetMockException(string message) : base(message) { }
		public InternalNetMockException(string message, IEnumerable<Exception> unexpectedExceptions) : base(message)
		{
			UnexpectedExceptions = unexpectedExceptions;
		}

		public IEnumerable<Exception> UnexpectedExceptions { get; }

		public override string ToString()
		{
			return Message + string.Join(string.Empty, UnexpectedExceptions.Select(ex => Environment.NewLine + Environment.NewLine + ex.ToString()));
		}
	}
}
