using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using Nexus.Design;

namespace Nexus
{
	[TypeConverter(typeof(Vector3DConverter))]
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector3D
	{
		public float X, Y, Z;

		#region Constructors

		public Vector3D(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		#endregion

		#region Indexer

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return X;
					case 1:
						return Y;
					case 2:
						return Z;
					default:
						throw new ArgumentOutOfRangeException("index");
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						X = value;
						break;
					case 1:
						Y = value;
						break;
					case 2:
						Z = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("index");
				}
			}
		}

		#endregion

		#region Static properties

		/// <summary>
		/// Returns a unit Vector3D designating backward in a right-handed coordinate system(0, 0, 1).
		/// </summary>
		public static Vector3D Backward
		{
			get { return new Vector3D(0f, 0f, 1f); }
		}

		/// <summary>
		/// Returns a unit Vector3D designating forward in a right-handed coordinate system(0, 0, −1).
		/// </summary>
		public static Vector3D Forward
		{
			get { return new Vector3D(0f, 0f, -1f); }
		}

		/// <summary>
		/// Returns a unit vector designating up (0, 1, 0).
		/// </summary>
		public static Vector3D Up
		{
			get { return new Vector3D(0f, 1f, 0f); }
		}

		/// <summary>
		/// Returns a unit vector designating down (0, -1, 0).
		/// </summary>
		public static Vector3D Down
		{
			get { return new Vector3D(0f, -1f, 0f); }
		}

		/// <summary>
		/// Returns a unit vector designating left (-1, 0, 0).
		/// </summary>
		public static Vector3D Left
		{
			get { return new Vector3D(-1f, 0, 0f); }
		}

		public static Vector3D Zero
		{
			get { return new Vector3D(0, 0, 0); }
		}

		public static short SizeInBytes
		{
			get { return sizeof(float) * 3; }
		}

		public static Vector3D One
		{
			get { return new Vector3D(1, 1, 1); }
		}

		#endregion

		#region Instance methods

		internal string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, "{1:" + format + "}{0}{2:" + format + "}{0}{3:" + format + "}", new object[] { numericListSeparator, this.X, this.Y, this.Z });
		}

		public void Normalize()
		{
			float length = Length();
			X /= length;
			Y /= length;
			Z /= length;
		}

		public float Length()
		{
			return MathUtility.Sqrt(LengthSquared());
		}

		public float LengthSquared()
		{
			return (X * X) + (Y * Y) + (Z * Z);
		}

		public ColorF ToColorF()
		{
			return new ColorF(X, Y, Z);
		}

		public override string ToString()
		{
			return string.Format("{{X:{0} Y:{1} Z:{2}}}", this.X, this.Y, this.Z);
		}

		#endregion

		#region Static methods

		public static float AngleBetween(Vector3D vector1, Vector3D vector2)
		{
			vector1.Normalize();
			vector2.Normalize();

			if (Dot(vector1, vector2) < 0.0f)
			{
				Vector3D vectord2 = -vector1 - vector2;
				return MathUtility.PI - (2.0f * MathUtility.Asin(vectord2.Length() / 2.0f));
			}

			Vector3D vectord = vector1 - vector2;
			return 2.0f * MathUtility.Asin(vectord.Length() / 2.0f);
		}

		public static Vector3D Cross(Vector3D v1, Vector3D v2)
		{
			return new Vector3D((v1.Y * v2.Z) - (v1.Z * v2.Y),
				(v1.Z * v2.X) - (v1.X * v2.Z),
				(v1.X * v2.Y) - (v1.Y * v2.X));
		}

		public static Vector3D Cross(Vector3D v1, Normal3D v2)
		{
			return new Vector3D((v1.Y * v2.Z) - (v1.Z * v2.Y),
				(v1.Z * v2.X) - (v1.X * v2.Z),
				(v1.X * v2.Y) - (v1.Y * v2.X));
		}

		public static Vector3D Cross(Normal3D v1, Vector3D v2)
		{
			return new Vector3D((v1.Y * v2.Z) - (v1.Z * v2.Y),
				(v1.Z * v2.X) - (v1.X * v2.Z),
				(v1.X * v2.Y) - (v1.Y * v2.X));
		}

		public static float Dot(Vector3D v1, Vector3D v2)
		{
			return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
		}

		public static float Dot(Vector3D v1, Normal3D n2)
		{
			return (v1.X * n2.X) + (v1.Y * n2.Y) + (v1.Z * n2.Z);
		}

		public static float Dot(Normal3D n1, Vector3D v2)
		{
			return (n1.X * v2.X) + (n1.Y * v2.Y) + (n1.Z * v2.Z);
		}

		public static float AbsDot(Vector3D v1, Vector3D v2)
		{
			return System.Math.Abs(Dot(v1, v2));
		}

		public static float AbsDot(Vector3D v1, Normal3D n2)
		{
			return System.Math.Abs(Dot(v1, n2));
		}

		public static float AbsDot(Normal3D n1, Vector3D v2)
		{
			return System.Math.Abs(Dot(n1, v2));
		}

		public static Vector3D Normalize(Vector3D v)
		{
			return v / v.Length();
		}

		public static void CoordinateSystem(Vector3D v1, out Vector3D v2, out Vector3D v3)
		{
			if (System.Math.Abs(v1.Y) > System.Math.Abs(v1.Y))
			{
				float invLen = 1.0f / MathUtility.Sqrt(v1.X * v1.X + v1.Z * v1.Z);
				v2 = new Vector3D(-v1.Z * invLen, 0.0f, v1.X * invLen);
			}
			else
			{
				float invLen = 1.0f / MathUtility.Sqrt(v1.Y * v1.Y + v1.Z * v1.Z);
				v2 = new Vector3D(0.0f, v1.Z * invLen, -v1.Y * invLen);
			}
			v3 = Vector3D.Cross(v1, v2);
		}

		public static Vector3D Parse(string source)
		{
			IFormatProvider cultureInfo = CultureInfo.InvariantCulture;
			TokenizerHelper helper = new TokenizerHelper(source, cultureInfo);
			string str = helper.NextTokenRequired();
			Vector3D vectord = new Vector3D(Convert.ToSingle(str, cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo));
			helper.LastTokenRequired();
			return vectord;
		}

		public static Vector3D Reflect(Vector3D vector, Vector3D normal)
		{
			Vector3D vector2;
			float num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
			vector2.X = vector.X - ((2f * num) * normal.X);
			vector2.Y = vector.Y - ((2f * num) * normal.Y);
			vector2.Z = vector.Z - ((2f * num) * normal.Z);
			return vector2;
		}

		public static Vector3D TransformNormal(Vector3D normal, Matrix3D matrix)
		{
			float num3 = ((normal.X * matrix.M11) + (normal.Y * matrix.M21)) + (normal.Z * matrix.M31);
			float num2 = ((normal.X * matrix.M12) + (normal.Y * matrix.M22)) + (normal.Z * matrix.M32);
			float num = ((normal.X * matrix.M13) + (normal.Y * matrix.M23)) + (normal.Z * matrix.M33);

			return new Vector3D(num3, num2, num);
		}

		public static Vector3D Transform(Vector3D position, Matrix3D matrix)
		{
			Vector3D vector;
			float num3 = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			float num2 = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			float num = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			vector.X = num3;
			vector.Y = num2;
			vector.Z = num;
			return vector;
		}

		public static Vector3D Abs(Vector3D vector)
		{
			return new Vector3D(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
		}

		#endregion

		#region Operators

		public static Vector3D operator +(Vector3D value1, Vector3D value2)
		{
			Vector3D vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			return vector;
		}

		public static Vector3D operator -(Vector3D value)
		{
			return new Vector3D(-value.X, -value.Y, -value.Z);
		}

		public static Vector3D operator -(Vector3D value1, Vector3D value2)
		{
			Vector3D vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			return vector;
		}

		public static Vector3D operator *(Vector3D value, float scaleFactor)
		{
			Vector3D vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}

		public static Vector3D operator *(float scaleFactor, Vector3D value)
		{
			Vector3D vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			return vector;
		}

		public static Vector3D operator /(Vector3D value, float divider)
		{
			Vector3D vector;
			float num = 1f / divider;
			vector.X = value.X * num;
			vector.Y = value.Y * num;
			vector.Z = value.Z * num;
			return vector;
		}

		public static explicit operator Point3D(Vector3D vector)
		{
			return new Point3D(vector.X, vector.Y, vector.Z);
		}

		public static bool operator ==(Vector3D left, Vector3D right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
		}

		public static bool operator !=(Vector3D left, Vector3D right)
		{
			return !(left == right);
		}

		#endregion
	}
}