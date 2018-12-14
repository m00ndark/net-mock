using System.Collections.Generic;
using System.Text;
using NetMock.Rest;
using NetMock.Utils;

namespace NetMock.Exceptions
{
	public class StrictMockException : NetMockException
	{
		public StrictMockException(string message, IEnumerable<IReceivedRequest> unmatchedRequests) : base(message)
		{
			UnmatchedRequests = unmatchedRequests;
		}

		public IEnumerable<IReceivedRequest> UnmatchedRequests { get; }

		public override string ToString()
		{
			StringBuilder result = new StringBuilder(Message);
			result.AppendLine();
			result.AppendLine();
			UnmatchedRequests.Print(output => result.AppendLine(output), RestMock.DefaultRequestPrinterSelectors);
			return result.ToString();
		}
	}
}
