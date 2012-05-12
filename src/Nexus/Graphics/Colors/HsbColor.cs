namespace Nexus.Graphics.Colors
{
	/// <summary>
	/// http://www.student.kuleuven.be/~m0216922/CG/color.html#RGB_to_HSL_
	/// HSB is also known as HSV.
	/// </summary>
	public struct HsbColor
	{
		#region Constructor

		public HsbColor(float h, float s, float b)
			: this()
		{
			H = h;
			S = s;
			B = b;
		}

		#endregion

		#region Properties

		public float H { get; set; }
		public float S { get; set; }
		public float B { get; set; }

		#endregion

		#region Methods

		public Color ToRgbColor()
		{
			float r = 0, g = 0, b = 0;

			// If saturation is 0, the colour is a shade of grey.
			if (S == 0.0f)
			{
				r = g = b = B;
			}
			else
			{
				// The HSB model can be presented on a cone with hexagonal shape.
				// For each side of the hexagon, a separate case is calculated.
				float h = H * 6.0f; // to bring hue to a number between 0 and 6, better for the calcuations
				int i = (int) System.Math.Floor(h); // eg. 2.7 becomes 2, 3.01 becomes 3, and 4.9999 becomes 4
				float f = h - i; // the fractional part of h
				float p = B * (1 - S);
				float q = B * (1 - (S * f));
				float t = B * (1 - (S * (1 - f)));
				switch (i)
				{
					case 0: r = B; g = t; b = p; break;
					case 1: r = q; g = B; b = p; break;
					case 2: r = p; g = B; b = t; break;
					case 3: r = p; g = q; b = B; break;
					case 4: r = t; g = p; b = B; break;
					case 5: r = B; g = p; b = q; break;
				}
			}

			return new Color((byte) (r * 255.0f), (byte) (g * 255.0f), (byte) (b * 255.0f));
		}

		#endregion

		#region Static stuff

		public static HsbColor FromRgbColor(Color rgb)
		{
			return FromRgbColorF(ColorF.FromRgbColor(rgb));
		}

		public static HsbColor FromRgbColorF(ColorF rgb)
		{
			float minColour = System.Math.Min(System.Math.Min(rgb.R, rgb.G), rgb.B);
			float maxColour = System.Math.Max(System.Math.Max(rgb.R, rgb.G), rgb.B);
			float delta = maxColour - minColour;

			float b = maxColour;

			// If the colour is black, the value of saturation doesn't matter so it
			// can be set to 0. This has to be done to avoid a division by 0.
			float s = (maxColour != 0) ? 255 * delta / maxColour : 0.0f;

			// Calculate hue. If saturation is 0, the colour is gray so hue doesn't
			// matter. Again this case is handled separately to avoid divisions by 0.
			float h;
			if (s == 0.0f)
			{
				h = 0.0f;
			}
			else
			{
				if (rgb.R == maxColour)
					h = (rgb.G - rgb.B) / delta;
				else if (rgb.G == maxColour)
					h = 2.0f + (rgb.B - rgb.R) / delta;
				else
					h = 4.0f + (rgb.R - rgb.G) / delta;
				h /= 6.0f; // to bring it to a number between 0 and 1.
				if (h < 0)
					++h;
			}

			return new HsbColor(h, s, b);
		}

		#endregion
	}
}