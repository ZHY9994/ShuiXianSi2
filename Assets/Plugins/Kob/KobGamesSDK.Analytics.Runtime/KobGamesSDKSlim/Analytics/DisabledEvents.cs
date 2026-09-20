using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics
{
	[Serializable]
	public class DisabledEvents
	{
		[SerializeField]
		private List<string> m_DisabledEvents;

		public bool Contains(string i_EventName)
		{
			return m_DisabledEvents != null && !string.IsNullOrEmpty(i_EventName) && m_DisabledEvents.Contains(i_EventName);
		}

		private void getValueDropdown()
		{
		}
	}
}
