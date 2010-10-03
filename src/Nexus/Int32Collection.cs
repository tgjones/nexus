using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.ComponentModel;
using Nexus.Design;

namespace Nexus
{
	[TypeConverter(typeof(Int32CollectionConverter))]
	public class Int32Collection : List<int>
	{
		public Int32Collection()
		{

		}

		public Int32Collection(int[] collection)
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

		public static Int32Collection Parse(string source)
		{
			IFormatProvider cultureInfo = CultureInfo.InvariantCulture;
			TokenizerHelper helper = new TokenizerHelper(source, cultureInfo);
			Int32Collection ints = new Int32Collection();
			while (helper.NextToken())
			{
				int num = Convert.ToInt32(helper.GetCurrentToken(), cultureInfo);
				ints.Add(num);
			}
			return ints;
		}
	}
}