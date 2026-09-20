using System.Collections.Generic;
using Unity.Usercentrics;

namespace UCM
{
	public class UserConsentContext
	{
		private const string PlayerPrefsConsentKey = "cons";

		private const string PlayerPrefsAttKey = "consatt";

		private const string PlayerPrefsCmpKey = "conscmp";

		public List<UsercentricsServiceConsent> CmpConsents;

		public bool AttEverAnswered => UnityEngine.PlayerPrefs.HasKey(PlayerPrefsAttKey);

		public bool GdprEverAnswered => UnityEngine.PlayerPrefs.HasKey(PlayerPrefsConsentKey);

		public bool CmpEverShown => UnityEngine.PlayerPrefs.HasKey(PlayerPrefsCmpKey);

		public bool GdprConsent
		{
			get
			{
				return UnityEngine.PlayerPrefs.GetInt(PlayerPrefsConsentKey, 0) > 0;
			}
			set
			{
				UnityEngine.PlayerPrefs.SetInt(PlayerPrefsConsentKey, value ? 1 : 0);
				UnityEngine.PlayerPrefs.Save();
			}
		}

		public UsercentricsReadyStatus UsercentricsReadyStatus { get; set; }

		public bool CmpInitializationError { get; set; }

		public void SetAttAnswered()
		{
			UnityEngine.PlayerPrefs.SetInt(PlayerPrefsAttKey, 1);
			UnityEngine.PlayerPrefs.Save();
		}

		public void SetCmpAnswered()
		{
			UnityEngine.PlayerPrefs.SetInt(PlayerPrefsCmpKey, 1);
			UnityEngine.PlayerPrefs.Save();
		}

		public UserConsentContext()
		{
			CmpConsents = new List<UsercentricsServiceConsent>();
		}
	}
}
