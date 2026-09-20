using System.Collections.Generic;

namespace KobGamesSDKSlim.Analytics
{
	public class StringListPooled : PooledList<string>
	{
		private StringListPooled()
		{
		}

		static StringListPooled()
		{
		}

		public static List<string> CreatePooled()
		{
			return createObject().GetList();
		}
	}
}
