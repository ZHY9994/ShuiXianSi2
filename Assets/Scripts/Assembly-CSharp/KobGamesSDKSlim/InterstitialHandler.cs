using System;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class InterstitialHandler
	{
		public enum eInterstitialType
		{
			on_win = 0,
			on_lose = 1,
			time_based = 2,
			on_launch = 3,
			debug = 4
		}

		private const int k_LabelWidth = 320;

		private int m_InterstitialLevelFailedCounter = -1;

		private int m_InterstitialLevelCompleteCounter;

		private bool m_IsInterstitialPendingToShow;

		private eInterstitialType m_PendingInterstitialType;

		private bool m_WonLevel;

		public int CurrentLevel => Managers.Instance != null && Managers.Instance.Analytics != null ? Managers.Instance.Analytics.CurrentLevel : 0;

		private string m_LevelFinishedPlacement => m_WonLevel ? eInterstitialType.on_win.ToString() : eInterstitialType.on_lose.ToString();

		public int InterstitialLevelFailedCounter
		{
			get
			{
				if (m_InterstitialLevelFailedCounter < 0) m_InterstitialLevelFailedCounter = IsInterstitialRemoteLevelFailedCounterPositive ? 1 : 0;
				return m_InterstitialLevelFailedCounter;
			}
			set
			{
				m_InterstitialLevelFailedCounter = value;
			}
		}

		public int InterstitialLevelCompleteCounter
		{
			get
			{
				if (m_InterstitialLevelCompleteCounter < 0) m_InterstitialLevelCompleteCounter = IsInterstitialRemoteLevelCompletedCounterPositive ? 1 : 0;
				return m_InterstitialLevelCompleteCounter;
			}
			set
			{
				m_InterstitialLevelCompleteCounter = value;
			}
		}

		public bool IsInterstitialRemotelyEnabled => GameConfig.Instance != null && GameConfig.Instance.AdsMediation != null && GameConfig.Instance.AdsMediation.IsInterstitialEnabled;

		public bool IsInterstitialRemoteLevelFailedCounterPositive => GameConfig.Instance != null && GameConfig.Instance.AdsMediation != null && GameConfig.Instance.AdsMediation.InterstitialMinLevel > 0;

		public bool IsInterstitialRemoteLevelCompletedCounterPositive => GameConfig.Instance != null && GameConfig.Instance.AdsMediation != null && GameConfig.Instance.AdsMediation.InterstitialMinLevel > 0;

		public int InterstitialCoolDown => GameConfig.Instance?.AdsMediation?.InterstitialCoolDown ?? 0;

		public int InterstitialMinLevel => GameConfig.Instance?.AdsMediation?.InterstitialMinLevel ?? 0;

		public int InterstitialCoolDownCounter => Managers.Instance?.Storage?.InterstitialCoolDownCounter ?? 0;

		public bool IsInterstitialCoolDownSatisfied => InterstitialCoolDownCounter < 1;

		public void TryShowInterstitial(eInterstitialType i_InterstitialType, bool i_WonLevel)
		{
			return;
		}

		private void tryShowInterstitialTimedBased()
		{
			return;
		}

		private void tryShowInterstitial(eInterstitialType i_InterstitialType)
		{
			return;
		}

		private void showPendingInterstitial()
		{
			m_IsInterstitialPendingToShow = false;
		}

		private bool IsInterstitialRemoteCounterPositive(eInterstitialType i_InterstitialType)
		{
			return i_InterstitialType == eInterstitialType.on_win ? IsInterstitialRemoteLevelCompletedCounterPositive : IsInterstitialRemoteLevelFailedCounterPositive;
		}

		private void SetInterstitialCounter(eInterstitialType i_InterstitialType, int i_Counter)
		{
			if (i_InterstitialType == eInterstitialType.on_win) InterstitialLevelCompleteCounter = i_Counter;
			else if (i_InterstitialType == eInterstitialType.on_lose) InterstitialLevelFailedCounter = i_Counter;
		}

		private void DecreaseInterstitialCounter(eInterstitialType i_InterstitialType)
		{
			SetInterstitialCounter(i_InterstitialType, Math.Max(0, GetInterstitialCounter(i_InterstitialType) - 1));
		}

		private int GetInterstitialCounter(eInterstitialType i_InterstitialType)
		{
			return i_InterstitialType == eInterstitialType.on_win ? InterstitialLevelCompleteCounter : InterstitialLevelFailedCounter;
		}
	}
}
