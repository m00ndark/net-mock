using System;

namespace NetMock.Rest
{
	public static class Header
	{
		public static IMatch Is<TValue>(string name, TValue value)
			=> Is(name, value, CompareCase.Insensitive);

		public static IMatch Is<TValue>(string name, TValue value, CompareCase compareCase)
			=> new HeaderMatch<TValue>(HeaderMatchOperation.Is, name, value, compareCase);

		public static IMatch Is(string name, Func<string, bool> condition)
			=> Is<string>(name, condition);

		public static IMatch Is<TValue>(string name, Func<TValue, bool> condition)
			=> new HeaderMatch<TValue>(HeaderMatchOperation.Is, name, condition);

		public static IMatch IsNot<TValue>(string name, TValue value)
			=> IsNot(name, value, CompareCase.Insensitive);

		public static IMatch IsNot<TValue>(string name, TValue value, CompareCase compareCase)
			=> new HeaderMatch<TValue>(HeaderMatchOperation.IsNot, name, value, compareCase);

		public static IMatch IsSet(string name)
			=> new HeaderMatch<string>(HeaderMatchOperation.IsSet, name);

		public static IMatch IsNotSet(string name)
			=> new HeaderMatch<string>(HeaderMatchOperation.IsNotSet, name);

		public static IMatch Contains<TValue>(string name, TValue value)
			=> Contains(name, value, CompareCase.Insensitive);

		public static IMatch Contains<TValue>(string name, TValue value, CompareCase compareCase)
			=> new HeaderMatch<TValue>(HeaderMatchOperation.Contains, name, value, compareCase);

		public static IMatch NotContains<TValue>(string name, TValue value)
			=> NotContains(name, value, CompareCase.Insensitive);

		public static IMatch NotContains<TValue>(string name, TValue value, CompareCase compareCase)
			=> new HeaderMatch<TValue>(HeaderMatchOperation.NotContains, name, value, compareCase);
	}
}
