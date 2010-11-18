namespace Nexus.Graphics
{
	public class ColorSurface : Surface<ColorF>
	{
		public ColorSurface(int width, int height, int multiSampleCount)
			: base(width, height, multiSampleCount)
		{

		}

		public void Resolve(IImageBuffer destination)
		{
			for (int y = 0; y < Height; ++y)
				for (int x = 0; x < Width; ++x)
					destination[x, y] = ResolvePixel(x, y);
		}

		public ColorF ResolvePixel(int x, int y)
		{
			ColorF color = ColorsF.Transparent;
			for (int sampleIndex = 0; sampleIndex < MultiSampleCount; ++sampleIndex)
				color += Values[x, y, sampleIndex];
			return color / MultiSampleCount;
		}
	}
}
