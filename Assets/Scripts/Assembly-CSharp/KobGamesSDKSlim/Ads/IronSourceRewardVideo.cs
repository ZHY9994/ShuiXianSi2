using UnityEngine;

namespace KobGamesSDKSlim.Ads
{
	public class IronSourceRewardVideo : MonoBehaviour, IRewardVideo
	{
		private RewardVideoEvents m_RewardVideoEventsDummy;

		private bool m_IsRVSuccess;

		private bool m_Clicked;

		public static string ClassName => "IronSourceRewardVideo";

		public void Initialize()
		{
			return;
		}

		public bool IsRewardVideoAvailable()
		{
			return false;
		}

		public void ShowRewardVideo(RewardVideoEvents i_RewardVideoCallbacks, bool i_IsDummyAd)
		{
			return;
		}

		private void OnRewardedAdLoadedEvent(bool i_IsAvailable)
		{
			return;
		}

		private void OnRewardedAdFailedToDisplayEvent(IronSourceError i_IronSourceError)
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				if (m_RewardVideoEventsDummy == null)
					return;
				m_RewardVideoEventsDummy.OnFailed(new IAdNetworkError("100", "Dummy Description"));
				Utils.UnMuteAllAudioSources();
			});
		}

		private void OnRewardedAdDisplayedEvent()
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				Utils.MuteAllAudioSources();
				m_RewardVideoEventsDummy?.OnOpened();
			});
		}

		private void OnRewardedAdClickedEvent(IronSourcePlacement i_Obj)
		{
			m_Clicked = true;
		}

		private void OnRewardedAdReceivedRewardEvent(IronSourcePlacement i_Placement)
		{
			UnityMainThreadDispatcher.Instance().Enqueue(() =>
			{
				m_IsRVSuccess = true;
				m_RewardVideoEventsDummy?.OnSuccess((eVideoAdsSuccessResult)(m_Clicked ? 1 : 0));
			});
		}

		private void OnRewardedAdDismissedEvent()
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
			OnRewardedAdReceivedRewardEvent(new IronSourcePlacement("Simulated", "Simulated", 100));
			OnRewardedAdDismissedEvent();
		}

		public void SimulateFailedRewardVideoLoad()
		{
			if (m_RewardVideoEventsDummy == null)
			{
				m_RewardVideoEventsDummy = new RewardVideoEvents();
				m_RewardVideoEventsDummy.InjectCallBacks(AdsManager.RewardVideoEventsGlobal, true);
			}
			OnRewardedAdFailedToDisplayEvent(new IronSourceError(100, "Simulated RewardVideo Error"));
			OnRewardedAdDismissedEvent();
		}
	}
}
