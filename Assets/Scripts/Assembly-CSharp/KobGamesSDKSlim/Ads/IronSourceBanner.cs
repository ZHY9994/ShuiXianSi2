using UnityEngine;

namespace KobGamesSDKSlim.Ads
{
	public class IronSourceBanner : MonoBehaviour, IBanner
	{
		private BannerEvents m_BannerEventsDummy;

		private bool m_IsBannerRequested;

		public static string ClassName => "IronSourceBanner";

		private string m_BannerAdUnitId => Application.platform == RuntimePlatform.Android ? GameSettings.Instance.AdsMediation.IronSourceAndroidID : GameSettings.Instance.AdsMediation.IronSourceAppleID;

		public void Initialize()
		{
			return;
		}

		public void ShowBanner(BannerEvents i_BannerEvents)
		{
			return;
		}

		public void ShowBanner()
		{
			return;
		}

		public void HideBanner()
		{
			return;
		}

		public void DestroyBanner()
		{
			return;
		}

		private void BannerAdLoadFailedEvent(IronSourceError i_IronSourceError)
		{
			if (m_BannerEventsDummy == null) return;
			m_BannerEventsDummy.OnFailed(new IAdNetworkError(i_IronSourceError.getErrorCode().ToString(), "Dummy Description"));
		}

		private void BannerAdScreenDismissedEvent()
		{
			m_BannerEventsDummy?.OnHidden();
		}

		private void BannerAdDestroyedEvent()
		{
			if (m_BannerEventsDummy == null) return;
			m_BannerEventsDummy.OnDestroyed();
			m_BannerEventsDummy.OnHidden();
		}

		private void OnBannerAdScreenPresentedEvent(string i_AdUnitId)
		{
			m_BannerEventsDummy?.OnShown();
		}

		public void SimulateFailedBannerLoad()
		{
			debugPrepareDummy();
			var error = new IronSourceError(100, "Simulated Banner Error");
			BannerAdLoadFailedEvent(error);
		}

		private void debugPrepareDummy()
		{
			if (m_BannerEventsDummy == null) m_BannerEventsDummy = new BannerEvents();
		}

		public void OnApplicationPause(bool i_Pause)
		{
			return;
		}
	}
}
