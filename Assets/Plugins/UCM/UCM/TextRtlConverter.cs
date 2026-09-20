using System;

namespace UCM
{
	public class TextRtlConverter
	{
		public static string SwitchRTL(string input) { return string.IsNullOrEmpty(input) ? string.Empty : ReverseEachWord(input); }
		private static string Reversed(string input) { return ReverseEachWord(input); }
		private static string Reversed2(string input) { return Reverse(input); }
		private static string Reverse(string input) { if (string.IsNullOrEmpty(input)) return string.Empty; char[] c=input.ToCharArray(); Array.Reverse(c); return new string(c); }
		private static string ReverseEachWord(string input) { string[] w=(input ?? string.Empty).Split(' '); for(int i=0;i<w.Length;i++) w[i]=Reverse(w[i]); return string.Join(" ",w); }
		private static string CapitalizedTheFirstLetter(string input) { return string.IsNullOrEmpty(input) ? input : char.ToUpper(input[0])+input.Substring(1); }
		private static string BreakToEachWord(string input) { return input ?? string.Empty; }
		public string RemoveCharacters(string str1, string str2) { return string.IsNullOrEmpty(str1) ? string.Empty : str1.Replace(str2 ?? string.Empty, string.Empty); }
		private static string BreakToEachWord2(string input) { return BreakToEachWord(input); }
		private static string ToCamelCase(string input) { return input == null ? string.Empty : CapitalizedTheFirstLetter(input.ToLowerInvariant()); }
	}
}
