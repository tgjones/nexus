using System.Runtime.InteropServices;

namespace Nexus
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte4
	{
		public byte X, Y, Z, W;

		public Byte4(byte x, byte y, byte z, byte w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}
	}
}