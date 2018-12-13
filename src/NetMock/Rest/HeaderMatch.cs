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

	internal abstract class HeaderMatch : MatchBase
	{
		protected HeaderMatch(HeaderMatchOperation operation, string name)
		{
			Operation = operation;
			Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public string Name { get; }
		public HeaderMatchOperation Operation { get; }
	}

	internal class HeaderMatch<TValue> : HeaderMatch
	{
		public HeaderMatch(HeaderMatchOperation operation, string name) : base(operation, name) { }

		public HeaderMatch(HeaderMatchOperation operation, string name, TValue value, CompareCase compareCase = CompareCase.Insensitive)
			: base(operation, name)
		{
			if (value == null) // pattern matching not possible
				throw new ArgumentNullException(nameof(value));

			Value = value;
			CompareCase = compareCase;
		}

		public HeaderMatch(HeaderMatchOperation operation, string name, Func<TValue, bool> condition)
			: this(operation, name)
		{
			Condition = condition ?? throw new ArgumentNullException(nameof(condition));
		}

		public TValue Value { get; }
		public CompareCase CompareCase { get; }
		public Func<TValue, bool> Condition { get; }

		private string StringValue => Value as string ?? Value.ToString();

		public override MatchResult Match(string value)
		{
			bool isMatch;
			switch (Operation)
			{
				case HeaderMatchOperation.Is:
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
				case HeaderMatchOperation.IsNot:
				{
					isMatch = !value.Equals(StringValue, CompareCase == CompareCase.Insensitive
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
					isMatch = value.IndexOf(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) != -1;
					return new MatchResult(this, isMatch, value);
				}
				case HeaderMatchOperation.NotContains:
				{
					isMatch = value.IndexOf(StringValue, CompareCase == CompareCase.Insensitive
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
