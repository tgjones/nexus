using System;
using System.ComponentModel;
using System.Globalization;
#if !SILVERLIGHT
using System.ComponentModel.Design.Serialization;
using System.Reflection;
#endif

namespace Nexus.Design
{
	public sealed class Vector3DCollectionConverter : TypeConverter
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
				return Vector3DCollection.Parse(source);
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
			if ((destinationType != null) && (value is Vector3DCollection))
			{
				Vector3DCollection vectords = (Vector3DCollection) value;
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo ci = typeof(Vector3DCollection).GetConstructor(new Type[] { typeof(Vector3D[]) });
					return new InstanceDescriptor(ci, new object[] { vectords.ToArray() });
				}
				else if (destinationType == typeof(string))
					return vectords.ConvertToString(null, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

#endif
	}
}