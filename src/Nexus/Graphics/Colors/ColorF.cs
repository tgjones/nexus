using System.Runtime.InteropServices;

namespace Nexus.Graphics.Colors
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorF
	{
		#region Fields

		public float A;
		public float B;
		public float G;
		public float R;

		#endregion

		#region Constructor

		public ColorF(float value)
		{
			A = 1.0f;
			R = value;
			G = value;
			B = value;
		}

		public ColorF(float r, float g, float b)
		{
			A = 1.0f;
			R = r;
			G = g;
			B = b;
		}

		public ColorF(float a, float r, float g, float b)
		{
			A = a;
			R = r;
			G = g;
			B = b;
		}

		public ColorF(ColorRgbF rgb, float a)
		{
			A = a;
			R = rgb.R;
			G = rgb.G;
			B = rgb.B;
		}

		#endregion

		#region Methods

		public float Min()
		{
			return System.Math.Min(System.Math.Min(R, G), B);
		}

		public float Max()
		{
			return System.Math.Max(System.Math.Max(R, G), B);
		}

		public static ColorF Min(ColorF value1, ColorF value2)
		{
			return new ColorF(
				System.Math.Min(value1.R, value2.R),
				System.Math.Min(value1.G, value2.G),
				System.Math.Min(value1.B, value2.B));
		}

		public static ColorF Max(ColorF value1, ColorF value2)
		{
			return new ColorF(
				System.Math.Max(value1.R, value2.R),
				System.Math.Max(value1.G, value2.G),
				System.Math.Max(value1.B, value2.B));
		}

		public static ColorF Exp(ColorF value)
		{
			return new ColorF(
				(float) System.Math.Exp(value.R),
				(float) System.Math.Exp(value.G),
				(float) System.Math.Exp(value.B));
		}

		public static ColorF Saturate(ColorF value)
		{
			return new ColorF(
				MathUtility.Saturate(value.R),
				MathUtility.Saturate(value.G),
				MathUtility.Saturate(value.B));
		}

		public override string ToString()
		{
			return string.Format("{0:F10}, {1:F10}, {2:F10}", R, G, B);
		}

		#endregion

		#region Static stuff

		public static ColorF FromRgbColor(Color value)
		{
			return new ColorF(
				value.A / 255.0f,
				value.R / 255.0f,
				value.G / 255.0f,
				value.B / 255.0f);
		}

		public static ColorF FromHexRef(string hexRef)
		{
			return FromRgbColor(Color.FromHexRef(hexRef));
		}

		#endregion

		#region Operators

		public static ColorF operator *(ColorF value, float multiplier)
		{
			return new ColorF(
				value.A * multiplier,
				value.R * multiplier,
				value.G * multiplier,
				value.B * multiplier);
		}

		public static ColorF operator -(ColorF value, float valueToSubtract)
		{
			return new ColorF(
				value.R - valueToSubtract,
				value.G - valueToSubtract,
				value.B - valueToSubtract);
		}

		public static ColorF operator +(ColorF value, float valueToAdd)
		{
			return new ColorF(
				value.R + valueToAdd,
				value.G + valueToAdd,
				value.B + valueToAdd);
		}

		public static ColorF operator /(ColorF value, float divider)
		{
			return new ColorF(
				value.R / divider,
				value.G / divider,
				value.B / divider);
		}

		public static ColorF operator +(ColorF left, ColorF right)
		{
			return new ColorF(
				left.A + right.A,
				left.R + right.R,
				left.G + right.G,
				left.B + right.B);
		}

		public static ColorF operator -(ColorF left, ColorF right)
		{
			return new ColorF(
				left.R - right.R,
				left.G - right.G,
				left.B - right.B);
		}

		public static ColorF operator *(ColorF left, ColorF right)
		{
			return new ColorF(
				left.A * right.A,
				left.R * right.R,
				left.G * right.G,
				left.B * right.B);
		}

		public static explicit operator Color(ColorF value)
		{
			return new Color(
				(byte)(MathUtility.Saturate(value.A) * 255.0f),
				(byte)(MathUtility.Saturate(value.R) * 255.0f),
				(byte)(MathUtility.Saturate(value.G) * 255.0f),
				(byte)(MathUtility.Saturate(value.B) * 255.0f));
		}

		public static explicit operator Vector3D(ColorF value)
		{
			return new Vector3D(value.R, value.G, value.B);
		}

		#endregion

		public ColorRgbF Rgb
		{
			get { return new ColorRgbF(R, G, B); }
		}

		public float Red
		{
			get { return R; }
			set { R = value; }
		}

		public float Green
		{
			get { return G; }
			set { G = value; }
		}

		public float Blue
		{
			get { return B; }
			set { B = value; }
		}

		public float Alpha
		{
			get { return A; }
			set { A = value; }
		}

		public static ColorF Invert(ColorF value)
		{
			return new ColorF(1 - value.A, 1 - value.R, 1 - value.G, 1 - value.B);
		}
	}
}