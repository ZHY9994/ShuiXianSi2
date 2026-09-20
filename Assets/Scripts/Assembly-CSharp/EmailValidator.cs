using System;
using System.Text.RegularExpressions;

internal class EmailValidator
{
	private bool invalid;

	public bool IsValidEmail(string strIn)
	{
		invalid = false;
		if (string.IsNullOrEmpty(strIn))
			return false;
		string normalized = Regex.Replace(strIn, "(@)(.+)$", DomainMapper);
		if (invalid)
			return false;
		return Regex.IsMatch(normalized, "^(?(\\\")(\\\".+?(?<!\\\\)\\\"@)|(([0-9a-z]((\\\\.(?!\\\\.))|[-!#\\\\$%&'\\\\*\\\\+/=\\\\?\\\\^`\\\\{\\\\}\\\\|~\\\\w])*)(?<=[0-9a-z])@))(?(\\\\[)(\\\\[(\\\\d{1,3}\\\\.){3}\\\\d{1,3}\\\\])|(([0-9a-z][-\\\\w]*[0-9a-z]*\\\\.)+[a-z0-9][\\\\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase);
	}

	private string DomainMapper(Match match)
	{
	try
		{
			System.Globalization.IdnMapping mapping = new System.Globalization.IdnMapping();
			string ascii = mapping.GetAscii(match.Groups[2].Value);
			return match.Groups[1].Value + ascii;
		}
		catch (ArgumentException)
		{
			invalid = true;
			return match.Value;
		}
	}
}
