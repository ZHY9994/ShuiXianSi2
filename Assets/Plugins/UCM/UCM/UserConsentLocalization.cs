using UnityEngine;

namespace UCM
{
	[CreateAssetMenu(menuName = "Azur UCM/GDPRLocalization")]
	public class UserConsentLocalization : ScriptableObject
	{
		private const string EnglishIso = "en-US";

		public UserConsentLanguage[] localizations;

		private string currentLanguage;

		private int currentLanguageIndex;

		private void OnEnable()
		{
			currentLanguage = EnglishIso;
			currentLanguageIndex = 0;
		}

		public void SetLanguage(string languageIso)
		{
			currentLanguage = string.IsNullOrEmpty(languageIso) ? EnglishIso : languageIso;
			currentLanguageIndex = 0;
			if (localizations == null) return;
			for (int i = 0; i < localizations.Length; i++)
			{
				if (localizations[i] != null && string.Equals(localizations[i].languageIso, currentLanguage, System.StringComparison.OrdinalIgnoreCase))
				{
					currentLanguageIndex = i;
					return;
				}
			}
		}

		public bool IsOverflowRequired()
		{
			return GetLocalization()?.GoesOverflow() ?? false;
		}

		public bool GetIsHindi()
		{
			return GetLocalization()?.IsHindi() ?? false;
		}

		public bool GetIsThai()
		{
			return GetLocalization()?.IsThai() ?? false;
		}

		public string GetGDPRHeaderLocalized(bool fromSettings)
		{
			return GetLocalization()?.Header(fromSettings) ?? string.Empty;
		}

		public string GetGDPRDescriptionLocalized(string companyName, bool fromSettings)
		{
			return GetLocalization()?.Description(companyName, fromSettings) ?? string.Empty;
		}

		public string GetGDPRTermsButtonTextLocalized(bool forceNotNative)
		{
			return GetLocalization()?.Terms(forceNotNative) ?? string.Empty;
		}

		public string GetGDPRPrivacyButtonTextLocalized(bool forceNotNative)
		{
			return GetLocalization()?.Privacy(forceNotNative) ?? string.Empty;
		}

		public string GetAcceptText(bool fromSettings)
		{
			return GetLocalization()?.Accept(fromSettings) ?? string.Empty;
		}

		private UserConsentLanguage GetLocalization()
		{
			if (localizations == null || localizations.Length == 0) return null;
			if (currentLanguageIndex >= 0 && currentLanguageIndex < localizations.Length && localizations[currentLanguageIndex] != null) return localizations[currentLanguageIndex];
			return localizations[0];
		}

		private string GetLanguageIso()
		{
			return string.IsNullOrEmpty(currentLanguage) ? EnglishIso : currentLanguage;
		}

		private int AndroidApiVersion()
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			using (var version = new UnityEngine.AndroidJavaClass("android.os.Build$VERSION")) return version.GetStatic<int>("SDK_INT");
			#else
			return 0;
			#endif
		}

		public string GetCurrentLanguage()
		{
			return GetLanguageIso();
		}
	}
}
