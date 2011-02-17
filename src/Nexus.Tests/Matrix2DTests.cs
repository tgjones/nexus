using NUnit.Framework;

namespace Nexus.Tests
{
	[TestFixture]
	public class Matrix2DTests
	{
		[Test]
		public void CanFindAdjoint()
		{
			Matrix2D actual = Matrix2D.Adjoint(new Matrix2D
			{
				M11 = -3,
				M12 = 2,
				M13 = -5,
				M21 = -1,
				M22 = 0,
				M23 = -2,
				M31 = 3,
				M32 = -4,
				M33 = 1
			});

			Matrix2D expected = new Matrix2D
			{
				M11 = -8,
				M12 = 18,
				M13 = -4,
				M21 = -5,
				M22 = 12,
				M23 = -1,
				M31 = 4,
				M32 = -6,
				M33 = 2
			};

			AssertMatricesAreEqual(expected, actual);
		}

		[Test]
		public void CanDetectNoInverse()
		{
			Matrix2D matrix = Matrix2D.Adjoint(new Matrix2D
			{
				M11 = 1,
				M12 = 2,
				M13 = 3,
				M21 = 4,
				M22 = 5,
				M23 = 6,
				M31 = 7,
				M32 = 8,
				M33 = 9
			});

			Assert.IsFalse(matrix.HasInverse);
		}

		[Test]
		public void CanFindInverse()
		{
			Matrix2D actual = Matrix2D.Invert(new Matrix2D
			{
				M11 = 3,
				M12 = 2,
				M13 = 3,
				M21 = 4,
				M22 = 5,
				M23 = 6,
				M31 = 7,
				M32 = 8,
				M33 = 9
			});

			Matrix2D expected = new Matrix2D
			{
				M11 = 0.5f,
				M12 = -1,
				M13 = 0.5f,
				M21 = -1,
				M22 = -1,
				M23 = 1,
				M31 = 0.5f,
				M32 = 1.6666666667f,
				M33 = -1.1666666667f
			};

			AssertMatricesAreEqual(expected, actual);
		}

		private static void AssertMatricesAreEqual(Matrix2D expectedMatrix, Matrix2D actualMatrix)
		{
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M11, actualMatrix.M11);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M12, actualMatrix.M12);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M13, actualMatrix.M13);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M21, actualMatrix.M21);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M22, actualMatrix.M22);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M23, actualMatrix.M23);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M31, actualMatrix.M31);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M32, actualMatrix.M32);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M33, actualMatrix.M33);
		}
	}
}