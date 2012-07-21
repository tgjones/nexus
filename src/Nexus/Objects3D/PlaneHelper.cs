namespace Nexus.Objects3D
{
	internal class PlaneHelper
	{
		/// <summary>
		/// Returns a value indicating what side (positive/negative) of a plane a point is
		/// </summary>
		/// <param name="point">The point to check with</param>
		/// <param name="plane">The plane to check against</param>
		/// <returns>Greater than zero if on the positive side, less than zero if on the negative size, 0 otherwise</returns>
		public static float ClassifyPoint(ref Point3D point, ref Plane3D plane)
		{
			return point.X * plane.Normal.X + point.Y * plane.Normal.Y + point.Z * plane.Normal.Z + plane.D;
		}
	}
}