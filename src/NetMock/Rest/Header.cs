using System;

namespace NetMock.Rest
{
	public static class Header
	{
		public static IMatch Is(string name, string value)
			=> Is(name, value, CompareCase.Insensitive);

		public static IMatch Is(string name, string value, CompareCase compareCase)
			=> new HeaderMatch(HeaderMatchOperation.Is, name, value, compareCase);

		public static IMatch Is(string name, Func<string, bool> condition)
			=> new HeaderMatch(HeaderMatchOperation.Is, name, condition);

		public static IMatch IsNot(string name, string value)
			=> IsNot(name, value, CompareCase.Insensitive);

		public static IMatch IsNot(string name, string value, CompareCase compareCase)
			=> new HeaderMatch(HeaderMatchOperation.IsNot, name, value, compareCase);

		public static IMatch IsSet(string name)
			=> new HeaderMatch(HeaderMatchOperation.IsSet, name);

		public static IMatch IsNotSet(string name)
			=> new HeaderMatch(HeaderMatchOperation.IsNotSet, name);

		public static IMatch Contains(string name, string value)
			=> Contains(name, value, CompareCase.Insensitive);

		public static IMatch Contains(string name, string value, CompareCase compareCase)
			=> new HeaderMatch(HeaderMatchOperation.Contains, name, value, compareCase);

		public static IMatch NotContains(string name, string value)
			=> NotContains(name, value, CompareCase.Insensitive);

		public static IMatch NotContains(string name, string value, CompareCase compareCase)
			=> new HeaderMatch(HeaderMatchOperation.NotContains, name, value, compareCase);
	}
}
