using System;

namespace NetMock.Rest
{
	public static class Body
	{
		public static IMatch Is(object value)
			=> new BodyMatch(BodyMatchOperation.Is, value);

		public static IMatch Is(string value)
			=> Is(value, CompareCase.Insensitive);

		public static IMatch Is(string value, CompareCase compareCase)
			=> new BodyMatch(BodyMatchOperation.Is, value, compareCase);

		public static IMatch Is(Func<string, bool> condition)
			=> new BodyMatch(BodyMatchOperation.Is, condition);

		//public static IMatch Is<TValue>(Func<TValue, bool> condition)
		//	=> new BodyMatch(BodyMatchOperation.Is, condition);

		public static IMatch IsNot(object value)
			=> new BodyMatch(BodyMatchOperation.IsNot, value);

		public static IMatch IsNot(string value)
			=> IsNot(value, CompareCase.Insensitive);

		public static IMatch IsNot(string value, CompareCase compareCase)
			=> new BodyMatch(BodyMatchOperation.IsNot, value, compareCase);

		public static IMatch IsEmpty()
			=> new BodyMatch(BodyMatchOperation.IsEmpty);

		public static IMatch IsNotEmpty()
			=> new BodyMatch(BodyMatchOperation.IsNotEmpty);

		public static IMatch Contains(string value)
			=> Contains(value, CompareCase.Insensitive);

		public static IMatch Contains(string value, CompareCase compareCase)
			=> new BodyMatch(BodyMatchOperation.Contains, value, compareCase);

		public static IMatch NotContains(string value)
			=> NotContains(value, CompareCase.Insensitive);

		public static IMatch NotContains(string value, CompareCase compareCase)
			=> new BodyMatch(BodyMatchOperation.NotContains, value, compareCase);

		public static IMatch ContainsWord(string word)
			=> Contains(word, CompareCase.Insensitive);

		public static IMatch ContainsWord(string word, CompareCase compareCase)
			=> new BodyMatch(BodyMatchOperation.Contains, word, compareCase);

		public static IMatch NotContainsWord(string word)
			=> NotContainsWord(word, CompareCase.Insensitive);

		public static IMatch NotContainsWord(string word, CompareCase compareCase)
			=> new BodyMatch(BodyMatchOperation.NotContainsWord, word, compareCase);
	}
}
