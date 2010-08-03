namespace Nexus.Graphics.Transforms
{
	public class RotateTransform3D : AffineTransform
	{
		public Rotation Rotation { get; set; }

		public RotateTransform3D(Rotation rotation)
		{
			Rotation = rotation;
		}

		public RotateTransform3D()
		{

		}

		public override Matrix3D Value
		{
			get
			{
				if (Rotation != null)
					return Matrix3D.CreateFromQuaternion(Rotation.Value);
				return Matrix3D.Identity;
			}
		}
	}
}