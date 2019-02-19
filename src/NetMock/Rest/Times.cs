using System;

namespace NetMock.Rest
{
	public class Times
	{
		internal Times(Func<int, bool> condition, string description)
		{
			Condition = condition;
			Description = description;
		}

		internal Func<int, bool> Condition { get; }
		internal string Description { get; }

		public static Times Never { get; } = new Times(count => count == 0, "should never have been performed");
		public static Times Once { get; } = new Times(count => count == 1, "once");
		public static Times Twice { get; } = new Times(count => count == 2, "twice");
		public static Times Exactly(int callCount) => new Times(count => count == callCount, $"exactly {callCount} times");
		public static Times AtLeastOnce => new Times(count => count >= 1, "at least once");
		public static Times AtMostOnce => new Times(count => count <= 1, "at most once");
		public static Times AtLeast(int callCount) => new Times(count => count >= callCount, $"at least {callCount} times");
		public static Times AtMost(int callCount) => new Times(count => count <= callCount, $"at most {callCount} times");
		public static Times Between(int callCountFrom, int callCountTo, Range range) => new Times(
			count => range == Range.Inclusive
				? count >= callCountFrom && count <= callCountTo
				: count > callCountFrom && count < callCountTo,
			$"between {callCountFrom} and {callCountTo} times ({range.ToString().ToLowerInvariant()})");
	}
}
