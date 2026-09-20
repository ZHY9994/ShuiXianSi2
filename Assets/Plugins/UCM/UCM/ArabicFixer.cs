namespace UCM
{
	public class ArabicFixer
	{
		public static string Fix(string str)
		{
			return Fix(str, true, false, false);
		}

		public static string Fix(string str, bool rtl)
		{
			return Fix(str, true, false, false);
		}

		public static string Fix(string str, bool showTashkeel, bool useHinduNumbers)
		{
			return Fix(str, showTashkeel, false, useHinduNumbers);
		}

		public static string Fix(string str, bool showTashkeel, bool combineTashkeel, bool useHinduNumbers)
		{
			if (string.IsNullOrEmpty(str)) return string.Empty;
			ArabicFixerTool.showTashkeel = showTashkeel;
			ArabicFixerTool.combineTashkeel = combineTashkeel;
			ArabicFixerTool.useHinduNumbers = useHinduNumbers;
			string[] lines = str.Replace("\r\n", "\n").Split('\n');
			for (int i = 0; i < lines.Length; i++) lines[i] = ArabicFixerTool.FixLine(lines[i]);
			return string.Join("\n", lines);
		}
	}
}
