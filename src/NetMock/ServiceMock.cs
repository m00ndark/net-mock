using System;
using System.Collections.Generic;
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
		private readonly List<INetMock> _mocks;

		public ServiceMock(ActivationStrategy activationStrategy = ActivationStrategy.Manual)
		{
			ActivationStrategy = activationStrategy;
			_mocks = new List<INetMock>();
		}

		public ActivationStrategy ActivationStrategy { get; }

		public RestMock CreateRestMock(int port, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateRestMock(string.Empty, port, mockBehavior);

		public RestMock CreateRestMock(string basePath, int port, MockBehavior mockBehavior = MockBehavior.Loose)
		{
			return _mocks.AddAndReturn(new RestMock(basePath, port, Scheme.Http, mockBehavior: mockBehavior));
		}

		public RestMock CreateSecureRestMock(int port, string certificateThumbprint, StoreName storeName, StoreLocation storeLocation, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateSecureRestMock(string.Empty, port, CertificateUtil.LoadCertifiace(certificateThumbprint, storeName, storeLocation), mockBehavior);

		public RestMock CreateSecureRestMock(string basePath, int port, string certificateThumbprint, StoreName storeName, StoreLocation storeLocation, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateSecureRestMock(basePath, port, CertificateUtil.LoadCertifiace(certificateThumbprint, storeName, storeLocation), mockBehavior);

		public RestMock CreateSecureRestMock(int port, X509Certificate2 certificate, MockBehavior mockBehavior = MockBehavior.Loose)
			=> CreateSecureRestMock(string.Empty, port, certificate, mockBehavior);

		public RestMock CreateSecureRestMock(string basePath, int port, X509Certificate2 certificate, MockBehavior mockBehavior = MockBehavior.Loose)
		{
			return _mocks.AddAndReturn(new RestMock(basePath, port, Scheme.Https, certificate, mockBehavior));
		}

		public void Activate()
		{
			if (ActivationStrategy == ActivationStrategy.Manual)
				_mocks.ForEach(mock => mock.Activate());
		}

		public void TearDown()
		{
			_mocks.ForEach(mock => mock.TearDown());
		}

		public void Dispose()
		{
			TearDown();
		}
	}
}
