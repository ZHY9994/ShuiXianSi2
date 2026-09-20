using System.Collections.Generic;
using KobGamesSDKSlim.Analytics.EventsData;

namespace KobGamesSDKSlim.Analytics
{
	public class GAData : TimedPoolObject<GAData>, IFactoryObject
	{
		private List<string> m_CategoryList;

		public long Value { get; private set; }

		public string Category { get; private set; }

		private GAData()
		{
		}

		static GAData()
		{
		}

		IFactoryObject IFactoryObject.Instantiate()
		{
			return new GAData();
		}

		private GAData setValues(List<string> i_Categories, long i_Value)
		{
			m_CategoryList = i_Categories; Category = buildGADesignEventName(i_Categories); Value = i_Value; return this;
		}

		public void ConvertEventData(in EventsSO i_Events, ePartners i_Partner)
		{
		}

		private static string buildGADesignEventName(List<string> elements)
		{
			return string.Join(":", elements ?? new List<string>());
		}

		public static GAData CreatePooled(string i_Category1 = null, string i_Category2 = null, string i_Category3 = null, string i_Category4 = null, string i_Category5 = null, long i_Value = 0L)
		{
			var list = new List<string>(); if (i_Category1 != null) list.Add(i_Category1); if (i_Category2 != null) list.Add(i_Category2); if (i_Category3 != null) list.Add(i_Category3); if (i_Category4 != null) list.Add(i_Category4); if (i_Category5 != null) list.Add(i_Category5); return new GAData().setValues(list, i_Value);
		}
	}
}
