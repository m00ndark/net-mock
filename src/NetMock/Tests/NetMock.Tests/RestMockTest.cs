using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using NetMock.Rest;
using NetMock.Tests.Model;
using NetMock.Tests.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Method = NetMock.Rest.Method;
using Parameter = NetMock.Rest.Parameter;

namespace NetMock.Tests
{
	[TestFixture]
	public class RestMockTest
	{
		private Client _client;
		private Client _secureClient;

		[SetUp]
		public void Initialize()
		{
			_client = new Client(Uri.UriSchemeHttp, "/api/v1", 9001);
			_secureClient = new Client(Uri.UriSchemeHttps, "/api/v1", 9001);
		}

		[Test]
		public void Scenario01_Simple()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);
				restMock.Setup(Method.Get, "/alive").Returns(message);
				serviceMock.Activate();

				// act
				IRestResponse response = _client.Get("/alive");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);
			}
		}

		[Test]
		public void Scenario01_Simple_Secure()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateSecureRestMock("/api/v1", 9001,
					certificateThumbprint: "73f21fabb9f239159cfa76d42283200b55b74ed4",
					storeName: StoreName.My,
					storeLocation: StoreLocation.LocalMachine);
				restMock.Setup(Method.Get, "/alive").Returns(message);
				serviceMock.Activate();

				// act
				IRestResponse response = _secureClient.Get("/alive");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);
			}
		}

		[Test]
		public void Scenario02_UriSegmentParameter()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message/{id}", Parameter.IsAny<Guid>("id"))
					.Returns(message);

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Get("/message/e910015f-7026-402d-a0ef-cfa6fecab19f");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/message/{id}", Parameter.IsAny<Guid>("id"), Times.Once);
			}
		}

		[Test]
		public void Scenario02_QueryParameter()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message?msgid={id}&x=y", Parameter.IsAny<Guid>("id"))
					.Returns(message);

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Get("/message?msgid=e910015f-7026-402d-a0ef-cfa6fecab19f&x=y");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/message?msgid={id}", Parameter.IsAny<Guid>("id"), Times.Once);
			}
		}

		[Test]
		public void Scenario03_Body()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message requestMessage = new Message("Parrot");
				Message responseMessage = new Message("torraP");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupPost("/message/reverse", Body.Is(requestMessage))
					.Returns(responseMessage);

				serviceMock.Activate();

				// act
				IRestResponse response = _client.Post("/message/reverse", body: JsonConvert.SerializeObject(requestMessage));

				// assert
				JsonAssert.AreEqual(responseMessage, response.Content);
				restMock.VerifyPost("/message/reverse", Body.Is(requestMessage), Times.Once);
				restMock.VerifyPost("/message/reverse", Body.Is("{ 'Text': 'torraP' }"), Times.Never);
			}
		}
	}
}
