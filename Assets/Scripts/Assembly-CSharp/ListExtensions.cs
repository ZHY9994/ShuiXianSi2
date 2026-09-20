using System;
using System.Collections.Generic;

public static class ListExtensions
{
	public static void ForEachIfNotNull<T>(this IEnumerable<T> i_Array, Action<T> i_Action)
	{
		if (i_Array == null || i_Action == null) return;
		foreach (T item in i_Array) if (item != null) i_Action(item);
	}
	public static bool IsNullOrEmpty<T>(this T[] data)
	{
		return data == null || data.Length == 0;
	}
	public static bool IsNullOrEmpty<T>(this List<T> data)
	{
		return data == null || data.Count == 0;
	}

	public static bool IsNullOrEmpty<T1, T2>(this Dictionary<T1, T2> data)
	{
		return data == null || data.Count == 0;
	}

	public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> Predicate)
	{
		var seen = new HashSet<int>();
		foreach (T item in list) if (seen.Add(Predicate(item))) yield return item;
	}

	public static void RemoveNulls<T>(this IList<T> list)
	{
		for (int i = list.Count - 1; i >= 0; i--) if (list[i] == null) list.RemoveAt(i);
	}
	public static T DequeueOrNull<T>(this Queue<T> q)
	{
		if (q == null || q.Count == 0) return default(T);
		return q.Dequeue();
	}
}
