using System.Runtime.InteropServices;

namespace Nexus
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorRgbF
	{
		public float R;
		public float G;
		public float B;

		public ColorRgbF(float r, float g, float b)
		{
			R = r;
			G = g;
			B = b;
		}

		public static ColorRgbF FromRgbColor(Color value)
		{
			return new ColorRgbF(
				value.R / 255.0f,
				value.G / 255.0f,
				value.B / 255.0f);
		}

		#region Operators

		public static ColorRgbF operator *(ColorRgbF left, ColorRgbF right)
		{
			return new ColorRgbF(
				left.R * right.R,
				left.G * right.G,
				left.B * right.B);
		}

		public static ColorRgbF operator *(ColorRgbF value, float multiplier)
		{
			return new ColorRgbF(
				value.R * multiplier,
				value.G * multiplier,
				value.B * multiplier);
		}

		public static ColorRgbF operator +(ColorRgbF left, ColorRgbF right)
		{
			return new ColorRgbF(
				left.R + right.R,
				left.G + right.G,
				left.B + right.B);
		}

		#endregion
	}
}