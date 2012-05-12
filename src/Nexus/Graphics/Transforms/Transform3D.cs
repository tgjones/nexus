using Nexus.Objects3D;

namespace Nexus.Graphics.Transforms
{
	/// <summary>
	/// Provides a parent class for all three-dimensional transformations, 
	/// including translation, rotation, and scale transformations. 
	/// </summary>
	public abstract class Transform3D
	{
		/// <summary>
		/// Gets the Matrix3D that represents the value of the current transformation.
		/// </summary>
		public abstract Matrix3D Value
		{
			get;
		}

		/// <summary>
		/// Gets the Matrix3D that represents the inverse of the current transformation.
		/// </summary>
		public virtual Transform3D Inverse
		{
			get
			{
				Matrix3D matrix = Value;
				if (!matrix.HasInverse)
					return null;
				matrix.Invert();
				return new MatrixTransform(matrix);
			}
		}

		public bool HasScale
		{
			get { return Value.HasScale; }
		}

		public bool SwapsHandedness
		{
			get { return Value.SwapsHandedness; }
		}

		public void Transform(Point4D[] points)
		{
			Value.Transform(points);
		}

		public void Transform(Vector3D[] vectors)
		{
			Value.Transform(vectors);
		}

		public Point3D Transform(Point3D point)
		{
			return Value.Transform(point);
		}

		public Point4D Transform(Point4D point)
		{
			return Value.Transform(point);
		}

		public Vector3D Transform(Vector3D vector)
		{
			return Value.Transform(vector);
		}

		public Normal3D Transform(Normal3D normal)
		{
			float x = normal.X;
			float y = normal.Y;
			float z = normal.Z;

			Matrix3D inverse = Inverse.Value;

			return new Normal3D(
				inverse.M11 * x + inverse.M21 * y + inverse.M31 * z,
				inverse.M12 * x + inverse.M22 * y + inverse.M32 * z,
				inverse.M13 * x + inverse.M23 * y + inverse.M33 * z);
		}

		public Ray3D Transform(Ray3D ray)
		{
			Ray3D ret;
			ret.Origin = Transform(ray.Origin);
			ret.Direction = Transform(ray.Direction);
			return ret;
		}

		public void Transform(Point3D[] points)
		{
			Value.Transform(points);
		}

		public AxisAlignedBox3D Transform(AxisAlignedBox3D box)
		{
			return box.Transform(Value);
		}
	}
}