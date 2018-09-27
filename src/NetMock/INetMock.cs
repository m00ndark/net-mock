namespace NetMock
{
	public interface INetMock
	{
		void Activate();
		void TearDown();
		void PrintReceivedRequests();
	}
}
