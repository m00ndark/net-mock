using System;
using System.Linq;

namespace NetMock.Rest
{
	public enum ParameterMatchOperation
	{
		Is,
		IsAny,
		StartsWith,
		EndsWith,
		Contains,
		StartsWithWord,
		EndsWithWord,
		ContainsWord
	}

	internal class ParameterMatch : MatchBase
	{
		public ParameterMatch(ParameterMatchOperation operation, string name)
		{
			Operation = operation;
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public ParameterMatch(ParameterMatchOperation operation, string name, string value, CompareCase compareCase)
			: this(operation, name)
		{
			Value = value ?? throw new ArgumentNullException(nameof(value));
			CompareCase = compareCase;
		}

		public ParameterMatch(ParameterMatchOperation operation, string name, Type valueType)
			: this(operation, name)
		{
			ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));

			if (!_typeConverters.ContainsKey(ValueType))
				throw new NotSupportedException($"Parameter conversion to {valueType.Name} not supported; supported types are "
					+ string.Join(", ", _typeConverters.Keys.Select(type => type.Name)));
		}

		public ParameterMatch(ParameterMatchOperation operation, string name, Func<string, bool> condition)
			: this(operation, name)
		{
			Condition = condition ?? throw new ArgumentNullException(nameof(condition));
		}

		public ParameterMatchOperation Operation { get; }
		public string Name { get; }
		public string Value { get; }
		public CompareCase CompareCase { get; }
		public Type ValueType { get; }
		public Func<string, bool> Condition { get; }

		public override MatchResult Match(string value)
		{
			bool isMatch;
			switch (Operation)
			{
				case ParameterMatchOperation.Is:
				{
					if (Value != null)
					{
						isMatch = value.Equals(Value, CompareCase == CompareCase.Insensitive
							? StringComparison.OrdinalIgnoreCase
							: StringComparison.Ordinal);
					}
					else
					{
						isMatch = Condition(value);
					}
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.IsAny:
				{
					object matchedValue = _typeConverters[ValueType](value);
					return new MatchResult(this, matchedValue != null, value, matchedValue);
				}
				case ParameterMatchOperation.StartsWith:
				{
					isMatch = value.StartsWith(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal);
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.EndsWith:
				{
					isMatch = value.EndsWith(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal);
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.Contains:
				{
					isMatch = value.IndexOf(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) != -1;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.StartsWithWord:
				{
					string firstWord = _whitespaceRegex
						.Split(value)
						.FirstOrDefault(word => !string.IsNullOrEmpty(word));
					isMatch = firstWord?.Equals(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) ?? false;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.EndsWithWord:
				{
					string lastWord = _whitespaceRegex
						.Split(value)
						.LastOrDefault(word => !string.IsNullOrEmpty(word));
					isMatch = lastWord?.Equals(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) ?? false;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.ContainsWord:
				{
					isMatch = _whitespaceRegex
						.Split(value)
						.Where(word => !string.IsNullOrEmpty(word))
						.Any(word => word.Equals(Value, CompareCase == CompareCase.Insensitive
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
