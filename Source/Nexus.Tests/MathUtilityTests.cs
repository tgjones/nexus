using Apollo.Core.Math;
using NUnit.Framework;

namespace Apollo.Core.Tests
{
	[TestFixture]
	public class MathUtilityTests
	{
		[Test]
		public void CanClampFloatValueInsideRange()
		{
			float result = MathUtility.Clamp(1.0f, 0.0f, 2.0f);
			Assert.AreEqual(1.0f, result);
		}

		[Test]
		public void CanClampFloatValueBelowRange()
		{
			float result = MathUtility.Clamp(-3.0f, 0.0f, 2.0f);
			Assert.AreEqual(0.0f, result);
		}

		[Test]
		public void CanClampFloatValueAboveRange()
		{
			float result = MathUtility.Clamp(3.0f, 0.0f, 2.0f);
			Assert.AreEqual(2.0f, result);
		}

		[Test]
		public void CanClampIntValueInsideRange()
		{
			int result = MathUtility.Clamp(1, 0, 2);
			Assert.AreEqual(1, result);
		}

		[Test]
		public void CanClampIntValueBelowRange()
		{
			int result = MathUtility.Clamp(-3, 0, 2);
			Assert.AreEqual(0, result);
		}

		[Test]
		public void CanClampIntValueAboveRange()
		{
			int result = MathUtility.Clamp(3, 0, 2);
			Assert.AreEqual(2, result);
		}
	}
}