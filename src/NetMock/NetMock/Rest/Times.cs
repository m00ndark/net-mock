namespace NetMock.Rest {
	public class Times
	{
		public Times(int no)
		{
			No = no;
		}

		public int No { get; }

		public static Times Once { get; } = new Times(1);
		public static Times Twice { get; } = new Times(2);
		public static Times Exactly(int no) => new Times(no);
	}
}
