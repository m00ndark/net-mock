using System.Collections.Generic;

namespace NetMock.Tests.Model
{
	public class Message : Dictionary<string, string>
	{
		public Message(string text = null)
		{
			if (text != null)
				this["Text"] = text;
		}
	}
}
