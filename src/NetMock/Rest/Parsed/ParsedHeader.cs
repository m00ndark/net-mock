using System;

namespace NetMock.Rest.Parsed
{
	internal abstract class ParsedHeader
	{
		protected ParsedHeader(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public abstract MatchResult Match(string name, string value);
	}

	internal class StaticHeader : ParsedHeader
	{
		public StaticHeader(string name, string value) : base(name)
		{
			Value = value;
		}

		public string Value { get; }

		public override MatchResult Match(string name, string value)
		{
			return new MatchResult(name.Equals(Name, StringComparison.OrdinalIgnoreCase)
				&& value.Equals(Value, StringComparison.OrdinalIgnoreCase), value);
		}
	}

	internal class ParameterizedHeader : ParsedHeader
	{
		public ParameterizedHeader(string name, ParameterMatch parameterMatch) : base(name)
		{
			ParameterMatch = parameterMatch;
		}

		public ParameterMatch ParameterMatch { get; }

		public override MatchResult Match(string name, string value)
		{
			return !Name.Equals(name, StringComparison.OrdinalIgnoreCase)
				? MatchResult.NoMatch
				: ParameterMatch.Match(value);
		}
	}
}
