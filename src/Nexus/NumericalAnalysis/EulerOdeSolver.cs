namespace Nexus.NumericalAnalysis
{
	/// <summary>
	/// Solves an ODE using Euler integration
	/// </summary>
	public class EulerOdeSolver : OdeSolver
	{
		public EulerOdeSolver(int dimensions, CalculateDerivatives callback)
			: base(dimensions, callback)
		{
		}

		public override float[] Solve(float[] initial, float x, float h)
		{
			// final = initial + derived * h
			float[] derivs = Callback(initial, x);
			return DoEulerStep(initial, derivs, h);
		}
	}
}