using System.Collections.Generic;

namespace KobGamesSDKSlim.Analytics
{
	public class EventNameObjectPairListPooled : PooledList<EventNameObjectPair>
	{
		private EventNameObjectPairListPooled()
		{
		}

		static EventNameObjectPairListPooled()
		{
		}

		public static List<EventNameObjectPair> CreatePooled()
		{
			return createObject().GetList();
		}
	}
}
