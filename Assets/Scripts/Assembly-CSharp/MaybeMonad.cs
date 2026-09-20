using System;
using System.Collections.Generic;

public static class MaybeMonad
{
	public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator) where TInput : class where TResult : class
	{
		return o == null ? null : evaluator(o);
	}

	public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue) where TInput : class
	{
		return o == null ? failureValue : evaluator(o);
	}

	public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, Func<TInput, TResult> failureValue) where TInput : class
	{
		if (o == null)
		{
			return failureValue == null ? evaluator(o) : failureValue(o);
		}
		return evaluator(o);
	}

	public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
	{
		return o != null && evaluator(o) ? o : null;
	}

	public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
	{
		return o != null && !evaluator(o) ? o : null;
	}

	public static TInput Do<TInput>(this TInput o, Action<TInput> action) where TInput : class
	{
		if (o != null)
		{
			action(o);
		}
		return o;
	}

	public static TResult IfNotNull<T, TResult>(this T target, Func<T, TResult> getValue)
	{
		return EqualityComparer<T>.Default.Equals(target, default(T)) ? default(TResult) : getValue(target);
	}
}
