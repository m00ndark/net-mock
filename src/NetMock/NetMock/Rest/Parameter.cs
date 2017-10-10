using System;

namespace NetMock.Rest
{
	public static class Parameter
	{
		public static IMatch Is(string name, string value)
			=> Is(name, value, CompareCase.Insensitive);

		public static IMatch Is(string name, string value, CompareCase compareCase)
			=> new ParameterMatch(ParameterMatchOperation.Is, name, value, compareCase);

		public static IMatch Is(string name, Func<string, bool> condition)
			=> new ParameterMatch(ParameterMatchOperation.Is, name, condition);

		public static IMatch IsAny(string name)
			=> IsAny<string>(name);

		public static IMatch IsAny<TValue>(string name)
			=> new ParameterMatch(ParameterMatchOperation.IsAny, name, typeof(TValue));

		public static IMatch StartsWith(string name, string value)
			=> StartsWith(name, value, CompareCase.Insensitive);

		public static IMatch StartsWith(string name, string value, CompareCase compareCase)
			=> new ParameterMatch(ParameterMatchOperation.StartsWith, name, value, compareCase);

		public static IMatch EndsWith(string name, string value)
			=> EndsWith(name, value, CompareCase.Insensitive);

		public static IMatch EndsWith(string name, string value, CompareCase compareCase)
			=> new ParameterMatch(ParameterMatchOperation.EndsWith, name, value, compareCase);

		public static IMatch Contains(string name, string value)
			=> Contains(name, value, CompareCase.Insensitive);

		public static IMatch Contains(string name, string value, CompareCase compareCase)
			=> new ParameterMatch(ParameterMatchOperation.Contains, name, value, compareCase);

		public static IMatch StartsWithWord(string name, string word)
			=> StartsWithWord(name, word, CompareCase.Insensitive);

		public static IMatch StartsWithWord(string name, string word, CompareCase compareCase)
			=> new ParameterMatch(ParameterMatchOperation.StartsWithWord, name, word, compareCase);

		public static IMatch EndsWithWord(string name, string word)
			=> EndsWithWord(name, word, CompareCase.Insensitive);

		public static IMatch EndsWithWord(string name, string word, CompareCase compareCase)
			=> new ParameterMatch(ParameterMatchOperation.EndsWithWord, name, word, compareCase);

		public static IMatch ContainsWord(string name, string word)
			=> ContainsWord(name, word, CompareCase.Insensitive);

		public static IMatch ContainsWord(string name, string word, CompareCase compareCase)
			=> new ParameterMatch(ParameterMatchOperation.ContainsWord, name, word, compareCase);
	}
}
