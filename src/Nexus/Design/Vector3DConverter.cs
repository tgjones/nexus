using System;
using System.ComponentModel;
using System.Globalization;
#if !SILVERLIGHT
using System.ComponentModel.Design.Serialization;
using System.Reflection;
#endif

namespace Nexus.Design
{
	public sealed class Vector3DConverter : TypeConverter
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
				return Vector3D.Parse(source);
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
			if ((destinationType != null) && (value is Vector3D))
			{
				Vector3D vector = (Vector3D) value;
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo ci = typeof(Vector3D).GetConstructor(new Type[] { typeof(float), typeof(float), typeof(float) });
					return new InstanceDescriptor(ci, new object[] { vector.X, vector.Y, vector.Z });
				}
				else if (destinationType == typeof(string))
				{
					return vector.ConvertToString(null, culture);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

#endif
	}
}