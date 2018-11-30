using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using NetMock.Exceptions;
using NetMock.Utils;

namespace NetMock.Rest
{
	internal class RestResponseDefinition
	{
		private static readonly HashSet<Type> _validResponseProviderReturnTypes = new HashSet<Type>
			{
				typeof(int),
				typeof(HttpStatusCode),
				typeof(object),
				typeof(IEnumerable<AttachedHeader>),
				typeof((int, object)),
				typeof((HttpStatusCode, object)),
				typeof((int, object, IEnumerable<AttachedHeader>)),
				typeof((HttpStatusCode, object, IEnumerable<AttachedHeader>)),
				typeof((object, IEnumerable<AttachedHeader>)),
			};

		public RestResponseDefinition(RestRequestSetup setup, IEnumerable<AttachedHeader> headers = null)
			: this(setup, -1, null, headers) { }

		public RestResponseDefinition(RestRequestSetup setup, int statusCode, IEnumerable<AttachedHeader> headers = null)
			: this(setup, statusCode, null, headers) { }

		public RestResponseDefinition(RestRequestSetup setup, object body, IEnumerable<AttachedHeader> headers = null)
			: this(setup, -1, body, headers) { }

		public RestResponseDefinition(RestRequestSetup setup, int statusCode, object body, IEnumerable<AttachedHeader> headers = null)
		{
			Setup = setup ?? throw new ArgumentNullException(nameof(setup));
			StatusCode = statusCode;
			Body = body;
			Headers = headers;
		}

		public RestResponseDefinition(RestRequestSetup setup, Delegate responseProvider)
		{
			Setup = setup ?? throw new ArgumentNullException(nameof(setup));
			ResponseProvider = responseProvider ?? throw new ArgumentNullException(nameof(responseProvider));

			ValidateResponseProvider();
		}

		private RestRequestSetup Setup { get; }
		public int StatusCode { get; } = -1;
		public IEnumerable<AttachedHeader> Headers { get; }
		public object Body { get; }
		public Delegate ResponseProvider { get; }

		private void ValidateResponseProvider()
		{
			if (ResponseProvider == null)
				return;

			ParameterInfo[] parameters = ResponseProvider.Method.GetParameters();
			bool firstParameterIsRequest = parameters.FirstOrDefault()?.ParameterType == typeof(IReceivedRequest);
			int genericParameterCount = firstParameterIsRequest ? parameters.Length - 1 : parameters.Length;

			if (genericParameterCount != Setup.Matches.Length)
				throw new MockSetupException("Number of response provider generic arguments does not match number of provided matches");

			Type[] parameterTypes = parameters.Select(x => x.ParameterType).Skip(firstParameterIsRequest ? 1 : 0).ToArray();
			Type[] matchTypes = Setup.Matches.Select(x => x.GetType().GenericTypeArguments[0]).ToArray();

			if (!parameterTypes.SequenceEqual(matchTypes))
				throw new MockSetupException("Types of response provider generic arguments does not match value types of provided matches: "
					+ $"[Arguments: {string.Join(", ", parameterTypes.Select(x => x.Name))}] <-> [Matches: {string.Join(", ", matchTypes.Select(x => x.Name))}]");

			if (!_validResponseProviderReturnTypes.Contains(ResponseProvider.Method.ReturnType))
				throw new MockSetupException("Invalid return type of response provider");
		}

		public (int StatusCode, string Body, IEnumerable<AttachedHeader> Headers) Provide(IReceivedRequest request, IList<MatchResult> matchResults)
		{
			if (ResponseProvider == null)
				return (StatusCode, Body.ToStringBody(), Headers);

			IEnumerable<object> args = Setup.Matches
				.Select(match => matchResults.First(matchResult => matchResult.Match == match).MatchedValue);

			if (ResponseProvider.Method.GetParameters().FirstOrDefault()?.ParameterType == typeof(IReceivedRequest))
				args = args.Prepend(request);

			object result = ResponseProvider.DynamicInvoke(args.ToArray());

			switch (result)
			{
				case ValueTuple<int, object> typedResult:
					return (typedResult.Item1, typedResult.Item2.ToStringBody(), null);
				case ValueTuple<HttpStatusCode, object> typedResult:
					return ((int) typedResult.Item1, typedResult.Item2.ToStringBody(), null);
				case ValueTuple<int, object, IEnumerable<AttachedHeader>> typedResult:
					return (typedResult.Item1, typedResult.Item2.ToStringBody(), typedResult.Item3);
				case ValueTuple<HttpStatusCode, object, IEnumerable<AttachedHeader>> typedResult:
					return ((int) typedResult.Item1, typedResult.Item2.ToStringBody(), typedResult.Item3);
				case ValueTuple<object, IEnumerable<AttachedHeader>> typedResult:
					return (-1, typedResult.Item1.ToStringBody(), typedResult.Item2);
				case int typedResult:
					return (typedResult, null, null);
				case HttpStatusCode typedResult:
					return ((int) typedResult, null, null);
				case IEnumerable<AttachedHeader> typedResult:
					return (-1, null, typedResult);
				case object typedResult:
					return (-1, typedResult.ToStringBody(), null);
			}

			return (StatusCode, Body.ToStringBody(), Headers);
		}
	}
}
