namespace Nexus
{
	public static class ColorsF
	{
		public static readonly ColorF Black;
		public static readonly ColorF Blue;
		public static readonly ColorF Empty;
		public static readonly ColorF Gray;
		public static readonly ColorF Green;
		public static readonly ColorF Red;
		public static readonly ColorF Transparent;
		public static readonly ColorF White;

		static ColorsF()
		{
			Black = ColorF.FromRgbColor(Colors.Black);
			Blue = ColorF.FromRgbColor(Colors.Blue);
			Empty = ColorF.FromRgbColor(Colors.Empty);
			Gray = ColorF.FromRgbColor(Colors.Gray);
			Green = ColorF.FromRgbColor(Colors.Green);
			Red = ColorF.FromRgbColor(Colors.Red);
			Transparent = ColorF.FromRgbColor(Colors.Transparent);
			White = ColorF.FromRgbColor(Colors.White);
		}
	}
}