using System.Collections.Generic;

namespace UCM
{
	internal class ArabicFixerTool
	{
		internal static bool showTashkeel;

		internal static bool combineTashkeel;

		internal static bool useHinduNumbers;

		internal static string RemoveTashkeel(string str, out List<TashkeelLocation> tashkeelLocation)
		{
			tashkeelLocation = new List<TashkeelLocation>();
			return str ?? string.Empty;
		}

		internal static char[] ReturnTashkeel(char[] letters, List<TashkeelLocation> tashkeelLocation)
		{
			return letters;
		}

		internal static string FixLine(string str)
		{
			if (string.IsNullOrEmpty(str)) return string.Empty;
			char[] chars = str.ToCharArray();
			System.Array.Reverse(chars);
			return new string(chars);
		}

		internal static bool IsIgnoredCharacter(char ch)
		{
			return char.IsWhiteSpace(ch) || char.IsPunctuation(ch) || char.IsDigit(ch);
		}

		internal static bool IsLeadingLetter(char[] letters, int index)
		{
			return index == 0 || IsIgnoredCharacter(letters[index - 1]);
		}

		internal static bool IsFinishingLetter(char[] letters, int index)
		{
			return index == letters.Length - 1 || IsIgnoredCharacter(letters[index + 1]);
		}

		internal static bool IsMiddleLetter(char[] letters, int index)
		{
			return !IsLeadingLetter(letters, index) && !IsFinishingLetter(letters, index);
		}
	}
}
