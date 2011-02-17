using NUnit.Framework;
using SharpDX;

namespace Nexus.Tests
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
			TestUtility.AssertAreRoughlyEqual(expectedPoint.X, actualPoint.X);
			TestUtility.AssertAreRoughlyEqual(expectedPoint.Y, actualPoint.Y);
			TestUtility.AssertAreRoughlyEqual(expectedPoint.Z, actualPoint.Z);
			TestUtility.AssertAreRoughlyEqual(expectedPoint.W, actualPoint.W);
		}

		private static void AssertMatricesAreEqual(Matrix expectedMatrix, Matrix3D actualMatrix)
		{
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M11, actualMatrix.M11);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M12, actualMatrix.M12);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M13, actualMatrix.M13);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M14, actualMatrix.M14);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M21, actualMatrix.M21);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M22, actualMatrix.M22);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M23, actualMatrix.M23);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M24, actualMatrix.M24);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M31, actualMatrix.M31);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M32, actualMatrix.M32);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M33, actualMatrix.M33);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M34, actualMatrix.M34);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M41, actualMatrix.M41);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M42, actualMatrix.M42);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M43, actualMatrix.M43);
			TestUtility.AssertAreRoughlyEqual(expectedMatrix.M44, actualMatrix.M44);
		}

		[Test]
		public void CanCreateOrthographicMatrix()
		{
			const float width = 10;
			const float height = 10;
			const float zNearPlane = 2;
			const float zFarPlane = 20;
			Matrix3D projectionMatrix = Matrix3D.CreateOrthographic(width, height, zNearPlane, zFarPlane);
			AssertMatricesAreEqual(Matrix.OrthoRH(width, height, zNearPlane, zFarPlane), projectionMatrix);
		}

		[Test]
		public void CanCreateOrthographicOffCenterMatrix()
		{
			const float left = -12;
			const float right = 8;
			const float bottom = -3;
			const float top = 7;
			const float zNearPlane = 2;
			const float zFarPlane = 20;
			Matrix3D projectionMatrix = Matrix3D.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
			AssertMatricesAreEqual(Matrix.OrthoOffCenterRH(left, right, bottom, top, zNearPlane, zFarPlane), projectionMatrix);
		}

		[Test]
		public void CanCreateTranslationMatrixFromIndividualPoints()
		{
			const float x = 2;
			const float y = 3;
			const float z = 4;
			Matrix3D translationMatrix = Matrix3D.CreateTranslation(x, y, z);
			AssertMatricesAreEqual(Matrix.Translation(x, y, z), translationMatrix);
		}

		[Test]
		public void CanCreateTranslationMatrixFromPoint3()
		{
			const float x = 2;
			const float y = 3;
			const float z = 4;
			Point3D point = new Point3D(x, y, z);
			Matrix3D translationMatrix = Matrix3D.CreateTranslation(point);
			AssertMatricesAreEqual(Matrix.Translation(x, y, z), translationMatrix);
		}

		[Test]
		public void CanCreateScaleMatrixWithIndividualScaleValues()
		{
			const float x = 2;
			const float y = 3;
			const float z = 4;
			Matrix3D scaleMatrix = Matrix3D.CreateScale(x, y, z);
			AssertMatricesAreEqual(Matrix.Scaling(x, y, z), scaleMatrix);
		}

		[Test]
		public void CanCreateScaleMatrixWithSingleScaleValue()
		{
			const float scale = 2;
			Matrix3D scaleMatrix = Matrix3D.CreateScale(scale);
			AssertMatricesAreEqual(Matrix.Scaling(scale, scale, scale), scaleMatrix);
		}
	}
}