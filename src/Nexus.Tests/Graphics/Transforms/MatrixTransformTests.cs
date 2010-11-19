using Nexus.Graphics.Transforms;
using NUnit.Framework;

namespace Nexus.Tests.Graphics.Transforms
{
	[TestFixture]
	public class MatrixTransformTests
	{
		[Test]
		public void CanGetMatrixTransformValue()
		{
			// Arrange.
			MatrixTransform transform = new MatrixTransform(Matrix3D.Identity);

			// Act.
			Matrix3D matrix = transform.Value;

			// Assert.
			Assert.AreEqual(Matrix3D.Identity, matrix);
		}
	}
}