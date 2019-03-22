using System;
using System.Collections.Generic;
using System.Linq;

namespace NetMock.Rest
{
	public class StaticHeaders : Dictionary<string, string>
	{
		public void Add((string Name, string Value) header) => Add(header.Name, header.Value);
		public void Remove((string Name, string Value) header) => Remove(header.Name);

		public IDictionary<string, string> MergeWith(StaticHeaders staticHeaders)
		{
			IDictionary<string, string> result = new Dictionary<string, string>(this);

			foreach (KeyValuePair<string, string> header in staticHeaders)
			{
				if (!result.Keys.Any(key => key.Equals(header.Key, StringComparison.InvariantCultureIgnoreCase)))
					result.Add(header.Key, header.Value);
			}

			return result;
		}
	}
}
