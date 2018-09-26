using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NetMock.Utils
{
	public static class Extensions
	{
		public static TItem AddAndReturn<TList, TItem>(this ICollection<TList> list, TItem item, Action<TItem> action = null)
			where TItem : TList
		{
			list.Add(item);
			action?.Invoke(item);
			return item;
		}

		public static IDictionary<TKey, TValue> Apply<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> additionalKeyValuePairs)
		{
			if (additionalKeyValuePairs != null)
			{
				foreach (KeyValuePair<TKey, TValue> pair in additionalKeyValuePairs)
					dictionary[pair.Key] = pair.Value;
			}

			return dictionary;
		}

		public static bool IsParameter(this string value, out string parameterName)
		{
			if (value.StartsWith("{") && value.EndsWith("}"))
			{
				parameterName = value.Substring(1, value.Length - 2);
				return true;
			}

			parameterName = null;
			return false;
		}

		public static string[] TrimmedSegments(this Uri uri)
		{
			return uri.Segments
				.Select(segment => segment.Trim('/'))
				.SkipWhile(string.IsNullOrEmpty)
				.ToArray();
		}

		public static string GetBody(this HttpListenerRequest request)
		{
			if (request == null)
				return null;

			using (Stream inputStream = request.InputStream)
			{
				using (StreamReader streamReader = new StreamReader(inputStream, request.ContentEncoding))
				{
					return streamReader.ReadToEnd();
				}
			}
		}
	}
}
