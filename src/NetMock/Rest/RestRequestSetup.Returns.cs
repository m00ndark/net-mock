using System;
using System.Collections.Generic;
using System.Net;

namespace NetMock.Rest
{
	public partial class RestRequestSetup
	{
		public void Returns(params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(this, headers);

		public void Returns(int statusCode, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(this, statusCode, headers);

		public void Returns(HttpStatusCode statusCode, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(this, (int) statusCode, headers);

		public void Returns(object body, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(this, body, headers);

		public void Returns(int statusCode, object body, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(this, statusCode, body, headers);

		public void Returns(HttpStatusCode statusCode, object body, params AttachedHeader[] headers)
			=> Response = new RestResponseDefinition(this, (int) statusCode, body, headers);


		public void Returns(Func<int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns(Func<IReceivedRequest, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns(Func<HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns(Func<IReceivedRequest, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns(Func<object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns(Func<IReceivedRequest, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns(Func<IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns(Func<IReceivedRequest, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns(Func<(int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<IReceivedRequest, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<(HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<IReceivedRequest, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<(int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<IReceivedRequest, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<(HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<IReceivedRequest, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<(object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns(Func<IReceivedRequest, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1>(Func<T1, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1>(Func<T1, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1>(Func<T1, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1>(Func<T1, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1>(Func<T1, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<T1, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<T1, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<T1, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<T1, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1>(Func<IReceivedRequest, T1, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1, T2>(Func<T1, T2, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2>(Func<T1, T2, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2>(Func<T1, T2, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2>(Func<T1, T2, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2>(Func<T1, T2, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<T1, T2, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<T1, T2, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<T1, T2, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<T1, T2, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2>(Func<IReceivedRequest, T1, T2, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1, T2, T3>(Func<T1, T2, T3, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3>(Func<T1, T2, T3, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3>(Func<T1, T2, T3, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3>(Func<T1, T2, T3, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3>(Func<T1, T2, T3, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<T1, T2, T3, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<T1, T2, T3, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<T1, T2, T3, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<T1, T2, T3, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3>(Func<IReceivedRequest, T1, T2, T3, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<T1, T2, T3, T4, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4>(Func<IReceivedRequest, T1, T2, T3, T4, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5>(Func<IReceivedRequest, T1, T2, T3, T4, T5, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);


		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, int> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, HttpStatusCode> statusCodeProvider)
			=> Response = new RestResponseDefinition(this, statusCodeProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, object> bodyProvider)
			=> Response = new RestResponseDefinition(this, bodyProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, IEnumerable<AttachedHeader>> headersProvider)
			=> Response = new RestResponseDefinition(this, headersProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, (int StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, (HttpStatusCode StatusCode, object Body)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, (int StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, (HttpStatusCode StatusCode, object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);

		public void Returns<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<IReceivedRequest, T1, T2, T3, T4, T5, T6, T7, T8, T9, (object Body, IEnumerable<AttachedHeader> Headers)> responseProvider)
			=> Response = new RestResponseDefinition(this, responseProvider);
	}
}
