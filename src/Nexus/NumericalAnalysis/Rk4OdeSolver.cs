namespace Nexus.NumericalAnalysis
{
	/// <summary>
	/// Solves an ODE using RK4
	/// </summary>
	public class Rk4OdeSolver : OdeSolver
	{
		public Rk4OdeSolver(int dimensions, CalculateDerivatives callback)
			: base(dimensions, callback)
		{
		}

		public override float[] Solve(float[] initial, float x, float h)
		{
			float[] k1 = Callback(initial, x);
			float[] temp = DoEulerStep(initial, k1, h / 2.0f);

			float[] k2 = Callback(temp, x + (h / 2.0f));
			temp = DoEulerStep(initial, k2, h / 2.0f);

			float[] k3 = Callback(temp, x + (h / 2.0f));
			temp = DoEulerStep(initial, k3, h);

			float[] k4 = Callback(temp, x + h);

			float[] ret = DoEulerStep(initial, k1, h / 6.0f);
			ret = DoEulerStep(ret, k2, h / 3.0f);
			ret = DoEulerStep(ret, k3, h / 3.0f);
			ret = DoEulerStep(ret, k4, h / 6.0f);
			return ret;
		}
	}
}