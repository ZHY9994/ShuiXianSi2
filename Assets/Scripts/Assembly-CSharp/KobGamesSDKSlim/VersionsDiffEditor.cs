using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class VersionsDiffEditor
	{
		public VersionsData VersionsSDK;

		public VersionsData VersionsProject;

		public VersionsDiffEditor()
		{
			VersionsSDK = new VersionsData(true);
			VersionsProject = new VersionsData(false);
		}

		public string VersionsDiffStatus
		{
			get
			{
				if (VersionsProject.VersionsList.Versions.Count == 0)
					return "SDK Sync Needed: can't find versions json in project folder";

				string result = string.Empty;
				foreach (VersionInfo sdkVersion in VersionsSDK.VersionsList.Versions)
				{
					VersionInfo projectVersion = VersionsProject.VersionsList.Versions.Find(x => x.Name == sdkVersion.Name);
					if (projectVersion != null && projectVersion.Version != sdkVersion.Version)
					{
						if (result == string.Empty)
							result = "SDK Sync Needed: ";
						result += projectVersion.Name + " | ";
					}
				}
				return result;
			}
		}

		public Color GetDiffStatusColor()
		{
			return VersionsDiffStatus == string.Empty ? Color.green : new Color(1f, 0.92156863f, 0.015686275f, 1f);
		}

		public bool IsDiffExists()
		{
			return VersionsDiffStatus != string.Empty;
		}

		public void SyncJsonToProject()
		{
			string dataPath = Application.dataPath.Replace("Assets", "");
			Utils.CopyFile(dataPath + "/" + VersionsSDK.JsonFilePath, dataPath + "/" + VersionsProject.JsonFilePath, true);
		}
	}
}
