namespace Nexus
{
	/// <summary>
	/// http://www.student.kuleuven.be/~m0216922/CG/color.html#RGB_to_HSL_
	/// </summary>
	public struct HslColor
	{
		#region Constructor

		public HslColor(float h, float s, float l)
			: this()
		{
			H = h;
			S = s;
			L = l;
		}

		#endregion

		#region Properties

		public float H { get; set; }
		public float S { get; set; }
		public float L { get; set; }

		#endregion

		#region Methods

		public Color ToRgbColor()
		{
			float r = 0, g = 0, b = 0;

			if (S == 0.0f)
			{
				// If saturation is 0, the colour is a shade of grey.
				r = g = b = L;
			}
			else
			{
				// If saturation is higher than 0, more calculations are needed again. Red, green
				// and blue are calculated with the following formulas.
				//Set the temporary values      
				float temp2 = (L < 0.5f) ? L * (1 + S) : (L + S) - (L * S);
				float temp1 = 2.0f * L - temp2;
				float tempr = H + 1.0f / 3.0f;
				if (tempr > 1) tempr--;
				float tempg = H;
				float tempb = H - 1.0f / 3.0f;
				if (tempb < 0) tempb++;

				// Red
				if (tempr < 1.0f / 6.0f) r = temp1 + (temp2 - temp1) * 6.0f * tempr;
				else if (tempr < 0.5f) r = temp2;
				else if (tempr < 2.0f / 3.0f) r = temp1 + (temp2 - temp1) * ((2.0f / 3.0f) - tempr) * 6.0f;
				else r = temp1;

				// Green
				if (tempg < 1.0f / 6.0f) g = temp1 + (temp2 - temp1) * 6.0f * tempg;
				else if (tempg < 0.5f) g = temp2;
				else if (tempg < 2.0f / 3.0f) g = temp1 + (temp2 - temp1) * ((2.0f / 3.0f) - tempg) * 6.0f;
				else g = temp1;

				// Blue
				if (tempb < 1.0f / 6.0f) b = temp1 + (temp2 - temp1) * 6.0f * tempb;
				else if (tempb < 0.5f) b = temp2;
				else if (tempb < 2.0f / 3.0f) b = temp1 + (temp2 - temp1) * ((2.0f / 3.0f) - tempb) * 6.0f;
				else b = temp1;
			}

			return new Color((byte)(r * 255.0f), (byte)(g * 255.0f), (byte)(b * 255.0f));
		}

		#endregion

		#region Static stuff

		public static HslColor FromRgbColor(Color rgb)
		{
			return FromRgbColorF(ColorF.FromRgbColor(rgb));
		}

		public static HslColor FromRgbColorF(ColorF rgb)
		{
			float h, s, l;

			// These two variables are needed because the Lightness is defined as
			// (minColour + maxColour) / 2
			float minColour = System.Math.Min(System.Math.Min(rgb.R, rgb.G), rgb.B);
			float maxColour = System.Math.Max(System.Math.Max(rgb.R, rgb.G), rgb.B);

			// If minColour equals maxColour, we know that R = G = B and thus the colour
			// is a shade of grey. This is a trivial case, hue can be set to anything,
			// saturation has to be to 0 because only then it's a shade of grey, and lightness
			// is set to R = G = B, the shade of the grey.
			if (minColour == maxColour)
			{
				// R = G = B to it's a shade of grey
				h = 0.0f; // it doesn't matter what value it has
				s = 0.0f;
				l = rgb.R; // doesn't matter if you pick r, b, or b
			}
			else
			{
				// If minColour is not equal to maxColour, we have a real colour instead of
				// a shade of grey, so more calculations are needed:
				// - Lightness (l) is now set to its definition of (minColour + maxColour) / 2
				// - Saturation (s) is then calculated with a different formula depending if light
				//   is in the first half or the second half. This is because the HSL model can be
				//   represented as a double cone, the first cone has a black tip and corresponds
				//   to the first half of lightness values, the second cone has a white tip and
				//   contains the second half of lightness values.
				// - Hue (h) is calculated with a different formula depending on which of the 3
				//   colour components is the dominating one, and then normalised to a number
				//   between 0 and 1.
				l = (minColour + maxColour) / 2.0f;

				float delta = maxColour - minColour;
				if (l < 0.5f)
					s = delta / (maxColour + minColour);
				else
					s = delta / (2.0f - delta);

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

			return new HslColor(h, s, l);
		}

		#endregion
	}
}