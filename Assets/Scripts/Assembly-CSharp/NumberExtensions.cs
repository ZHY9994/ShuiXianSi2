using System;
using System.Text.RegularExpressions;

public static class NumberExtensions
{
	public static decimal Rounded(this decimal value, int decimals)
	{
		return decimal.Round(value, decimals);
	}

	public static bool IsNumeric(this string input)
	{
		return !string.IsNullOrEmpty(input) && new Regex("^-?[0-9]*\\.?[0-9]+$").IsMatch(input.Trim());
	}

	public static bool HasNumeric(this string input)
	{
		return !string.IsNullOrEmpty(input) && new Regex("[0-9]+").IsMatch(input);
	}

	public static int Negate(this int number)
	{
		return -number;
	}

	public static int AbsoluteValue(this int number)
	{
		return number >= 0 ? number : -number;
	}

	public static long Negate(this long number)
	{
		return -number;
	}

	public static long AbsoluteValue(this long number)
	{
		return number >= 0 ? number : -number;
	}
}
