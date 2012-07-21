using System;
using System.Globalization;

namespace Nexus.Objects3D
{
	public class Frustum3D : IEquatable<Frustum3D>
	{
		// Fields
		internal Point3D[] cornerArray;
		public const int CornerCount = 8;
		private Matrix3D matrix;
		private Plane3D[] planes;

		// Methods
		public Frustum3D(Matrix3D value)
		{
			this.planes = new Plane3D[6];
			this.cornerArray = new Point3D[8];
			this.SetMatrix(ref value);
		}

		public bool Equals(Frustum3D other)
		{
			if (other == null)
			{
				return false;
			}
			return (this.matrix == other.matrix);
		}

		public override bool Equals(object obj)
		{
			bool flag = false;
			Frustum3D frustum = obj as Frustum3D;
			if (frustum != null)
			{
				flag = this.matrix == frustum.matrix;
			}
			return flag;
		}

		public Point3D[] GetCorners()
		{
			return (Point3D[]) this.cornerArray.Clone();
		}

		public void GetCorners(Point3D[] corners)
		{
			if (corners == null)
			{
				throw new ArgumentNullException("corners");
			}
			if (corners.Length < 8)
			{
				throw new ArgumentOutOfRangeException("corners", "Not enough corners");
			}
			this.cornerArray.CopyTo(corners, 0);
		}

		public ContainmentType Contains(AxisAlignedBox3D box)
		{
			// FIXME: Is this a bug?
			// If the bounding box is of W * D * H = 0, then return disjoint
			if (box.Min == box.Max)
			{
				return ContainmentType.Disjoint;
			}

			int i;
			ContainmentType contained;
			Point3D[] corners = box.GetCorners();

			// First we assume completely disjoint. So if we find a point that is contained, we break out of this loop
			for (i = 0; i < corners.Length; i++)
			{
				if (Contains(corners[i]) != ContainmentType.Disjoint)
					break;
			}

			if (i == corners.Length) // This means we checked all the corners and they were all disjoint
			{
				return ContainmentType.Disjoint;
			}

			if (i != 0)             // if i is not equal to zero, we can fastpath and say that this box intersects
			{                       // because we know at least one point is outside and one is inside.
				return ContainmentType.Intersects;
			}

			// If we get here, it means the first (and only) point we checked was actually contained in the frustum.
			// So we assume that all other points will also be contained. If one of the points is disjoint, we can
			// exit immediately saying that the result is Intersects
			i++;
			for (; i < corners.Length; i++)
			{
				if (Contains(corners[i]) != ContainmentType.Contains)
				{
					return ContainmentType.Intersects;
				}
			}

			// If we get here, then we know all the points were actually contained, therefore result is Contains
			return ContainmentType.Contains;
		}

		public ContainmentType Contains(Point3D point)
		{
			// If a point is on the POSITIVE side of the plane, then the point is not contained within the frustum
			foreach (var plane in planes)
			{
				// Check the top
				Plane3D tempPlane = plane;
				float val = PlaneHelper.ClassifyPoint(ref point, ref tempPlane);
				if (val > 0)
				{
					return ContainmentType.Disjoint;
				}
			}

			// If we get here, it means that the point was on the correct side of each plane to be
			// contained. Therefore this point is contained
			return ContainmentType.Contains; 
		}

		public bool Intersects(AxisAlignedBox3D box)
		{
			return Contains(box) != ContainmentType.Disjoint;
		}

		public override int GetHashCode()
		{
			return this.matrix.GetHashCode();
		}

		public static bool operator ==(Frustum3D a, Frustum3D b)
		{
			return object.Equals(a, b);
		}

		public static bool operator !=(Frustum3D a, Frustum3D b)
		{
			return !object.Equals(a, b);
		}

		private static Point3D ComputeIntersection(ref Plane3D plane, ref Ray3D ray)
		{
			float num = (-plane.D - Vector3D.Dot(plane.Normal, (Vector3D) ray.Origin)) / Vector3D.Dot(plane.Normal, ray.Direction);
			return (ray.Origin + ray.Direction * num);
		}

