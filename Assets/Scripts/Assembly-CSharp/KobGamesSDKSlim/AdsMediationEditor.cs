using System;
using Sirenix.OdinInspector;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class AdsMediationEditor
	{
		public eMediationNetworks MediationNetworks;

		public string IronSourceAppleID = "None";

		public string IronSourceAndroidID = "None";

		private const string k_KobyMAXSDK = "i8Iz-ON-o5T8cn3iA6AXHWEd5MiybWDIfVFeudcgi5qKsu21-DZGlouq9bnl7-2nUIPNw12d6szzk_-iCTzxKs";

		private const string k_A_MAXSDK = "6AQkyPv9b4u7yTtMH9PT40gXg00uJOTsmBOf7hDxa_-FnNZvt_qTLnJAiKeb5-2_T8GsI_dGQKKKrtwZTlCzAR";

		private const string k_Mo_MAXSDK = "EghIU8MPrS5yShsp12hFZTN2T0sysd9F-mpr9MgBKNplGKZGGPVnpldaKrIrxV3vqXdmcNDl2FyCHT-KIC2hC2";

		private ValueDropdownList<string> MAXSDKKeysValues = new ValueDropdownList<string>();

		public string MAXSDKKey = "i8Iz-ON-o5T8cn3iA6AXHWEd5MiybWDIfVFeudcgi5qKsu21-DZGlouq9bnl7-2nUIPNw12d6szzk_-iCTzxKs";

		public string AdMobAppIdAndroid;

		public string AdMobAppIdIos;

		public string MAXBannerAdUnitIdIOS = "[Please Set]";

		public string MAXInterstitialAdUnitIdIOS = "[Please Set]";

		public string MAXRewardVideoAdUnitIdIOS = "[Please Set]";

		public string MAXBannerAdUnitIdAndroid = "[Please Set]";

		public string MAXInterstitialAdUnitIdAndroid = "[Please Set]";

		public string MAXRewardVideoAdUnitIdAndroid = "[Please Set]";

		public int MaxContinueAmount => Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.MaxContinueAmount?.Value ?? 0;

		public string IronSourceAppId => UnityEngine.Application.platform == UnityEngine.RuntimePlatform.Android ? IronSourceAndroidID : IronSourceAppleID;

		private bool m_IsAdMobAppIdAndroidEmpty => string.Equals(AdMobAppIdAndroid, string.Empty);

		private bool m_IsAdMobAppIdIosEmpty => string.Equals(AdMobAppIdIos, string.Empty);

		public bool IsIronSourceEnabled
		{
			get
			{
				return MediationNetworks == eMediationNetworks.ironSource;
			}
			set
			{
				MediationNetworks = eMediationNetworks.ironSource;
			}
		}

		public bool IsMAXEnabled
		{
			get
			{
				return MediationNetworks == eMediationNetworks.MAX;
			}
			set
			{
				MediationNetworks = eMediationNetworks.MAX;
			}
		}

		public bool IsSupereraEnabled
		{
			get
			{
				return MediationNetworks == eMediationNetworks.SUPERERA;
			}
			set
			{
				MediationNetworks = eMediationNetworks.SUPERERA;
			}
		}

		public void FetchMAXSDKKeyFromManifest()
		{
			var value = Utils.FetchAndroidManifestData("android:name='applovin.sdk.key'");
			if (!string.IsNullOrEmpty(value)) MAXSDKKey = value;
		}

		public void SetMAXSDKKeyOnManifest()
		{
			Utils.SaveAndroidManifestData(MAXSDKKey, "android:name='applovin.sdk.key'");
		}

		public void FetchAdMobAppIdOnManifest()
		{
			var value = Utils.FetchAndroidManifestData("android:name='com.google.android.gms.ads.APPLICATION_ID'");
			if (!string.IsNullOrEmpty(value)) AdMobAppIdAndroid = value;
		}

		public void SetAdMobAppIdOnManifest()
		{
			Utils.AddManifestNode("com.google.android.gms.ads.APPLICATION_ID", "None");
			Utils.SaveAndroidManifestData(AdMobAppIdAndroid, "android:name='com.google.android.gms.ads.APPLICATION_ID'");
		}

		[Obsolete]
		public void AddMAXNetworkSecurityConfig()
		{
			return;
		}

		public void RemoveMAXNetworkSecurityConfig()
		{
			Utils.RemoveManifestAttributeNew("android:networkSecurityConfig");
		}

		public void Load()
		{
			return;
		}
	}
}
