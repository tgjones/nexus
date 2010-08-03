namespace Nexus
{
	/// <summary>
	/// Defines a plane based on a normal vector and a distance along that vector from the origin.
	/// </summary>
	public struct Plane
	{
		/// <summary>
		/// The normal vector of the plane. Points x on the plane satisfy Dot(n,x) = d
		/// </summary>
		public Normal3D Normal;

		/// <summary>
		/// The distance of the plane along its normal from the origin.
		/// D = dot(n,p) for a given point p on the plane
		/// </summary>
		public float D;

		#region Constructors

		/// <summary>
		/// Creates a new plane.
		/// </summary>
		/// <param name="normal">The normal vector of the plane.</param>
		/// <param name="d">The distance of the plane along its normal from the origin.</param>
		public Plane(Normal3D normal, float d)
		{
			Normal = normal;
			D = d;
		}

		/// <summary>
		/// Points must be ordered CCW
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="c"></param>
		public Plane(Point3D a, Point3D b, Point3D c)
		{
			Normal = (Normal3D) Vector3D.Normalize(Vector3D.Cross(b - a, c - a));
			D = Vector3D.Dot(Normal, (Vector3D) a);
		}

		#endregion

		#region Static methods

		public static Plane Normalize(Plane value)
		{
			Plane plane;
			float num2 = ((value.Normal.X * value.Normal.X) + (value.Normal.Y * value.Normal.Y)) + (value.Normal.Z * value.Normal.Z);
			if (System.Math.Abs((float)(num2 - 1f)) < 1.192093E-07f)
			{
				plane.Normal = value.Normal;
				plane.D = value.D;
				return plane;
			}
			float num = 1f / ((float)System.Math.Sqrt((double)num2));
			plane.Normal.X = value.Normal.X * num;
			plane.Normal.Y = value.Normal.Y * num;
			plane.Normal.Z = value.Normal.Z * num;
			plane.D = value.D * num;
			return plane;
		}

		public static Plane Transform(Plane plane, Matrix3D matrix)
		{
			Matrix3D matrix2 = Matrix3D.Invert(matrix);
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float d = plane.D;

			Plane plane2;
			plane2.Normal.X = (((x * matrix2.M11) + (y * matrix2.M12)) + (z * matrix2.M13)) + (d * matrix2.M14);
			plane2.Normal.Y = (((x * matrix2.M21) + (y * matrix2.M22)) + (z * matrix2.M23)) + (d * matrix2.M24);
			plane2.Normal.Z = (((x * matrix2.M31) + (y * matrix2.M32)) + (z * matrix2.M33)) + (d * matrix2.M34);
			plane2.D = (((x * matrix2.M41) + (y * matrix2.M42)) + (z * matrix2.M43)) + (d * matrix2.M44);
			return plane2;
		}

		#endregion
	}
}