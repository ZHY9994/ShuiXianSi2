using System.Collections.Generic;

namespace UCM
{
	internal class ArabicTable
	{
		private static List<ArabicMapping> mapList;

		private static ArabicTable arabicMapper;

		internal static ArabicTable ArabicMapper => arabicMapper ?? (arabicMapper = new ArabicTable());

		private ArabicTable()
		{
			mapList = new List<ArabicMapping>();
		}

		internal int Convert(int toBeConverted)
		{
			return toBeConverted;
		}
	}
}
