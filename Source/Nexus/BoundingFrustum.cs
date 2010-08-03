using System;
using System.Globalization;

namespace Nexus
{
	public class BoundingFrustum : IEquatable<BoundingFrustum>
	{
		// Fields
		private const int BottomPlaneIndex = 5;
		internal Point3D[] cornerArray;
		public const int CornerCount = 8;
		private const int FarPlaneIndex = 1;
		private const int LeftPlaneIndex = 2;
		private Matrix3D matrix;
		private const int NearPlaneIndex = 0;
		private const int NumPlanes = 6;
		private Plane[] planes;
		private const int RightPlaneIndex = 3;
		private const int TopPlaneIndex = 4;

		// Methods
		private BoundingFrustum()
		{
			this.planes = new Plane[6];
			this.cornerArray = new Point3D[8];
		}

		public BoundingFrustum(Matrix3D value)
		{
			this.planes = new Plane[6];
			this.cornerArray = new Point3D[8];
			this.SetMatrix(ref value);
		}

		public bool Equals(BoundingFrustum other)
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
			BoundingFrustum frustum = obj as BoundingFrustum;
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

		public override int GetHashCode()
		{
			return this.matrix.GetHashCode();
		}

		public static bool operator ==(BoundingFrustum a, BoundingFrustum b)
		{
			return object.Equals(a, b);
		}

		public static bool operator !=(BoundingFrustum a, BoundingFrustum b)
		{
			return !object.Equals(a, b);
		}

		private static Point3D ComputeIntersection(ref Plane plane, ref Ray3D ray)
		{
			float num = (-plane.D - Vector3D.Dot(plane.Normal, (Vector3D) ray.Origin)) / Vector3D.Dot(plane.Normal, ray.Direction);
			return (ray.Origin + ray.Direction * num);
		}

		private static Ray3D ComputeIntersectionLine(ref Plane p1, ref Plane p2)
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
		public Plane Bottom
		{
			get
			{
				return this.planes[5];
			}
		}

		public Plane Far
		{
			get
			{
				return this.planes[1];
			}
		}

		public Plane Left
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

		public Plane Near
		{
			get
			{
				return this.planes[0];
			}
		}

		public Plane Right
		{
			get
			{
				return this.planes[3];
			}
		}

		public Plane Top
		{
			get
			{
				return this.planes[4];
			}
		}
	}
}