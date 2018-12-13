using System;

namespace NetMock.Rest
{
	public static class Parameter
	{
		public static IMatch Is<TValue>(string name, TValue value)
			=> Is(name, value, CompareCase.Insensitive);

		public static IMatch Is<TValue>(string name, TValue value, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.Is, name, value, compareCase);

		public static IMatch Is(string name, Func<string, bool> condition)
			=> Is<string>(name, condition);

		public static IMatch Is<TValue>(string name, Func<TValue, bool> condition)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.Is, name, condition);

		public static IMatch IsNot<TValue>(string name, TValue value)
			=> IsNot(name, value, CompareCase.Insensitive);

		public static IMatch IsNot<TValue>(string name, TValue value, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.IsNot, name, value, compareCase);

		public static IMatch IsAny(string name)
			=> IsAny<string>(name);

		public static IMatch IsAny<TValue>(string name)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.IsAny, name);

		public static IMatch StartsWith<TValue>(string name, TValue value)
			=> StartsWith(name, value, CompareCase.Insensitive);

		public static IMatch StartsWith<TValue>(string name, TValue value, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.StartsWith, name, value, compareCase);

		public static IMatch EndsWith<TValue>(string name, TValue value)
			=> EndsWith(name, value, CompareCase.Insensitive);

		public static IMatch EndsWith<TValue>(string name, TValue value, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.EndsWith, name, value, compareCase);

		public static IMatch Contains<TValue>(string name, TValue value)
			=> Contains(name, value, CompareCase.Insensitive);

		public static IMatch Contains<TValue>(string name, TValue value, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.Contains, name, value, compareCase);

		public static IMatch NotContains<TValue>(string name, TValue value)
			=> NotContains(name, value, CompareCase.Insensitive);

		public static IMatch NotContains<TValue>(string name, TValue value, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.NotContains, name, value, compareCase);

		public static IMatch StartsWithWord<TValue>(string name, TValue word)
			=> StartsWithWord(name, word, CompareCase.Insensitive);

		public static IMatch StartsWithWord<TValue>(string name, TValue word, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.StartsWithWord, name, word, compareCase);

		public static IMatch EndsWithWord<TValue>(string name, TValue word)
			=> EndsWithWord(name, word, CompareCase.Insensitive);

		public static IMatch EndsWithWord<TValue>(string name, TValue word, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.EndsWithWord, name, word, compareCase);

		public static IMatch ContainsWord<TValue>(string name, TValue word)
			=> ContainsWord(name, word, CompareCase.Insensitive);

		public static IMatch ContainsWord<TValue>(string name, TValue word, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.ContainsWord, name, word, compareCase);

		public static IMatch NotContainsWord<TValue>(string name, TValue word)
			=> NotContainsWord(name, word, CompareCase.Insensitive);

		public static IMatch NotContainsWord<TValue>(string name, TValue word, CompareCase compareCase)
			=> new ParameterMatch<TValue>(ParameterMatchOperation.NotContainsWord, name, word, compareCase);
	}
}
