using System.Collections.Generic;

namespace NetMock.Rest
{
	public class AttachedHeader
	{
		public AttachedHeader(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public string Name { get; }
		public string Value { get; }

		public static implicit operator AttachedHeader((string Name, string Value) header)
		{
			return new AttachedHeader(header.Name, header.Value);
		}

		public static implicit operator (string Name, string Value)(AttachedHeader header)
		{
			return (header.Name, header.Value);
		}

		public static implicit operator KeyValuePair<string, string>(AttachedHeader header)
		{
			return new KeyValuePair<string, string>(header.Name, header.Value);
		}
	}
}
