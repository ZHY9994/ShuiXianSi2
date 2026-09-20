using System;
using System.Collections.Generic;
using System.Linq;

public static class LINQEnumerableExtensions
{
	public static IEnumerable<T> Generate<T>(Func<T> generator) where T : class
	{
		while (true)
			yield return generator();
	}

	public static IEnumerable<T> Generate<T>(Func<T?> generator) where T : struct
	{
		T? value;
		while ((value = generator()).HasValue)
			yield return value.Value;
	}

	public static IEnumerable<T> FromEnumerator<T>(IEnumerator<T> enumerator)
	{
		while (enumerator.MoveNext())
			yield return enumerator.Current;
	}

	public static IEnumerable<T> Single<T>(T value)
	{
		yield return value;
	}

	public static IEnumerable<T> Single<T>(T value, int count)
	{
		for (int i = 0; i < count; i++)
			yield return value;
	}

	public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (T item in source)
		{
			action(item);
			yield return item;
		}
	}

	public static IEnumerable<TOut> Combine<TIn1, TIn2, TOut>(this IEnumerable<TIn1> in1, IEnumerable<TIn2> in2, Func<TIn1, TIn2, TOut> func)
	{
		using (IEnumerator<TIn1> e1 = in1.GetEnumerator())
		using (IEnumerator<TIn2> e2 = in2.GetEnumerator())
		{
			while (e1.MoveNext() && e2.MoveNext())
				yield return func(e1.Current, e2.Current);
		}
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
	{
		return source.OrderBy(_ => Guid.NewGuid());
	}

	public static IEnumerable<T> UseLINQDisposible<T>(this T obj) where T : IDisposable
	{
		try
		{
			yield return obj;
		}
		finally
		{
			obj.Dispose();
		}
	}

	public static IEnumerable<T> DoWhile<T>(this IEnumerable<T> source, Action<T> action, Func<bool> compareFunc)
	{
		do
		{
			foreach (T item in source)
			{
				action(item);
				yield return item;
			}
		}
		while (compareFunc());
	}

	public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> @this, Func<T, TKey> keySelector)
	{
		return @this.GroupBy(keySelector).Select(group => group.First());
	}

	public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> collection, Predicate<T> endCondition)
	{
		foreach (T item in collection)
		{
			if (endCondition(item))
				yield break;
			yield return item;
		}
	}
}
