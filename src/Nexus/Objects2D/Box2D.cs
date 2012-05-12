using System;

namespace Nexus.Objects2D
{
	public struct Box2D
	{
		private IntPoint2D _min;
		private IntPoint2D _max;

		public static Box2D Empty
		{
			get { return new Box2D(); }
		}

		public IntPoint2D Min
		{
			get { return _min; }
			set { _min = value; }
		}

		public IntPoint2D Max
		{
			get { return _max; }
			set { _max = value; }
		}

		public Box2D(IntPoint2D min, IntPoint2D max)
		{
			_min = min;
			_max = max;
		}

		public void Expand(IntPoint2D point)
		{
			if (point.X < _min.X)
				_min.X = point.X;
			if (point.Y < _min.Y)
				_min.Y = point.Y;

			if (point.X > _max.X)
				_max.X = point.X;
			if (point.Y > _max.Y)
				_max.Y = point.Y;
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