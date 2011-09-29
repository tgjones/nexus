namespace Nexus.NumericalAnalysis
{
	/// <summary>
	/// Solves an ODE using RK2
	/// </summary>
	public class Rk2OdeSolver : OdeSolver
	{
		public Rk2OdeSolver(int dimensions, CalculateDerivatives callback)
			: base(dimensions, callback)
		{
		}

		public override float[] Solve(float[] initial, float x, float h)
		{
			// do Euler step with half the step value
			float[] k1 = Callback(initial, x);
			float[] temp = DoEulerStep(initial, k1, h / 2.0f);

			// calculate again at midpoint
			float[] k2 = Callback(temp, x + (h / 2.0f));

			// use derivatives for complete timestep
			return DoEulerStep(initial, k2, h);
		}
	}
}