using System;

namespace NetMock.Rest
{
	internal enum HeaderMatchOperation
	{
		Is,
		IsNot,
		IsSet,
		IsNotSet,
		Contains,
		NotContains
	}

	internal class HeaderMatch : MatchBase
	{
		public HeaderMatch(HeaderMatchOperation operation, string name)
		{
			Operation = operation;
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public HeaderMatch(HeaderMatchOperation operation, string name, string value, CompareCase compareCase = CompareCase.Insensitive)
			: this(operation, name)
		{
			Value = value ?? throw new ArgumentNullException(nameof(value));
			CompareCase = compareCase;
		}

		public HeaderMatch(HeaderMatchOperation operation, string name, Func<string, bool> condition)
			: this(operation, name)
		{
			Condition = condition ?? throw new ArgumentNullException(nameof(condition));
		}

		public HeaderMatchOperation Operation { get; }
		public string Name { get; }
		public string Value { get; }
		public CompareCase CompareCase { get; }
		public Func<string, bool> Condition { get; }

		public override MatchResult Match(string value)
		{
			bool isMatch;
			switch (Operation)
			{
				case HeaderMatchOperation.Is:
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
				case HeaderMatchOperation.IsNot:
				{
					isMatch = !value.Equals(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal);
					return new MatchResult(this, isMatch, value);
				}
				case HeaderMatchOperation.IsSet:
				case HeaderMatchOperation.IsNotSet:
				{
					return new MatchResult(this, true, value);
				}
				case HeaderMatchOperation.Contains:
				{
					isMatch = value.IndexOf(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) != -1;
					return new MatchResult(this, isMatch, value);
				}
				case HeaderMatchOperation.NotContains:
				{
					isMatch = value.IndexOf(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) == -1;
					return new MatchResult(this, isMatch, value);
				}
				default:
					throw new ArgumentOutOfRangeException(nameof(Operation));
			}
		}
	}
}
