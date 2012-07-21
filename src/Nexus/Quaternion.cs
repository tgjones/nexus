using System;

namespace Nexus
{
	public struct Quaternion
	{
		public float X;
		public float Y;
		public float Z;
		public float W;

		public Quaternion(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		public Quaternion(Vector3D axis, float angle)
		{
			axis.Normalize();
			float num2 = angle * 0.5f;
			float num = MathUtility.Sin(num2);
			float num3 = MathUtility.Cos(num2);
			X = axis.X * num;
			Y = axis.Y * num;
			Z = axis.Z * num;
			W = num3;
		}

		public void Normalize()
		{
			float lengthSq = (X * X) + (Y * Y) + (Z * Z) + (W * W);
			float inverseLength = 1f / MathUtility.Sqrt(lengthSq);
			X *= inverseLength;
			Y *= inverseLength;
			Z *= inverseLength;
			W *= inverseLength;
		}

		#region Properties

		public bool IsIdentity
		{
			get { return X == 0.0f && Y == 0.0f && Z == 0.0f && W == 1.0f; }
		}

		/// <summary>
		/// http://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToAngle/index.htm
		/// </summary>
		public float Angle
		{
			get
			{
				if (IsIdentity)
					return 0.0f;

				float y = MathUtility.Sqrt((X * X) + (Y * Y) + (Z * Z));
				float x = W;
				if (y > float.MaxValue)
				{
					float num = System.Math.Max(System.Math.Abs(X), System.Math.Max(System.Math.Abs(Y), System.Math.Abs(Z)));
					float num5 = X / num;
					float num4 = Y / num;
					float num3 = Z / num;
					y = MathUtility.Sqrt((num5 * num5) + (num4 * num4) + (num3 * num3));
					x = W / num;
				}
				return MathUtility.Atan2(y, x);
			}
		}

		public Vector3D Axis
		{
			get
			{
				if (IsIdentity)
					return new Vector3D(0.0f, 1.0f, 0.0f);
				Vector3D vector = new Vector3D(X, Y, Z);
				vector.Normalize();
				return vector;
			}
		}

		#endregion

		#region Operators

		public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion;
			float x = quaternion1.X;
			float y = quaternion1.Y;
			float z = quaternion1.Z;
			float w = quaternion1.W;
			float num4 = quaternion2.X;
			float num3 = quaternion2.Y;
			float num2 = quaternion2.Z;
			float num = quaternion2.W;
			float num12 = (y * num2) - (z * num3);
			float num11 = (z * num4) - (x * num2);
			float num10 = (x * num3) - (y * num4);
			float num9 = ((x * num4) + (y * num3)) + (z * num2);
			quaternion.X = ((x * num) + (num4 * w)) + num12;
			quaternion.Y = ((y * num) + (num3 * w)) + num11;
			quaternion.Z = ((z * num) + (num2 * w)) + num10;
			quaternion.W = (w * num) - num9;
			return quaternion;
		}

		public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
		{
			Quaternion quaternion;
			quaternion.X = quaternion1.X * scaleFactor;
			quaternion.Y = quaternion1.Y * scaleFactor;
			quaternion.Z = quaternion1.Z * scaleFactor;
			quaternion.W = quaternion1.W * scaleFactor;
			return quaternion;
		}

		public static bool operator ==(Quaternion left, Quaternion right)
		{
			return left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
		}

		public static bool operator !=(Quaternion left, Quaternion right)
		{
			return !(left == right);
		}

		#endregion

		#region Static stuff

		public static readonly Quaternion Identity = new Quaternion(0f, 0f, 0f, 1f);

		public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			Quaternion quaternion;
			float num9 = roll * 0.5f;
			float num6 = MathUtility.Sin(num9);
			float num5 = MathUtility.Cos(num9);
			float num8 = pitch * 0.5f;
			float num4 = MathUtility.Sin(num8);
			float num3 = MathUtility.Cos(num8);
			float num7 = yaw * 0.5f;
			float num2 = MathUtility.Sin(num7);
			float num = MathUtility.Cos(num7);
			quaternion.X = ((num * num4) * num5) + ((num2 * num3) * num6);
			quaternion.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
			quaternion.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
			quaternion.W = ((num * num3) * num5) + ((num2 * num4) * num6);
			return quaternion;
		}

		public static Quaternion CreateFromAxisAngle(Vector3D axis, float angle)
		{
			Quaternion quaternion;
			float num2 = angle * 0.5f;
			float num = MathUtility.Sin(num2);
			float num3 = MathUtility.Cos(num2);
			quaternion.X = axis.X * num;
			quaternion.Y = axis.Y * num;
			quaternion.Z = axis.Z * num;
			quaternion.W = num3;
			return quaternion;
		}

		public static Quaternion CreateFromRotationMatrix(Matrix3D matrix)
		{
			float num8 = (matrix.M11 + matrix.M22) + matrix.M33;
			Quaternion quaternion = new Quaternion();
			if (num8 > 0f)
			{
				float num = (float)Math.Sqrt((double)(num8 + 1f));
				quaternion.W = num * 0.5f;
				num = 0.5f / num;
				quaternion.X = (matrix.M23 - matrix.M32) * num;
				quaternion.Y = (matrix.M31 - matrix.M13) * num;
				quaternion.Z = (matrix.M12 - matrix.M21) * num;
				return quaternion;
			}
			if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
			{
				float num7 = (float)Math.Sqrt((double)(((1f + matrix.M11) - matrix.M22) - matrix.M33));
				float num4 = 0.5f / num7;
				quaternion.X = 0.5f * num7;
				quaternion.Y = (matrix.M12 + matrix.M21) * num4;
				quaternion.Z = (matrix.M13 + matrix.M31) * num4;
				quaternion.W = (matrix.M23 - matrix.M32) * num4;
				return quaternion;
			}
			if (matrix.M22 > matrix.M33)
			{
				float num6 = (float)Math.Sqrt((double)(((1f + matrix.M22) - matrix.M11) - matrix.M33));
				float num3 = 0.5f / num6;
				quaternion.X = (matrix.M21 + matrix.M12) * num3;
				quaternion.Y = 0.5f * num6;
				quaternion.Z = (matrix.M32 + matrix.M23) * num3;
				quaternion.W = (matrix.M31 - matrix.M13) * num3;
				return quaternion;
			}
			float num5 = (float)Math.Sqrt((double)(((1f + matrix.M33) - matrix.M11) - matrix.M22));
			float num2 = 0.5f / num5;
			quaternion.X = (matrix.M31 + matrix.M13) * num2;
			quaternion.Y = (matrix.M32 + matrix.M23) * num2;
			quaternion.Z = 0.5f * num5;
			quaternion.W = (matrix.M12 - matrix.M21) * num2;

			return quaternion;
		}

		#endregion

		public static bool IsNan(Quaternion q)
		{
			return float.IsNaN(q.X) || float.IsNaN(q.Y) || float.IsNaN(q.Z) || float.IsNaN(q.W);
		}
	}
}