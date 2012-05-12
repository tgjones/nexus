using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexus.Objects3D
{
	public struct AxisAlignedBox3D
	{
		public static AxisAlignedBox3D Empty
		{
			get
			{
				return new AxisAlignedBox3D
				{
					Min = new Point3D(float.NaN, float.NaN, float.NaN),
					Max = new Point3D(float.NaN, float.NaN, float.NaN)
				};
			}
		}

		public Point3D Min;
		public Point3D Max;

		public Vector3D Size
		{
			get { return Vector3D.Abs(Max - Min); }
		}

		public Point3D Center
		{
			get { return Min + ((Max - Min) / 2); }
		}

		public AxisAlignedBox3D(Point3D p)
		{
			Min = Max = p;
		}

		public AxisAlignedBox3D(Point3D p1, Point3D p2)
		{
			Min = new Point3D(Math.Min(p1.X, p2.X),
				Math.Min(p1.Y, p2.Y),
				Math.Min(p1.Z, p2.Z));
			Max = new Point3D(Math.Max(p1.X, p2.X),
				Math.Max(p1.Y, p2.Y),
				Math.Max(p1.Z, p2.Z));
		}

		public AxisAlignedBox3D(IEnumerable<Point3D> points)
		{
			if (points == null || !points.Any())
				throw new ArgumentException();

			// Compute minimal and maximal bounds.
			Min = points.First();
			Max = points.First();

			foreach (Point3D point in points)
			{
				if (point.X < Min.X)
					Min.X = point.X;
				else if (point.X > Max.X)
					Max.X = point.X;

				if (point.Y < Min.Y)
					Min.Y = point.Y;
				else if (point.Y > Max.Y)
					Max.Y = point.Y;

				if (point.Z < Min.Z)
					Min.Z = point.Z;
				else if (point.Z > Max.Z)
					Max.Z = point.Z;
			}
		}

		#region Methods

		public void Expand(float delta)
		{
			Min -= new Vector3D(delta, delta, delta);
			Max += new Vector3D(delta, delta, delta);
		}

		public float Volume()
		{
			Vector3D d = Max - Min;
			return d.X * d.Y * d.Z;
		}

		public int MaximumExtent()
		{
			Vector3D diag = Max - Min;
			if (diag.X > diag.Y && diag.X > diag.Z)
				return 0;
			if (diag.Y > diag.Z)
				return 1;
			return 2;
		}

		public AxisAlignedBox3D CreateTransformedBoundingVolume(Matrix3D transform)
		{
			AxisAlignedBox3D result = new AxisAlignedBox3D();

			// For all three axes.
			for (int i = 0; i < 3; ++i)
			{
				// Start by adding in translation.
				result.Min[i] = result.Max[i] = transform[i, 3];

				// Form extent by summing smaller and larger terms respectively.
				for (int j = 0; j < 3; ++j)
				{
					float e = transform[i, j] * Min[j];
					float f = transform[i, j] * Max[j];
					if (e < f)
					{
						result.Min[i] += e;
						result.Max[i] += f;
					}
					else
					{
						result.Min[i] += f;
						result.Max[i] += e;
					}
				}
			}

			return result;
		}

		public Point3D[] GetCorners()
		{
			return new[]
			{
				new Point3D(this.Min.X, this.Max.Y, this.Max.Z),
				new Point3D(this.Max.X, this.Max.Y, this.Max.Z),
				new Point3D(this.Max.X, this.Min.Y, this.Max.Z),
				new Point3D(this.Min.X, this.Min.Y, this.Max.Z),
				new Point3D(this.Min.X, this.Max.Y, this.Min.Z),
				new Point3D(this.Max.X, this.Max.Y, this.Min.Z),
				new Point3D(this.Max.X, this.Min.Y, this.Min.Z),
				new Point3D(this.Min.X, this.Min.Y, this.Min.Z)
			};
		}

		#endregion

		#region Static stuff

		public static AxisAlignedBox3D Union(AxisAlignedBox3D b, Point3D p)
		{
			AxisAlignedBox3D ret = b;
			ret.Min.X = CheckedMin(b.Min.X, p.X);
			ret.Min.Y = CheckedMin(b.Min.Y, p.Y);
			ret.Min.Z = CheckedMin(b.Min.Z, p.Z);
			ret.Max.X = CheckedMax(b.Max.X, p.X);
			ret.Max.Y = CheckedMax(b.Max.Y, p.Y);
			ret.Max.Z = CheckedMax(b.Max.Z, p.Z);
			return ret;
		}

		private static float CheckedMin(float v1, float v2)
		{
			if (float.IsNaN(v1))
				return v2;

			if (float.IsNaN(v2))
				return v1;

			return Math.Min(v1, v2);
		}

		private static float CheckedMax(float v1, float v2)
		{
			if (float.IsNaN(v1))
				return v2;

			if (float.IsNaN(v2))
				return v1;

			return Math.Max(v1, v2);
		}

		public static AxisAlignedBox3D Union(AxisAlignedBox3D b, AxisAlignedBox3D b2)
		{
			AxisAlignedBox3D ret;
			ret.Min.X = CheckedMin(b.Min.X, b2.Min.X);
			ret.Min.Y = CheckedMin(b.Min.Y, b2.Min.Y);
			ret.Min.Z = CheckedMin(b.Min.Z, b2.Min.Z);
			ret.Max.X = CheckedMax(b.Max.X, b2.Max.X);
			ret.Max.Y = CheckedMax(b.Max.Y, b2.Max.Y);
			ret.Max.Z = CheckedMax(b.Max.Z, b2.Max.Z);
			return ret;
		}

		public AxisAlignedBox3D Transform(Matrix3D matrix)
		{
			AxisAlignedBox3D result = new AxisAlignedBox3D(matrix.Transform(new Point3D(Min.X, Min.Y, Min.Z)));
			result = Union(result, new AxisAlignedBox3D(matrix.Transform(new Point3D(Max.X, Min.Y, Min.Z))));
			result = Union(result, new AxisAlignedBox3D(matrix.Transform(new Point3D(Min.X, Max.Y, Min.Z))));
			result = Union(result, new AxisAlignedBox3D(matrix.Transform(new Point3D(Min.X, Min.Y, Max.Z))));
			result = Union(result, new AxisAlignedBox3D(matrix.Transform(new Point3D(Min.X, Max.Y, Max.Z))));
			result = Union(result, new AxisAlignedBox3D(matrix.Transform(new Point3D(Max.X, Max.Y, Min.Z))));
			result = Union(result, new AxisAlignedBox3D(matrix.Transform(new Point3D(Max.X, Min.Y, Max.Z))));
			result = Union(result, new AxisAlignedBox3D(matrix.Transform(new Point3D(Max.X, Max.Y, Max.Z))));
			return result;
		}

		#endregion
	}
}