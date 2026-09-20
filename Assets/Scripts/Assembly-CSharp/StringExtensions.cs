using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

public static class StringExtensions
{
	public static bool IsDate(this string value)
	{
		return DateTime.TryParse(value, out _);
	}

	public static bool IsInt(this string value)
	{
		return int.TryParse(value, out _);
	}

	public static string Take(this string value, int count, bool ellipsis = false)
	{
		if (value == null)
			return null;
		int length = Math.Min(count, value.Length);
		return value.Substring(0, length) + (ellipsis && length < value.Length ? "..." : string.Empty);
	}

	public static string Skip(this string value, int count)
	{
		if (value == null)
			throw new ArgumentNullException(nameof(value));
		return value.Substring(Math.Min(count, value.Length) - 1);
	}

	public static string Reverse(this string input)
	{
		if (input == null)
			throw new ArgumentNullException(nameof(input));
		char[] chars = input.ToCharArray();
		Array.Reverse(chars);
		return new string(chars);
	}

	public static bool IsNullOrEmpty(this string value)
	{
		return string.IsNullOrEmpty(value);
	}

	public static bool IsNOTNullOrEmpty(this string value)
	{
		return !string.IsNullOrEmpty(value);
	}

	public static string Formatted(this string format, params object[] args)
	{
		return string.Format(format, args);
	}

	public static string StripHtml(this string html)
	{
		return string.IsNullOrEmpty(html) ? string.Empty : Regex.Replace(html, "<[^>]*>", string.Empty);
	}

	public static bool Match(this string value, string pattern)
	{
		return Regex.IsMatch(value, pattern);
	}

	public static string RemoveSpaces(this string value)
	{
		return value.Replace(" ", string.Empty);
	}

	public static string ReplaceRNWithBr(this string value)
	{
		return value.Replace("\r\n", "<br />").Replace("\n", "<br />");
	}

	public static string ToEmptyString(string value)
	{
		return string.IsNullOrEmpty(value) ? string.Empty : value;
	}

	public static string ToStringPretty<T>(this IEnumerable<T> source)
	{
		return source.ToStringPretty("", ", ", "");
	}

	public static string ToStringPretty<T>(this IEnumerable<T> source, string delimiter)
	{
		return source.ToStringPretty("", delimiter, "");
	}

	public static string ToStringPretty<T>(this IEnumerable<T> source, string before, string delimiter, string after)
	{
		return before + string.Join(delimiter, source.Select(x => x?.ToString())) + after;
	}

