using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetMock.Exceptions;
using NetMock.Rest.Parsed;
using NetMock.Utils;

namespace NetMock.Rest
{
	public abstract class RestRequestDefinition
	{
		private static readonly Regex _pathRegEx = new Regex(@"^
			(?:/|(?:/([0-9a-zA-Z\-]+|\{[a-zA-Z][0-9a-zA-Z]*\}))*
			(?:\?([0-9a-zA-Z\-]+)(?:=([0-9a-zA-Z\-]+|\{[a-zA-Z][0-9a-zA-Z]*\})){0,1}
			(?:\&([0-9a-zA-Z\-]+)(?:=([0-9a-zA-Z\-]+|\{[a-zA-Z][0-9a-zA-Z]*\})){0,1})*)*)
			$", RegexOptions.IgnorePatternWhitespace);

		private string _parsedMethod;
		private List<ParsedUriSegment> _parsedUriSegments;
		private List<ParsedQueryParameter> _parsedQueryParameters;
		private List<ParsedHeader> _parsedHeaders;
		private ParsedBody _parsedBody;

		protected RestRequestDefinition(RestMock restMock, Method method, string path, IMatch[] matches)
		{
			RestMock = restMock;
			Method = method;
			Path = path ?? throw new ArgumentNullException(nameof(path));
			Matches = matches;
		}

		protected abstract string DefinitionType { get; }

		private RestMock RestMock { get; }
		public Method Method { get; }
		public string Path { get; }
		public IMatch[] Matches { get; }

		internal void Parse()
		{
			_parsedMethod = Method.ToString().ToUpperInvariant();

			Match regExMatch = _pathRegEx.Match(Path);

			if (!regExMatch.Success)
				throw new MockSetupException($"Invalid {DefinitionType} path: {Path}");

			IDictionary<string, ParameterMatch> parameterMatches = Matches
				.OfType<ParameterMatch>()
				.ToDictionary(parameterMatch => parameterMatch.Name);

			ParameterMatch GetParameterMatch(string parameterName)
			{
				if (!parameterMatches.TryGetValue(parameterName, out ParameterMatch parameterMatch))
					throw new MockSetupException($"Parameter in {DefinitionType} path not defined: {parameterName}");

				return parameterMatch;
			}

			_parsedUriSegments = regExMatch.Groups[1].Captures
				.Cast<Capture>()
				.Select(x => new
					{
						x.Value,
						ParameterMatch = x.Value.IsParameter(out string parameterName) ? GetParameterMatch(parameterName) : null
					})
				.Select(x => x.ParameterMatch != null
					? new ParameterizedUriSegment(x.ParameterMatch)
					: (ParsedUriSegment) new StaticUriSegment(x.Value))
				.ToList();

			IEnumerable<(string Name, string Value)> queryParameters = regExMatch.Groups[2].Success
				? regExMatch.Groups[4].Captures
					.Cast<Capture>()
					.Select((x, i) => (x.Value, regExMatch.Groups[5].Captures[i].Value))
					.Append((regExMatch.Groups[2].Value, regExMatch.Groups[3].Value))
				: null;

			_parsedQueryParameters = queryParameters?
				.Select(x => new
					{
						x.Name,
						x.Value,
						ParameterMatch = x.Value.IsParameter(out string parameterName) ? GetParameterMatch(parameterName) : null
					})
				.Select(x => x.ParameterMatch != null
					? new ParameterizedQueryParameter(x.Name, x.ParameterMatch)
					: (ParsedQueryParameter) new StaticQueryParameter(x.Name, x.Value))
				.ToList() ?? new List<ParsedQueryParameter>();

			// todo: parse headers here

			IList<BodyMatch> bodyMatches = Matches
				.OfType<BodyMatch>()
				.ToArray();

			if (bodyMatches.Count > 1)
				throw new MockSetupException($"Only one body match can be given per {DefinitionType}");

			if (bodyMatches.Count == 1)
				_parsedBody = new ParsedBody(bodyMatches[0], RestMock.InterpretBodyAsJson);
		}

		internal bool Match(IReceivedRequest request, out IList<MatchResult> matchResult)
		{
			matchResult = new List<MatchResult>();

			if (request.Method != _parsedMethod)
				return false;

			IList<string> baseUriSegments = RestMock.BaseUri.TrimmedSegments();
			IList<string> uriSegments = request.Uri.TrimmedSegments();

			if (!baseUriSegments.SequenceEqual(uriSegments.Take(baseUriSegments.Count), StringComparer.OrdinalIgnoreCase))
				return false;

			uriSegments = uriSegments
				.Skip(baseUriSegments.Count)
				.ToArray();

			if (!MatchUriSegments(uriSegments, matchResult))
				return false;

			IDictionary<string, string> parameters = request.Uri.Query
				.TrimStart('?')
				.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(parameter => parameter.Split(new[] { '=' }, 2))
				.ToDictionary(parameterSplit => parameterSplit[0], parameterSplit => parameterSplit.Length > 1 ? parameterSplit[1] : null);

			if (!MatchQueryParameters(parameters, matchResult))
				return false;

			// todo: match body and headers here

			if (!MatchBody(request.Body, matchResult))
				return false;

			return true;
		}

		private bool MatchUriSegments(IList<string> uriSegments, ICollection<MatchResult> matchResult)
		{
			if (uriSegments.Count != _parsedUriSegments.Count)
				return false;

			for (int i = 0; i < uriSegments.Count; i++)
			{
				MatchResult result = _parsedUriSegments[i].Match(uriSegments[i]);

				if (!result.IsMatch)
					return false;

				matchResult.Add(result);
			}

			return true;
		}

		private bool MatchQueryParameters(IDictionary<string, string> parameters, ICollection<MatchResult> matchResult)
		{
			if (RestMock.UndefinedQueryParameterHandling == UndefinedHandling.Fail && parameters.Count != _parsedQueryParameters.Count
				|| RestMock.UndefinedQueryParameterHandling == UndefinedHandling.Ignore && parameters.Count < _parsedQueryParameters.Count)
			{
				return false;
			}

			HashSet<ParsedQueryParameter> matchedParameters = new HashSet<ParsedQueryParameter>();
			foreach (var parameter in parameters)
			{
				var result = _parsedQueryParameters
					.Select(queryParameter => new
						{
							QueryParameter = queryParameter,
							MatchResult = queryParameter.Match(parameter.Key, parameter.Value)
						})
					.FirstOrDefault(x => x.MatchResult.IsMatch && !matchedParameters.Contains(x.QueryParameter));

				if (result != null)
				{
					matchedParameters.Add(result.QueryParameter);
					matchResult.Add(result.MatchResult);
				}
			}

			return matchedParameters.Count == _parsedQueryParameters.Count;
		}

		private bool MatchHeaders(IDictionary<string, string> headers, ICollection<MatchResult> matchResult)
		{
			//if (RestMock.UndefinedHeaderHandling == UndefinedHandling.Fail && headers.Count != _parsedQueryParameters.Count
			//	|| RestMock.UndefinedHeaderHandling == UndefinedHandling.Ignore && headers.Count < _parsedQueryParameters.Count)
			//{
			//	return false;
			//}

			//HashSet<QueryParameter> matchedParameters = new HashSet<QueryParameter>();
			//foreach (var parameter in headers)
			//{
			//	var result = _parsedQueryParameters
			//		.Select(queryParameter => new
			//			{
			//				QueryParameter = queryParameter,
			//				MatchResult = queryParameter.Match(parameter.Key, parameter.Value)
			//			})
			//		.FirstOrDefault(x => x.MatchResult.IsMatch && !matchedParameters.Contains(x.QueryParameter));

			//	if (result != null)
			//	{
			//		matchedParameters.Add(result.QueryParameter);
			//		matchResult.Add(result.MatchResult);
			//	}
			//}

			//return matchedParameters.Count == _parsedQueryParameters.Count;

			throw new NotImplementedException();
		}

		private bool MatchBody(string body, ICollection<MatchResult> matchResult)
		{
			if (_parsedBody == null)
				return true;

			MatchResult result = _parsedBody.Match(body);

			if (result.IsMatch)
				matchResult.Add(result);

			return result.IsMatch;
		}
	}
}
