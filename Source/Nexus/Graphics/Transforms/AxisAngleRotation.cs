namespace Nexus.Graphics.Transforms
{
	public class AxisAngleRotation : Rotation
	{
		public float Angle
		{
			get;
			set;
		}

		public Vector3D Axis
		{
			get;
			set;
		}

		public AxisAngleRotation()
		{
			Axis = new Vector3D(0, 1, 0);
		}

		public override Quaternion Value
		{
			get { return Quaternion.CreateFromAxisAngle(Axis, Angle); }
		}
	}
}