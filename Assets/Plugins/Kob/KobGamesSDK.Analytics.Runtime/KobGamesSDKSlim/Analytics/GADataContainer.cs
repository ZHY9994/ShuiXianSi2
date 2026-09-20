using System.Collections.Generic;
using KobGamesSDKSlim.Analytics.EventsData;

namespace KobGamesSDKSlim.Analytics
{
	public class GADataContainer : TimedPoolObject<GADataContainer>, IFactoryObject
	{
		public List<GAData> Entries { get; private set; }

		private GADataContainer()
		{
		}

		static GADataContainer()
		{
		}

		IFactoryObject IFactoryObject.Instantiate()
		{
			return new GADataContainer();
		}

		private void setGADataList(List<GAData> i_Entries)
		{
			Entries = i_Entries;
		}

		public static GADataContainer CreatePooled(string i_Category1 = null, string i_Category2 = null, string i_Category3 = null, string i_Category4 = null, string i_Category5 = null, long i_Value = 0L)
		{
			return createPooledGeneric(GAData.CreatePooled(i_Category1, i_Category2, i_Category3, i_Category4, i_Category5, i_Value));
		}

		public static GADataContainer CreatePooled(List<GAData> i_Entries)
		{
			return createPooledGeneric(i_Entries);
		}

		private static GADataContainer createPooledGeneric(GAData i_Entry)
		{
			var result = new GADataContainer(); result.setGADataList(new List<GAData> { i_Entry }); return result;
		}

		private static GADataContainer createPooledGeneric(List<GAData> i_Entries)
		{
			var result = new GADataContainer(); result.setGADataList(i_Entries); return result;
		}

		public void ConvertEventData(EventsSO i_Events, ePartners i_Partner)
		{
		}
	}
}
