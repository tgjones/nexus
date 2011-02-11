using System.Runtime.InteropServices;

namespace Nexus.Graphics
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Viewport
	{
		private int _x;
		private int _y;
		private int _width;
		private int _height;
		private float _minZ;
		private float _maxZ;

		public int X
		{
			get { return _x; }
			set { _x = value; }
		}

		public int Y
		{
			get { return _y; }
			set { _y = value; }
		}

		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}

		public int Height
		{
			get { return _height; }
			set { _height = value; }
		}

		public float MinDepth
		{
			get { return _minZ; }
			set { _minZ = value; }
		}

		public float MaxDepth
		{
			get { return _maxZ; }
			set { _maxZ = value; }
		}

		public float AspectRatio
		{
			get
			{
				if ((_height != 0 && _width != 0))
					return _width / (float)_height;
				return 0;
			}
		}

		public Viewport(int x, int y, int width, int height)
		{
			_x = x;
			_y = y;
			_width = width;
			_height = height;
			_minZ = 0f;
			_maxZ = 1f;
		}

		private static bool WithinEpsilon(float a, float b)
		{
			float num = a - b;
			return ((-1.401298E-45f <= num) && (num <= float.Epsilon));
		}

		public Point3D Project(Point3D source, Matrix3D projection, Matrix3D view, Matrix3D world)
		{
			Matrix3D matrix = world * view * projection;
			Point3D vector = Point3D.Transform(source, matrix);
			float a = (((source.X * matrix.M14) + (source.Y * matrix.M24)) + (source.Z * matrix.M34)) + matrix.M44;
			if (!WithinEpsilon(a, 1f))
				vector = vector / a;
			vector.X = (((vector.X + 1f) * 0.5f) * Width) + X;
			vector.Y = (((-vector.Y + 1f) * 0.5f) * Height) + Y;
			vector.Z = (vector.Z * (MaxDepth - MinDepth)) + MinDepth;
			return vector;
		}

		public Point3D Unproject(Point3D source, Matrix3D projection, Matrix3D view, Matrix3D world)
		{
			Matrix3D matrix = Matrix3D.Invert(world * view * projection);
			source.X = (((source.X - X) / Width) * 2f) - 1f;
			source.Y = -((((source.Y - Y) / Height) * 2f) - 1f);
			source.Z = (source.Z - MinDepth) / (MaxDepth - MinDepth);
			Point3D vector = Point3D.Transform(source, matrix);
			float a = (((source.X * matrix.M14) + (source.Y * matrix.M24)) + (source.Z * matrix.M34)) + matrix.M44;
			if (!WithinEpsilon(a, 1f))
				vector = vector / a;
			return vector;
		}
	}
}