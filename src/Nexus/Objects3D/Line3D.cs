namespace Nexus.Objects3D
{
	public struct Line3D
	{
		public Point3D Start;
		public Point3D End;

		public Vector3D Direction
		{
			get { return End - Start; }
		}

		public Line3D(Point3D start, Point3D end)
		{
			Start = start;
			End = end;
		}
	}
}