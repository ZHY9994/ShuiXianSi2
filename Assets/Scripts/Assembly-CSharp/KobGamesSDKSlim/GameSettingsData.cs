using UnityEngine;

namespace KobGamesSDKSlim
{
	[CreateAssetMenu(fileName = "GameSettingsData")]
	public class GameSettingsData : SingletonScriptableObject<GameSettingsData>, IFocus
	{
		public SyncData SyncData;

		public AnalyticsData AnalyticsData;

		public GameSettingsData()
		{
		}

		public void OnFocus()
		{
			SyncData.OnFocus();
			AnalyticsData.OnFocus();
		}
	}
}
