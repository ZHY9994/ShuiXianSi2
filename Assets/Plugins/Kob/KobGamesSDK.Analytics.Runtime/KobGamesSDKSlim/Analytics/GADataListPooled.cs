using System.Collections.Generic;

namespace KobGamesSDKSlim.Analytics
{
	public class GADataListPooled : PooledList<GAData>
	{
		private GADataListPooled()
		{
		}

		static GADataListPooled()
		{
		}

		public static List<GAData> CreatePooled()
		{
			return createObject().GetList();
		}
	}
}
