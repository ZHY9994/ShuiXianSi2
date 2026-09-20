using System;
using KobGamesSDKSlim.Core;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.EventsData
{
	[Serializable]
	public class EventsNames
	{
		[Serializable]
		public class EventsDictionary : SerializedDictionary<string, Events>
		{
		}

		[SerializeField]
		private EventsDictionary m_EventsDict;
	}
}
