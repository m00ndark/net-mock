using System;
using System.Security.Cryptography.X509Certificates;
using NetMock.Rest;
using NetMock.Utils;

namespace NetMock
{
	public enum ActivationStrategy
	{
		Manual,
		AutomaticOnCreation
	}

	public class ServiceMock : IDisposable
	{
		public static class GlobalConfig
		{
			public static ActivationStrategy ActivationStrategy { get; set; } = ActivationStrategy.AutomaticOnCreation;
			public static MockBehavior MockBehavior { get; set; } = MockBehavior.Loose;
			public static bool PrintReceivedRequestsOnTearDown { get; set; } = false;
		}

		private readonly ProtectedList<INetMock> _mocks;
		private ActivationStrategy? _activationStrategy;
		private MockBehavior? _mockBehavior;
		private bool? _printReceivedRequestsOnTearDown;

		public ServiceMock(ActivationStrategy? activationStrategy = null, MockBehavior? mockBehavior = null, bool? printReceivedRequestsOnTearDown = null)
		{
			_mocks = new ProtectedList<INetMock>();
			_activationStrategy = activationStrategy;
			_mockBehavior = mockBehavior;
			_printReceivedRequestsOnTearDown = printReceivedRequestsOnTearDown;
		}

		public ActivationStrategy ActivationStrategy
		{
			get => _activationStrategy ?? GlobalConfig.ActivationStrategy;
			set => _activationStrategy = value;
		}

		public MockBehavior MockBehavior
		{
			get => _mockBehavior ?? GlobalConfig.MockBehavior;
			set => _mockBehavior = value;
		}

		public bool PrintReceivedRequestsOnTearDown
		{
			get => _printReceivedRequestsOnTearDown ?? GlobalConfig.PrintReceivedRequestsOnTearDown;
			set => _printReceivedRequestsOnTearDown = value;
		}

		public RestMock CreateRestMock(int port, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateRestMock(string.Empty, port, mockBehavior);

		public RestMock CreateRestMock(string basePath, int port, MockBehavior mockBehavior = MockBehavior.Loose)
		{
			return _mocks.AddAndReturn(new RestMock(this, basePath, port, Scheme.Http, mockBehavior: mockBehavior), restMock =>
				{
					if (ActivationStrategy == ActivationStrategy.AutomaticOnCreation)
						restMock.Activate();
				});
		}

		public RestMock CreateSecureRestMock(int port, X509FindType certificateFindType, string certificateFindValue, StoreName storeName, StoreLocation storeLocation, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateSecureRestMock(string.Empty, port, CertificateUtil.LoadCertificate(certificateFindType, certificateFindValue, storeName, storeLocation), false, mockBehavior);

		public RestMock CreateSecureRestMock(string basePath, int port, X509FindType certificateFindType, string certificateFindValue, StoreName storeName, StoreLocation storeLocation, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateSecureRestMock(basePath, port, CertificateUtil.LoadCertificate(certificateFindType, certificateFindValue, storeName, storeLocation), false, mockBehavior);

		public RestMock CreateSecureRestMock(int port, X509Certificate2 certificate, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateSecureRestMock(string.Empty, port, certificate, true, mockBehavior);

		public RestMock CreateSecureRestMock(string basePath, int port, X509Certificate2 certificate, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateSecureRestMock(basePath, port, certificate, true, mockBehavior);

		private RestMock CreateSecureRestMock(string basePath, int port, X509Certificate2 certificate, bool installCertificate, MockBehavior mockBehavior = MockBehavior.Loose)
		{
			return _mocks.AddAndReturn(new RestMock(this, basePath, port, Scheme.Https, certificate, installCertificate, mockBehavior), restMock =>
			{
				if (ActivationStrategy == ActivationStrategy.AutomaticOnCreation)
					restMock.Activate();
			});
		}

		public void PrintReceivedRequests()
		{
			_mocks.ForEach(mock => mock.PrintReceivedRequests());
		}

		public void Activate()
		{
			if (ActivationStrategy == ActivationStrategy.Manual)
				_mocks.ForEach(mock => mock.Activate());
		}

		public void TearDown()
		{
			foreach (INetMock mock in _mocks)
			{
				if (PrintReceivedRequestsOnTearDown)
					mock.PrintReceivedRequests();

				mock.TearDown();
			}
		}

		public void Dispose()
		{
			TearDown();
		}
	}
}
