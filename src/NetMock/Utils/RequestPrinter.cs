using System;
using System.Collections.Generic;
using System.Linq;
using NetMock.Rest;

namespace NetMock.Utils
{
	public static class RequestPrinter
	{
		private const string DEFAULT_SEPARATOR = " ";
		private static readonly Action<string> _defaultPrinter = Console.WriteLine;

		public static void Print(this IEnumerable<IReceivedRequest> receivedRequests, params Func<IReceivedRequest, string>[] selectors)
			=> receivedRequests.Print(selectors.AsEnumerable());

		public static void Print(this IEnumerable<IReceivedRequest> receivedRequests, IEnumerable<Func<IReceivedRequest, string>> selectors)
			=> receivedRequests.Print(DEFAULT_SEPARATOR, selectors);

		public static void Print(this IEnumerable<IReceivedRequest> receivedRequests, string separator, params Func<IReceivedRequest, string>[] selectors)
			=> receivedRequests.Print(separator, selectors.AsEnumerable());

		public static void Print(this IEnumerable<IReceivedRequest> receivedRequests, string separator, IEnumerable<Func<IReceivedRequest, string>> selectors)
			=> receivedRequests.Print(_defaultPrinter, separator, selectors);

		public static void Print(this IEnumerable<IReceivedRequest> receivedRequests, Action<string> printer, params Func<IReceivedRequest, string>[] selectors)
			=> receivedRequests.Print(printer, selectors.AsEnumerable());

		public static void Print(this IEnumerable<IReceivedRequest> receivedRequests, Action<string> printer, IEnumerable<Func<IReceivedRequest, string>> selectors)
			=> receivedRequests.Print(printer, DEFAULT_SEPARATOR, selectors);

		public static void Print(this IEnumerable<IReceivedRequest> receivedRequests, Action<string> printer, string separator, params Func<IReceivedRequest, string>[] selectors)
			=> receivedRequests.Print(printer, separator, selectors.AsEnumerable());

		public static void Print(this IEnumerable<IReceivedRequest> receivedRequests, Action<string> printer, string separator, IEnumerable<Func<IReceivedRequest, string>> selectors)
		{
			IList<Func<IReceivedRequest, string>> selectorList = selectors as IList<Func<IReceivedRequest, string>> ?? selectors.ToArray();

			foreach (IReceivedRequest receivedRequest in receivedRequests)
				receivedRequest.Print(printer, separator, selectorList);
		}

		public static void Print(this IReceivedRequest receivedRequest, params Func<IReceivedRequest, string>[] selectors)
			=> receivedRequest.Print(selectors.AsEnumerable());

		public static void Print(this IReceivedRequest receivedRequest, IEnumerable<Func<IReceivedRequest, string>> selectors)
			=> receivedRequest.Print(DEFAULT_SEPARATOR, selectors);

		public static void Print(this IReceivedRequest receivedRequest, string separator, params Func<IReceivedRequest, string>[] selectors)
			=> receivedRequest.Print(separator, selectors.AsEnumerable());

		public static void Print(this IReceivedRequest receivedRequest, string separator, IEnumerable<Func<IReceivedRequest, string>> selectors)
			=> receivedRequest.Print(_defaultPrinter, separator, selectors);

		public static void Print(this IReceivedRequest receivedRequest, Action<string> printer, params Func<IReceivedRequest, string>[] selectors)
			=> receivedRequest.Print(printer, selectors.AsEnumerable());

		public static void Print(this IReceivedRequest receivedRequest, Action<string> printer, IEnumerable<Func<IReceivedRequest, string>> selectors)
			=> receivedRequest.Print(printer, DEFAULT_SEPARATOR, selectors);

		public static void Print(this IReceivedRequest receivedRequest, Action<string> printer, string separator, params Func<IReceivedRequest, string>[] selectors)
			=> receivedRequest.Print(printer, separator, selectors.AsEnumerable());

		public static void Print(this IReceivedRequest receivedRequest, Action<string> printer, string separator, IEnumerable<Func<IReceivedRequest, string>> selectors)
		{
			if (receivedRequest == null)
				return;

			IList<Func<IReceivedRequest, string>> selectorList = selectors as IList<Func<IReceivedRequest, string>> ?? selectors.ToArray();

			printer(string.Join(separator, selectorList.Select(selector => selector(receivedRequest))));
		}
	}
}
