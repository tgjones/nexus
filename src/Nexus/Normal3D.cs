using System;
using System.Runtime.InteropServices;
using Nexus.Util;

namespace Nexus
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Normal3D
	{
		public float X, Y, Z;

		#region Constructors

		public Normal3D(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		#endregion

		#region Instance methods

		internal string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, "{1:" + format + "}{0}{2:" + format + "}{0}{3:" + format + "}", new object[] { numericListSeparator, this.X, this.Y, this.Z });
		}

		public float Length()
		{
			float lengthSq = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			return (float)System.Math.Sqrt(lengthSq);
		}

		public float LengthSquared()
		{
			return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
		}

		public void Normalize()
		{
			float lengthSq = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
			float lengthInv = 1f / ((float)System.Math.Sqrt(lengthSq));
			this.X *= lengthInv;
			this.Y *= lengthInv;
			this.Z *= lengthInv;
		}

		public override string ToString()
		{
			return string.Format("{{X:{0} Y:{1} Z:{2}}}", this.X, this.Y, this.Z);
		}

		#endregion

		#region Static methods

		public static float Dot(Normal3D n1, Normal3D n2)
		{
			return (n1.X * n2.X) + (n1.Y * n2.Y) + (n1.Z * n2.Z);
		}

		public static float AbsDot(Normal3D n1, Normal3D n2)
		{
			return System.Math.Abs(Dot(n1, n2));
		}

		public static Normal3D Normalize(Normal3D v)
		{
			return v / v.Length();
		}

		public static Normal3D Cross(Normal3D v1, Normal3D v2)
		{
			return new Normal3D((v1.Y * v2.Z) - (v1.Z * v2.Y),
				(v1.Z * v2.X) - (v1.X * v2.Z),
				(v1.X * v2.Y) - (v1.Y * v2.X));
		}

		#endregion

		#region Operators

		public static Normal3D operator +(Normal3D value1, Normal3D value2)
		{
			Normal3D vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			return vector;
		}

		public static Normal3D operator -(Normal3D value)
		{
			return new Normal3D(-value.X, -value.Y, -value.Z);
		}

		public static Normal3D operator *(Normal3D value, float scaleFactor)
		{
			Normal3D vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}

		public static Normal3D operator *(float scaleFactor, Normal3D value)
		{
			Normal3D vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}

		public static Normal3D operator /(Normal3D value, float divider)
		{
			Normal3D vector;
			float num = 1f / divider;
			vector.X = value.X * num;
			vector.Y = value.Y * num;
			vector.Z = value.Z * num;
			return vector;
		}

		public static explicit operator Normal3D(Vector3D vector)
		{
			return new Normal3D(vector.X, vector.Y, vector.Z);
		}

		public static explicit operator Vector3D(Normal3D normal)
		{
			return new Vector3D(normal.X, normal.Y, normal.Z);
		}

		#endregion
	}
}