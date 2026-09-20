using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class VersionsData
	{
		private bool m_IsSDK;

		public VersionsList VersionsList;

		private string m_JsonFilePath;

		public string JsonFilePath => m_JsonFilePath;

		public VersionsData(bool i_IsSDK = false)
		{
			VersionsList = new VersionsList();
			m_IsSDK = i_IsSDK;
			m_JsonFilePath = string.Empty;
		}

		private void onInspectorInit()
		{
			LoadFromJson();
		}

		private void setJsonFilePath()
		{
			return;
		}

		public void SaveToJson()
		{
			File.WriteAllText(m_JsonFilePath, JsonUtility.ToJson(VersionsList, true));
		}

		public void LoadFromJson()
		{
			VersionsList = JsonUtility.FromJson<VersionsList>(File.ReadAllText(m_JsonFilePath));
		}

		public void UpdateVersions()
		{
			if (VersionsList == null) VersionsList = new VersionsList();
			if (VersionsList.Versions == null) VersionsList.Versions = new List<VersionInfo>();
			var versions = new Dictionary<string, string>(StringComparer.Ordinal)
			{
				["IronSource"] = "7.0.4.1-r",
				["MaxSdk"] = "3.1.18",
				["AppsFlyer"] = "5.3.0",
				["GameAnalytics"] = "6.3.10",
				["AppMetrica"] = "3.5.0",
				["Facebook"] = "8.1.1",
				["Fx"] = "v0.0.8",
				["SimpleSDK"] = "v0.2.2",
				["SimpleSDKAttribution"] = "v0.0.4-appsflyer",
				["SimpleSDKTopon"] = "global-v5.7.3"
			};
			foreach (VersionInfo version in VersionsList.Versions)
			{
				if (version != null && version.Name != null && versions.TryGetValue(version.Name, out string value))
					version.Version = value;
			}
		}
	}
}
