using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using Nexus.Design;

namespace Nexus
{
	[TypeConverter(typeof(Vector3DCollectionConverter))]
	public class Vector3DCollection : List<Vector3D>
	{
		public Vector3DCollection()
		{

		}

		public Vector3DCollection(Vector3D[] collection)
			: base(collection)
		{

		}

		internal string ConvertToString(string format, IFormatProvider provider)
		{
			if (this.Count == 0)
				return string.Empty;
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < this.Count; i++)
			{
				builder.AppendFormat(provider, "{0:" + format + "}", new object[] { this[i] });
				if (i != (this.Count - 1))
					builder.Append(" ");
			}
			return builder.ToString();
		}

		public static Vector3DCollection Parse(string source)
		{
			IFormatProvider cultureInfo = CultureInfo.InvariantCulture;
			TokenizerHelper helper = new TokenizerHelper(source, cultureInfo);
			Vector3DCollection vectords = new Vector3DCollection();
			while (helper.NextToken())
			{
				Vector3D vectord = new Vector3D(Convert.ToSingle(helper.GetCurrentToken(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo));
				vectords.Add(vectord);
			}
			return vectords;
		}
	}
}