using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	[Serializable]
	public class ModuleManager : MonoBehaviour
	{
		[SerializeReference]
		private List<Module> m_Modules;

		public List<Module> Modules => m_Modules ?? (m_Modules = new List<Module>());

		public void Validate(bool i_AnalyticsEnabled)
		{
			if (m_Modules == null) m_Modules = new List<Module>();
			for (int i = m_Modules.Count - 1; i >= 0; i--)
			{
				if (m_Modules[i] == null) m_Modules.RemoveAt(i);
				else m_Modules[i].enabled = i_AnalyticsEnabled;
			}
		}
	}
}
