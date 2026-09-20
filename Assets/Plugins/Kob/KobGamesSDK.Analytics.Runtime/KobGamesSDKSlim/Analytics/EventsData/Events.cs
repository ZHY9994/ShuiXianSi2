using System;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.EventsData
{
	[Serializable]
	public class Events
	{
		[SerializeField]
		private ServicesDictionary m_ServicesDict;

		public string DefaultEventName { get; private set; }
	}
}
