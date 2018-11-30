using System;
using System.Linq;

namespace NetMock.Rest
{
	internal enum ParameterMatchOperation
	{
		Is,
		IsNot,
		IsAny,
		StartsWith,
		EndsWith,
		Contains,
		NotContains,
		StartsWithWord,
		EndsWithWord,
		ContainsWord,
		NotContainsWord
	}

	internal abstract class ParameterMatch : MatchBase
	{
		protected ParameterMatch(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public string Name { get; }
	}

	internal class ParameterMatch<TValue> : ParameterMatch
	{
		public ParameterMatch(ParameterMatchOperation operation, string name)
			: base(name)
		{
			Operation = operation;

			if (operation == ParameterMatchOperation.IsAny && !_typeConverters.ContainsKey(typeof(TValue)))
				throw new NotSupportedException($"Parameter conversion to {typeof(TValue).Name} not supported; supported types are "
					+ string.Join(", ", _typeConverters.Keys.Select(type => type.Name)));
		}

		public ParameterMatch(ParameterMatchOperation operation, string name, TValue value, CompareCase compareCase)
			: this(operation, name)
		{
			if (value == null) // pattern matching not possible
				throw new ArgumentNullException(nameof(value));

			Value = value;
			CompareCase = compareCase;
		}

		public ParameterMatch(ParameterMatchOperation operation, string name, Func<TValue, bool> condition)
			: this(operation, name)
		{
			Condition = condition ?? throw new ArgumentNullException(nameof(condition));
		}

		public ParameterMatchOperation Operation { get; }
		public TValue Value { get; }
		public CompareCase CompareCase { get; }
		public Func<TValue, bool> Condition { get; }

		private string StringValue => Value as string ?? Value.ToString();

		public override MatchResult Match(string value)
		{
			bool isMatch;
			switch (Operation)
			{
				case ParameterMatchOperation.Is:
				{
					if (Value != null)
					{
						isMatch = value.Equals(StringValue, CompareCase == CompareCase.Insensitive
							? StringComparison.OrdinalIgnoreCase
							: StringComparison.Ordinal);
						return new MatchResult(this, isMatch, value);
					}

					object convertedValue = _typeConverters[typeof(TValue)](value);
					isMatch = convertedValue != null && Condition((TValue) convertedValue);
					return new MatchResult(this, isMatch, value, isMatch ? convertedValue : null);
				}
				case ParameterMatchOperation.IsNot:
				{
					isMatch = !value.Equals(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal);
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.IsAny:
				{
					object matchedValue = _typeConverters[typeof(TValue)](value);
					return new MatchResult(this, matchedValue != null, value, matchedValue);
				}
				case ParameterMatchOperation.StartsWith:
				{
					isMatch = value.StartsWith(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal);
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.EndsWith:
				{
					isMatch = value.EndsWith(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal);
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.Contains:
				{
					isMatch = value.IndexOf(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) != -1;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.NotContains:
				{
					isMatch = value.IndexOf(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) == -1;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.StartsWithWord:
				{
					string firstWord = _whitespaceRegex
						.Split(value)
						.FirstOrDefault(word => !string.IsNullOrEmpty(word));
					isMatch = firstWord?.Equals(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) ?? false;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.EndsWithWord:
				{
					string lastWord = _whitespaceRegex
						.Split(value)
						.LastOrDefault(word => !string.IsNullOrEmpty(word));
					isMatch = lastWord?.Equals(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) ?? false;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.ContainsWord:
				{
					isMatch = _whitespaceRegex
						.Split(value)
						.Where(word => !string.IsNullOrEmpty(word))
						.Any(word => word.Equals(StringValue, CompareCase == CompareCase.Insensitive
							? StringComparison.OrdinalIgnoreCase
							: StringComparison.Ordinal));
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.NotContainsWord:
				{
					isMatch = _whitespaceRegex
						.Split(value)
						.Where(word => !string.IsNullOrEmpty(word))
						.All(word => !word.Equals(StringValue, CompareCase == CompareCase.Insensitive
							? StringComparison.OrdinalIgnoreCase
							: StringComparison.Ordinal));
					return new MatchResult(this, isMatch, value);
				}
				default:
					throw new ArgumentOutOfRangeException(nameof(Operation));
			}
		}
	}
}
