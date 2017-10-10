using System;

namespace NetMock.Rest
{
	internal abstract class Header
	{
		protected Header(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public abstract MatchResult Match(string name, string value);
	}

	internal class StaticHeader : Header
	{
		public StaticHeader(string name, string value) : base(name)
		{
			Value = value;
		}

		public string Value { get; }

		public override MatchResult Match(string name, string value)
		{
			return new MatchResult(name.Equals(Name, StringComparison.InvariantCultureIgnoreCase)
				&& value.Equals(Value, StringComparison.InvariantCultureIgnoreCase), value);
		}
	}

	internal class ParameterizedHeader : Header
	{
		public ParameterizedHeader(string name, ParameterMatch parameterMatch) : base(name)
		{
			ParameterMatch = parameterMatch;
		}

		public ParameterMatch ParameterMatch { get; }

		public override MatchResult Match(string name, string value)
		{
			return !Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
				? MatchResult.NoMatch
				: ParameterMatch.Match(value);
		}
	}
}
