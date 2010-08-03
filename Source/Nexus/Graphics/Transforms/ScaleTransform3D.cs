namespace Nexus.Graphics.Transforms
{
	public class ScaleTransform3D : AffineTransform
	{
		public float ScaleX
		{
			get;
			set;
		}

		public float ScaleY
		{
			get;
			set;
		}

		public float ScaleZ
		{
			get;
			set;
		}

		public override Matrix3D Value
		{
			get { return Matrix3D.CreateScale(this.ScaleX, this.ScaleY, this.ScaleZ); }
		}

		public ScaleTransform3D()
		{
			ScaleX = ScaleY = ScaleZ = 1;
		}
	}
}