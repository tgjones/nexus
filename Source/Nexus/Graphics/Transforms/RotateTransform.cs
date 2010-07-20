namespace Nexus.Graphics.Transforms
{
	public class RotateTransform : AffineTransform
	{
		public Rotation Rotation
		{
			get;
			set;
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