namespace UCM
{
	public class UnicodeToKruti
	{
		private static string[] array_one = new string[] { "‘", "’", "“", "”", "(", ")", "{", "}", "=", "।", "?", "-", "µ", "॰", ",", ".", "् ", "०", "१", "२", "३", "४", "५", "६", "७", "८", "९", "x", "फ़्", "क़", "ख़", "ग़", "ज़्", "ज़", "ड़", "ढ़", "फ़", "य़", "ऱ", "ऩ", "त्त्", "त्त", "क्त", "दृ", "कृ", "ह्न", "ह्य", "हृ", "ह्म", "ह्र", "ह्", "द्द", "क्ष्", "क्ष", "त्र्", "त्र", "ज्ञ", "छ्य", "ट्य", "ठ्य", "ड्य", "ढ्य", "द्य", "द्व", "श्र", "ट्र", "ड्र", "ढ्र", "छ्र", "क्र", "फ्र", "द्र", "प्र", "ग्र", "रु", "रू", "्र", "ओ", "औ", "आ", "अ", "ई", "इ", "उ", "ऊ", "ऐ", "ए", "ऋ", "क्", "क", "क्क", "ख्", "ख", "ग्", "ग", "घ्", "घ", "ङ", "चै", "च्", "च", "छ", "ज्", "ज", "झ्", "झ", "ञ", "ट्ट", "ट्ठ", "ट", "ठ", "ड्ड", "ड्ढ", "ड", "ढ", "ण्", "ण", "त्", "त", "थ्", "थ", "द्ध", "द", "ध्", "ध", "न्", "न", "प्", "प", "फ्", "फ", "ब्", "ब", "भ्", "भ", "म्", "म", "य्", "य", "र", "ल्", "ल", "ळ", "व्", "व", "श्", "श", "ष्", "ष", "स्", "स", "ह", "ऑ", "ॉ", "ो", "ौ", "ा", "ी", "ु", "ू", "ृ", "े", "ै", "ं", "ँ", "ः", "्" };

		private static string[] array_two = new string[] { "^", "*", "Þ", "ß", "¼", "½", "¿", "À", "¾", "A", "\\\\", "&", "&", "Œ", "]", "-", "~ ", "å", "ƒ", "„", "…", "†", "‡", "ˆ", "‰", "Š", "‹", "Û", "¶", "d", "[k", "x", "T", "t", "M+", "<+", "Q", ";", "j", "u", "Ù", "Ùk", "Dr", "–", "—", "à", "á", "â", "ã", "ºz", "º", "í", "{", "{k", "«", "=", "K", "Nî", "Vî", "Bî", "Mî", "<î", "|", "}", "J", "Vª", "Mª", "<ªª", "Nª", "Ø", "Ý", "æ", "ç", "xz", "#", ":", "z", "vks", "vkS", "vk", "v", "bZ", "b", "m", "Å", ",s", ",", "_", "D", "d", "ô", "[", "[k", "X", "x", "?", "?k", "³", "pkS", "P", "p", "N", "T", "t", "÷", ">", "¥", "ê", "ë", "V", "B", "ì", "ï", "M", "<", ".", ".k", "R", "r", "F", "Fk", ")", "n", "/", "/k", "U", "u", "I", "i", "¶", "Q", "C", "c", "H", "Hk", "E", "e", "¸", ";", "j", "Y", "y", "G", "O", "o", "'", "'k", "\\\"", "\\\"k", "L", "l", "g", "v‚", "‚", "ks", "kS", "k", "h", "q", "w", "`", "s", "S", "a", "¡", "%", "W", "·", "~ ", "~" };

		public static string Convert(string unicode_substring)
		{
			if (string.IsNullOrEmpty(unicode_substring)) return unicode_substring;
			unicode_substring = unicode_substring.Replace("क़", "क़").Replace("ख़", "ख़").Replace("ग़", "ग़").Replace("ज़", "ज़").Replace("ड़", "ड़").Replace("ढ़", "ढ़").Replace("ऩ", "ऩ").Replace("फ़", "फ़").Replace("य़", "य़").Replace("ऱ", "ऱ");
			for (int i = 0; i < array_one.Length && i < array_two.Length; i++)
				unicode_substring = unicode_substring.Replace(array_one[i], array_two[i]);
			return unicode_substring;
		}

		public static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
		{
			if (Source == null || Find == null) return Source;
			int index = Source.IndexOf(Find, System.StringComparison.Ordinal);
			return index < 0 ? Source : Source.Remove(index, Find.Length).Insert(index, Replace ?? string.Empty);
		}
	}
}
