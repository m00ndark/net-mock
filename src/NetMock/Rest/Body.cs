using System;

namespace NetMock.Rest
{
	public static class Body
	{
		public static IMatch Is<TValue>(TValue value)
			=> new BodyMatch<TValue>(BodyMatchOperation.Is, value);

		public static IMatch Is<TValue>(TValue value, CompareCase compareCase)
			=> new BodyMatch<TValue>(BodyMatchOperation.Is, value, compareCase);

		public static IMatch Is(Func<string, bool> condition)
			=> Is<string>(condition);

		public static IMatch Is<TValue>(Func<TValue, bool> condition)
			=> new BodyMatch<TValue>(BodyMatchOperation.Is, condition);

		public static IMatch IsNot<TValue>(TValue value)
			=> new BodyMatch<TValue>(BodyMatchOperation.IsNot, value);

		public static IMatch IsNot<TValue>(TValue value, CompareCase compareCase)
			=> new BodyMatch<TValue>(BodyMatchOperation.IsNot, value, compareCase);

		public static IMatch IsAny()
			=> IsAny<string>();

		public static IMatch IsAny<TValue>()
			=> new BodyMatch<TValue>(BodyMatchOperation.IsAny);

		public static IMatch IsEmpty()
			=> new BodyMatch<string>(BodyMatchOperation.IsEmpty);

		public static IMatch IsNotEmpty()
			=> new BodyMatch<string>(BodyMatchOperation.IsNotEmpty);

		public static IMatch Contains<TValue>(TValue value)
			=> Contains(value, CompareCase.Insensitive);

		public static IMatch Contains<TValue>(TValue value, CompareCase compareCase)
			=> new BodyMatch<TValue>(BodyMatchOperation.Contains, value, compareCase);

		public static IMatch NotContains<TValue>(TValue value)
			=> NotContains(value, CompareCase.Insensitive);

		public static IMatch NotContains<TValue>(TValue value, CompareCase compareCase)
			=> new BodyMatch<TValue>(BodyMatchOperation.NotContains, value, compareCase);

		public static IMatch ContainsWord<TValue>(TValue word)
			=> Contains(word, CompareCase.Insensitive);

		public static IMatch ContainsWord<TValue>(TValue word, CompareCase compareCase)
			=> new BodyMatch<TValue>(BodyMatchOperation.Contains, word, compareCase);

		public static IMatch NotContainsWord<TValue>(TValue word)
			=> NotContainsWord(word, CompareCase.Insensitive);

		public static IMatch NotContainsWord<TValue>(TValue word, CompareCase compareCase)
			=> new BodyMatch<TValue>(BodyMatchOperation.NotContainsWord, word, compareCase);
	}
}
