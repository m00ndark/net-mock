using System;

namespace NetMock.Rest.Parsed
{
	internal abstract class ParsedUriSegment
	{
		public abstract MatchResult Match(string value);
	}

	internal class StaticUriSegment : ParsedUriSegment
	{
		public StaticUriSegment(string value)
		{
			Value = value;
		}

		public string Value { get; }

		public override MatchResult Match(string value)
		{
			return new MatchResult(value.Equals(Value, StringComparison.OrdinalIgnoreCase), value);
		}
	}

	internal class ParameterizedUriSegment : ParsedUriSegment
	{
		public ParameterizedUriSegment(ParameterMatch parameterMatch)
		{
			ParameterMatch = parameterMatch;
		}

		public ParameterMatch ParameterMatch { get; }

		public override MatchResult Match(string value)
		{
			return ParameterMatch.Match(value);
		}
	}
}
