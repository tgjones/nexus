namespace Nexus.Graphics.Transforms
{
	public class TranslateTransform : AffineTransform
	{
		public float OffsetX { get; set; }
		public float OffsetY { get; set; }
		public float OffsetZ { get; set; }

		public override Matrix3D Value
		{
			get { return Matrix3D.CreateTranslation(OffsetX, OffsetY, OffsetZ); }
		}
	}
}