using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	[Serializable]
	public class Module_GameAnalyticsConfig
	{
		[SerializeField]
		private bool m_SubmitFPS;

		[SerializeField]
		private List<string> m_CustomDimensions01;

		[SerializeField]
		private List<string> m_CustomDimensions02;

		[SerializeField]
		private List<string> m_CustomDimensions03;

		public bool SubmitFPS => m_SubmitFPS;

		public List<string> CustomDimensions01 => m_CustomDimensions01;

		public List<string> CustomDimensions02 => m_CustomDimensions02;

		public List<string> CustomDimensions03 => m_CustomDimensions03;

		public string GAAndroidName => getGAName("Android");

		public string GAIOSName => getGAName("iOS");

		private string getGAName(string i_Platform)
		{
			if (string.IsNullOrEmpty(i_Platform)) return string.Empty;
			if (i_Platform == "iOS") return m_CustomDimensions01 != null && m_CustomDimensions01.Count > 0 ? m_CustomDimensions01[0] : string.Empty;
			return m_CustomDimensions02 != null && m_CustomDimensions02.Count > 0 ? m_CustomDimensions02[0] : string.Empty;
		}
	}
}
