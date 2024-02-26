using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using NetMock.Exceptions;
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

		[OneTimeSetUp]
		public void Initialize()
		{
			RestMock.GlobalConfig.UseWildcardHostWhenListening = false;
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
			_client = new Client(Uri.UriSchemeHttp, "/api/v1", 9001);
			_secureClient = new Client(Uri.UriSchemeHttps, "/api/v1", 9001);
			ServiceMock.GlobalConfig.PrintReceivedRequestsOnTearDown = true;
		}

		[Test]
		public void Simple_ManualActivation()
		{
			using (ServiceMock serviceMock = new ServiceMock(ActivationStrategy.Manual))
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
		public void Simple_NoBasePath()
		{
			using (ServiceMock serviceMock = new ServiceMock(printReceivedRequestsOnTearDown: false))
			{
				// arrange
				const string responseBody = "{ 'state': 'alive' }";
				RestMock restMock = serviceMock.CreateRestMock(9001);
				restMock.Setup(Method.Get, "/alive").Returns(responseBody);

				// act
				Client client = new Client(Uri.UriSchemeHttp, string.Empty, 9001);
				IRestResponse response = client.Get("/alive");

				// assert
				JsonAssert.AreEqual(responseBody, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);

				serviceMock.PrintReceivedRequests();
			}
		}

		[Test]
		[Explicit]
		public void Simple_Secure()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateSecureRestMock("/api/v1", 9001,
					certificateFindType: X509FindType.FindByThumbprint,
					certificateFindValue: "78ac133aaf23b4d39e701b342cb5a5eb9a3924a0",
					storeName: StoreName.My,
					storeLocation: StoreLocation.LocalMachine);
				restMock.Setup(Method.Get, "/alive").Returns(message);

				// act
				IRestResponse response = _secureClient.Get("/alive");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);
			}
		}

		[Test]
		[Explicit]
		public void Simple_Secure2()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateSecureRestMock("/api/v1", 9001,
					certificateFindType: X509FindType.FindByExtension,
					certificateFindValue: "1.3.6.1.5.5.7.13.3",
					storeName: StoreName.My,
					storeLocation: StoreLocation.LocalMachine);
				restMock.Setup(Method.Get, "/alive").Returns(message);

				// act
				IRestResponse response = _secureClient.Get("/alive");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);
			}
		}

		[Test]
		public void UriSegmentParameter()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message/{id}", Parameter.IsAny<Guid>("id"))
					.Returns(message);

				// act
				IRestResponse response = _client.Get("/message/e910015f-7026-402d-a0ef-cfa6fecab19f");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/message/{id}", Parameter.IsAny<Guid>("id"), Times.Once);
			}
		}

		[Test]
		public void QueryParameter()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message?msgid={id}&x=y", Parameter.IsAny<Guid>("id"))
					.Returns(message);

				// act
				IRestResponse response = _client.Get("/message?msgid=e910015f-7026-402d-a0ef-cfa6fecab19f&x=y");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/message?msgid={id}&x=y", Parameter.IsAny<Guid>("id"), Times.Once);
			}
		}

		[Test]
		public void UriSegmentParameter_QueryParameter()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				string[] categories = { "FRUIT", "MEAT", "JAM" };
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message/{category}?msgid={id}&x=y",
						Parameter.IsAny<Guid>("id"),
						Parameter.Is("category", x => categories.Contains(x.ToUpper())))
					.Returns(message);

				// act
				IRestResponse response = _client.Get("/message/jam?msgid=e910015f-7026-402d-a0ef-cfa6fecab19f&x=y");

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/message/jam?msgid={id}&x=y", Parameter.IsAny<Guid>("id"), Times.Once);
			}
		}

		[Test]
		public void RequestBody_MockBehaviorLoose_NonMatchedRequests()
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

				// act
				IRestResponse response = _client.Post("/message/reverse", body: JsonConvert.SerializeObject(requestMessage));

				_client.Get("/alive");
				_client.Get("/message/e910015f-7026-402d-a0ef-cfa6fecab19f");
				_client.Get("/message/jam?msgid=e910015f-7026-402d-a0ef-cfa6fecab19f&x=y");

				// assert
				JsonAssert.AreEqual(responseMessage, response.Content);
				restMock.VerifyPost("/message/reverse", Body.Is(requestMessage), Times.Once);
				restMock.VerifyPost("/message/reverse", Body.Is("{ 'Text': 'torraP' }"), Times.Never);
			}
		}

		[Test]
		public void Patch()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message requestMessage = new Message("Parrot");
				Message responseMessage = new Message("torraP");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupPatch("/message/reverse", Body.Is(requestMessage))
					.Returns(responseMessage);

				// act
				IRestResponse response = _client.Patch("/message/reverse", body: JsonConvert.SerializeObject(requestMessage));

				// assert
				JsonAssert.AreEqual(responseMessage, response.Content);
				restMock.VerifyPatch("/message/reverse", Body.Is(requestMessage), Times.Once);
			}
		}

		[Test]
		public void RequestBody_MockBehaviorStrict_NonMatchedRequests()
		{
			using (ServiceMock serviceMock = new ServiceMock(printReceivedRequestsOnTearDown: false))
			{
				// arrange
				Message requestMessage = new Message("Parrot");
				Message responseMessage = new Message("torraP");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001, MockBehavior.Strict);

				restMock
					.SetupPost("/message/reverse", Body.Is(requestMessage))
					.Returns(responseMessage);

				// act
				IRestResponse response = _client.Post("/message/reverse", body: JsonConvert.SerializeObject(requestMessage));

				_client.Get("/alive");
				_client.Get("/message/e910015f-7026-402d-a0ef-cfa6fecab19f");
				_client.Get("/message/jam?msgid=e910015f-7026-402d-a0ef-cfa6fecab19f&x=y");

				// assert
				JsonAssert.AreEqual(responseMessage, response.Content);
				restMock.VerifyPost("/message/reverse", Body.Is(requestMessage), Times.Once);
				restMock.VerifyPost("/message/reverse", Body.Is("{ 'Text': 'torraP' }"), Times.Never);
				Console.WriteLine(Assert.Throws<StrictMockException>(() => restMock.TearDown()));
			}
		}

		[Test]
		public void RequestBodyMatchByCondition()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message requestMessage = new Message("Parrot");
				Message responseMessage = new Message("torraP");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupPost("/message/reverse", Body.Is(body => body.Length > 0))
					.Returns(responseMessage);

				// act
				IRestResponse response = _client.Post("/message/reverse", body: JsonConvert.SerializeObject(requestMessage));

				// assert
				JsonAssert.AreEqual(responseMessage, response.Content);
				restMock.VerifyPost("/message/reverse", Body.Is(requestMessage), Times.Once);
				restMock.VerifyPost("/message/reverse", Body.Is("{ 'Text': 'torraP' }"), Times.Never);
			}
		}

		[Test]
		public void NoResponseDefined_DefaultResponseStatusCode()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);
				restMock.DefaultResponseStatusCode = HttpStatusCode.NoContent;

				restMock.Setup(Method.Get, "/alive");

				// act
				IRestResponse response = _client.Get("/alive");

				// assert
				Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
				JsonAssert.AreEqual(string.Empty, response.Content);
				restMock.Verify(Method.Get, "/alive", Times.Once);
			}
		}

		[Test]
		public void StatusCodeAndResponseHeaders()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message requestMessage = new Message("Parrot");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);
				restMock.StaticResponseHeaders.Add(("Content-Type", "application/json"));

				restMock
					.SetupPost("/message/reverse/store", Body.Is(requestMessage))
					.Returns(HttpStatusCode.Created, ("X-Message-Mode", "normal"), ("X-Message-Case-Sensitive", "true"));

				// act
				IRestResponse response = _client.Post("/message/reverse/store", body: JsonConvert.SerializeObject(requestMessage));

				// assert
				Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
				CollectionAssert.Contains(response.Headers.Select(x => (x.Name, x.Value.ToString())), ("X-Message-Mode", "normal"));
				CollectionAssert.Contains(response.Headers.Select(x => (x.Name, x.Value.ToString())), ("X-Message-Case-Sensitive", "true"));
				CollectionAssert.Contains(response.Headers.Select(x => (x.Name, x.Value.ToString())), ("Content-Type", "application/json"));
				restMock.VerifyPost("/message/reverse/store", Body.Is(requestMessage), Times.Once);
			}
		}

		[Test]
		public void Headers()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Message message = new Message("Running");
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);
				restMock
					.SetupGet("/alive", Header.IsNotSet("X-Alive-State"), Header.Is("x-extra-data", value => value.Length == 6))
					.Returns(message);

				// act
				IRestResponse response = _client.Get("/alive", headers: new Dictionary<string, string>
					{
						{ "X-Extra-Data", "NoLife" },
						{ "X-Advanced-Config", "IncludeAfterLife" }
					});

				// assert
				JsonAssert.AreEqual(message, response.Content);
				restMock.VerifyGet("/alive", Header.IsSet("X-Advanced-Config"), Times.Once);
				restMock.VerifyGet("/alive", Header.Contains("X-Advanced-Config", "After"), Times.Once);
				restMock.VerifyPost("/alive", Header.IsSet("X-Advanced-Config"), Times.Never);
			}
		}

		[Test]
		public void VerifyTimes()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				void Call(Action call, int times)
				{
					for (int i = 0; i < times; i++)
						call();
				}

				// act
				Call(() => _client.Get("/never/0"), 0);
				Call(() => _client.Get("/never/1"), 1);
				Call(() => _client.Get("/once/0"), 0);
				Call(() => _client.Get("/once/1"), 1);
				Call(() => _client.Get("/once/2"), 2);
				Call(() => _client.Get("/twice/1"), 1);
				Call(() => _client.Get("/twice/2"), 2);
				Call(() => _client.Get("/twice/3"), 3);
				Call(() => _client.Get("/exactly/4"), 4);
				Call(() => _client.Get("/exactly/5"), 5);
				Call(() => _client.Get("/exactly/6"), 6);
				Call(() => _client.Get("/atleast/3"), 3);
				Call(() => _client.Get("/atleast/4"), 4);
				Call(() => _client.Get("/atleast/once/0"), 0);
				Call(() => _client.Get("/atleast/once/1"), 1);
				Call(() => _client.Get("/atleast/once/2"), 2);
				Call(() => _client.Get("/atmost/3"), 3);
				Call(() => _client.Get("/atmost/2"), 2);
				Call(() => _client.Get("/atmost/once/2"), 2);
				Call(() => _client.Get("/atmost/once/1"), 1);
				Call(() => _client.Get("/atmost/once/0"), 0);
				Call(() => _client.Get("/between/inclusive/3"), 3);
				Call(() => _client.Get("/between/inclusive/5"), 5);
				Call(() => _client.Get("/between/inclusive/7"), 7);
				Call(() => _client.Get("/between/exclusive/4"), 4);
				Call(() => _client.Get("/between/exclusive/5"), 5);
				Call(() => _client.Get("/between/exclusive/6"), 6);

				// assert
				restMock.VerifyGet("/never/0", Times.Never);
				restMock.VerifyGet("/once/1", Times.Once);
				restMock.VerifyGet("/twice/2", Times.Twice);
				restMock.VerifyGet("/exactly/5", Times.Exactly(5));
				restMock.VerifyGet("/atleast/3", Times.AtLeast(3));
				restMock.VerifyGet("/atleast/4", Times.AtLeast(3));
				restMock.VerifyGet("/atleast/once/1", Times.AtLeastOnce);
				restMock.VerifyGet("/atleast/once/2", Times.AtLeastOnce);
				restMock.VerifyGet("/atmost/3", Times.AtMost(3));
				restMock.VerifyGet("/atmost/2", Times.AtMost(3));
				restMock.VerifyGet("/atmost/once/1", Times.AtMostOnce);
				restMock.VerifyGet("/atmost/once/0", Times.AtMostOnce);
				restMock.VerifyGet("/between/inclusive/3", Times.Between(3, 7, Range.Inclusive));
				restMock.VerifyGet("/between/inclusive/5", Times.Between(3, 7, Range.Inclusive));
				restMock.VerifyGet("/between/inclusive/7", Times.Between(3, 7, Range.Inclusive));
				restMock.VerifyGet("/between/exclusive/4", Times.Between(3, 7, Range.Exclusive));
				restMock.VerifyGet("/between/exclusive/5", Times.Between(3, 7, Range.Exclusive));
				restMock.VerifyGet("/between/exclusive/6", Times.Between(3, 7, Range.Exclusive));

				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/never/1", Times.Never));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/once/0", Times.Once));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/once/2", Times.Once));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/twice/1", Times.Twice));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/twice/3", Times.Twice));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/exactly/4", Times.Exactly(5)));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/exactly/6", Times.Exactly(5)));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/atleast/3", Times.AtLeast(4)));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/atleast/once/0", Times.AtLeastOnce));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/atmost/3", Times.AtMost(2)));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/atmost/once/2", Times.AtMostOnce));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/between/inclusive/3", Times.Between(4, 6, Range.Inclusive)));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/between/inclusive/7", Times.Between(4, 6, Range.Inclusive)));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/between/exclusive/4", Times.Between(4, 6, Range.Exclusive)));
				Assert.Throws<NetMockException>(() => restMock.VerifyGet("/between/exclusive/6", Times.Between(4, 6, Range.Exclusive)));
			}
		}

		[Test]
		public void ResponseWithBodyProvider1()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message/{category}?meta={includeMeta}",
						Parameter.IsAny("category"),
						Parameter.IsAny<bool>("includeMeta"))
					.Returns<string, bool>((category, includeMeta) =>
						(200, new Message { { "Category", category.ToUpper() }, { "IncludeMeta", includeMeta.ToString() } }));

				// act
				IRestResponse response = _client.Get("/message/jam?meta=true");

				// assert
				JsonAssert.AreEqual("{ \"Category\": \"JAM\", \"IncludeMeta\": \"True\" }", response.Content);
				restMock.VerifyGet("/message/jam?meta={includeMeta}", Parameter.IsAny<bool>("includeMeta"), Times.Once);
			}
		}

		[Test]
		public void ResponseWithBodyProvider2()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupPost("/message/{category}",
						Parameter.IsAny("category"))
					.Returns<string>((request, category) =>
						(201, new Message { { "Category", category.ToUpper() }, { "Owner", request.Headers.TryGetValue("X-Auth-User", out string user) ? user : null } }));

				// act
				IRestResponse response = _client.Post("/message/jam",
					headers: new Dictionary<string, string> { { "X-Auth-User", "j.doe" } });

				// assert
				Assert.AreEqual(201, (int) response.StatusCode);
				JsonAssert.AreEqual("{ \"Category\": \"JAM\", \"Owner\": \"j.doe\" }", response.Content);
				restMock.VerifyPost("/message/jam", Header.IsSet("X-Auth-User"), Times.Once);
			}
		}

		[Test]
		public void ResponseWithBodyProvider3()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupPost("/message",
						Body.IsNotEmpty(),
						Header.IsSet("X-Category"))
					.Returns<string, string>((body, category) => new Message($"Category: {category.ToUpper()}"));

				Message requestMessage = new Message("Create-Category");

				// act
				IRestResponse response = _client.Post("/message",
					headers: new Dictionary<string, string> { { "X-Category", "Cake" } },
					body: JsonConvert.SerializeObject(requestMessage));

				// assert
				JsonAssert.AreEqual("{ \"Text\": \"Category: CAKE\" }", response.Content);
				restMock.VerifyPost("/message", Body.IsNotEmpty(), Times.Once);
			}
		}

		[Test]
		public void Callback()
		{
			using (ServiceMock serviceMock = new ServiceMock())
			{
				// arrange
				Dictionary<string, int> categoryHitCount = new Dictionary<string, int>();

				void IncrementHitCount(string category)
				{
					if (categoryHitCount.ContainsKey(category))
						categoryHitCount[category]++;
					else
						categoryHitCount[category] = 1;
				}

				RestMock restMock = serviceMock.CreateRestMock("/api/v1", 9001);

				restMock
					.SetupGet("/message/{category}?meta={includeMeta}",
						Parameter.IsAny("category"),
						Parameter.IsAny<bool>("includeMeta"))
					.Callback<string, bool>((category, _) => IncrementHitCount(category.ToUpper()));

				// act
				for (int i = 0; i < 4; i++)
				{
					for (int j = 0; j < i + 1; j++)
					{
						_client.Get($"/message/cat{j + 1}?meta=true");
					}
				}

				// assert
				Assert.IsTrue(categoryHitCount.ContainsKey("CAT1"));
				Assert.AreEqual(4, categoryHitCount["CAT1"]);
				Assert.IsTrue(categoryHitCount.ContainsKey("CAT2"));
				Assert.AreEqual(3, categoryHitCount["CAT2"]);
				Assert.IsTrue(categoryHitCount.ContainsKey("CAT3"));
				Assert.AreEqual(2, categoryHitCount["CAT3"]);
				Assert.IsTrue(categoryHitCount.ContainsKey("CAT4"));
				Assert.AreEqual(1, categoryHitCount["CAT4"]);
			}
		}
	}
}
