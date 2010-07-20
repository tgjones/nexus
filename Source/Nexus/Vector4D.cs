using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using Nexus.Design;

namespace Nexus
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vector4D
	{
		public float X, Y, Z, W;

		#region Constructors

		public Vector4D(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		#endregion

		#region Indexer

		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return X;
					case 1:
						return Y;
					case 2:
						return Z;
					case 3:
						return W;
					default:
						throw new ArgumentOutOfRangeException("index");
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						X = value;
						break;
					case 1:
						Y = value;
						break;
					case 2:
						Z = value;
						break;
					case 3:
						W = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("index");
				}
			}
		}

		#endregion

		#region Static properties

		public static int SizeInBytes
		{
			get { return sizeof(float) * 4; }
		}

		#endregion

		#region Operators

		public static Vector4D operator +(Vector4D value1, Vector4D value2)
		{
			Vector4D vector;
			vector.X = value1.X + value2.X;
			vector.Y = value1.Y + value2.Y;
			vector.Z = value1.Z + value2.Z;
			vector.W = value1.W + value2.W;
			return vector;
		}

		public static Vector4D operator -(Vector4D value)
		{
			return new Vector4D(-value.X, -value.Y, -value.Z, -value.W);
		}

		public static Vector4D operator -(Vector4D value1, Vector4D value2)
		{
			Vector4D vector;
			vector.X = value1.X - value2.X;
			vector.Y = value1.Y - value2.Y;
			vector.Z = value1.Z - value2.Z;
			vector.W = value1.W - value2.W;
			return vector;
		}

		public static Vector4D operator *(Vector4D value, float scaleFactor)
		{
			Vector4D vector;
			vector.X = value.X * scaleFactor;
			vector.Y = value.Y * scaleFactor;
			vector.Z = value.Z * scaleFactor;
			vector.W = value.W * scaleFactor;
			return vector;
		}

		public static explicit operator Point4D(Vector4D vector)
		{
			return new Point4D(vector.X, vector.Y, vector.Z, vector.W);
		}

		#endregion
	}
}