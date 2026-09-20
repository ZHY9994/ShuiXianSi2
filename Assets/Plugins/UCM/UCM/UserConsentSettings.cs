using System.Collections.Generic;
using UCM.Steps;
using UnityEngine;

namespace UCM
{
	[CreateAssetMenu(menuName = "Azur UCM/Settings", fileName = "UserConsentSettings")]
	public class UserConsentSettings : ScriptableObject
	{
		private const string GdprSettingsFileName = "UserConsentSettings";

		private const string GdprSettingsDefaultsFileName = "UserConsentDefaultSettings";

		private const string FirebaseDefine = "FIREBASE_ANALYTICS";

		[SerializeField]
		private string usercentricsId;

		[SerializeField]
		private string termsLink;

		[SerializeField]
		private string privacyLink;

		[SerializeField]
		private bool isUsingFirebaseAnalytics;

		[Tooltip("Use base value if empty")]
		[InspectorName("Terms Link Ios*")]
		[SerializeField]
		private string termsLinkIos;

		[Tooltip("Use base value if empty")]
		[InspectorName("Privacy Link Ios*")]
		[SerializeField]
		private string privacyLinkIos;

		[SerializeField]
		private string companyName;

		[SerializeField]
		private Sprite companyLogo;

		[SerializeField]
		private Sprite sharkLogo;

		[SerializeField]
		private GameObject popupPrefab;

		[Space(10f)]
		[SerializeField]
		private string[] supportedCountryCodes;

		[Space(10f)]
		[SerializeField]
		private InitStep initStep;

		[SerializeField]
		private PrewarmedAttStep prewarmedAttUcmStep;

		[SerializeField]
		private SettingsCmpStep settingsCmpStep;

		[Space(10f)]
		[SerializeField]
		private List<BaseStep> steps;

		public GameObject PopupPrefab => popupPrefab;

		public string TermsLink => termsLink;

		public string PrivacyLink => privacyLink;

		public string CompanyName => companyName;

		public Sprite CompanyLogo => companyLogo;

		public Sprite SharkLogo => sharkLogo;

		public string UsercentricsId => usercentricsId;

		public List<BaseStep> Steps => steps;

		public InitStep InitUcmStep => initStep;

		public PrewarmedAttStep PrewarmedAttUcmStep => prewarmedAttUcmStep;

		public SettingsCmpStep SettingsCmpStep => settingsCmpStep;

		public bool IsCmpCountry(string locationCountryCode)
		{
			if (supportedCountryCodes == null || supportedCountryCodes.Length == 0 || locationCountryCode == null) return false;
			string country = locationCountryCode.ToLowerInvariant();
			foreach (string supportedCountryCode in supportedCountryCodes)
				if (country == supportedCountryCode) return true;
			return false;
		}
	}
}
