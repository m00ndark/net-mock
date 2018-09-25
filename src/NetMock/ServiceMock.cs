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

		public RestMock CreateRestMock(int port)
			=> CreateRestMock(string.Empty, port);

		public RestMock CreateRestMock(string basePath, int port)
		{
			return _mocks.AddAndReturn(new RestMock(basePath, port, Scheme.Http));
		}

		public RestMock CreateSecureRestMock(int port, string certificateThumbprint, StoreName storeName, StoreLocation storeLocation)
			=> CreateSecureRestMock(string.Empty, port, CertificateUtil.LoadCertifiace(certificateThumbprint, storeName, storeLocation));

		public RestMock CreateSecureRestMock(string basePath, int port, string certificateThumbprint, StoreName storeName, StoreLocation storeLocation)
			=> CreateSecureRestMock(basePath, port, CertificateUtil.LoadCertifiace(certificateThumbprint, storeName, storeLocation));

		public RestMock CreateSecureRestMock(int port, X509Certificate2 certificate)
			=> CreateSecureRestMock(string.Empty, port, certificate);

		public RestMock CreateSecureRestMock(string basePath, int port, X509Certificate2 certificate)
		{
			return _mocks.AddAndReturn(new RestMock(basePath, port, Scheme.Https, certificate));
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
