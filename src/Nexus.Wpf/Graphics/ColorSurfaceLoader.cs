using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
#if SILVERLIGHT
using System.Windows.Resources;
#endif

namespace Nexus.Graphics
{
	public static class ColorSurfaceLoader
	{
		public static ColorSurface LoadFromFile(string uri)
		{
#if SILVERLIGHT
			StreamResourceInfo imageStream = Application.GetResourceStream(new Uri(uri, UriKind.Relative));
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.SetSource(imageStream.Stream);
#else
			BitmapImage bitmapImage = new BitmapImage(new Uri(uri));
#endif

			WriteableBitmapWrapper writeableBitmap = new WriteableBitmapWrapper(new WriteableBitmap(bitmapImage));

			ColorSurface surface = new ColorSurface(writeableBitmap.Width, writeableBitmap.Height, 1);
			PopulateSurface(surface, writeableBitmap);
			return surface;
		}

#if !SILVERLIGHT
		public static void PopulateFromStream(ColorSurface surface, Stream stream)
		{
			BitmapFrame bitmapImage = BitmapFrame.Create(stream);
			PopulateSurface(surface, new WriteableBitmapWrapper(new WriteableBitmap(bitmapImage)));
		}
#endif

		private static void PopulateSurface(ColorSurface surface, WriteableBitmapWrapper writeableBitmap)
		{
			for (int y = 0; y < surface.Height; ++y)
				for (int x = 0; x < surface.Width; ++x)
				{
					var c = writeableBitmap.GetPixel(x, y);
					surface[x, y, 0] = (ColorF) c;
				}
		}
	}
}