using System;
using KobGamesSDKSlim.Ads;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public class AdsDebugButtons : MonoBehaviour
	{
		public AdsDebugButtons()
		{
			ClickCounterDefault = 6;
			AllowedLastClickDeltaTime = 2f;
			ClickCounter = -1;
			m_TimeFromLastClick = -1f;
			m_ForceDummyAdsFormat = "{0}";
		}
		public int ClickCounterDefault;

		public float AllowedLastClickDeltaTime;

		[NonSerialized]
		public int ClickCounter;

		private string m_ForceDummyAdsFormat;

		public Text ForceDummyAds;

		private float m_TimeFromLastClick;

		private float m_TimeFromLastClickDelta => Time.realtimeSinceStartup - m_TimeFromLastClick;

		private void Awake()
		{
			gameObject.SetActive(false);
			if (ForceDummyAds != null && m_ForceDummyAdsFormat == ForceDummyAds.text)
				m_ForceDummyAdsFormat = ForceDummyAds.text;
		}

		private void OnEnable()
		{
			if (ForceDummyAds != null)
			{
				var mediation = SingletonScriptableObject<GameConfig>.Instance?.AdsMediation;
				if (mediation != null)
					ForceDummyAds.text = string.Format(m_ForceDummyAdsFormat, mediation.UseDummyAdsOnDevice);
			}
		}

		public void TryOpenTestSuiteClicked()
		{
			if (m_TimeFromLastClick >= 0f && m_TimeFromLastClickDelta <= AllowedLastClickDeltaTime)
			{
				ClickCounter--;
				if (ClickCounter < 0 || ClickCounter == 0)
					open();
			}
			else
			{
				m_TimeFromLastClick = Time.realtimeSinceStartup;
				ClickCounter = ClickCounterDefault - 1;
			}
			m_TimeFromLastClick = Time.realtimeSinceStartup;
		}

		private void open()
		{
			gameObject.SetActive(true);
			ClickCounter = ClickCounterDefault;
			Managers.Instance.Analytics.LogEvent("TestSuite", "ShowingAdsDebugButtons", "None");
		}

		public void Close()
		{
			gameObject.SetActive(false);
			ClickCounter = ClickCounterDefault;
		}

		public void ShowMediationDebugPanel()
		{
			return;
		}

		public void ToggleForceDummyAds(TextMeshProUGUI i_Text)
		{
			var mediation = SingletonScriptableObject<GameConfig>.Instance?.AdsMediation;
			if (mediation == null || i_Text == null)
				return;
			mediation.ToggleForceUseDummyAdsOnDevice();
			i_Text.text = string.Format(m_ForceDummyAdsFormat, mediation.UseDummyAdsOnDevice);
		}

		public void TestShowBanner()
		{
			return;
		}

		public void TestFailBanner()
		{
			return;
		}

		public void TestHideBanner()
		{
			return;
		}

		public void TestDestroyBanner()
		{
			return;
		}

		public void TestLoadInterstitial()
		{
			return;
		}

		public void TestShowInterstitial()
		{
			return;
		}

		public void TestFailInterstitial()
		{
			return;
		}

		public void TestShowRewardVideo()
		{
			ADManager.Instance.ShowRewardAD(() => { });
		}

		public void TestFailShowRewardVideo()
		{
			return;
		}
	}
}
