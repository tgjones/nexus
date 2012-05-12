using System.Runtime.InteropServices;

namespace Nexus.Graphics.Colors
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ColorRgb
	{
		public byte R;
		public byte G;
		public byte B;

		public ColorRgb(byte r, byte g, byte b)
		{
			R = r;
			G = g;
			B = b;
		}
	}
}