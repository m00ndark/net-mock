namespace NetMock.Rest
{
	public class RestRequestVerification : RestRequestDefinition
	{
		internal RestRequestVerification(RestMock restMock, Method method, string path, object body, IMatch[] matches)
			: base(restMock, method, path, body, matches)
		{
			Parse();
		}

		protected override string DefinitionType => "verification";
	}
}
