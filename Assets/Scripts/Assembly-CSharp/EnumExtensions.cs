using System;

public static class EnumExtensions
{
	public static T ToEnum<T>(this string s, bool ignoreCase) where T : struct
	{
		if (!s.IsNullOrEmpty() && typeof(T).IsEnum)
		{
			try
			{
				return (T)Enum.Parse(typeof(T), s, ignoreCase);
			}
			catch (ArgumentException)
			{
				foreach (T value in Enum.GetValues(typeof(T)))
				{
					if (string.Compare(value.ToString(), s, ignoreCase) == 0)
						return value;
				}
			}
		}
		return default;
	}

	public static T ToEnum<T>(this string s) where T : struct
	{
		return s.ToEnum<T>(false);
	}
}
