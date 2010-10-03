using System.Runtime.InteropServices;

namespace Nexus
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Point2D
	{
		public float X, Y;

		public static Point2D Zero
		{
			get { return new Point2D(0, 0); }
		}

		public Point2D(float x, float y)
		{
			X = x;
			Y = y;
		}

		public static int SizeInBytes
		{
			get { return sizeof(float) * 2; }
		}

		public override string ToString()
		{
			return string.Format("{{X:{0} Y:{1}}}", X, Y);
		}

		#region Operators

		public static Point2D operator *(Point2D value, float scaleFactor)
		{
			Point2D vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		public static Point2D operator *(float scaleFactor, Point2D value)
		{
			Point2D vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			return vector;
		}

		public static Point2D operator +(Point2D value1, Point2D value2)
		{
			Point2D vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			return vector;
		}

		public static Vector2D operator -(Point2D point1, Point2D point2)
		{
			return new Vector2D(point1.X - point2.X, point1.Y - point2.Y);
		}

		#endregion
	}
}