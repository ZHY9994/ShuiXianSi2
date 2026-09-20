using System.Collections.Generic;

namespace KobGamesSDKSlim.Analytics
{
	public class StringStringDictionary : PooledDictionary<string, string>
	{
		private StringStringDictionary()
		{
		}

		static StringStringDictionary()
		{
		}

		public static Dictionary<string, string> CreatePooled()
		{
			return createObject().GetDictionary();
		}
	}
}
