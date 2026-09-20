using UnityEngine;

namespace KobGamesSDKSlim.Ads
{
	public class MAXBanner : MonoBehaviour, IBanner
	{
		private BannerEvents m_BannerEventsDummy;

		private bool m_IsBannerRequested;

		public static string ClassName => "MAXBanner";

		private string m_BannerAdUnitId => Application.platform == RuntimePlatform.Android ? GameSettings.Instance.AdsMediation.MAXBannerAdUnitIdAndroid : GameSettings.Instance.AdsMediation.MAXBannerAdUnitIdIOS;

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

		private void OnBannerAdLoadFailedEvent(string i_AdUnitId, object i_ErrorInfo)
		{
			if (m_BannerEventsDummy != null)
				m_BannerEventsDummy.OnFailed(new IAdNetworkError("100", i_AdUnitId));
		}

		private void BannerAdScreenDismissedEvent()
		{
			m_BannerEventsDummy?.OnHidden();
		}

		private void BannerAdDestroyedEvent()
		{
			m_BannerEventsDummy?.OnDestroyed();
			m_BannerEventsDummy?.OnHidden();
		}

		private void OnBannerAdScreenPresentedEvent(string i_AdUnitId)
		{
			m_BannerEventsDummy?.OnShown();
		}

		public void SimulateFailedBannerLoad()
		{
			debugPrepareDummy();
			OnBannerAdLoadFailedEvent(m_BannerAdUnitId, null);
		}

		private void debugPrepareDummy()
		{
			if (m_BannerEventsDummy == null)
				m_BannerEventsDummy = new BannerEvents();
		}

		public void OnApplicationPause(bool i_Pause)
		{
			return;
		}
	}
}
