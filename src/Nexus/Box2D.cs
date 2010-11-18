using System;

namespace Nexus
{
	public struct Box2D
	{
		public IntPoint2D Min { get; set; }
		public IntPoint2D Max { get; set; }

		public Box2D(IntPoint2D min, IntPoint2D max)
			: this()
		{
			Min = min;
			Max = max;
		}

		public static Box2D Intersection(Box2D box1, Box2D box2)
		{
			return new Box2D(
				new IntPoint2D(Math.Max(box1.Min.X, box2.Min.X), Math.Max(box1.Min.Y, box2.Min.Y)),
				new IntPoint2D(Math.Min(box1.Max.X, box2.Max.X), Math.Min(box1.Max.Y, box2.Max.Y)));
		}

		public static Box2D Union(Box2D box1, Box2D box2)
		{
			return new Box2D(
				new IntPoint2D(Math.Min(box1.Min.X, box2.Min.X), Math.Min(box1.Min.Y, box2.Min.Y)),
				new IntPoint2D(Math.Max(box1.Max.X, box2.Max.X), Math.Max(box1.Max.Y, box2.Max.Y)));
		}
	}
}