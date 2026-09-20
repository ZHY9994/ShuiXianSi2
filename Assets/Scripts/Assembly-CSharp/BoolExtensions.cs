public static class BoolExtensions
{
	public static bool IsTrue(this bool item)
	{
		return item;
	}

	public static bool IsFalse(this bool item)
	{
		return !item;
	}

	public static bool IsNotTrue(this bool item)
	{
		return !item;
	}

	public static bool IsNotFalse(this bool item)
	{
		return item;
	}

	public static bool Toggle(this bool item)
	{
		return !item;
	}

	public static void ToggleByRef(ref bool Item)
	{
		Item = !Item;
	}

	public static int ToInt(this bool item)
	{
		return item ? 1 : 0;
	}

	public static string ToLowerString(this bool item)
	{
		return item.ToString().ToLower();
	}

	public static string ToYesNo(this bool item)
	{
		return ToString(item, "Yes", "No");
	}

	public static string ToString(this bool item, string trueString, string falseString)
	{
		return item ? trueString : falseString;
	}

	public static T ToType<T>(this bool item, T trueValue, T falseValue)
	{
		return item ? trueValue : falseValue;
	}
}
