namespace Nexus
{
	public struct Matrix2D
	{
		public float M11;
		public float M12;
		public float M13;
		public float M21;
		public float M22;
		public float M23;
		public float M31;
		public float M32;
		public float M33;

		public Matrix2D(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
		}

		public void Transform(Point2D[] points)
		{
			if (points != null)
				for (int i = 0; i < points.Length; i++)
					MultiplyPoint(ref points[i].X, ref points[i].Y);
		}

		private void MultiplyPoint(ref float x, ref float y)
		{
			x *= M11;
			x += (y * M21) + M31;
			y *= M22;
			y += (x * M12) + M32;
		}

		// static stuff

		public static readonly Matrix2D Identity;
		public static readonly Matrix2D Zero;

		static Matrix2D()
		{
			Identity = new Matrix2D(1, 0, 0, 0, 1, 0, 0, 0, 1);
			Zero = new Matrix2D(0, 0, 0, 0, 0, 0, 0, 0, 0);
		}

		/// <summary>
		/// Returns a matrix that can be used to rotate a set of points.
		/// </summary>
		/// <param name="radians">The amount, in radians, in which to rotate.</param>
		/// <returns>The created rotation matrix.</returns>
		public static Matrix2D CreateRotation(float radians)
		{
			float s = MathUtility.Sin(radians);
			float c = MathUtility.Cos(radians);

			return new Matrix2D(
				c, s, 0,
				-s, c, 0,
				0, 0, 1);
		}
	}
}