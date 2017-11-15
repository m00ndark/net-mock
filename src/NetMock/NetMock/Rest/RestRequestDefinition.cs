using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NetMock.Exceptions;
using NetMock.Utils;

namespace NetMock.Rest
{
	public class RestRequestDefinition
	{
		private static readonly Regex _pathRegEx = new Regex(@"^
			(?:/|(?:/([0-9a-zA-Z\-]+|\{[a-zA-Z][0-9a-zA-Z]*\}))*
			(?:\?([0-9a-zA-Z\-]+)(?:=([0-9a-zA-Z\-]+|\{[a-zA-Z][0-9a-zA-Z]*\})){0,1}
			(?:\&([0-9a-zA-Z\-]+)(?:=([0-9a-zA-Z\-]+|\{[a-zA-Z][0-9a-zA-Z]*\})){0,1})*)*)
			$", RegexOptions.IgnorePatternWhitespace);

		private List<UriSegment> _uriSegments;
		private List<QueryParameter> _queryParameters;

		internal RestRequestDefinition(RestMock restMock, Method method, string path, object body, IMatch[] matches)
		{
			RestMock = restMock;
			Method = method;
			Path = path ?? throw new ArgumentNullException(nameof(path));
			Body = body;
			Matches = matches;
		}

		private RestMock RestMock { get; }
		public Method Method { get; }
		public string Path { get; }
		public object Body { get; }
		public IMatch[] Matches { get; }

		internal void Parse()
		{
			Match regExMatch = _pathRegEx.Match(Path);

			if (!regExMatch.Success)
				throw new MockSetupException($"Invalid path: {Path}");

			IDictionary<string, ParameterMatch> parameterMatches = Matches
				.OfType<ParameterMatch>()
				.ToDictionary(parameterMatch => parameterMatch.Name);

			ParameterMatch GetParameterMatch(string parameterName)
			{
				if (!parameterMatches.TryGetValue(parameterName, out ParameterMatch parameterMatch))
					throw new MockSetupException($"Parameter in path not defined: {parameterName}");

				return parameterMatch;
			}

			_uriSegments = regExMatch.Groups[1].Captures
				.Cast<Capture>()
				.Select(x => new
					{
						x.Value,
						ParameterMatch = x.Value.IsParameter(out string parameterName) ? GetParameterMatch(parameterName) : null
					})
				.Select(x => x.ParameterMatch != null
					? new ParameterizedUriSegment(x.ParameterMatch)
					: (UriSegment) new StaticUriSegment(x.Value))
				.ToList();

			IEnumerable<(string Name, string Value)> queryParameters = regExMatch.Groups[2].Success
				? regExMatch.Groups[4].Captures
					.Cast<Capture>()
					.Select((x, i) => (x.Value, regExMatch.Groups[5].Captures[i].Value))
					.Append((regExMatch.Groups[2].Value, regExMatch.Groups[3].Value))
				: null;

			_queryParameters = queryParameters?
				.Select(x => new
					{
						x.Name,
						x.Value,
						ParameterMatch = x.Value.IsParameter(out string parameterName) ? GetParameterMatch(parameterName) : null
					})
				.Select(x => x.ParameterMatch != null
					? new ParameterizedQueryParameter(x.Name, x.ParameterMatch)
					: (QueryParameter) new StaticQueryParameter(x.Name, x.Value))
				.ToList() ?? new List<QueryParameter>();
		}

		internal bool Match(Uri uri, IDictionary<string, string> headers, out IList<MatchResult> matchResult)
		{
			matchResult = new List<MatchResult>();

			string[] baseUriSegments = RestMock.BaseUri.TrimmedSegments();
			string[] trimmedUriSegments = uri.TrimmedSegments();

			if (!baseUriSegments.SequenceEqual(trimmedUriSegments.Take(baseUriSegments.Length), StringComparer.OrdinalIgnoreCase))
				return false;

			IList<string> uriSegments = trimmedUriSegments
				.Skip(baseUriSegments.Length)
				.Select(segment => segment.Trim('/'))
				.ToArray();

			if (!MatchUriSegments(uriSegments, matchResult))
				return false;

			IDictionary<string, string> parameters = uri.Query
				.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(parameter => parameter.Split(new[] { '=' }, 2))
				.ToDictionary(parameterSplit => parameterSplit[0], parameterSplit => parameterSplit.Length > 1 ? parameterSplit[1] : null);

			if (!MatchQueryParameters(parameters, matchResult))
				return false;

			// todo: match body and headers here

			return true;
		}

		private bool MatchUriSegments(IList<string> uriSegments, ICollection<MatchResult> matchResult)
		{
			if (uriSegments.Count != _uriSegments.Count)
				return false;

			for (int i = 0; i < uriSegments.Count; i++)
			{
				MatchResult result = _uriSegments[i].Match(uriSegments[i]);

				if (!result.IsMatch)
					return false;

				matchResult.Add(result);
			}

			return true;
		}

		private bool MatchQueryParameters(IDictionary<string, string> parameters, ICollection<MatchResult> matchResult)
		{
			if (RestMock.UndefinedQueryParameterHandling == UndefinedHandling.Fail && parameters.Count != _queryParameters.Count
				|| RestMock.UndefinedQueryParameterHandling == UndefinedHandling.Ignore && parameters.Count < _queryParameters.Count)
			{
				return false;
			}

			HashSet<QueryParameter> matchedParameters = new HashSet<QueryParameter>();
			foreach (var parameter in parameters)
			{
				var result = _queryParameters
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

			return matchedParameters.Count == _queryParameters.Count;
		}

		private bool MatchHeaders(IDictionary<string, string> headers, ICollection<MatchResult> matchResult)
		{
			throw new NotImplementedException();
		}
	}
}
