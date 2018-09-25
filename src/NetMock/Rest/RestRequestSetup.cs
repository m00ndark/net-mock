using System;
using System.Collections.Generic;
using System.Net;

namespace NetMock.Rest
{
	public class RestRequestSetup : RestRequestDefinition
	{
		internal RestRequestSetup(RestMock restMock, Method method, string path, IMatch[] matches)
			: base(restMock, method, path, matches) { }

		protected override string DefinitionType => "setup";

		public RestResponseDefinition Response { get; private set; }

		public RestResponseDefinition Returns(int statusCode, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(statusCode, headers);

		public RestResponseDefinition Returns(HttpStatusCode statusCode, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition((int) statusCode, headers);

		public RestResponseDefinition Returns(object body, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(body, headers);

		public RestResponseDefinition Returns(int statusCode, object body, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(statusCode, body, headers);

		public RestResponseDefinition Returns(HttpStatusCode statusCode, object body, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition((int) statusCode, body, headers);

		//public RestResponseDefinition Returns(Func<int> statusCodeProvider)
		//	=> Response = new RestResponseDefinition(responseProvider);

		public RestResponseDefinition Returns(Func<(int, object, IEnumerable<AttachedHeader>)> responseProvider)
			=> Response = new RestResponseDefinition(responseProvider);

		public RestResponseDefinition Returns<T1>(Func<T1, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(bodyProvider, headers);

		public RestResponseDefinition Returns<T1>(int statusCode, Func<T1, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1>(HttpStatusCode statusCode, Func<T1, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition((int) statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2>(Func<T1, T2, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2>(int statusCode, Func<T1, T2, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2>(HttpStatusCode statusCode, Func<T1, T2, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition((int) statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3>(Func<T1, T2, T3, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3>(int statusCode, Func<T1, T2, T3, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3>(HttpStatusCode statusCode, Func<T1, T2, T3, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition((int) statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3, T4>(int statusCode, Func<T1, T2, T3, T4, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3, T4>(HttpStatusCode statusCode, Func<T1, T2, T3, T4, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition((int) statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3, T4, T5>(int statusCode, Func<T1, T2, T3, T4, T5, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(statusCode, bodyProvider, headers);

		public RestResponseDefinition Returns<T1, T2, T3, T4, T5>(HttpStatusCode statusCode, Func<T1, T2, T3, T4, T5, object> bodyProvider, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition((int) statusCode, bodyProvider, headers);
	}
}
