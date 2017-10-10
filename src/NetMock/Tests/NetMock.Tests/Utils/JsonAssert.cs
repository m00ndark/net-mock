using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace NetMock.Tests.Utils
{
	public static class JsonAssert
	{
		public static void AreEqual(object expected, object actual)
		{
			if (expected == null || actual == null)
			{
				Assert.AreEqual(expected, actual);
			}
			else
			{
				string actualJson = actual as string;
				string expectedJson = expected as string;

				if (expectedJson == string.Empty || actualJson == string.Empty)
				{
					Assert.AreEqual(expectedJson ?? expected, actualJson ?? actual);
				}
				else
				{
					JToken actualJToken = actualJson != null
						? JToken.Parse(actualJson)
						: JToken.FromObject(actual);

					JToken expectedJToken = expectedJson != null
						? JToken.Parse(expectedJson)
						: JToken.FromObject(expected);

					Assert.IsTrue(JToken.DeepEquals(expectedJToken, actualJToken));
				}
			}
		}
	}
}
