using UnityEngine;

namespace KobGamesSDKSlim.Analytics.EventsData
{
	[CreateAssetMenu(fileName = "AnalyticsLogsDictionary", menuName = "KobGames/Analytics/AnalyticsLogsDictionary")]
	public class EventsSO : ScriptableObject
	{
		[SerializeField]
		private HashedEvents m_HashedEvents;

		public void ConvertStringObjectPair(EventNameObjectPair i_Pair, eAnalyticsServices i_Service, ePartners i_Partner)
		{
			if (i_Pair == null) throw new System.ArgumentNullException(nameof(i_Pair));
			if (i_Pair.OriginalValue is string && i_Pair.OriginalName != "")
			{
				i_Pair.SetConverted(ConvertEntryName(i_Pair.OriginalName, i_Service, i_Partner), ConvertEntryName((string)i_Pair.OriginalValue, i_Service, i_Partner));
			}
			else
			{
				i_Pair.SetConverted(ConvertEntryName(i_Pair.OriginalName, i_Service, i_Partner));
			}
		}

		public string ConvertEntryName(string i_EventName, eAnalyticsServices i_Service, ePartners i_Partner)
		{
			string key = getHashedKey(i_EventName, i_Service, i_Partner);
			if (m_HashedEvents != null && m_HashedEvents.ContainsKey(key))
				return KobGamesSDKSlim.Analytics.Utils.ExtendedMethods.ToAllowedEventName(m_HashedEvents[key]);
			return i_EventName;
		}

		private string getHashedKey(string i_EventName, eAnalyticsServices i_Service, ePartners i_Partner)
		{
			return string.Format("{0}_{1}_{2}", i_EventName, i_Service, i_Partner);
		}
	}
}
