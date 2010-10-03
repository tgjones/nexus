namespace Nexus.Graphics.Transforms
{
	public class QuaternionRotation : Rotation
	{
		public Quaternion Quaternion { get; set; }

		public override Quaternion Value
		{
			get { return Quaternion; }
		}
	}
}