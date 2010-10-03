namespace Nexus.Graphics.Cameras
{
	/// <summary>
	/// Represents an imaginary viewing position and direction in 3-D coordinate space 
	/// that describes how a 3-D model is projected onto a 2-D visual.
	/// </summary>
	public abstract class Camera
	{
		public abstract Matrix3D GetProjectionMatrix(float aspectRatio);
		public abstract Matrix3D GetViewMatrix();
	}
}