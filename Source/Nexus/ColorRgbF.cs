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
	}
}