namespace Nexus.Graphics.Cameras
{
	/// <summary>
	/// Represents a perspective projection camera. 
	/// </summary>
	/// <remarks>
	/// PerspectiveCamera specifies a projection of a 3-D model to a 2-D visual surface. This projection includes perspective foreshortening. 
	/// In other words, the PerspectiveCamera describes a frustrum whose sides converge toward a point on the horizon. Objects closer to 
	/// the camera appear larger, and objects farther from the camera appear smaller.
	/// </remarks>
	public class PerspectiveCamera : ProjectionCamera
	{
		public PerspectiveCamera()
		{
			FieldOfView = MathUtility.PI_OVER_4;
		}

		/// <summary>
		/// Gets or sets a value that represents the camera's horizontal field of view in radians. 
		/// </summary>
		public float FieldOfView { get; set; }

		public override Matrix3D GetProjectionMatrix(float aspectRatio)
		{
			return Matrix3D.CreatePerspectiveFieldOfView(FieldOfView,
				aspectRatio, NearPlaneDistance, FarPlaneDistance);
		}
	}
}