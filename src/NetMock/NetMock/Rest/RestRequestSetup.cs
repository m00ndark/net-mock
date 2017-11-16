using System;

namespace NetMock.Rest
{
	public class RestRequestSetup : RestRequestDefinition
	{
		internal RestRequestSetup(RestMock restMock, Method method, string path, IMatch[] matches)
			: base(restMock, method, path, matches) { }

		protected override string DefinitionType => "setup";

		public RestResponseDefinition Response { get; private set; }

		public RestResponseDefinition Returns(object body)
			=> Response = new RestResponseDefinition(body);

		//public RestResponseDefinition Returns(int statusCode, object body)
		//	=> Response = new RestResponseDefinition(body);

		public RestResponseDefinition Returns<T1>(Func<T1, object> bodyProvider)
			=> Response = new RestResponseDefinition(bodyProvider);

		//public RestResponseDefinition Returns<T1>(int statusCode, Func<T1, object> bodyProvider)
		//	=> Response = new RestResponseDefinition(bodyProvider);

		public RestResponseDefinition Returns<T1, T2>(Func<T1, T2, object> bodyProvider)
			=> Response = new RestResponseDefinition(bodyProvider);

		public RestResponseDefinition Returns<T1, T2, T3>(Func<T1, T2, T3, object> bodyProvider)
			=> Response = new RestResponseDefinition(bodyProvider);

		public RestResponseDefinition Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, object> bodyProvider)
			=> Response = new RestResponseDefinition(bodyProvider);

		public RestResponseDefinition Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, object> bodyProvider)
			=> Response = new RestResponseDefinition(bodyProvider);
	}
}
