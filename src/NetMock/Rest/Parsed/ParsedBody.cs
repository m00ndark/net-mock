namespace NetMock.Rest.Parsed
{
	internal class ParsedBody
	{
		public ParsedBody(BodyMatch bodyMatch, bool interpretBodyAsJson)
		{
			BodyMatch = bodyMatch;
			BodyMatch.Parse(interpretBodyAsJson);
		}

		public BodyMatch BodyMatch { get; }

		public MatchResult Match(string value)
		{
			return BodyMatch.Match(value);
		}
	}
}
