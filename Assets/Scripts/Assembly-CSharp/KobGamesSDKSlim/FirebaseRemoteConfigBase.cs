using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class FirebaseRemoteConfigBase
	{
		[Serializable]
		public class FirebaseGenericValueType<T>
		{
			[HideInInspector]
			public string Key;

			[SerializeField]
			public T m_DefaultValue;

			public virtual T Value { get; set; }

			public FirebaseGenericValueType(string i_Key, T i_DefaultValue)
			{
				Key = i_Key;
				m_DefaultValue = i_DefaultValue;
			}

			public void SetToDefaultValue(T i_DefaultValue, bool i_UpdateValue = false)
			{
				m_DefaultValue = i_DefaultValue;
				if (i_UpdateValue) Value = i_DefaultValue;
			}
		}

		[Serializable]
		public class FirebaseStringValue : FirebaseGenericValueType<string>
		{
			public override string Value
			{
				get
				{
					return PlayerPrefs.GetString(Key, m_DefaultValue);
				}
				set
				{
					PlayerPrefs.SetString(Key, value);
				}
			}

			public FirebaseStringValue(string i_Key, string i_DefaultValue)
				: base(i_Key, i_DefaultValue)
			{
			}
		}

		[Serializable]
		public class FirebaseIntValue : FirebaseGenericValueType<int>
		{
			public override int Value
			{
				get
				{
					return PlayerPrefs.GetInt(Key, m_DefaultValue);
				}
				set
				{
					PlayerPrefs.SetInt(Key, value);
				}
			}

		public FirebaseIntValue(string i_Key, int i_DefaultValue)
			: base(i_Key, i_DefaultValue)
		{
		}
		}

		[Serializable]
		public class FirebaseFloatValue : FirebaseGenericValueType<float>
		{
			public override float Value
			{
				get
				{
					return PlayerPrefs.GetFloat(Key, m_DefaultValue);
				}
				set
				{
					PlayerPrefs.SetFloat(Key, value);
				}
			}

		public FirebaseFloatValue(string i_Key, float i_DefaultValue)
			: base(i_Key, i_DefaultValue)
		{
		}
		}

		[Serializable]
		public class FirebaseBoolValue : FirebaseGenericValueType<bool>
		{
			public override bool Value
			{
				get
				{
					return PlayerPrefs.GetInt(Key, m_DefaultValue ? 1 : 0) == 1;
				}
				set
				{
					PlayerPrefs.SetInt(Key, value ? 1 : 0);
				}
			}

		public FirebaseBoolValue(string i_Key, bool i_DefaultValue)
			: base(i_Key, i_DefaultValue)
		{
		}
		}

		public int FetchTimeSpanHours = 6;

		public string FlushDeviceConfig = "1";

		public FirebaseStringValue ConfigVersion = new FirebaseStringValue("ConfigVersion", "0.1");

		public FirebaseIntValue MaxContinueAmount = new FirebaseIntValue("MaxContinueAmount", 1);

		public FirebaseStringValue ABTestVersion = new FirebaseStringValue("ABTestVersion", "");

		public FirebaseBoolValue IsAdsEnabled = new FirebaseBoolValue("IsAdsEnabled", true);

		public FirebaseBoolValue IsBannerEnabled = new FirebaseBoolValue("IsBannerEnabled", false);

		public FirebaseBoolValue IsInterstitialEnabled = new FirebaseBoolValue("IsInterstitialEnabled", false);

		public FirebaseBoolValue IsRewardVideosEnabled = new FirebaseBoolValue("IsRewardVideosEnabled", true);

		public FirebaseBoolValue InterstitalSessionStartEnabled = new FirebaseBoolValue("InterstitalSessionStartEnabled", false);

		public FirebaseIntValue InterstitialLevelCompleteCounter = new FirebaseIntValue("InterstitialLevelCompleteCounter", 0);

		public FirebaseIntValue InterstitialLevelFailedCounter = new FirebaseIntValue("InterstitialLevelFailedCounter", 0);

		[Tooltip("The Level in which Interstitials will start showing.\nExample: Value==2 will show interstitials on level 2 and beyond.")]
		public FirebaseIntValue InterstitialMinLevel = new FirebaseIntValue("InterstitialMinLevel", 0);

		public FirebaseIntValue RewardVideoCoolDown = new FirebaseIntValue("RewardVideoCoolDown", 0);

		public FirebaseIntValue InterstitialCoolDown = new FirebaseIntValue("InterstitialCoolDown", 0);

		public FirebaseBoolValue IsTestSuiteEnabled = new FirebaseBoolValue("IsTestSuiteEnabled", false);

		public FirebaseBoolValue IsRateUsEnabled = new FirebaseBoolValue("IsRateUsEnabled", true);

		[NonSerialized]
		protected Dictionary<string, object> ConfigDefaults = new Dictionary<string, object>();

		private bool IsInterstitialOrRewardVideoDisabled => RewardVideoCoolDown != null && RewardVideoCoolDown.Value >= 1 && InterstitialCoolDown != null && InterstitialCoolDown.Value < 1;

		public bool IsInterstitialTimeBased => RewardVideoCoolDown != null && RewardVideoCoolDown.Value > 0 || InterstitialCoolDown != null && InterstitialCoolDown.Value > 0;

		public virtual void Awake()
		{
			SetDefaults();
			flushDeviceConfigIfNeeded();
		}

		public virtual void SetDefaults()
		{
			if (ConfigDefaults == null) ConfigDefaults = new Dictionary<string, object>();
			ConfigDefaults.Add(ConfigVersion.Key, ConfigVersion.Value);
			ConfigDefaults.Add(MaxContinueAmount.Key, MaxContinueAmount.Value);
			ConfigDefaults.Add(ABTestVersion.Key, ABTestVersion.Value);
			ConfigDefaults.Add(IsAdsEnabled.Key, IsAdsEnabled.Value);
			ConfigDefaults.Add(IsBannerEnabled.Key, IsBannerEnabled.Value);
			ConfigDefaults.Add(IsInterstitialEnabled.Key, IsInterstitialEnabled.Value);
			ConfigDefaults.Add(IsRewardVideosEnabled.Key, IsRewardVideosEnabled.Value);
			ConfigDefaults.Add(InterstitalSessionStartEnabled.Key, InterstitalSessionStartEnabled.Value);
			ConfigDefaults.Add(InterstitialLevelCompleteCounter.Key, InterstitialLevelCompleteCounter.Value);
			ConfigDefaults.Add(InterstitialLevelFailedCounter.Key, InterstitialLevelFailedCounter.Value);
			ConfigDefaults.Add(InterstitialMinLevel.Key, InterstitialMinLevel.Value);
			ConfigDefaults.Add(RewardVideoCoolDown.Key, RewardVideoCoolDown.Value);
			ConfigDefaults.Add(InterstitialCoolDown.Key, InterstitialCoolDown.Value);
			ConfigDefaults.Add(IsTestSuiteEnabled.Key, IsTestSuiteEnabled.Value);
			ConfigDefaults.Add(IsRateUsEnabled.Key, IsRateUsEnabled.Value);
		}

		[Obsolete]
		public virtual void OnFirebaseInitialized()
		{
			return;
		}

		[Obsolete]
		protected virtual void RefrectPropertiesSafe()
		{
			return;
		}

		private void flushDeviceConfigIfNeeded()
		{
			if (PlayerPrefs.GetString("FlushDeviceConfig", "-1") == FlushDeviceConfig) return;
			PlayerPrefs.SetString("FlushDeviceConfig", FlushDeviceConfig);
			if (ConfigDefaults == null) return;
			foreach (var pair in ConfigDefaults) PlayerPrefs.DeleteKey(pair.Key);
			ConfigDefaults.Clear();
			SetDefaults();
		}

		private void PrintRemoteConfigValues()
		{
			string text = string.Format("Firebase-RemoteConfig-DefaultValues (Vars: {0}):\n", ConfigDefaults != null ? ConfigDefaults.Count : 0);
			if (ConfigDefaults != null)
			{
				foreach (KeyValuePair<string, object> pair in ConfigDefaults)
					text = string.Concat(text, string.Format("{0} = {1}\n", pair.Key, pair.Value));
			}
			Debug.Log(text);
		}
	}
}
