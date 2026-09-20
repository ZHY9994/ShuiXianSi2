using UnityEngine;

namespace KobGamesSDKSlim.Ads
{
	public class IronSourceInterstitial : MonoBehaviour, IInterstitial
	{
		private InterstitialEvents m_InterstitialEventsDummy;

		public string AndroidPlacementId;

		public string IOSPlacementId;

		private bool m_Clicked;

		public static string ClassName => "IronSourceInterstitial";

		public string PlatformPlacementId => AndroidPlacementId;

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

		private void OnInterstitialLoadedEvent()
		{
			return;
		}

		private void OnInterstitialLoadFailedEvent(IronSourceError i_IronSourceError)
		{
			return;
		}

		private void OnInterstitialAdShowFailedEvent(IronSourceError i_IronSourceError)
		{
			if (m_InterstitialEventsDummy != null)
				m_InterstitialEventsDummy.OnShownFailed(new IAdNetworkError(i_IronSourceError.getErrorCode(), "Dummy Description"));
		}

		private void OnInterstitialAdOpenedEvent()
		{
			m_InterstitialEventsDummy?.OnOpened();
		}

		private void OnInterstitialAdClickedEvent()
		{
			m_Clicked = true;
		}

		private void OnInterstitialAdShowSucceededEvent()
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				m_InterstitialEventsDummy?.OnShownSuccess((eVideoAdsSuccessResult)(m_Clicked ? 1 : 0));
			});
		}

		private void OnInterstitialAdClosedEvent()
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				m_InterstitialEventsDummy?.OnClosed();
				Utils.UnMuteAllAudioSources();
			});
		}

		public void SimulateShownSuccessAndClose()
		{
			if (m_InterstitialEventsDummy == null)
			{
				m_InterstitialEventsDummy = new InterstitialEvents();
				m_InterstitialEventsDummy.InjectCallBacks(AdsManager.InterstitialEventsGlobal, true);
			}
			OnInterstitialAdShowSucceededEvent();
			OnInterstitialAdClosedEvent();
		}

		public void SimulateFailedInterstitialLoad()
		{
			if (m_InterstitialEventsDummy == null)
			{
				m_InterstitialEventsDummy = new InterstitialEvents();
				m_InterstitialEventsDummy.InjectCallBacks(AdsManager.InterstitialEventsGlobal, true);
			}
			new IronSourceError(100, "Simulated Interstitial Error");
		}

		public IronSourceInterstitial()
		{
			AndroidPlacementId = "interstitial_android";
			IOSPlacementId = "interstitial_ios";
		}
	}
}
