using System;
using System.Linq;
using NetMock.Exceptions;
using Newtonsoft.Json.Linq;

namespace NetMock.Rest
{
	internal enum BodyMatchOperation
	{
		Is,
		IsNot,
		IsAny,
		IsEmpty,
		IsNotEmpty,
		Contains,
		NotContains,
		ContainsWord,
		NotContainsWord
	}

	internal abstract class BodyMatch : MatchBase
	{
		internal abstract void Parse(bool interpretBodyAsJson);
	}

	internal class BodyMatch<TValue> : BodyMatch
	{
		public BodyMatch(BodyMatchOperation operation)
		{
			Operation = operation;
		}

		public BodyMatch(BodyMatchOperation operation, TValue value, CompareCase compareCase = CompareCase.Insensitive)
			: this(operation)
		{
			if (value == null) // pattern matching not possible
				throw new ArgumentNullException(nameof(value));

			Value = value;
			CompareCase = compareCase;
		}

		public BodyMatch(BodyMatchOperation operation, Func<TValue, bool> condition)
			: this(operation)
		{
			Condition = condition ?? throw new ArgumentNullException(nameof(condition));

			if (!_typeConverters.ContainsKey(typeof(TValue)))
				throw new NotSupportedException($"Body conversion to {typeof(TValue).Name} not supported; supported types are "
					+ string.Join(", ", _typeConverters.Keys.Select(type => type.Name)));
		}

		public BodyMatchOperation Operation { get; }
		public TValue Value { get; }
		public CompareCase CompareCase { get; }
		public Func<TValue, bool> Condition { get; }
		private JToken JsonValue { get; set; }

		private string StringValue => Value as string ?? Value.ToString();

		internal override void Parse(bool interpretBodyAsJson)
		{
			switch (Operation)
			{
				case BodyMatchOperation.Is:
				case BodyMatchOperation.IsNot:
				{
					if (Value == null)
						break;

					if (!interpretBodyAsJson)
						break;

					if ((object) Value is string strValue)
					{
						try
						{
							JsonValue = JToken.Parse(strValue);
						}
						catch (Exception ex)
						{
							throw new MockSetupException($"Body value \"{strValue}\" provided in setup could not be parsed as Json. Consider disabling {nameof(RestMock)}.{nameof(RestMock.InterpretBodyAsJson)}.", ex);
						}
					}
					else
					{
						JsonValue = JToken.FromObject(Value);
					}
					break;
				}
				case BodyMatchOperation.IsAny:
				case BodyMatchOperation.IsEmpty:
				case BodyMatchOperation.IsNotEmpty:
				case BodyMatchOperation.Contains:
				case BodyMatchOperation.NotContains:
				case BodyMatchOperation.ContainsWord:
				case BodyMatchOperation.NotContainsWord:
					break;
			}
		}

		public override MatchResult Match(string value)
		{
			bool isMatch;
			switch (Operation)
			{
				case BodyMatchOperation.Is:
				{
					if (Value != null)
					{
						if (JsonValue != null)
						{
							try
							{
								isMatch = JToken.DeepEquals(JsonValue, JToken.Parse(value));
							}
							catch
							{
								isMatch = false;
							}
						}
						else
						{
							isMatch = value.Equals(StringValue, CompareCase == CompareCase.Insensitive
								? StringComparison.OrdinalIgnoreCase
								: StringComparison.Ordinal);
						}
						return new MatchResult(this, isMatch, value);
					}

					object convertedValue = _typeConverters[typeof(TValue)](value);
					isMatch = convertedValue != null && Condition((TValue) convertedValue);
					return new MatchResult(this, isMatch, value, isMatch ? convertedValue : null);
				}
				case BodyMatchOperation.IsNot:
				{
					if (JsonValue != null)
					{
						try
						{
							isMatch = !JToken.DeepEquals(JsonValue, JToken.Parse(value));
						}
						catch
						{
							isMatch = false;
						}
					}
					else
					{
						isMatch = !value.Equals(StringValue, CompareCase == CompareCase.Insensitive
							? StringComparison.OrdinalIgnoreCase
							: StringComparison.Ordinal);
					}
					return new MatchResult(this, isMatch, value);
				}
				case BodyMatchOperation.IsAny:
				{
					object matchedValue = _typeConverters[typeof(TValue)](value);
					return new MatchResult(this, value == null || matchedValue != null, value, matchedValue);
				}
				case BodyMatchOperation.IsEmpty:
				{
					return new MatchResult(this, string.IsNullOrEmpty(value), value);
				}
				case BodyMatchOperation.IsNotEmpty:
				{
					return new MatchResult(this, !string.IsNullOrEmpty(value), value);
				}
				case BodyMatchOperation.Contains:
				{
					isMatch = value.IndexOf(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) != -1;
					return new MatchResult(this, isMatch, value);
				}
				case BodyMatchOperation.NotContains:
				{
					isMatch = value.IndexOf(StringValue, CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) == -1;
					return new MatchResult(this, isMatch, value);
				}
				case BodyMatchOperation.ContainsWord:
				{
					isMatch = _whitespaceRegex
						.Split(value)
						.Where(word => !string.IsNullOrEmpty(word))
						.Any(word => word.Equals(StringValue, CompareCase == CompareCase.Insensitive
							? StringComparison.OrdinalIgnoreCase
							: StringComparison.Ordinal));
					return new MatchResult(this, isMatch, value);
				}
				case BodyMatchOperation.NotContainsWord:
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
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
