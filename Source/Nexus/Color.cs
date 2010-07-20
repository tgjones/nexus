using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Nexus.Design;

namespace Nexus
{
	[TypeConverter(typeof(ColorConverter))]
	[StructLayout(LayoutKind.Sequential)]
	public struct Color
	{
		#region Fields

		public byte A;
		public byte R;
		public byte G;
		public byte B;

		#endregion

		#region Constructors

		public Color(byte r, byte g, byte b)
		{
			A = 255;
			R = r;
			G = g;
			B = b;
		}

		public Color(byte a, byte r, byte g, byte b)
		{
			A = a;
			R = r;
			G = g;
			B = b;
		}

		#endregion

		public Color Clone()
		{
			return new Color(A, R, G, B);
		}

		public int GetMeanBrightness()
		{
			return (R + G + B) / 3;
		}

		public static Color FromRgb(byte red, byte green, byte blue)
		{
			return new Color(red, green, blue);
		}

		public static Color FromArgb(byte alpha, byte red, byte green, byte blue)
		{
			return new Color(alpha, red, green, blue);
		}

		public static Color FromArgb(byte alpha, Color baseColor)
		{
			return new Color(alpha, baseColor.R, baseColor.G, baseColor.B);
		}

		public static Color FromHexRef(string hexRef)
		{
			return FromRgb(
				(byte)Convert.ToInt32(hexRef.Substring(1, 2), 16),
				(byte)Convert.ToInt32(hexRef.Substring(3, 2), 16),
				(byte)Convert.ToInt32(hexRef.Substring(5, 2), 16));
		}

		public static Color Lerp(float t, Color rgb1, Color rgb2)
		{
			return new Color(
				MathUtility.Lerp(rgb1.A, rgb2.A, t),
				MathUtility.Lerp(rgb1.R, rgb2.R, t),
				MathUtility.Lerp(rgb1.G, rgb2.G, t),
				MathUtility.Lerp(rgb1.B, rgb2.B, t));
		}

		#region Operators

		public static explicit operator ColorF(Color color)
		{
			return new ColorF(color.A / 255.0f, color.R / 255.0f, color.G / 255.0f, color.B / 255.0f);
		}

		#endregion
	}
}