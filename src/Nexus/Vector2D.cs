namespace Nexus
{
	public struct Vector2D
	{
		public float X, Y;

		public static Vector2D Zero
		{
			get { return new Vector2D(0, 0); }
		}

		public static short SizeInBytes
		{
			get { return sizeof(float) * 2; }
		}

		public Vector2D(float x, float y)
		{
			X = x;
			Y = y;
		}

		#region Operators

		public static explicit operator Point2D(Vector2D vector)
		{
			return new Point2D(vector.X, vector.Y);
		}

		#endregion
	}
}