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

		public float Determinant
		{
			get
			{
				float temp1 = M11 * (M22 * M33 - M32 * M23);
				float temp2 = M12 * (M21 * M33 - M31 * M23);
				float temp3 = M13 * (M21 * M32 - M31 * M22);
				return temp1 - temp2 + temp3;
			}
		}

		public bool HasInverse
		{
			get { return !MathUtility.IsZero(Determinant); }
		}

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

		public static Matrix2D Adjoint(Matrix2D matrix)
		{
			Matrix2D adjoint;
			adjoint.M11 = matrix.M33 * matrix.M22 - matrix.M32 * matrix.M23;
			adjoint.M12 = -(matrix.M33 * matrix.M12 - matrix.M32 * matrix.M13);
			adjoint.M13 = matrix.M23 * matrix.M12 - matrix.M22 * matrix.M13;
			adjoint.M21 = -(matrix.M33 * matrix.M21 - matrix.M31 * matrix.M23);
			adjoint.M22 = matrix.M33 * matrix.M11 - matrix.M31 * matrix.M13;
			adjoint.M23 = -(matrix.M23 * matrix.M11 - matrix.M21 * matrix.M13);
			adjoint.M31 = matrix.M32 * matrix.M21 - matrix.M31 * matrix.M22;
			adjoint.M32 = -(matrix.M32 * matrix.M11 - matrix.M31 * matrix.M12);
			adjoint.M33 = matrix.M22 * matrix.M11 - matrix.M21 * matrix.M12;
			return adjoint;
		}

		public static Matrix2D Invert(Matrix2D matrix)
		{
			return Adjoint(matrix) / matrix.Determinant;
		}

		public static Matrix2D Transpose(Matrix2D matrix)
		{
			Matrix2D result = new Matrix2D();
			result.M11 = matrix.M11;
			result.M12 = matrix.M21;
			result.M13 = matrix.M31;
			result.M21 = matrix.M12;
			result.M22 = matrix.M22;
			result.M23 = matrix.M32;
			result.M31 = matrix.M13;
			result.M32 = matrix.M23;
			result.M33 = matrix.M33;
			return result;
		}

		public Vector3D Transform(Vector3D vector)
		{
			Vector3D result;
			result.X = vector.X * M11 + vector.Y * M21 + vector.Z * M31;
			result.Y = vector.X * M12 + vector.Y * M22 + vector.Z * M32;
			result.Z = vector.X * M13 + vector.Y * M23 + vector.Z * M33;
			return result;
		}

		#region Operators

		public static Matrix2D operator/(Matrix2D matrix, float value)
		{
			matrix.M11 /= value;
			matrix.M12 /= value;
			matrix.M13 /= value;
			matrix.M21 /= value;
			matrix.M22 /= value;
			matrix.M23 /= value;
			matrix.M31 /= value;
			matrix.M32 /= value;
			matrix.M33 /= value;
			return matrix;
		}

		#endregion
	}
}