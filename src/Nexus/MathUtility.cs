namespace Nexus
{
	public static class MathUtility
	{
		public const float PI = (float) System.Math.PI;
		public const float PI_OVER_2 = (float) (System.Math.PI / 2.0f);
		public const float PI_OVER_4 = (float) (System.Math.PI / 4.0f);
		public const float TWO_PI = (float)(System.Math.PI * 2.0f);

		/// <summary>
		/// Clamps a value to an interval.
		/// </summary>
		/// <param name="value">The input parameter.</param>
		/// <param name="min">The lower clamp threshold.</param>
		/// <param name="max">The upper clamp threshold.</param>
		/// <returns>The clamped value.</returns>
		public static float Clamp(float value, float min, float max)
		{
			value = (value > max) ? max : value;
			value = (value < min) ? min : value;
			return value;
		}

		/// <summary>
		/// Clamps a value to an interval.
		/// </summary>
		/// <param name="value">The input parameter.</param>
		/// <param name="min">The lower clamp threshold.</param>
		/// <param name="max">The upper clamp threshold.</param>
		/// <returns>The clamped value.</returns>
		public static int Clamp(int value, int min, int max)
		{
			value = (value > max) ? max : value;
			value = (value < min) ? min : value;
			return value;
		}

		/// <summary>
		/// Clamps a value to an interval.
		/// </summary>
		/// <param name="value">The input parameter.</param>
		/// <param name="min">The lower clamp threshold.</param>
		/// <param name="max">The upper clamp threshold.</param>
		/// <returns>The clamped value.</returns>
		public static byte Clamp(byte value, byte min, byte max)
		{
			value = (value > max) ? max : value;
			value = (value < min) ? min : value;
			return value;
		}

		public static int Floor(float value)
		{
			return (int) System.Math.Floor(value);
		}

		public static int Ceiling(float value)
		{
			return (int)System.Math.Ceiling(value);
		}

		/// <summary>
		/// Returns a mod b. This differs from the % operator with respect to negative numbers.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <returns>a mod b</returns>
		public static double Mod(double a, double b)
		{
			int n = (int)(a / b);

			a -= n * b;
			if (a < 0)
				return a + b;
			return a;
		}

		/// <summary>
		/// Returns a mod b. This differs from the % operator with respect to negative numbers.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <returns>a mod b</returns>
		public static int Mod(int a, int b)
		{
			int n = a / b;

			a -= n * b;
			if (a < 0)
				return a + b;
			return a;
		}

		/// <summary>
		/// Interpolates linearly between the supplied values.
		/// </summary>
		/// <param name="value1">The lower interpolation bound.</param>
		/// <param name="value2">The upper interpolation bound.</param>
		/// <param name="amount">The interpolation parameter.</param>
		/// <returns>The interpolated value.</returns>
		public static float Lerp(float value1, float value2, float amount)
		{
			return (1 - amount) * value1 + amount * value2;
			//return Lerp(value1, value2, 0, 1, amount); // This is the same thing
		}

		/// <summary>
		/// Interpolates linearly between the supplied values.
		/// </summary>
		/// <param name="value1">The lower interpolation bound.</param>
		/// <param name="value2">The upper interpolation bound.</param>
		/// <param name="amount">The interpolation parameter.</param>
		/// <returns>The interpolated value.</returns>
		public static int Lerp(int value1, int value2, float amount)
		{
			return (int) ((1 - amount) * value1 + amount * value2);
		}

		/// <summary>
		/// Interpolates linearly between the supplied values.
		/// </summary>
		/// <param name="value1">The lower interpolation bound.</param>
		/// <param name="value2">The upper interpolation bound.</param>
		/// <param name="amount">The interpolation parameter.</param>
		/// <returns>The interpolated value.</returns>
		public static byte Lerp(byte value1, byte value2, float amount)
		{
			return (byte)((1 - amount) * value1 + amount * value2);
		}

		public static float Lerp(float value1, float value2, float startAmount, float endAmount, float amount)
		{
			return (((value2 - value1) * amount) + ((value1 * endAmount) + (value2 * startAmount))) / (endAmount - startAmount);
		}

		public static float Log2(float d)
		{
			return (float) System.Math.Log(d, 2.0f);
		}

		public static float PerspectiveInterpolate(float value1, float value2, float w1, float w2, float amountRangeStart, float amountRangeEnd, float amount)
		{
			// Don't ask me how I got this... it's mostly from page 124 of 3D Game Engine Design,
			// with a correction as noted on http://www.geometrictools.com/Books/GameEngineDesign2/BookCorrections.html.
			float numerator = (((value2 * w1) - (value1 * w2)) * amount) + ((value1 * w2 * amountRangeEnd) - (value2 * w1 * amountRangeStart));
			float denominator = ((w1 - w2) * amount) + ((w2 * amountRangeEnd) - (w1 * amountRangeStart));
			return numerator / denominator;
		}

		public static int Round(float value)
		{
			return (int) System.Math.Round(value);
		}

		/// <summary>
		/// A smoothed step function. A cubic function is used to smooth the step between two thresholds.
		/// </summary>
		/// <param name="a">The lower threshold position.</param>
		/// <param name="b">The upper threshold position.</param>
		/// <param name="x">The input parameter.</param>
		/// <returns>The interpolated value.</returns>
		public static float SmoothStep(float a, float b, float x)
		{
			if (x < a)
				return 0;
			if (x >= b)
				return 1;
			x = (x - a) / (b - a);
			return x * x * (3 - 2 * x);
		}

		public static float Saturate(float value)
		{
			value = (value > 1.0f) ? 1.0f : value;
			value = (value < 0.0f) ? 0.0f : value;
			return value;
		}

		public static float Exp(float value)
		{
			return (float)System.Math.Exp(value);
		}

		public static float Pow(float x, float y)
		{
			return (float)System.Math.Pow(x, y);
		}

		public static float Sqrt(float value)
		{
			return (float) System.Math.Sqrt(value);
		}

		public static float ToDegrees(float radians)
		{
			return (radians * 57.29578f);
		}

		public static float ToRadians(float degrees)
		{
			return (degrees * 0.01745329f);
		}

		public static float Cos(float radians)
		{
			return (float) System.Math.Cos(radians);
		}

		public static float Acos(float radians)
		{
			return (float)System.Math.Acos(radians);
		}

		public static float Sin(float radians)
		{
			return (float) System.Math.Sin(radians);
		}

		public static float Asin(float sin)
		{
			return (float) System.Math.Asin(sin);
		}

		public static float Tan(float radians)
		{
			return (float) System.Math.Tan(radians);
		}

		public static float Atan2(float y, float x)
		{
			return (float)System.Math.Atan2(y, x);
		}

		public static void Swap(ref float v1, ref float v2)
		{
			float temp = v1;
			v1 = v2;
			v2 = temp;
		}

		public static bool IsZero(float value)
		{
			return (System.Math.Abs(value) < 1.0E-6f);
		}

		public static bool Quadratic(float A, float B, float C, out float t0, out float t1)
		{
			// Find quadratic discriminant
			float discrim = B * B - 4.0f * A * C;
			if (discrim < 0.0f)
			{
				t0 = t1 = float.MinValue;
				return false;
			}
			float rootDiscrim = Sqrt(discrim);

			// Compute quadratic _t_ values
			float q;
			if (B < 0) q = -0.5f * (B - rootDiscrim);
			else q = -0.5f * (B + rootDiscrim);
			t0 = q / A;
			t1 = C / q;
			if (t0 > t1) Swap(ref t0, ref t1);
			return true;
		}
	}
}