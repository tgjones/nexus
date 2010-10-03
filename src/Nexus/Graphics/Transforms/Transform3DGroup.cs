namespace Nexus.Graphics.Transforms
{
	public class Transform3DGroup : Transform3D
	{
		public Transform3DCollection Children { get; set; }

		public Transform3DGroup()
		{
			Children = new Transform3DCollection();
		}

		public override Matrix3D Value
		{
			get
			{
				Matrix3D value = Matrix3D.Identity;
				foreach (Transform3D transform in Children)
					value *= transform.Value;
				return value;
			}
		}
	}
}