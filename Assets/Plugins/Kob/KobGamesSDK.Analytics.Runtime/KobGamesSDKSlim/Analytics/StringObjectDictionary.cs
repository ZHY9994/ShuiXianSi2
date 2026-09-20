using System.Collections.Generic;

namespace KobGamesSDKSlim.Analytics
{
	public class StringObjectDictionary : PooledDictionary<string, object>
	{
		private StringObjectDictionary()
		{
		}

		static StringObjectDictionary()
		{
		}

		public static Dictionary<string, object> CreatePooled()
		{
			return createObject().GetDictionary();
		}
	}
}
