namespace NetMock.Rest {
	public class Times
	{
		internal Times(int callCount)
		{
			CallCount = callCount;
		}

		public int CallCount { get; }

		public static Times Never { get; } = new Times(0);
		public static Times Once { get; } = new Times(1);
		public static Times Twice { get; } = new Times(2);
		public static Times Exactly(int callCount) => new Times(callCount);
	}
}
