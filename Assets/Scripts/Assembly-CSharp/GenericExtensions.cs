using System.Collections.Generic;
using System.Linq;

public static class GenericExtensions
{
	public static bool IsIn<T>(this T source, params T[] list) where T : class
	{
		return source != null && list != null && list.Contains(source);
	}

	public static bool IsIn<T>(this T source, params T?[] list) where T : struct
	{
		return list != null && list.Contains(source);
	}

	public static bool IsIn(this int source, params int[] list)
	{
		return list != null && list.Contains(source);
	}

	public static bool IsNotIn<T>(this T source, params T[] list) where T : class
	{
		return source != null && (list == null || !list.Contains(source));
	}

	public static bool IsNotIn<T>(this T source, params T?[] list) where T : struct
	{
		return list == null || !list.Contains(source);
	}

	public static bool IsNotIn(this int source, params int[] list)
	{
		return list == null || !list.Contains(source);
	}

	public static List<T> AsList<T>(this T tobject)
	{
		return new List<T> { tobject };
	}

	public static bool IsTNull<T>(this T tObj)
	{
		return EqualityComparer<T>.Default.Equals(tObj, default(T));
	}
}
