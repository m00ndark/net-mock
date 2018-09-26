using NetMock.Utils;

namespace NetMock.Rest
{
	public partial class RestMock
	{
		public RestRequestSetup SetupGet(string path, params IMatch[] matches)
			=> Setup(Method.Get, path, matches);

		public RestRequestSetup SetupPost(string path, params IMatch[] matches)
			=> Setup(Method.Post, path, matches);

		public RestRequestSetup SetupPut(string path, params IMatch[] matches)
			=> Setup(Method.Put, path, matches);

		public RestRequestSetup SetupDelete(string path, params IMatch[] matches)
			=> Setup(Method.Delete, path, matches);

		public RestRequestSetup SetupHead(string path, params IMatch[] matches)
			=> Setup(Method.Head, path, matches);

		public RestRequestSetup SetupOptions(string path, params IMatch[] matches)
			=> Setup(Method.Options, path, matches);

		public RestRequestSetup SetupTrace(string path, params IMatch[] matches)
			=> Setup(Method.Trace, path, matches);

		public RestRequestSetup SetupConnect(string path, params IMatch[] matches)
			=> Setup(Method.Connect, path, matches);

		public RestRequestSetup Setup(Method method, string path, params IMatch[] matches)
			=> _requestDefinitions.AddAndReturn(new RestRequestSetup(this, method, path, matches));
	}
}
