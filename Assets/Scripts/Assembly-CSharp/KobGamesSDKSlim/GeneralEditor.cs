using System;
using Crystal;
using KobGamesSDKSlim.Rendering;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class GeneralEditor
	{
		public bool IsInit;

		[SerializeField]
		[HideInInspector]
		private bool m_IsAdsEnabled;

		public Sprite Icon;

		public Sprite SplashImage;

		public bool UpdateSplash;

		public bool IOSTeamIDTest;

		public bool AutoSign;

		public string SignIOSTeamID;

		public const string k_KobyTeamID = "6T5SK42BUG";

		public const string k_YakovTeamID = "Q2K5DK5986";

		public const string k_VTeamID = "QUZ546KL38";

		public const string k_Wak = "JDUQM39U2T";

		public const string k_Az = "XCVXFM7SV3";

		public const string k_AzAI = "SXQF39B47F";

		public const string k_BepCarlos = "D89KY442NR";

		public const string k_BepMaria = "9W632B3D96";

		public const string k_BepArtyom = "2W94JVYPBL";

		public const string k_KpicGuy = "ZVB6BWCW85";

		private ValueDropdownList<string> SignIOSTeamIDValues;

		[Tooltip("Separated Bundles")]
		public bool SeparateBundles;

		public string BundleIdentifier;

		public string BundleIdentifierAndroid;

		public string GameName;

		public string BuildVersion;

		public int BuildNumber;

		public string GooglePlayId;

		public string AppStoreId;

		private const string k_AppSpecificPassKoby = "ovxr-ihfe-kmlj-njut";

		private const string k_AppSpecificPassYakov = "msfj-vnux-axaa-ozre";

		[HideInInspector]
		public bool IsBuildIOS;

		[HideInInspector]
		public bool IsBuildAndroid;

		private const int k_SelectStorageAndRemoteOrder = -3;

		[NonSerialized]
		[HideInInspector]
		public string[] MAXPathsToDelete;

		[NonSerialized]
		[HideInInspector]
		public string[] IronSourcePathsToDelete;

		[NonSerialized]
		[HideInInspector]
		public string[] AdsPathsToDelete;

		private string m_PathToProjectUpdateBatchFile;

		private const int m_AnalyticsOrder = 100;

		private const int m_CultureInfoOrder = 200;

		public bool SetSpecificCulture;

		public string CultureName;

		private const int m_RenderPipeline = 300;

		[SerializeField]
		private eRenderingPipeline m_RenderingPipeline;

		public bool EnforceQualitySettings;

		[SerializeField]
		private string m_DummySpacer;

		private const int m_DebugButtonOrder = 500;

		private const int m_APKOrder = 600;

		private string m_AdsDetailsStatus => $"Ads: {GameConfig.Instance.AdsMediation.IsAdsEnabled} | Banner: {GameConfig.Instance.AdsMediation.IsBannerEnabled} | Interstitial: {GameConfig.Instance.AdsMediation.IsInterstitialEnabled} | RewardVideo: {GameConfig.Instance.AdsMediation.IsRewardVideosEnabled} | DummyAds: {GameConfig.Instance.AdsMediation.UseDummyAdsOnDevice}";

		private string m_VersionsDiffStatus => GameSettings.Instance.VersionsDiff.VersionsDiffStatus;

		public bool IsAdsEnabled
		{
			get
			{
					return GameConfig.Instance.AdsMediation.IsAdsEnabled;
			}
			set
			{
					GameConfig.Instance.AdsMediation.IsAdsEnabled = value;
			}
		}

		private bool IsBundleIdentifierAndroidAvailable => SeparateBundles;

		[Obsolete]
		public string CurrentSimulateDevice => $"Simulate Device: {Crystal.SafeArea.Sim}";

		[Obsolete]
		public SafeArea[] SafeAreas => UnityEngine.Object.FindObjectsOfType<SafeArea>();

		public eRenderingPipeline RenderingPipeline
		{
			get
			{
				return m_RenderingPipeline;
			}
			set
			{
				if (m_RenderingPipeline != value)
					m_RenderingPipeline = value;
			}
		}

		public Color GetDiffStatusColor() => GameSettings.Instance.VersionsDiff.GetDiffStatusColor();

		public bool IsDiffExists()
		{
			return GameSettings.Instance.VersionsDiff.IsDiffExists();
		}

		private static Color GetAdsDetailsStatusColor()
		{
				return GameConfig.Instance.AdsMediation.IsAdsEnabled ? Color.green : Color.red;
		}

		private void openQualitySettings()
		{
			return;
		}

		public void UpdateIsAdsEnabled()
		{
			if (m_IsAdsEnabled != IsAdsEnabled)
				m_IsAdsEnabled = IsAdsEnabled;
		}

		public void DeleteIAPPlugins(bool i_IsForce = false)
		{
			iAPManagerBase.DeleteIAPPlugins(i_IsForce);
		}

		public string GetBundleIdentifierAndroid()
		{
			return SeparateBundles ? BundleIdentifierAndroid : BundleIdentifier;
		}

		public void OnBuildSuccess()
		{
			return;
		}

		private void buildIOS()
		{
			return;
		}

		private void buildAndroid()
		{
			return;
		}

		private void SelectAdjustGO()
		{
			return;
		}

		public bool VerifyQualitySettings()
		{
			return QualitySettings.GetQualityLevel() < 2;
		}

		public void selectRemoteConfig()
		{
			return;
		}

		public void selectStorageManager()
		{
			return;
		}

		public void SetSignAPK()
		{
			return;
		}

		public void DeleteAdsPlugins()
		{
			return;
		}

		public void ReCopyProject()
		{
			return;
		}

		public void UpdatePackageInManifest()
		{
			Utils.UpdateAndroidManifestPackageName(GetBundleIdentifierAndroid());
			GameSettings settings = GameSettings.Instance;
			if (settings != null && settings.AdsMediation != null)
			{
				settings.AdsMediation.SetMAXSDKKeyOnManifest();
				settings.AdsMediation.SetAdMobAppIdOnManifest();
			}
		}

		public void GetPackageInManifest()
		{
			Utils.GetAndroidManifestPackageName();
		}

		private void OnAdjustIdChanged()
		{
			return;
		}

		public void SetAdjustAppId()
		{
			return;
		}

		private void SaveBuild()
		{
			return;
		}

		private void Save()
		{
			return;
		}

		public void OpenAppStoreURL()
		{
			Utils.OpenUrlStore(GooglePlayId, AppStoreId);
		}

		public void SetSplashAndIconSettings()
		{
			return;
		}

		private void updateFacebookManifest()
		{
			return;
		}

		private ValueDropdownList<string> GetCultureList()
		{
			ValueDropdownList<string> result = new ValueDropdownList<string>();
			foreach (System.Globalization.CultureInfo culture in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures))
				result.Add(culture.Name, culture.DisplayName);
			return result;
		}

		private void updateRendering()
		{
			return;
		}

		private void setQualitySettings()
		{
			if (!EnforceQualitySettings)
				return;
			QualitySettings.masterTextureLimit = 0;
			QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			QualitySettings.antiAliasing = 0;
			QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Medium;
		}

		public GeneralEditor()
		{
			SignIOSTeamID = k_KobyTeamID;
			UpdateSplash = true;
			AutoSign = true;
			SignIOSTeamIDValues = new ValueDropdownList<string>
			{
				{ k_KobyTeamID + " - Koby", k_KobyTeamID },
				{ k_YakovTeamID + " - Yakov", k_YakovTeamID },
				{ k_VTeamID + " - V", k_VTeamID },
				{ k_Wak + " - Wak", k_Wak },
				{ k_Az + " - Az", k_Az },
				{ k_AzAI + " - AzAI", k_AzAI },
				{ k_BepCarlos + " - Bp Carlos", k_BepCarlos },
				{ k_BepMaria + " - Bp Maria", k_BepMaria },
				{ k_BepArtyom + " - Bp Artyom", k_BepArtyom },
				{ k_KpicGuy + " - Kpic Guy", k_KpicGuy }
			};
			MAXPathsToDelete = new[] { "/MaxSdk", "/MaxSdk.meta", "/Plugins/Android/MaxMediationGoogle", "/Plugins/Android/res", "/Plugins/Android/MaxMediationGoogle", "/Plugins/Android/MaxMediationGoogle.meta", "/Plugins/Android/MaxMediationGoogle.androidlib", "/Plugins/Android/MaxMediationGoogle.androidlib.meta", "/Plugins/Android/res", "/Plugins/Android/res.meta" };
			IronSourcePathsToDelete = new[] { "/IronSource", "/IronSource.meta", "/Plugins/Android/IronSource", "/Plugins/iOS/IronSource" };
			AdsPathsToDelete = new[] { "/_KobGamesSDK_Slim_Ads", "/_KobGamesSDK_Slim_Ads.meta", "/MaxSdk", "/MaxSdk.meta", "/IronSource", "/IronSource.meta", "/Plugins/Android/IronSource", "/Plugins/iOS/IronSource", "/Plugins/Android/MaxMediationGoogle", "/Plugins/Android/MaxMediationGoogle.meta", "/Plugins/Android/MaxMediationGoogle.androidlib", "/Plugins/Android/MaxMediationGoogle.androidlib.meta", "/Plugins/Android/res", "/Plugins/Android/res.meta" };
			m_PathToProjectUpdateBatchFile = "/_KobGamesSDK_Slim/BAT Scripts/ReCopyProjectFolders.bat";
			SetSpecificCulture = true;
			CultureName = "en-US";
			EnforceQualitySettings = true;
			m_DummySpacer = string.Empty;
		}
	}
}
