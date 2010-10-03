namespace Nexus.Graphics.Transforms
{
	public class MatrixTransform : Transform3D
	{
		public MatrixTransform()
		{

		}

		public MatrixTransform(Matrix3D matrix)
		{
			Matrix = matrix;
		}

		public Matrix3D Matrix
		{
			get;
			set;
		}

		public override Matrix3D Value
		{
			get { return this.Value; }
		}
	}
}