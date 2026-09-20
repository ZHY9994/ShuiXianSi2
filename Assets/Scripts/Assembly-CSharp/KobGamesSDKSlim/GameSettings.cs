using UnityEngine;

namespace KobGamesSDKSlim
{
	[CreateAssetMenu(fileName = "GameSettings")]
	public class GameSettings : SingletonScriptableObject<GameSettings>
	{
		public GeneralEditor General = new GeneralEditor();

		public ScreenshotsEditor Screenshots = new ScreenshotsEditor();

		public AdsMediationEditor AdsMediation = new AdsMediationEditor();

		public RemoteSettingsEditor RemoteSettings = new RemoteSettingsEditor();

		public AnalyticsEditor Analytics = new AnalyticsEditor();

		public RateUsEditor RateUs = new RateUsEditor();

		public ShortcutsEditor Shortcuts = new ShortcutsEditor();

		public SyncEditor Sync = new SyncEditor();

		public VersionsDiffEditor VersionsDiff = new VersionsDiffEditor();

		public virtual void OnFocus()
		{
			return;
		}
	}
}
