namespace Nexus.NumericalAnalysis
{
	/// <summary>
	/// Calculate the derivative of the values in y
	/// with respect to x
	/// </summary>
	/// <param name="y">Array of values to calculate
	/// the derivatives of</param>
	/// <param name="x">Value to derive with respect to (usually time)</param>
	/// <returns>Array of the derivatives of the values of y</returns>
	public delegate float[] CalculateDerivatives(float[] y, float x);

	/// <summary>
	/// This class implements functions that solve first-order
	/// ordinary differential equations (ODEs).
	/// </summary>
	public abstract class OdeSolver
	{
		protected OdeSolver(int dimensions, CalculateDerivatives callback)
		{
			Dimensions = dimensions;
			Callback = callback;
		}

		protected int Dimensions { get; set; }
		protected CalculateDerivatives Callback { get; set; }

		/// <summary>
		/// This method solves first-order ODEs. It is passed an array
		/// of dependent variables in "array" at x, and returns an
		/// array of new values at x + h.
		/// </summary>
		/// <param name="initial">Array of initial values</param>
		/// <param name="x">Initial value of x (usually time)</param>
		/// <param name="h">Increment value for x (usually delta time)</param>
		/// <returns></returns>
		public abstract float[] Solve(float[] initial, float x, float h);

		/// <summary>
		/// Processes a simple Euler step
		/// </summary>
		/// <param name="initial"></param>
		/// <param name="derivs"></param>
		/// <param name="h"></param>
		/// <returns></returns>
		protected static float[] DoEulerStep(float[] initial, float[] derivs, float h)
		{
			int len = initial.Length;
			float[] ret = new float[len];
			for (int i = 0; i < len; i++)
				ret[i] = initial[i] + derivs[i] * h;
			return ret;
		}
	}
}