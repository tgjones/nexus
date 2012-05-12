using Nexus.Graphics.Colors;

namespace Nexus.Graphics
{
	public class WriteableBitmapBuffer : IImageBuffer
	{
		private readonly WriteableBitmapWrapper _writeableBitmap;

		public WriteableBitmapBuffer(WriteableBitmapWrapper writeableBitmap)
		{
			_writeableBitmap = writeableBitmap;
		}

		public ColorF this[int x, int y]
		{
			get { return (ColorF) _writeableBitmap.GetPixel(x, y); }
			set { _writeableBitmap.SetPixel(x, y, (Color) value); }
		}
	}
}