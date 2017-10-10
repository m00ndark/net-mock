namespace NetMock.Rest
{
	internal class MatchResult
	{
		public static MatchResult NoMatch { get; } = new MatchResult(false, null);

		public MatchResult(bool isMatch, string value)
			: this(null, isMatch, value, isMatch ? value : null) { }

		public MatchResult(IMatch match, bool isMatch, string value)
			: this(match, isMatch, value, isMatch ? value : null) { }

		public MatchResult(IMatch match, bool isMatch, string value, object matchedValue)
		{
			Match = match;
			IsMatch = isMatch;
			Value = value;
			MatchedValue = matchedValue;
		}

		public IMatch Match { get; }
		public bool IsMatch { get; }
		public string Value { get; }
		public object MatchedValue { get; }
	}
}
