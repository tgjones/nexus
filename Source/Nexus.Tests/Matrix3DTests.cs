using Apollo.Core.Math;
using NUnit.Framework;
using SlimDX;

namespace Apollo.Core.Tests
{
	[TestFixture]
	public class Matrix3DTests
	{
		[Test]
		public void CanCreateLookAtMatrix()
		{
			Matrix3D viewMatrix = Matrix3D.CreateLookAt(Point3D.Zero, Vector3D.Forward, Vector3D.Up);
			Matrix expectedViewMatrix = Matrix.LookAtRH(Vector3.Zero, Vector3.UnitZ * -1, Vector3.UnitY);
			AssertMatricesAreEqual(expectedViewMatrix, viewMatrix);
		}

		[Test]
		public void CanCreateFieldOfViewPerspectiveMatrix()
		{
			const float fieldOfView = MathUtility.PI_OVER_2;
			Matrix3D projectionMatrix = Matrix3D.CreatePerspectiveFieldOfView(fieldOfView, 1, 1, 200);

			Matrix otherProjectionMatrix = Matrix.PerspectiveFovRH(fieldOfView, 1, 1, 200);
			AssertMatricesAreEqual(otherProjectionMatrix, projectionMatrix);
		}

		[Test]
		public void CanTransformPointWithPerspectiveProjection()
		{
			const float fieldOfView = MathUtility.PI_OVER_2;
			Matrix3D actualProjectionMatrix = Matrix3D.CreatePerspectiveFieldOfView(fieldOfView, 1, 1, 200);
			Matrix expectedProjectionMatrix = Matrix.PerspectiveFovRH(fieldOfView, 1, 1, 200);
			Point4D actualPoint = new Point4D(100, 100, -100, 1);
			Vector4 expectedPoint = new Vector4(100, 100, -100, 1);

			Point4D actualTransformedPoint = actualProjectionMatrix.Transform(actualPoint);
			Vector4 expectedTransformedPoint = Vector4.Transform(expectedPoint, expectedProjectionMatrix);

			AssertPointsAreEqual(expectedTransformedPoint, actualTransformedPoint);
		}

		private static void AssertPointsAreEqual(Vector4 expectedPoint, Point4D actualPoint)
		{
			AssertAreRoughlyEqual(expectedPoint.X, actualPoint.X);
			AssertAreRoughlyEqual(expectedPoint.Y, actualPoint.Y);
			AssertAreRoughlyEqual(expectedPoint.Z, actualPoint.Z);
			AssertAreRoughlyEqual(expectedPoint.W, actualPoint.W);
		}

		private static void AssertMatricesAreEqual(Matrix expectedMatrix, Matrix3D actualMatrix)
		{
			AssertAreRoughlyEqual(expectedMatrix.M11, actualMatrix.M11);
			AssertAreRoughlyEqual(expectedMatrix.M12, actualMatrix.M12);
			AssertAreRoughlyEqual(expectedMatrix.M13, actualMatrix.M13);
			AssertAreRoughlyEqual(expectedMatrix.M14, actualMatrix.M14);
			AssertAreRoughlyEqual(expectedMatrix.M21, actualMatrix.M21);
			AssertAreRoughlyEqual(expectedMatrix.M22, actualMatrix.M22);
			AssertAreRoughlyEqual(expectedMatrix.M23, actualMatrix.M23);
			AssertAreRoughlyEqual(expectedMatrix.M24, actualMatrix.M24);
			AssertAreRoughlyEqual(expectedMatrix.M31, actualMatrix.M31);
			AssertAreRoughlyEqual(expectedMatrix.M32, actualMatrix.M32);
			AssertAreRoughlyEqual(expectedMatrix.M33, actualMatrix.M33);
			AssertAreRoughlyEqual(expectedMatrix.M34, actualMatrix.M34);
			AssertAreRoughlyEqual(expectedMatrix.M41, actualMatrix.M41);
			AssertAreRoughlyEqual(expectedMatrix.M42, actualMatrix.M42);
			AssertAreRoughlyEqual(expectedMatrix.M43, actualMatrix.M43);
			AssertAreRoughlyEqual(expectedMatrix.M44, actualMatrix.M44);
		}

		private static void AssertAreRoughlyEqual(float expectedValue, float actualValue)
		{
			if (System.Math.Abs(expectedValue - actualValue) > 0.01f)
				Assert.Fail("Expected value: " + expectedValue + "; actual value: " + actualValue);
		}

