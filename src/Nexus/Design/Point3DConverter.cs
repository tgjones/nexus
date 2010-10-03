using System;
using System.ComponentModel;
using System.Globalization;
#if !SILVERLIGHT
using System.ComponentModel.Design.Serialization;
using System.Reflection;
#endif

namespace Nexus.Design
{
	public sealed class Point3DConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
#if SILVERLIGHT
				return base.ConvertFrom(context, culture, value);
#else
				throw base.GetConvertFromException(value);
#endif

			string source = value as string;
			if (source != null)
				return Point3D.Parse(source);

			return base.ConvertFrom(context, culture, value);
		}

#if !SILVERLIGHT

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
				return true;

			return ((destinationType == typeof(string)) || base.CanConvertTo(context, destinationType));
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if ((destinationType != null) && (value is Point3D))
			{
				Point3D point = (Point3D) value;
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo ci = typeof(Point3D).GetConstructor(new Type[] { typeof(float), typeof(float), typeof(float) });
					return new InstanceDescriptor(ci, new object[] { point.X, point.Y, point.Z });
				}
				else if (destinationType == typeof(string))
					return point.ConvertToString(null, culture);
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return new PropertyDescriptorCollection(new[]
				{
					new FieldPropertyDescriptor(typeof(Point3D).GetField("X")),
					new FieldPropertyDescriptor(typeof(Point3D).GetField("Y")),
					new FieldPropertyDescriptor(typeof(Point3D).GetField("Z"))
				});
		}

#endif
	}
}