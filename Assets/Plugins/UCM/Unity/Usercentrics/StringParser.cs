namespace Unity.Usercentrics
{
	using System;
	using System.Globalization;
	internal class StringParser
	{
		public static bool? ToBool(string input)
		{
			bool value;
			return bool.TryParse(input, out value) ? (bool?)value : null;
		}

		public static int? ToInt(string input)
		{
			int value;
			return int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) ? (int?)value : null;
		}

		public static double? ToDouble(string input)
		{
			double value;
			return double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value) ? (double?)value : null;
		}

		public static long? ToLong(string input)
		{
			long value;
			return long.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) ? (long?)value : null;
		}
	}
}
