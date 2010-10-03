using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Nexus
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Point4D
	{
		#region Fields

		public float X;
		public float Y;
		public float Z;
		public float W;

		#endregion

		#region Properties

		public static short SizeInBytes
		{
			get { return sizeof(float) * 4; }
		}

		#endregion

		#region Constructors

		public Point4D(Point3D point, float w)
			: this(point.X, point.Y, point.Z, w)
		{
			
		}

		public Point4D(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		#endregion

		#region Methods

		internal string ConvertToString(string format, IFormatProvider provider)
		{
			char numericListSeparator = TokenizerHelper.GetNumericListSeparator(provider);
			return string.Format(provider, "{1:" + format + "}{0}{2:" + format + "}{0}{3:" + format + "}{0}{4:" + format + "}", new object[] { numericListSeparator, this.X, this.Y, this.Z, W });
		}

		public override bool Equals(object obj)
		{
			bool flag = false;
			if (obj is Point4D)
				flag = (this == (Point4D) obj);
			return flag;
		}

		public override int GetHashCode()
		{
			return ((this.X.GetHashCode() + this.Y.GetHashCode()) + this.Z.GetHashCode() + W.GetHashCode());
		}

		public override string ToString()
		{
			return string.Format("{{X:{0} Y:{1} Z:{2} W:{3}}}", this.X, this.Y, this.Z, W);
		}

		public static Point4D Parse(string source)
		{
			IFormatProvider cultureInfo = CultureInfo.InvariantCulture;
			TokenizerHelper helper = new TokenizerHelper(source, cultureInfo);
			string str = helper.NextTokenRequired();
			Point4D pointd = new Point4D(Convert.ToSingle(str, cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo));
			helper.LastTokenRequired();
			return pointd;
		}

		public static Point4D LinearInterpolate(Point4D value1, Point4D value2, float amountRangeStart, float amountRangeEnd, float amount)
		{
			Point4D result = new Point4D();
			result.X = MathUtility.Lerp(value1.X, value2.X, amountRangeStart, amountRangeEnd, amount);
			result.Y = MathUtility.Lerp(value1.Y, value2.Y, amountRangeStart, amountRangeEnd, amount);
			result.Z = MathUtility.Lerp(value1.Z, value2.Z, amountRangeStart, amountRangeEnd, amount);
			result.W = MathUtility.Lerp(value1.W, value2.W, amountRangeStart, amountRangeEnd, amount);
			return result;
		}

		public static Point4D PerspectiveInterpolate(Point4D value1, Point4D value2, float w1, float w2, float amountRangeStart, float amountRangeEnd, float amount)
		{
			Point4D result = new Point4D();
			result.X = MathUtility.PerspectiveInterpolate(value1.X, value2.X, w1, w2, amountRangeStart, amountRangeEnd, amount);
			result.Y = MathUtility.PerspectiveInterpolate(value1.Y, value2.Y, w1, w2, amountRangeStart, amountRangeEnd, amount);
			result.Z = MathUtility.PerspectiveInterpolate(value1.Z, value2.Z, w1, w2, amountRangeStart, amountRangeEnd, amount);
			result.W = MathUtility.PerspectiveInterpolate(value1.W, value2.W, w1, w2, amountRangeStart, amountRangeEnd, amount);
			return result;
		}

		/// <summary>
		/// Transforms a Point3D by the given Matrix. 
		/// </summary>
		/// <param name="position">The source Point3D.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The HomogeneousPoint3D resulting from the transformation.</returns>
		public static Point4D Transform(Point3D position, Matrix3D matrix)
		{
			Point4D result;
			result.X = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
			result.Y = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
			result.Z = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;
			result.W = (((position.X * matrix.M14) + (position.Y * matrix.M24)) + (position.Z * matrix.M34)) + matrix.M44;
			return result;
		}

		public Point3D ToPoint3D()
		{
			return new Point3D(X / W, Y / W, Z / W);
		}

		#endregion

		#region Operators

		public static Point4D operator +(Point4D value1, Point4D value2)
		{
			Point4D vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			vector.W = value1.W + value2.W;
			return vector;
		}

		public static Point4D operator +(Point4D point, Vector4D vector)
		{
			return new Point4D(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z, point.W + vector.W);
		}

		public static Vector4D operator -(Point4D point1, Point4D point2)
		{
			return new Vector4D(point1.X - point2.X, point1.Y - point2.Y, point1.Z - point2.Z, point1.W - point2.W);
		}

		public static Point4D operator *(Point4D point, Matrix3D matrix)
		{
			return matrix.Transform(point);
		}

		public static Point4D operator *(Point4D value, float scaleFactor)
		{
			Point4D result;
			result.X = value.X * scaleFactor;
			result.Y = value.Y * scaleFactor;
			result.Z = value.Z * scaleFactor;
			result.W = value.W * scaleFactor;
			return result;
		}

		public static bool operator ==(Point4D value1, Point4D value2)
		{
			return (((value1.X == value2.X) && (value1.Y == value2.Y)) && (value1.Z == value2.Z) && (value1.W == value2.W));
		}

		public static bool operator !=(Point4D value1, Point4D value2)
		{
			return !(value1 == value2);
		}

		public static explicit operator Point3D(Point4D point)
		{
			return point.ToPoint3D();
		}

		public static explicit operator Point4D(Point3D point)
		{
			return point.ToHomogeneousPoint3D();
		}

		#endregion
	}
}