		private static Ray3D ComputeIntersectionLine(ref Plane3D p1, ref Plane3D p2)
		{
			Ray3D ray = new Ray3D
			{
				Direction = (Vector3D) Normal3D.Cross(p1.Normal, p2.Normal)
			};
			float num = ray.Direction.LengthSquared();
			ray.Origin = (Point3D) (Vector3D.Cross((-p1.D * p2.Normal) + (p2.D * p1.Normal), ray.Direction) / num);
			return ray;
		}


		private void SetMatrix(ref Matrix3D value)
		{
			this.matrix = value;
			this.planes[2].Normal.X = -value.M14 - value.M11;
			this.planes[2].Normal.Y = -value.M24 - value.M21;
			this.planes[2].Normal.Z = -value.M34 - value.M31;
			this.planes[2].D = -value.M44 - value.M41;
			this.planes[3].Normal.X = -value.M14 + value.M11;
			this.planes[3].Normal.Y = -value.M24 + value.M21;
			this.planes[3].Normal.Z = -value.M34 + value.M31;
			this.planes[3].D = -value.M44 + value.M41;
			this.planes[4].Normal.X = -value.M14 + value.M12;
			this.planes[4].Normal.Y = -value.M24 + value.M22;
			this.planes[4].Normal.Z = -value.M34 + value.M32;
			this.planes[4].D = -value.M44 + value.M42;
			this.planes[5].Normal.X = -value.M14 - value.M12;
			this.planes[5].Normal.Y = -value.M24 - value.M22;
			this.planes[5].Normal.Z = -value.M34 - value.M32;
			this.planes[5].D = -value.M44 - value.M42;
			this.planes[0].Normal.X = -value.M13;
			this.planes[0].Normal.Y = -value.M23;
			this.planes[0].Normal.Z = -value.M33;
			this.planes[0].D = -value.M43;
			this.planes[1].Normal.X = -value.M14 + value.M13;
			this.planes[1].Normal.Y = -value.M24 + value.M23;
			this.planes[1].Normal.Z = -value.M34 + value.M33;
			this.planes[1].D = -value.M44 + value.M43;
			for (int i = 0; i < 6; i++)
			{
				float num2 = this.planes[i].Normal.Length();
				this.planes[i].Normal = (this.planes[i].Normal / num2);
				this.planes[i].D /= num2;
			}
			Ray3D ray = ComputeIntersectionLine(ref this.planes[0], ref this.planes[2]);
			this.cornerArray[0] = ComputeIntersection(ref this.planes[4], ref ray);
			this.cornerArray[3] = ComputeIntersection(ref this.planes[5], ref ray);
			ray = ComputeIntersectionLine(ref this.planes[3], ref this.planes[0]);
			this.cornerArray[1] = ComputeIntersection(ref this.planes[4], ref ray);
			this.cornerArray[2] = ComputeIntersection(ref this.planes[5], ref ray);
			ray = ComputeIntersectionLine(ref this.planes[2], ref this.planes[1]);
			this.cornerArray[4] = ComputeIntersection(ref this.planes[4], ref ray);
			this.cornerArray[7] = ComputeIntersection(ref this.planes[5], ref ray);
			ray = ComputeIntersectionLine(ref this.planes[1], ref this.planes[3]);
			this.cornerArray[5] = ComputeIntersection(ref this.planes[4], ref ray);
			this.cornerArray[6] = ComputeIntersection(ref this.planes[5], ref ray);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{{Near:{0} Far:{1} Left:{2} Right:{3} Top:{4} Bottom:{5}}}", new object[] { this.Near.ToString(), this.Far.ToString(), this.Left.ToString(), this.Right.ToString(), this.Top.ToString(), this.Bottom.ToString() });
		}

		// Properties
		public Plane3D Bottom
		{
			get
			{
				return this.planes[5];
			}
		}

		public Plane3D Far
		{
			get
			{
				return this.planes[1];
			}
		}

		public Plane3D Left
		{
			get
			{
				return this.planes[2];
			}
		}

		public Matrix3D Matrix
		{
			get
			{
				return this.matrix;
			}
			set
			{
				this.SetMatrix(ref value);
			}
		}

		public Plane3D Near
		{
			get
			{
				return this.planes[0];
			}
		}

		public Plane3D Right
		{
			get
			{
				return this.planes[3];
			}
		}

		public Plane3D Top
		{
			get
			{
				return this.planes[4];
			}
		}
	}
}