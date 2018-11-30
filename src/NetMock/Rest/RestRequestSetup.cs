using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetMock.Exceptions;

namespace NetMock.Rest
{
	public partial class RestRequestSetup : RestRequestDefinition
	{
		internal RestRequestSetup(RestMock restMock, Method method, string path, IMatch[] matches)
			: base(restMock, method, path, matches)
		{
			if (restMock.ServiceMock.ActivationStrategy == ActivationStrategy.AutomaticOnCreation)
				Parse();
		}

		protected override string DefinitionType => "setup";

		internal RestResponseDefinition Response { get; private set; }
		internal Delegate CallbackAction { get; private set; }

		private void SetCallback(Delegate callback)
		{
			ParameterInfo[] parameters = callback.Method.GetParameters();
			bool firstParameterIsRequest = parameters.FirstOrDefault()?.ParameterType == typeof(IReceivedRequest);
			int genericParameterCount = firstParameterIsRequest ? parameters.Length - 1 : parameters.Length;

			if (genericParameterCount != Matches.Length)
				throw new MockSetupException("Number of callback generic arguments does not match number of provided matches");

			Type[] parameterTypes = parameters.Select(x => x.ParameterType).Skip(firstParameterIsRequest ? 1 : 0).ToArray();
			Type[] matchTypes = Matches.Select(x => x.GetType().GenericTypeArguments[0]).ToArray();

			if (!parameterTypes.SequenceEqual(matchTypes))
				throw new MockSetupException("Types of callback generic arguments does not match value types of provided matches: "
					+ $"[Arguments: {string.Join(", ", parameterTypes.Select(x => x.Name))}] <-> [Matches: {string.Join(", ", matchTypes.Select(x => x.Name))}]");

			if (callback.Method.ReturnType != typeof(void))
				throw new MockSetupException("Invalid callback delegate; return type should be void");

			CallbackAction = callback;
		}

		internal void TryCallback(IReceivedRequest request, IList<MatchResult> matchResults)
		{
			if (CallbackAction == null)
				return;

			IEnumerable<object> args = Matches
				.Select(match => matchResults.First(matchResult => matchResult.Match == match).MatchedValue);

			if (CallbackAction.Method.GetParameters().FirstOrDefault()?.ParameterType == typeof(IReceivedRequest))
				args = args.Prepend(request);

			CallbackAction.DynamicInvoke(args.ToArray());
		}
	}
}
