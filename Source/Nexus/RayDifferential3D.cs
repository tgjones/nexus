namespace Nexus
{
	public struct RayDifferential3D
	{
		public const float EPSILON = 1e-3f;

		public Point3D Origin;
		public Vector3D Direction;

		public float MinT;
		public float MaxT;
		public float Time;

		public bool HasDifferentials;
		public Ray3D RayX, RayY;

		public RayDifferential3D(Point3D origin, Vector3D direction, float start = EPSILON, float end = float.MaxValue, float time = 0.0f)
		{
			this.Origin = origin;
			this.Direction = direction;
			MinT = start;
			MaxT = end;
			Time = time;

			HasDifferentials = false;

			RayX = new Ray3D();
			RayY = new Ray3D();
		}

		public Point3D Evaluate(float t)
		{
			return Origin + Direction * t;
		}

		public override string ToString()
		{
			return string.Format("{{Origin:{0} Direction:{1}}}", this.Origin, this.Direction);
		}
	}
}