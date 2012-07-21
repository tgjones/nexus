namespace Nexus.Objects3D
{
	public struct Ray3D
	{
		public Point3D Origin;
		public Vector3D Direction;

		public Ray3D(Point3D origin, Vector3D direction)
		{
			Origin = origin;
			Direction = direction;
		}

		public Point3D Evaluate(float t)
		{
			return Origin + Direction * t;
		}

		public override string ToString()
		{
			return string.Format("{{Origin:{0} Direction:{1}}}", Origin, Direction);
		}

		public float? Intersects(AxisAlignedBox3D box)
		{
			//first test if start in box
			if (Origin.X >= box.Min.X
				&& Origin.X <= box.Max.X
				&& Origin.Y >= box.Min.Y
				&& Origin.Y <= box.Max.Y
				&& Origin.Z >= box.Min.Z
				&& Origin.Z <= box.Max.Z)
				return 0.0f;// here we concidere cube is full and origine is in cube so intersect at origine

			//Second we check each face
			Vector3D maxT = new Vector3D(-1.0f);
			//Vector3 minT = new Vector3(-1.0f);
			//calcul intersection with each faces
			if (Origin.X < box.Min.X && Direction.X != 0.0f)
				maxT.X = (box.Min.X - Origin.X) / Direction.X;
			else if (Origin.X > box.Max.X && Direction.X != 0.0f)
				maxT.X = (box.Max.X - Origin.X) / Direction.X;
			if (Origin.Y < box.Min.Y && Direction.Y != 0.0f)
				maxT.Y = (box.Min.Y - Origin.Y) / Direction.Y;
			else if (Origin.Y > box.Max.Y && Direction.Y != 0.0f)
				maxT.Y = (box.Max.Y - Origin.Y) / Direction.Y;
			if (Origin.Z < box.Min.Z && Direction.Z != 0.0f)
				maxT.Z = (box.Min.Z - Origin.Z) / Direction.Z;
			else if (Origin.Z > box.Max.Z && Direction.Z != 0.0f)
				maxT.Z = (box.Max.Z - Origin.Z) / Direction.Z;

			//get the maximum maxT
			if (maxT.X > maxT.Y && maxT.X > maxT.Z)
			{
				if (maxT.X < 0.0f)
					return null;// ray go on opposite of face
				//coordonate of hit point of face of cube
				float coord = Origin.Z + maxT.X * Direction.Z;
				// if hit point coord ( intersect face with ray) is out of other plane coord it miss 
				if (coord < box.Min.Z || coord > box.Max.Z)
					return null;
				coord = Origin.Y + maxT.X * Direction.Y;
				if (coord < box.Min.Y || coord > box.Max.Y)
					return null;
				return maxT.X;
			}
			if (maxT.Y > maxT.X && maxT.Y > maxT.Z)
			{
				if (maxT.Y < 0.0f)
					return null;// ray go on opposite of face
				//coordonate of hit point of face of cube
				float coord = Origin.Z + maxT.Y * Direction.Z;
				// if hit point coord ( intersect face with ray) is out of other plane coord it miss 
				if (coord < box.Min.Z || coord > box.Max.Z)
					return null;
				coord = Origin.X + maxT.Y * Direction.X;
				if (coord < box.Min.X || coord > box.Max.X)
					return null;
				return maxT.Y;
			}
			else //Z
			{
				if (maxT.Z < 0.0f)
					return null;// ray go on opposite of face
				//coordonate of hit point of face of cube
				float coord = Origin.X + maxT.Z * Direction.X;
				// if hit point coord ( intersect face with ray) is out of other plane coord it miss 
				if (coord < box.Min.X || coord > box.Max.X)
					return null;
				coord = Origin.Y + maxT.Z * Direction.Y;
				if (coord < box.Min.Y || coord > box.Max.Y)
					return null;
				return maxT.Z;
			}
		}
	}
}