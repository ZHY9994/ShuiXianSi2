using UnityEngine;
using System;

namespace KobGamesSDKSlim.Ads
{
	public class MAXInterstitial : MonoBehaviour, IInterstitial
	{
		private InterstitialEvents m_InterstitialEventsDummy;

		private int m_RetryAttempt;

		public string AndroidPlacementId;

		public string IOSPlacementId;

		private bool m_Clicked;

		private double m_RetryDelay;

		public static string ClassName => "MAXInterstitial";

		public string PlatformPlacementId => Application.platform == RuntimePlatform.Android ? AndroidPlacementId : IOSPlacementId;

		private string m_InterstitialAdUnitId => Application.platform == RuntimePlatform.Android ? GameSettings.Instance.AdsMediation.MAXInterstitialAdUnitIdAndroid : GameSettings.Instance.AdsMediation.MAXInterstitialAdUnitIdIOS;

		public void Initialize()
		{
			return;
		}

		public void LoadInterstitial()
		{
			return;
		}

		public bool IsInterstitialAvailable()
		{
			return false;
		}

		public void ShowInterstitial(InterstitialEvents i_InterstitialEvents, bool i_IsDummyAd = false)
		{
			return;
		}

		private void OnInterstitialLoadedEvent(string adUnitId, object i_AdInfo)
		{
			m_RetryAttempt = 0;
		}

		private void OnInterstitialLoadFailedEvent(string adUnitId, object i_ErrorInfo)
		{
			return;
		}

		private void OnInterstitialAdShowFailedEvent(string adUnitId, object i_ErrorInfo, object i_AdInfo)
		{
			m_InterstitialEventsDummy?.OnShownFailed(new IAdNetworkError("100", adUnitId));
		}

		private void OnInterstitialAdOpenedEvent(string adUnitId)
		{
			m_InterstitialEventsDummy?.OnOpened();
		}

		private void onAdClickedEvent(string i_Arg1, object i_Arg2)
		{
			m_Clicked = true;
		}

		private void OnInterstitialAdShowSucceededEvent(string adUnitId, object i_AdInfo)
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() => m_InterstitialEventsDummy?.OnShownSuccess((eVideoAdsSuccessResult)(m_Clicked ? 1 : 0)));
		}

		private void OnInterstitialAdClosedEvent(string adUnitId, object i_AdInfo)
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() => { m_InterstitialEventsDummy?.OnClosed(); Utils.UnMuteAllAudioSources(); });
		}

		public void SimulateShownSuccessAndClose()
		{
			if (m_InterstitialEventsDummy == null) { m_InterstitialEventsDummy = new InterstitialEvents(); m_InterstitialEventsDummy.InjectCallBacks(AdsManager.InterstitialEventsGlobal, true); }
			OnInterstitialAdShowSucceededEvent(m_InterstitialAdUnitId, null);
			OnInterstitialAdClosedEvent(m_InterstitialAdUnitId, null);
		}

		public void SimulateFailedInterstitialLoad()
		{
			if (m_InterstitialEventsDummy == null) { m_InterstitialEventsDummy = new InterstitialEvents(); m_InterstitialEventsDummy.InjectCallBacks(AdsManager.InterstitialEventsGlobal, true); }
			OnInterstitialLoadFailedEvent(m_InterstitialAdUnitId, null);
		}

		public MAXInterstitial()
		{
			AndroidPlacementId = "interstitial_android";
			IOSPlacementId = "interstitial_ios";
		}
	}
}
