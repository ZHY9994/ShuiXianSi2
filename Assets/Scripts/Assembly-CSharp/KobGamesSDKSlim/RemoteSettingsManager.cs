using System.Runtime.CompilerServices;

namespace KobGamesSDKSlim
{
	public class RemoteSettingsManager : Singleton<RemoteSettingsManager>
	{
		public float FirebaseRemoteConfigTimeout = 3f;

		public bool IgnoreRemoteConfigInEditor;

		public FirebaseRemoteConfig FirebaseRemoteConfig;

		private bool m_IsFirebaseRemoteCalled;

		public static event FirebaseRemoteConfigUpdatedEvent OnFirebaseRemotConfigUpdated = delegate { };

		public void SetProductionMode()
		{
			FirebaseRemoteConfig.FetchTimeSpanHours = 6;
		}

		public void SetTestMode()
		{
			FirebaseRemoteConfig.FetchTimeSpanHours = 0;
		}

		public void ClearPlayerPrefs()
		{
			UnityEngine.PlayerPrefs.DeleteAll();
		}

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			FirebaseRemoteConfig.Awake();
		}

		private void onFirebaseRemotConfigUpdatedCompletion(bool i_IsUpdated)
		{
			if (m_IsFirebaseRemoteCalled) return;
			m_IsFirebaseRemoteCalled = true;
			CancelInvoke(nameof(onFirebaseRemotConfigUpdatedCompletionDelayed));
			OnFirebaseRemotConfigUpdated?.Invoke(i_IsUpdated);
		}

		private void onFirebaseRemotConfigUpdatedCompletionDelayed()
		{
			onFirebaseRemotConfigUpdatedCompletion(false);
		}

		public void UpdateRemoteConfig(int i_InterstitalLevel, int i_Cooldown)
		{
			FirebaseRemoteConfig.UpdateRemoteConfigs(i_InterstitalLevel, i_Cooldown);
		}
	}
}
