using System;

namespace NetMock.Rest
{
	internal abstract class UriSegment
	{
		public abstract MatchResult Match(string value);
	}

	internal class StaticUriSegment : UriSegment
	{
		public StaticUriSegment(string value)
		{
			Value = value;
		}

		public string Value { get; }

		public override MatchResult Match(string value)
		{
			return new MatchResult(value.Equals(Value, StringComparison.InvariantCultureIgnoreCase), value);
		}
	}

	internal class ParameterizedUriSegment : UriSegment
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
