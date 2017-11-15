using System;
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
					JToken actualJToken;
					try
					{
						actualJToken = actualJson != null
							? JToken.Parse(actualJson)
							: JToken.FromObject(actual);
					}
					catch
					{
						Assert.Fail($"Actual value is invalid Json:{Environment.NewLine}{actual}");
						return;
					}

					JToken expectedJToken;
					try
					{
						expectedJToken = expectedJson != null
							? JToken.Parse(expectedJson)
							: JToken.FromObject(expected);
					}
					catch
					{
						Assert.Fail($"Expected value is invalid Json:{Environment.NewLine}{expected}");
						return;
					}

					Assert.IsTrue(JToken.DeepEquals(expectedJToken, actualJToken));
				}
			}
		}
	}
}
