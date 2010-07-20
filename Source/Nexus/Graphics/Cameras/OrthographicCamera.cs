namespace Nexus.Graphics.Cameras
{
	/// <summary>
	/// Represents an orthographic projection camera. 
	/// </summary>
	/// <remarks>
	/// This class specifies an orthogonal projection of a 3-D model to a 2-D visual surface. Like PerspectiveCamera, it specifies a position, 
	/// viewing direction, and "upward" direction. Unlike PerspectiveCamera, however, OrthographicCamera describes a projection that does 
	/// not include perspective foreshortening. In other words, OrthographicCamera describes a viewing box whose sides are parallel, 
	/// instead of one whose sides meet in a point at the scene's horizon. 
	/// </remarks>
	public class OrthographicCamera : ProjectionCamera
	{
		public OrthographicCamera()
		{
			Width = 2.0f;
		}

		/// <summary>
		/// Gets or sets the width of the camera's viewing box, in world units.
		/// </summary>
		/// <remarks>
		/// Because the OrthographicCamera describes a projection that does not include perspective foreshortening, its viewing box 
		/// has parallel sides. The width of the viewing box can therefore be specified with a single value.
		/// </remarks>
		public float Width { get; set; }

		public override Matrix3D GetProjectionMatrix(float aspectRatio)
		{
			return Matrix3D.CreateOrthographic(Width, Width / aspectRatio,
				NearPlaneDistance, FarPlaneDistance);
		}
	}
}