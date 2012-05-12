namespace Nexus.Objects3D
{
	public struct Ray3D
	{
		public Point3D Origin;
		public Vector3D Direction;

		public Ray3D(Point3D origin, Vector3D direction)
		{
			Origin = origin;
			Direction = direction;
		}

		public Point3D Evaluate(float t)
		{
			return Origin + Direction * t;
		}

		public override string ToString()
		{
			return string.Format("{{Origin:{0} Direction:{1}}}", Origin, Direction);
		}
	}
}