namespace Nexus
{
	public static class Quadratic
	{
		public static bool Solve(float A, float B, float C, out float t0, out float t1)
		{
			// Find quadratic discriminant
			float discrim = B * B - 4.0f * A * C;
			if (discrim < 0.0f)
			{
				t0 = t1 = float.MinValue;
				return false;
			}
			float rootDiscrim = MathUtility.Sqrt(discrim);

			// Compute quadratic _t_ values
			float q;
			if (B < 0) q = -0.5f * (B - rootDiscrim);
			else q = -0.5f * (B + rootDiscrim);
			t0 = q / A;
			t1 = C / q;
			if (t0 > t1) MathUtility.Swap(ref t0, ref t1);
			return true;
		}
	}
}