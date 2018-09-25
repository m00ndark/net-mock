using System.Collections.Generic;

namespace NetMock.Rest
{
	public class StaticHeaders : Dictionary<string, string>
	{
		public void Add((string Name, string Value) header) => Add(header.Name, header.Value);
		public void Remove((string Name, string Value) header) => Remove(header.Name);
	}
}