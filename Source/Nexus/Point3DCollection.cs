using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Nexus.Design;

namespace Nexus
{
	[TypeConverter(typeof(Point3DCollectionConverter))]
	public class Point3DCollection : List<Point3D>
	{
		public Point3DCollection()
		{

		}

		public Point3DCollection(IEnumerable<Point3D> collection)
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
				{
					builder.Append(" ");
				}
			}
			return builder.ToString();
		}

		public static Point3DCollection Parse(string source)
		{
			IFormatProvider cultureInfo = CultureInfo.InvariantCulture;
			TokenizerHelper helper = new TokenizerHelper(source, cultureInfo);
			Point3DCollection pointds = new Point3DCollection();
			while (helper.NextToken())
			{
				Point3D pointd = new Point3D(Convert.ToSingle(helper.GetCurrentToken(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo));
				pointds.Add(pointd);
			}
			return pointds;
		}
	}
}