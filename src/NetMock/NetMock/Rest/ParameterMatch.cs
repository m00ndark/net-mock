using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

	internal class ParameterMatch : IMatch
	{
		private static readonly Regex _whitespaceRegex = new Regex(@"\s");
		private static readonly IDictionary<Type, Func<string, object>> _typeConverters;

		static ParameterMatch()
		{
			_typeConverters = new Dictionary<Type, Func<string, object>>
				{
					{ typeof(string), value => value },
					{ typeof(object), value => value },
					{ typeof(int), value => int.TryParse(value, out int intValue) ? (object) intValue : null },
					{ typeof(uint), value => uint.TryParse(value, out uint uintValue) ? (object) uintValue : null },
					{ typeof(long), value => long.TryParse(value, out long longValue) ? (object) longValue : null },
					{ typeof(ulong), value => ulong.TryParse(value, out ulong ulongValue) ? (object) ulongValue : null },
					{ typeof(short), value => short.TryParse(value, out short shortValue) ? (object) shortValue : null },
					{ typeof(ushort), value => ushort.TryParse(value, out ushort ushortValue) ? (object) ushortValue : null },
					{ typeof(float), value => float.TryParse(value, out float floatValue) ? (object) floatValue : null },
					{ typeof(double), value => double.TryParse(value, out double doubleValue) ? (object) doubleValue : null },
					{ typeof(decimal), value => decimal.TryParse(value, out decimal decimalValue) ? (object) decimalValue : null },
					{ typeof(sbyte), value => sbyte.TryParse(value, out sbyte sbyteValue) ? (object) sbyteValue : null },
					{ typeof(byte), value => byte.TryParse(value, out byte byteValue) ? (object) byteValue : null },
					{ typeof(bool), value => bool.TryParse(value, out bool boolValue) ? (object) boolValue : null },
					{ typeof(char), value => char.TryParse(value, out char charValue) ? (object) charValue : null },
					{ typeof(Guid), value => Guid.TryParse(value, out Guid guidValue) ? (object) guidValue : null },
					{ typeof(DateTime), value => DateTime.TryParse(value, out DateTime dateTimeValue) ? (object) dateTimeValue : null },
				};
		}

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

		internal MatchResult Match(string value)
		{
			bool isMatch;
			switch (Operation)
			{
				case ParameterMatchOperation.Is:
				{
					if (Value != null)
					{
						isMatch = value.Equals(Value, CompareCase == CompareCase.Insensitive
							? StringComparison.InvariantCultureIgnoreCase
							: StringComparison.InvariantCulture);
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
						? StringComparison.InvariantCultureIgnoreCase
						: StringComparison.InvariantCulture);
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.EndsWith:
				{
					isMatch = value.EndsWith(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.InvariantCultureIgnoreCase
						: StringComparison.InvariantCulture);
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.Contains:
				{
					isMatch = value.IndexOf(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.InvariantCultureIgnoreCase
						: StringComparison.InvariantCulture) != -1;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.StartsWithWord:
				{
					string firstWord = _whitespaceRegex
						.Split(value)
						.FirstOrDefault(word => !string.IsNullOrEmpty(word));
					isMatch = firstWord?.Equals(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.InvariantCultureIgnoreCase
						: StringComparison.InvariantCulture) ?? false;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.EndsWithWord:
				{
					string lastWord = _whitespaceRegex
						.Split(value)
						.LastOrDefault(word => !string.IsNullOrEmpty(word));
					isMatch = lastWord?.Equals(Value, CompareCase == CompareCase.Insensitive
						? StringComparison.InvariantCultureIgnoreCase
						: StringComparison.InvariantCulture) ?? false;
					return new MatchResult(this, isMatch, value);
				}
				case ParameterMatchOperation.ContainsWord:
				{
					isMatch = _whitespaceRegex
						.Split(value)
						.Where(word => !string.IsNullOrEmpty(word))
						.Any(word => word.Equals(Value, CompareCase == CompareCase.Insensitive
							? StringComparison.InvariantCultureIgnoreCase
							: StringComparison.InvariantCulture));
					return new MatchResult(this, isMatch, value);
				}
				default:
					throw new ArgumentOutOfRangeException(nameof(Operation));
			}
		}
	}
}
