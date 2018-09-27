using System;

namespace NetMock.Rest.Parsed
{
	internal class ParsedHeader
	{
		public ParsedHeader(string name, HeaderMatch headerMatch)
		{
			Name = name;
			HeaderMatch = headerMatch;
		}

		public string Name { get; }
		public HeaderMatch HeaderMatch { get; }

		public MatchResult Match(string name, string value)
		{
			return !Name.Equals(name, StringComparison.OrdinalIgnoreCase)
				? MatchResult.NoMatch
				: HeaderMatch.Match(value);
		}
	}
}
