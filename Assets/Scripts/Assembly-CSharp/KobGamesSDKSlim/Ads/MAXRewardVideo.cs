using UnityEngine;

namespace KobGamesSDKSlim.Ads
{
	public class MAXRewardVideo : MonoBehaviour, IRewardVideo
	{
		private RewardVideoEvents m_RewardVideoEventsDummy;

		private bool m_IsRVSuccess;

		private int m_RetryAttempt;

		private bool m_Clicked;

		private double m_RetryDelay;

		public static string ClassName => "MAXRewardVideo";

		private string m_RewardVideoAdUnitId => Application.platform == RuntimePlatform.Android ? GameSettings.Instance.AdsMediation.MAXRewardVideoAdUnitIdAndroid : GameSettings.Instance.AdsMediation.MAXRewardVideoAdUnitIdIOS;

		public void Initialize()
		{
			return;
		}

		private void LoadRewardVideo()
		{
			return;
		}

		public bool IsRewardVideoAvailable()
		{
			return false;
		}

		public void ShowRewardVideo(RewardVideoEvents i_RewardVideoCallbacks, bool i_IsDummyAd = false)
		{
			return;
		}

		private void OnRewardedAdLoadedEvent(string adUnitId, object i_AdInfo)
		{
			m_RetryAttempt = 0;
		}

		private void OnRewardedAdLoadFailedEvent(string adUnitId, object i_ErrorInfo)
		{
			return;
		}

		private void OnRewardedAdFailedToDisplayEvent(string adUnitId, object i_ErrorInfo, object i_AdInfo)
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				m_RewardVideoEventsDummy?.OnFailed(new IAdNetworkError("100", adUnitId));
				Utils.UnMuteAllAudioSources();
			});
		}

		private void OnRewardedAdOpenEvent()
		{
			m_RewardVideoEventsDummy?.OnOpen();
		}

		private void OnRewardedAdDisplayedEvent(string adUnitId, object i_AdInfo)
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				Utils.MuteAllAudioSources();
				m_RewardVideoEventsDummy?.OnOpened();
			});
		}

		private void OnRewardedAdClickedEvent(string adUnitId, object i_AdInfo)
		{
			m_Clicked = true;
		}

		private void OnRewardedAdReceivedRewardEvent(string adUnitId, object reward, object i_AdInfo)
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				m_IsRVSuccess = true;
				m_RewardVideoEventsDummy?.OnSuccess((eVideoAdsSuccessResult)(m_Clicked ? 1 : 0));
			});
		}

		private void OnRewardedAdDismissedEvent(string adUnitId, object i_AdInfo)
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				m_RewardVideoEventsDummy?.OnClosed(m_IsRVSuccess);
				m_IsRVSuccess = false;
				Utils.UnMuteAllAudioSources();
			}, 0.05f);
		}

		public void SimulateShownSuccessAndClose()
		{
			if (m_RewardVideoEventsDummy == null)
			{
				m_RewardVideoEventsDummy = new RewardVideoEvents();
				m_RewardVideoEventsDummy.InjectCallBacks(AdsManager.RewardVideoEventsGlobal, true);
			}
			OnRewardedAdReceivedRewardEvent(m_RewardVideoAdUnitId, null, null);
			OnRewardedAdDismissedEvent(m_RewardVideoAdUnitId, null);
		}

		public void SimulateFailedRewardVideoLoad()
		{
			if (m_RewardVideoEventsDummy == null)
			{
				m_RewardVideoEventsDummy = new RewardVideoEvents();
				m_RewardVideoEventsDummy.InjectCallBacks(AdsManager.RewardVideoEventsGlobal, true);
			}
			OnRewardedAdFailedToDisplayEvent(m_RewardVideoAdUnitId, null, null);
			OnRewardedAdDismissedEvent(m_RewardVideoAdUnitId, null);
		}
	}
}
