using System;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Nexus.Design
{
	public sealed class Point3DCollectionConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
				throw GetConvertFromException(value);

			string source = value as string;
			if (source != null)
				return Point3DCollection.Parse(source);
			return base.ConvertFrom(context, culture, value);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
				return true;

			return ((destinationType == typeof(string)) || base.CanConvertTo(context, destinationType));
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if ((destinationType != null) && (value is Point3DCollection))
			{
				Point3DCollection points = (Point3DCollection) value;
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo ci = typeof(Point3DCollection).GetConstructor(new Type[] { typeof(Point3D[]) });
					return new InstanceDescriptor(ci, new object[] { points.ToArray() });
				}
				else if (destinationType == typeof(string))
					return points.ConvertToString(null, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}