using UnityEngine;

namespace KobGamesSDKSlim
{
	public class ManagersBase : Singleton<Managers>
	{
		public bool IsHapticEnabled;

		public SplashScreenManager SplashScreen;

		public AdsManager Ads;

		public AnalyticsManager Analytics;

		public RemoteSettingsManager RemoteSettings;

		public StorageManager Storage;

		public GameManager GameManager;

		public iAPManager iAPManager;

		public HapticManager HapticManager;

		public InteractionManager InteractionManager;

		public ScriptableObject[] DummyReferences;

		public GameObject Prefab;

		public virtual void Reset()
		{
			if (SplashScreen == null)
				SplashScreen = GetComponentInChildren<SplashScreenManager>();
			if (Ads == null)
				Ads = GetComponentInChildren<AdsManager>();
			if (Analytics == null)
				Analytics = GetComponentInChildren<AnalyticsManager>();
			if (RemoteSettings == null)
				RemoteSettings = GetComponentInChildren<RemoteSettingsManager>();
			if (Storage == null)
				Storage = GetComponentInChildren<StorageManager>();
			if (GameManager == null)
				GameManager = GetComponentInChildren<GameManager>();
			if (iAPManager == null)
				iAPManager = GetComponentInChildren<iAPManager>();
			if (HapticManager == null)
				HapticManager = GetComponentInChildren<HapticManager>();
			if (InteractionManager == null)
				InteractionManager = GetComponentInChildren<InteractionManager>();
		}

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			//Debug.unityLogger.logEnabled = true;
		}

		public void ReloadScene()
		{
			return;
		}

		public void SelectGameSettings()
		{
			return;
		}

		public void SimulateLevelCompleted()
		{
			ShortcutsManager.Instance.TriggerShortcut("Level Complete");
		}

		public void SimulateLevelFailedWithRevive()
		{
			ShortcutsManager.Instance.TriggerShortcut("Level Failed With Revive");
		}

		public void SimulateLevelFailed()
		{
			ShortcutsManager.Instance.TriggerShortcut("Level Failed");
		}

		public void ApplyPrefabInstance()
		{
			return;
		}

		public void AddPrefabToManagers()
		{
			return;
		}

		public void FixAdsManagerV2()
		{
			return;
		}

		public ManagersBase()
		{
		}
	}
}
