namespace Nexus.Objects3D
{
	public struct Triangle3D
	{
		public Point3D A;
		public Point3D B;
		public Point3D C;

		private readonly Vector3D _v0, _v1;
		private readonly float _d00, _d01, _d11, _denom;

		public Triangle3D(Point3D a, Point3D b, Point3D c)
		{
			A = a;
			B = b;
			C = c;

			_v0 = b - a;
			_v1 = c - a;
			_d00 = Vector3D.Dot(_v0, _v0);
			_d01 = Vector3D.Dot(_v0, _v1);
			_d11 = Vector3D.Dot(_v1, _v1);
			_denom = _d00 * _d11 - _d01 * _d01;
		}

		/// <summary>
		/// Compute barycentric coordinates (u, v, w) for
		/// point p with respect to this triangle.
		/// 
		/// Real Time Collision Detection, p47.
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public Vector3D Barycentric(Point3D p)
		{
			Vector3D v2 = p - A;
			float d20 = Vector3D.Dot(v2, _v0);
			float d21 = Vector3D.Dot(v2, _v1);

			Vector3D result;
			result.Y = (_d11 * d20 - _d01 * d21) / _denom;
			result.Z = (_d00 * d21 - _d01 * d20) / _denom;
			result.X = 1.0f - result.Y - result.Z;
			return result;
		}

		public static float Area2D(float x1, float y1, float x2, float y2, float x3, float y3)
		{
			return (x1 - x2) * (y2 - y3) - (x2 - x3) * (y1 - y2);
		}
	}
}