	public static string InvertCase(this string s)
	{
		return new string(s.Select(c => !char.IsLetter(c) ? c : char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c)).ToArray());
	}

	public static bool IsNullOrEmptyAfterTrimmed(this string s)
	{
		return string.IsNullOrEmpty(s) || string.IsNullOrEmpty(s.Trim());
	}

	public static List<char> ToCharList(this string s)
	{
		return string.IsNullOrEmpty(s) ? null : s.ToCharArray().ToList();
	}

	public static string SubstringFromXToY(this string s, int start, int end)
	{
		if (string.IsNullOrEmpty(s) || s.Length <= start)
			return string.Empty;
		int safeEnd = s.Length <= end ? s.Length - 1 : end;
		return s.Substring(start, safeEnd - start);
	}

	public static string RemoveChar(this string s, char c)
	{
		return string.IsNullOrEmpty(s) ? string.Empty : s.Replace(c.ToString(), string.Empty);
	}

	public static int GetWordCount(this string s)
	{
		return Regex.Matches(s, "\\w+").Count;
	}

	public static bool IsPalindrome(this string s)
	{
		return s == s.Reverse();
	}

	public static bool IsNotPalindrome(this string s)
	{
		return !s.IsPalindrome();
	}

	public static bool IsValidEmail(this string email)
	{
		return !string.IsNullOrEmpty(email) && email.Length <= 100 && new EmailValidator().IsValidEmail(email);
	}

	public static bool IsValidIPAddress(this string s)
	{
		return Regex.IsMatch(s, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
	}

	public static string AppendSep(this string s, string sep)
	{
		return s + sep;
	}

	public static string AppendComma(this string s)
	{
		return s + ",";
	}

	public static string AppendNewLine(this string s)
	{
		return s + "\r\n";
	}

	public static string AppendHtmlBr(this string s)
	{
		return s + "<br />";
	}

	public static string AppendSpace(this string s)
	{
		return s + " ";
	}

	public static string AppendHyphen(this string s)
	{
		return s + "-";
	}

	public static string AppendSep(this string s, char sep)
	{
		return s + sep;
	}

	public static string AppendWithSep(this string s, string newString, string sep)
	{
		return s + sep + newString;
	}

	public static string AppendWithSep(this string s, string newString, char sep)
	{
		return s + sep + newString;
	}

	public static string AppendWithComma(this string s, string newString)
	{
		return s + "," + newString;
	}

	public static string AppendWithNewLine(this string s, string newString)
	{
		return s + "\r\n" + newString;
	}

	public static string AppendWithHtmlBr(this string s, string newString)
	{
		return s + "<br />" + newString;
	}

	public static string AppendWithHyphen(this string s, string newString)
	{
		return s + "-" + newString;
	}

	public static string AppendWithSpace(this string s, string newString)
	{
		return s + " " + newString;
	}

	public static string CreateRandomPassword(this string sVal, int PasswordLength)
	{
		return CreateRandomPassword(sVal, PasswordLength, true, true, true, true, true, true, false);
	}

	public static string CreateRandomPassword(this string sVal, int PasswordLength, bool allowMixedCase, bool allowNumbers, bool allowSpecialCharacters)
	{
		return CreateRandomPassword(sVal, PasswordLength, true, true, true, false, false, false, false);
	}

	public static string CreateRandomPassword(this string sVal, int PasswordLength, bool ignoreProgrammingCharacters)
	{
		return CreateRandomPassword(sVal, PasswordLength, true, true, true, true, true, true, ignoreProgrammingCharacters);
	}

	public static string CreateRandomPassword(this string sVal, int PasswordLength, bool allowMixedCase, bool allowNumbers, bool allowSpecialCharacters, bool ignoreProgrammingCharacters)
	{
		return CreateRandomPassword(sVal, PasswordLength, true, true, true, false, false, false, ignoreProgrammingCharacters);
	}

	public static string CreateRandomPassword(this string sVal, int PasswordLength, bool allowMixedCase, bool allowNumbers, bool allowSpecialCharacters, bool mixedCaseRequired, bool numberRequired, bool specialRequired, bool ignoreProgrammingCharacters)
	{
		if ((!allowMixedCase && mixedCaseRequired) || (!allowNumbers && numberRequired) || (!allowSpecialCharacters && specialRequired))
			throw new ArgumentException();
		StringBuilder result = new StringBuilder();
		int mixed = CryptoExtensions.RandomRangeInclusive(0, PasswordLength);
		int number = CryptoExtensions.RandomRangeInclusive(1, PasswordLength);
		while (number == mixed) number = CryptoExtensions.RandomRangeInclusive(1, PasswordLength);
		int special = CryptoExtensions.RandomRangeInclusive(1, PasswordLength);
		while (special == mixed || special == number) special = CryptoExtensions.RandomRangeInclusive(1, PasswordLength);
		for (int i = 0; i < PasswordLength; i++)
		{
			char c;
			if (mixedCaseRequired && i == mixed) c = (char)CryptoExtensions.RandomRangeInclusive(65, 90);
			else if (numberRequired && i == number) c = (char)CryptoExtensions.RandomRangeInclusive(48, 57);
			else if (specialRequired && i == special) c = GetSpecialCode(ignoreProgrammingCharacters);
			else
			{
				int kind;
				do kind = CryptoExtensions.RandomRangeInclusive(1, 4); while ((kind == 2 && !allowMixedCase) || (kind == 3 && !allowNumbers) || (kind == 4 && !allowSpecialCharacters));
				c = kind == 1 ? (char)CryptoExtensions.RandomRangeInclusive(97, 122) : kind == 2 ? (char)CryptoExtensions.RandomRangeInclusive(65, 90) : kind == 3 ? (char)CryptoExtensions.RandomRangeInclusive(48, 58) : GetSpecialCode(ignoreProgrammingCharacters);
			}
			result.Append(c);
		}
		return result.ToString().Trim();
	}

	private static char GetSpecialCode(bool ignoreProgrammingCharacters)
	{
		while (true)
		{
			char c = (char)CryptoExtensions.RandomRangeInclusive(32, 65);
			if (char.IsDigit(c)) continue;
			if (!ignoreProgrammingCharacters || "\\\"'`$&|;<>".IndexOf(c) < 0) return c;
		}
	}

	public static string ToTitleCase(this string mText)
	{
		if (string.IsNullOrEmpty(mText)) return mText;
		return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mText);
	}

	public static string SubsetString(this string s, string startText, string endText, bool ignoreCase)
	{
		if (string.IsNullOrEmpty(s)) return string.Empty;
		if (string.IsNullOrEmpty(startText) || string.IsNullOrEmpty(endText)) throw new ArgumentException();
		string source = ignoreCase ? s.ToUpperInvariant() : s;
		string startValue = ignoreCase ? startText.ToUpperInvariant() : startText;
		string endValue = ignoreCase ? endText.ToUpperInvariant() : endText;
		int start = source.IndexOf(startValue, StringComparison.Ordinal);
		int end = source.IndexOf(endValue, start, StringComparison.Ordinal);
		return s.SubstringFromXToY(start, end);
	}

	public static string PadRightEx(this string s, int length)
	{
		if (string.IsNullOrEmpty(s)) return s == null ? new string(' ', length) : s.PadRight(length);
		return s.Length < length ? s + new string(' ', length - s.Length) : s;
	}
}
