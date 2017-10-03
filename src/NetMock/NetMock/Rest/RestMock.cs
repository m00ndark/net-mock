using System.Collections.Generic;
using NetMock.Utils;

namespace NetMock.Rest
{
	public class RestMock : INetMock
	{
		private readonly List<RestMethodRequest> _requests;
		private bool _isActivated;

	    internal RestMock(string basePath, int port, Scheme scheme)
	    {
			_requests = new List<RestMethodRequest>();
		    _isActivated = false;

		    BasePath = basePath;
		    Port = port;
		    Scheme = scheme;
	    }

		public string BasePath { get; }
		public int Port { get; }
		public Scheme Scheme { get; }

		public void Activate()
	    {
			// todo: perform activation

		    _isActivated = true;
	    }

	    public void TearDown()
	    {
			// todo: perform tear down

		    _isActivated = false;
	    }

		public RestMethodRequest Setup(Method method, string path)
		{
			return _requests.AddAndReturn(new RestMethodRequest(method, path));
		}

		public void Verify(Method method, string path, Times times)
		{
			
		}
	}
}
