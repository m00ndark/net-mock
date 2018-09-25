using System;
using System.Linq;
using NetMock.Exceptions;
using Newtonsoft.Json.Linq;

namespace NetMock.Rest
{
	public enum BodyMatchOperation
	{
		Is,
		IsEmpty,
		IsNotEmpty,
		Contains,
		ContainsWord
	}

	internal class BodyMatch : MatchBase
	{
		public BodyMatch(BodyMatchOperation operation)
		{
			Operation = operation;
		}

		public BodyMatch(BodyMatchOperation operation, object value, CompareCase compareCase = CompareCase.Insensitive)
			: this(operation)
		{
			Value = value ?? throw new ArgumentNullException(nameof(value));
			CompareCase = compareCase;
		}

		public BodyMatch(BodyMatchOperation operation, Func<string, bool> condition)
			: this(operation)
		{
			Condition = condition ?? throw new ArgumentNullException(nameof(condition));
		}

		public BodyMatchOperation Operation { get; }
		public object Value { get; }
		public CompareCase CompareCase { get; }
		public Func<string, bool> Condition { get; }
		private JToken JsonValue { get; set; }

		internal BodyMatch Parse(bool interpretBodyAsJson)
		{
			switch (Operation)
			{
				case BodyMatchOperation.Is:
				{
					if (!interpretBodyAsJson)
						break;

					if (Value is string strValue)
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
				case BodyMatchOperation.IsEmpty:
				case BodyMatchOperation.IsNotEmpty:
				case BodyMatchOperation.Contains:
				case BodyMatchOperation.ContainsWord:
					break;
			}
			return this;
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
							isMatch = value.Equals(Value as string ?? Value.ToString(), CompareCase == CompareCase.Insensitive
								? StringComparison.OrdinalIgnoreCase
								: StringComparison.Ordinal);
						}
					}
					else
					{
						isMatch = Condition(value);
					}
					return new MatchResult(this, isMatch, value);
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
					isMatch = value.IndexOf(Value as string ?? Value.ToString(), CompareCase == CompareCase.Insensitive
						? StringComparison.OrdinalIgnoreCase
						: StringComparison.Ordinal) != -1;
					return new MatchResult(this, isMatch, value);
				}
				case BodyMatchOperation.ContainsWord:
				{
					isMatch = _whitespaceRegex
						.Split(value)
						.Where(word => !string.IsNullOrEmpty(word))
						.Any(word => word.Equals(Value as string ?? Value.ToString(), CompareCase == CompareCase.Insensitive
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
