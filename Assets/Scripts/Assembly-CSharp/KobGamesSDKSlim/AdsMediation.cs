using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class AdsMediation
	{
		public delegate void UseDummyAdsOnDeviceFromTestSuiteChange(bool i_Value);

		private static UseDummyAdsOnDeviceFromTestSuiteChange m_OnUseDummyAdsOnDeviceFromTestSuiteChangeEvent = delegate { };

		public string dummyText;

		[SerializeField]
		[HideInInspector]
		private bool m_UseDummyAdsOnDevice;

		[SerializeField]
		private RewardVideoPlacementID m_PlacementIDPair = new RewardVideoPlacementID();

		public bool UseDummyAdsOnDevice => Application.isEditor ? m_UseDummyAdsOnDevice : m_UseDummyAdsOnDevice && Debug.isDebugBuild;

		public bool IsAdsEnabled
		{
			get
			{
				return Managers.Instance != null && Managers.Instance.RemoteSettings != null && Managers.Instance.RemoteSettings.FirebaseRemoteConfig != null && Managers.Instance.RemoteSettings.FirebaseRemoteConfig.IsAdsEnabled.Value;
			}
			set
			{
				Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.IsAdsEnabled?.SetToDefaultValue(value, true);
			}
		}

		public bool IsBannerEnabled
		{
			get
			{
				return Managers.Instance != null && Managers.Instance.RemoteSettings != null && Managers.Instance.RemoteSettings.FirebaseRemoteConfig != null && Managers.Instance.RemoteSettings.FirebaseRemoteConfig.IsBannerEnabled.Value;
			}
			set
			{
				Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.IsBannerEnabled?.SetToDefaultValue(value, true);
			}
		}

		public bool IsInterstitialEnabled
		{
			get
			{
				return Managers.Instance != null && Managers.Instance.RemoteSettings != null && Managers.Instance.RemoteSettings.FirebaseRemoteConfig != null && Managers.Instance.RemoteSettings.FirebaseRemoteConfig.IsInterstitialEnabled.Value;
			}
			set
			{
				Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.IsInterstitialEnabled?.SetToDefaultValue(value, true);
			}
		}

		public bool IsRewardVideosEnabled
		{
			get
			{
				return Managers.Instance != null && Managers.Instance.RemoteSettings != null && Managers.Instance.RemoteSettings.FirebaseRemoteConfig != null && Managers.Instance.RemoteSettings.FirebaseRemoteConfig.IsRewardVideosEnabled.Value;
			}
			set
			{
				Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.IsRewardVideosEnabled?.SetToDefaultValue(value, true);
			}
		}

		public int InterstitialMinLevel
		{
			get
			{
				return Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.InterstitialMinLevel?.Value ?? 0;
			}
			set
			{
				Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.InterstitialMinLevel?.SetToDefaultValue(value, true);
			}
		}

		public int RewardVideoCoolDown
		{
			get
			{
				return Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.RewardVideoCoolDown?.Value ?? 0;
			}
			set
			{
				Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.RewardVideoCoolDown?.SetToDefaultValue(value, true);
			}
		}

		public int InterstitialCoolDown
		{
			get
			{
				return Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.InterstitialCoolDown?.Value ?? 0;
			}
			set
			{
				Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.InterstitialCoolDown?.SetToDefaultValue(value, true);
			}
		}

		public static event UseDummyAdsOnDeviceFromTestSuiteChange OnUseDummyAdsOnDeviceFromTestSuiteChangeEvent
		{
			[CompilerGenerated]
			add
			{
				UseDummyAdsOnDeviceFromTestSuiteChange current;
				UseDummyAdsOnDeviceFromTestSuiteChange updated;
				do { current = m_OnUseDummyAdsOnDeviceFromTestSuiteChangeEvent; updated = (UseDummyAdsOnDeviceFromTestSuiteChange)Delegate.Combine(current, value); }
				while (System.Threading.Interlocked.CompareExchange(ref m_OnUseDummyAdsOnDeviceFromTestSuiteChangeEvent, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				UseDummyAdsOnDeviceFromTestSuiteChange current;
				UseDummyAdsOnDeviceFromTestSuiteChange updated;
				do { current = m_OnUseDummyAdsOnDeviceFromTestSuiteChangeEvent; updated = (UseDummyAdsOnDeviceFromTestSuiteChange)Delegate.Remove(current, value); }
				while (System.Threading.Interlocked.CompareExchange(ref m_OnUseDummyAdsOnDeviceFromTestSuiteChangeEvent, updated, current) != current);
			}
		}

		public void SetForceUseDummyAdsOnDevice(bool i_Value)
		{
			m_UseDummyAdsOnDevice = i_Value;
		}

		public void ToggleForceUseDummyAdsOnDevice()
		{
			SetForceUseDummyAdsOnDevice(!m_UseDummyAdsOnDevice);
		}

		public void TryApplyManagersPrefab()
		{
			return;
		}

		public void SelectRemoteConfig()
		{
			return;
		}

		public string GetPlacement_RV(eRewardVideoPlacements i_PlacementId)
		{
			if (m_PlacementIDPair == null) return i_PlacementId.ToString();
			if (!m_PlacementIDPair.ContainsKey(i_PlacementId)) m_PlacementIDPair.Add(i_PlacementId, i_PlacementId.ToString());
			return m_PlacementIDPair[i_PlacementId];
		}
	}
}
