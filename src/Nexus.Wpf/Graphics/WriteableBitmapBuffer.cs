using Nexus.Util;

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
			get
			{
				System.Windows.Media.Color color = _writeableBitmap.GetPixel(x, y);
				return (ColorF)new Color(color.A, color.R, color.G, color.B);
			}
			set
			{
				Color color = (Color)value;
				_writeableBitmap.SetPixel(x, y, System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
			}
		}
	}
}