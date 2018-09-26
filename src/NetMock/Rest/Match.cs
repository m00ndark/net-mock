using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetMock.Rest
{
	public interface IMatch { }

	internal abstract class MatchBase : IMatch
	{
		protected static readonly Regex _whitespaceRegex = new Regex(@"\s");
		protected static readonly IDictionary<Type, Func<string, object>> _typeConverters;

		static MatchBase()
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

		public abstract MatchResult Match(string value);
	}
}
