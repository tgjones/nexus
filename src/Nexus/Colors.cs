namespace Nexus
{
	public static class Colors
	{
		public static readonly Color Black;
		public static readonly Color Blue;
		public static readonly Color Empty;
		public static readonly Color Gray;
		public static readonly Color Green;
		public static readonly Color Red;
		public static readonly Color Transparent;
		public static readonly Color White;

		static Colors()
		{
			Black = new Color(0, 0, 0);
			Blue = new Color(0, 0, 255);
			Empty = new Color(0, 0, 0, 0);
			Gray = new Color(128, 128, 128);
			Green = new Color(0, 255, 0);
			Red = new Color(255, 0, 0);
			Transparent = new Color(0, 0, 0, 0);
			White = new Color(255, 255, 255);
		}
	}
}