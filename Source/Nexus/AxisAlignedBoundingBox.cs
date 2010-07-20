using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexus
{
	public struct AxisAlignedBoundingBox
	{
		public Point3D Min;
		public Point3D Max;

		public AxisAlignedBoundingBox(Point3D p)
		{
			Min = Max = p;
		}

		public AxisAlignedBoundingBox(Point3D p1, Point3D p2)
		{
			Min = new Point3D(Math.Min(p1.X, p2.X),
				Math.Min(p1.Y, p2.Y),
				Math.Min(p1.Z, p2.Z));
			Max = new Point3D(Math.Max(p1.X, p2.X),
				Math.Max(p1.Y, p2.Y),
				Math.Max(p1.Z, p2.Z));
		}

		public AxisAlignedBoundingBox(IEnumerable<Point3D> points)
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

		public AxisAlignedBoundingBox CreateTransformedBoundingVolume(Matrix3D transform)
		{
			AxisAlignedBoundingBox result = new AxisAlignedBoundingBox();

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

		#endregion

		#region Static stuff

		public static AxisAlignedBoundingBox Union(AxisAlignedBoundingBox b, Point3D p)
		{
			AxisAlignedBoundingBox ret = b;
			ret.Min.X = System.Math.Min(b.Min.X, p.X);
			ret.Min.Y = System.Math.Min(b.Min.Y, p.Y);
			ret.Min.Z = System.Math.Min(b.Min.Z, p.Z);
			ret.Max.X = System.Math.Max(b.Max.X, p.X);
			ret.Max.Y = System.Math.Max(b.Max.Y, p.Y);
			ret.Max.Z = System.Math.Max(b.Max.Z, p.Z);
			return ret;
		}

		public static AxisAlignedBoundingBox Union(AxisAlignedBoundingBox b, AxisAlignedBoundingBox b2)
		{
			AxisAlignedBoundingBox ret;
			ret.Min.X = System.Math.Min(b.Min.X, b2.Min.X);
			ret.Min.Y = System.Math.Min(b.Min.Y, b2.Min.Y);
			ret.Min.Z = System.Math.Min(b.Min.Z, b2.Min.Z);
			ret.Max.X = System.Math.Max(b.Max.X, b2.Max.X);
			ret.Max.Y = System.Math.Max(b.Max.Y, b2.Max.Y);
			ret.Max.Z = System.Math.Max(b.Max.Z, b2.Max.Z);
			return ret;
		}

		public AxisAlignedBoundingBox Transform(Matrix3D matrix)
		{
			AxisAlignedBoundingBox result = new AxisAlignedBoundingBox(matrix.Transform(new Point3D(Min.X, Min.Y, Min.Z)));
			result = Union(result, new AxisAlignedBoundingBox(matrix.Transform(new Point3D(Max.X, Min.Y, Min.Z))));
			result = Union(result, new AxisAlignedBoundingBox(matrix.Transform(new Point3D(Min.X, Max.Y, Min.Z))));
			result = Union(result, new AxisAlignedBoundingBox(matrix.Transform(new Point3D(Min.X, Min.Y, Max.Z))));
			result = Union(result, new AxisAlignedBoundingBox(matrix.Transform(new Point3D(Min.X, Max.Y, Max.Z))));
			result = Union(result, new AxisAlignedBoundingBox(matrix.Transform(new Point3D(Max.X, Max.Y, Min.Z))));
			result = Union(result, new AxisAlignedBoundingBox(matrix.Transform(new Point3D(Max.X, Min.Y, Max.Z))));
			result = Union(result, new AxisAlignedBoundingBox(matrix.Transform(new Point3D(Max.X, Max.Y, Max.Z))));
			return result;
		}

		#endregion
	}
}