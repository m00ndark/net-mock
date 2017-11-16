namespace NetMock.Rest
{
	public class RestRequestVerification : RestRequestDefinition
	{
		internal RestRequestVerification(RestMock restMock, Method method, string path, IMatch[] matches)
			: base(restMock, method, path, matches)
		{
			Parse();
		}

		protected override string DefinitionType => "verification";
	}
}
