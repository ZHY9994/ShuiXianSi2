using UnityEngine;

namespace UCM
{
	public class TestUserConsent : MonoBehaviour
	{
		public GameObject buttonSettings;

		private UserConsentLocalization _userConsentLocalization;

		private const string GDPRLocalizationFileName = "UserConsentLocalization";

		private void Start()
		{
			_userConsentLocalization = Resources.Load<UserConsentLocalization>(GDPRLocalizationFileName);
		}

		public void CheckAtt()
		{
			UserConsentManager.Instance.CheckATTStatus(status => Debug.Log("ATT status: " + status));
		}

		public void ShowAtt()
		{
			_ = UserConsentManager.Instance.ShowAtt();
		}

		public void ShowCMP()
		{
			_ = UserConsentManager.Instance.TryShowCmpIfNeverShown();
		}

		public void ShowCMPFromSettings()
		{
			_ = UserConsentManager.Instance.ShowCmpFromSettings();
		}

		private void Update()
		{
			return;
		}
	}
}
