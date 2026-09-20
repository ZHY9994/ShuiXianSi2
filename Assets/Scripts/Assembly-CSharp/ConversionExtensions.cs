using System;

public static class ConversionExtensions
{
	public static float ToFloat(this int value)
	{
		return value;
	}

	public static decimal ToDecimal(this int value)
	{
		return value;
	}

	public static char ToChar(this int value)
	{
		return Convert.ToChar(value);
	}

	public static int ToInt(this string value, int defaultValue)
	{
		int result;
		return !string.IsNullOrEmpty(value) && int.TryParse(value, out result) ? result : defaultValue;
	}

	public static int? ToIntNull(this string value)
	{
		int result;
		return !string.IsNullOrEmpty(value) && int.TryParse(value, out result) ? (int?)result : null;
	}

	public static long ToLong(this string value, long defaultValue)
	{
		long result;
		return !string.IsNullOrEmpty(value) && long.TryParse(value, out result) ? result : defaultValue;
	}

	public static long? ToLongNull(this string value)
	{
		long result;
		return !string.IsNullOrEmpty(value) && long.TryParse(value, out result) ? (long?)result : null;
	}

	public static decimal ToDecimal(this string value, decimal defaultValue)
	{
		decimal result;
		return !string.IsNullOrEmpty(value) && decimal.TryParse(value, out result) ? result : defaultValue;
	}

	public static double ToDouble(this string value, double defaultValue)
	{
		double result;
		return !string.IsNullOrEmpty(value) && double.TryParse(value, out result) ? result : defaultValue;
	}

	public static decimal? ToDecimalNull(this string value)
	{
		decimal result;
		return !string.IsNullOrEmpty(value) && decimal.TryParse(value, out result) ? (decimal?)result : null;
	}

	public static float ToFloat(this string value, float defaultValue)
	{
		float result;
		return !string.IsNullOrEmpty(value) && float.TryParse(value, out result) ? result : defaultValue;
	}

	public static float? ToFloatNull(this string value)
	{
		float result;
		return !string.IsNullOrEmpty(value) && float.TryParse(value, out result) ? (float?)result : null;
	}

	public static bool ToBool(this string value, bool defaultValue)
	{
		bool result;
		return !string.IsNullOrEmpty(value) && bool.TryParse(value, out result) ? result : defaultValue;
	}

	public static bool? ToBoolNull(this string value)
	{
		bool result;
		return !string.IsNullOrEmpty(value) && bool.TryParse(value, out result) ? (bool?)result : null;
	}

	public static DateTime ToDateTime(this string value, DateTime defaultValue)
	{
		DateTime result;
		return !string.IsNullOrEmpty(value) && DateTime.TryParse(value, out result) ? result : defaultValue;
	}

	public static DateTime? ToDateTimeNull(this string value)
	{
		DateTime result;
		return !string.IsNullOrEmpty(value) && DateTime.TryParse(value, out result) ? (DateTime?)result : null;
	}

	public static Guid? ToGuid(this string gString)
	{
		return new Guid(gString);
	}

	public static double? ToDoubleNullable(this string value)
	{
		// [未翻译] IDA 未定位到该方法的 native 实体，保留现有骨架实现并记录证据缺口。
		double result;
		return !string.IsNullOrEmpty(value) && double.TryParse(value, out result) ? (double?)result : null;
	}

	public static decimal? ToDecimalNullable(this string value)
	{
		return ToDecimalNull(value);
	}

	public static int? ToIntNullable(this string value)
	{
		return ToIntNull(value);
	}

	public static int ToInt(this byte value)
	{
		return Convert.ToInt32(value);
	}

	public static long ToLong(this byte value)
	{
		return Convert.ToInt64(value);
	}

	public static double ToDouble(this byte value)
	{
		return Convert.ToDouble(value);
	}

	public static string ToBase64(this byte[] data)
	{
		return Convert.ToBase64String(data);
	}

	public static byte[] FromBase64(this string data)
	{
		return Convert.FromBase64String(data);
	}

	public static T To<T>(this IConvertible obj)
	{
		return (T)Convert.ChangeType(obj, typeof(T));
	}

	public static T ToOrDefault<T>(this IConvertible obj)
	{
		T value;
		return obj.ToOrDefault(out value) ? value : default(T);
	}

	public static bool ToOrDefault<T>(this IConvertible obj, out T newObj)
	{
		// [未翻译] IDA 未定位到该泛型 out 重载的 native 实体，保留现有骨架实现并记录证据缺口。
		try
		{
			newObj = obj.To<T>();
			return true;
		}
		catch
		{
			newObj = default(T);
			return false;
		}
	}

	public static T ToOrOther<T>(this IConvertible obj, T other)
	{
		T value;
		return obj.ToOrDefault(out value) ? value : other;
	}

	public static bool ToOrOther<T>(this IConvertible obj, out T newObj, T other)
	{
		// [未翻译] IDA 未定位到该泛型 out 重载的 native 实体，保留现有骨架实现并记录证据缺口。
		if (obj.ToOrDefault(out newObj))
		{
			return true;
		}
		newObj = other;
		return false;
	}

	public static T ToOrNull<T>(this IConvertible obj) where T : class
	{
		T value;
		return obj.ToOrNull(out value) ? value : null;
	}

	public static bool ToOrNull<T>(this IConvertible obj, out T newObj) where T : class
	{
		// [未翻译] IDA 未定位到该泛型 out 重载的 native 实体，保留现有骨架实现并记录证据缺口。
		try
		{
			newObj = obj.To<T>();
			return true;
		}
		catch
		{
			newObj = null;
			return false;
		}
	}
}