		[Test]
		public void CanCreateOrthographicMatrix()
		{
			const float WIDTH = 10;
			const float HEIGHT = 10;
			const float Z_NEAR_PLANE = 2;
			const float Z_FAR_PLANE = 20;
			Matrix3D projectionMatrix = Matrix3D.CreateOrthographic(WIDTH, HEIGHT, Z_NEAR_PLANE, Z_FAR_PLANE);
			AssertMatrixValuesAreEqual(projectionMatrix,
				2 / WIDTH, 0, 0, 0,
				0, 2 / HEIGHT, 0, 0,
				0, 0, 1 / (Z_NEAR_PLANE - Z_FAR_PLANE), 0,
				0, 0, Z_NEAR_PLANE, 1);
		}

		[Test]
		public void CanCreateOrthographicOffCenterMatrix()
		{
			const float LEFT = -12;
			const float RIGHT = 8;
			const float BOTTOM = -3;
			const float TOP = 7;
			const float Z_NEAR_PLANE = 2;
			const float Z_FAR_PLANE = 20;
			Matrix3D projectionMatrix = Matrix3D.CreateOrthographicOffCenter(LEFT, RIGHT, BOTTOM, TOP, Z_NEAR_PLANE, Z_FAR_PLANE);
			AssertMatrixValuesAreEqual(projectionMatrix,
				2 / (RIGHT - LEFT), 0, 0, 0,
				0, 2 / (TOP - BOTTOM), 0, 0,
				0, 0, 1 / (Z_NEAR_PLANE - Z_FAR_PLANE), 0,
				(LEFT + RIGHT) / (LEFT - RIGHT), (BOTTOM + TOP) / (BOTTOM - TOP), Z_NEAR_PLANE / (Z_NEAR_PLANE - Z_FAR_PLANE), 1);
		}

		[Test]
		public void CanCreateTranslationMatrixFromIndividualPoints()
		{
			const float X = 2;
			const float Y = 3;
			const float Z = 4;
			Matrix3D translationMatrix = Matrix3D.CreateTranslation(X, Y, Z);
			AssertMatrixValuesAreEqual(translationMatrix, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, X, Y, Z, 1);
		}

		[Test]
		public void CanCreateTranslationMatrixFromPoint3()
		{
			const float X = 2;
			const float Y = 3;
			const float Z = 4;
			Point3D point = new Point3D(X, Y, Z);
			Matrix3D translationMatrix = Matrix3D.CreateTranslation(point);
			AssertMatrixValuesAreEqual(translationMatrix, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, X, Y, Z, 1);
		}

		[Test]
		public void CanCreateScaleMatrixWithIndividualScaleValues()
		{
			const float X = 2;
			const float Y = 3;
			const float Z = 4;
			Matrix3D scaleMatrix = Matrix3D.CreateScale(X, Y, Z);
			AssertMatrixValuesAreEqual(scaleMatrix, X, 0, 0, 0, 0, Y, 0, 0, 0, 0, Z, 0, 0, 0, 0, 1);
		}

		[Test]
		public void CanCreateScaleMatrixWithSingleScaleValue()
		{
			const float SCALE = 2;
			Matrix3D scaleMatrix = Matrix3D.CreateScale(SCALE);
			AssertMatrixValuesAreEqual(scaleMatrix, SCALE, 0, 0, 0, 0, SCALE, 0, 0, 0, 0, SCALE, 0, 0, 0, 0, 1);
		}

		private static void AssertMatrixValuesAreEqual(Matrix3D actualResult, float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			Assert.AreEqual(m11, actualResult.M11);
			Assert.AreEqual(m12, actualResult.M12);
			Assert.AreEqual(m13, actualResult.M13);
			Assert.AreEqual(m14, actualResult.M14);
			Assert.AreEqual(m21, actualResult.M21);
			Assert.AreEqual(m22, actualResult.M22);
			Assert.AreEqual(m23, actualResult.M23);
			Assert.AreEqual(m24, actualResult.M24);
			Assert.AreEqual(m31, actualResult.M31);
			Assert.AreEqual(m32, actualResult.M32);
			Assert.AreEqual(m33, actualResult.M33);
			Assert.AreEqual(m34, actualResult.M34);
			Assert.AreEqual(m41, actualResult.M41);
			Assert.AreEqual(m42, actualResult.M42);
			Assert.AreEqual(m43, actualResult.M43);
			Assert.AreEqual(m44, actualResult.M44);
		}
	}
}