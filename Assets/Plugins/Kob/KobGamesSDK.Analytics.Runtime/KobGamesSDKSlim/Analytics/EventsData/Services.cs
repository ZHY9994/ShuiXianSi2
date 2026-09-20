using System;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.EventsData
{
	[Serializable]
	public class Services
	{
		[SerializeField]
		private string m_ServiceEventName;

		[SerializeField]
		private PartnersDict m_PartnersDict;

		public string ServiceEventName
		{
			get
			{
				return m_ServiceEventName;
			}
			private set
			{
				m_ServiceEventName = value;
			}
		}
	}
}
