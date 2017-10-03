using NetMock.Rest;
using NetMock.Tests.Model;
using NUnit.Framework;

namespace NetMock.Tests
{
	[TestFixture]
	public class RestMockTest
	{
		[Test]
		public void Scenario01()
		{
			// arrange
			ServiceMock serviceMock = new ServiceMock();
			RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);
			restMock.Setup(Method.Get, "/alive").Returns(new Message("Running"));
			serviceMock.Activate();

			// act
			// ...

			// assert
			restMock.Verify(Method.Get, "/alive", Times.Once);

			// teardown
			serviceMock.TearDown();
		}
	}
}
