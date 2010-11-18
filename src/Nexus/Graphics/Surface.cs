namespace Nexus.Graphics
{
	public class Surface<T>
		where T : struct
	{
		private readonly T[, ,] _values;

		protected T[, ,] Values
		{
			get { return _values; }
		}

		public int Width { get; private set; }
		public int Height { get; private set; }
		public int MultiSampleCount { get; private set; }

		public T this[int x, int y]
		{
			get { return _values[x, y, 0]; }
		}

		public T this[int x, int y, int sampleIndex]
		{
			get { return _values[x, y, sampleIndex]; }
			set { _values[x, y, sampleIndex] = value; }
		}

		public Surface(int width, int height, int multiSampleCount)
		{
			_values = new T[width, height, multiSampleCount];

			Width = width;
			Height = height;
			MultiSampleCount = multiSampleCount;
		}

		public void Clear(T value)
		{
			for (int y = 0; y < Height; ++y)
				for (int x = 0; x < Width; ++x)
					for (int i = 0; i < MultiSampleCount; ++i)
						_values[x, y, i] = value;
		}
	}
}