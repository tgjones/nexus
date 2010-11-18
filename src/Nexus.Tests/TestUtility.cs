using NUnit.Framework;

namespace Nexus.Tests
{
	internal static class TestUtility
	{
		public static void AssertAreRoughlyEqual(float expectedValue, float actualValue)
		{
			if (System.Math.Abs(expectedValue - actualValue) > 0.01f)
				Assert.Fail("Expected value: " + expectedValue + "; actual value: " + actualValue);
		}
	}
}