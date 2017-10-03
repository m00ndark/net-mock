using System;
using System.Collections.Generic;
using NetMock.Rest;
using NetMock.Utils;

namespace NetMock
{
	public class ServiceMock
	{
		private readonly List<INetMock> _mocks;

		public ServiceMock()
		{
			_mocks = new List<INetMock>();
		}

		public RestMock CreateRestMock(string basePath, int port, Scheme scheme = Scheme.Http)
		{
			return _mocks.AddAndReturn(new RestMock(basePath, port, scheme));
		}

		public void Activate()
		{
			_mocks.ForEach(mock => mock.Activate());
		}

		public void TearDown()
		{
			_mocks.ForEach(mock => mock.TearDown());
		}
	}
}
