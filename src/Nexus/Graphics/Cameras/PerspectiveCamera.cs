using System;

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

		public static PerspectiveCamera CreateFromBounds(AxisAlignedBoundingBox bounds, Viewport viewport,
			float fieldOfView, float yaw, float pitch, float zoom)
		{
			// Calculate initial guess at camera settings.
			Matrix3D transform = Matrix3D.CreateFromYawPitchRoll(yaw, pitch, 0);
			Vector3D cameraDirection = Vector3D.Normalize(transform.Transform(Vector3D.Forward));
			PerspectiveCamera initialGuess = new PerspectiveCamera
			{
				FieldOfView = fieldOfView,
				NearPlaneDistance = 1.0f,
				FarPlaneDistance = bounds.Size.Length() * 10,
				Position = bounds.Center - cameraDirection * bounds.Size.Length() * 3,
				LookDirection = cameraDirection,
				UpDirection = Vector3D.Up
			};

			Matrix3D projection = initialGuess.GetProjectionMatrix(viewport.AspectRatio);
			Matrix3D view = initialGuess.GetViewMatrix();

			// Project bounding box corners onto screen, and calculate screen bounds.
			float closestZ = float.MaxValue;
			Box2D? screenBounds = null;
			Point3D[] corners = bounds.GetCorners();
			foreach (Point3D corner in corners)
			{
				Point3D screenPoint = viewport.Project(corner,
					projection, view, Matrix3D.Identity);

				if (screenPoint.Z < closestZ)
					closestZ = screenPoint.Z;

				IntPoint2D intScreenPoint = new IntPoint2D((int) screenPoint.X, (int) screenPoint.Y);
				if (screenBounds == null)
					screenBounds = new Box2D(intScreenPoint, intScreenPoint);
				else
				{
					Box2D value = screenBounds.Value;
					value.Expand(intScreenPoint);
					screenBounds = value;
				}
			}

			// Now project back from screen bounds into scene, setting Z to the minimum bounding box Z value.
			Point3D min = viewport.Unproject(new Point3D(screenBounds.Value.Min.X, screenBounds.Value.Min.Y, closestZ),
				projection, view, Matrix3D.Identity);
			Point3D max = viewport.Unproject(new Point3D(screenBounds.Value.Max.X, screenBounds.Value.Max.Y, closestZ),
				projection, view, Matrix3D.Identity);

			// Use these new values to calculate the distance the camera should be from the AABB centre.
			Vector3D size = Vector3D.Abs(max - min);
			float largestDimensionSize = Math.Max(size.X, Math.Max(size.Y, size.Z));
			float dist = largestDimensionSize / (2 * MathUtility.Tan(fieldOfView / 2));
			Point3D closestBoundsCenter = (Point3D) min + ((max - min) / 2);
			Point3D position = closestBoundsCenter
				- cameraDirection * dist * (1 / zoom);

			return new PerspectiveCamera
			{
				FieldOfView = fieldOfView,
				NearPlaneDistance = 1.0f,
				FarPlaneDistance = dist * 10,
				Position = position,
				LookDirection = cameraDirection,
				UpDirection = Vector3D.Up
			};
		}
	}
}