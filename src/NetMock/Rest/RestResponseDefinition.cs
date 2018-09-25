using System;
using System.Collections.Generic;
using System.Text;

namespace NetMock.Rest
{
	public class RestResponseDefinition
	{
		internal RestResponseDefinition(object body)
		{
			Body = body;
		}

		internal RestResponseDefinition(Delegate bodyProvider)
		{
			BodyProvider = bodyProvider;
		}

		public object Body { get; }
		public Delegate BodyProvider { get; }
	}
}
