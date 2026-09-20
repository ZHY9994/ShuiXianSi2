using System;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-65)]
	public abstract class LevelManagerBase : Singleton<LevelManager>
	{
		protected string m_GameMode;

		private int m_ContinueAmount;

		private DateTime m_StartLevelTime;

		private int m_LevelCumulativeDuration;

		private bool m_PauseStatus;

		public abstract Level CurrentLevel { get; }

		public LevelData CurrentLevelData => CurrentLevel != null && CurrentLevel.LevelData != null ? CurrentLevel.LevelData : new LevelData();

		protected abstract int m_CurrentLevelProgress { get; }

		protected virtual void setRefs()
		{
			return;
		}

		public sealed override void Start()
		{
			base.Start();
			startImpl();
		}

		protected virtual void startImpl()
		{
			return;
		}

		protected sealed override void OnEnable()
		{
			base.OnEnable();
			GameManagerV1.GameManagerBase.OnLevelLoadStarted += gameManager_OnLevelLoadStarted;
			GameManagerV1.GameManagerBase.OnLevelStartedEvent += gameManager_OnLevelStart;
			GameManagerV1.GameManagerBase.OnLevelFailedEvent += gameManager_OnLevelFailedEvent;
			GameManagerV1.GameManagerBase.OnLevelFailedNoContinueEvent += gameManager_OnLevelFailedNoContinueEvent;
			GameManagerV1.GameManagerBase.OnLevelContinueEvent += gameManager_OnLevelContinueEvent;
			GameManagerV1.GameManagerBase.OnLevelCompletedEvent += gameManager_OnLevelCompletedEvent;
			onEnableImpl();
		}

		protected virtual void onEnableImpl()
		{
			return;
		}

		public sealed override void OnDisable()
		{
			GameManagerV1.GameManagerBase.OnLevelLoadStarted -= gameManager_OnLevelLoadStarted;
			GameManagerV1.GameManagerBase.OnLevelStartedEvent -= gameManager_OnLevelStart;
			GameManagerV1.GameManagerBase.OnLevelFailedEvent -= gameManager_OnLevelFailedEvent;
			GameManagerV1.GameManagerBase.OnLevelFailedNoContinueEvent -= gameManager_OnLevelFailedNoContinueEvent;
			GameManagerV1.GameManagerBase.OnLevelContinueEvent -= gameManager_OnLevelContinueEvent;
			GameManagerV1.GameManagerBase.OnLevelCompletedEvent -= gameManager_OnLevelCompletedEvent;
			onDisableImpl();
			base.OnDisable();
		}

		protected virtual void onDisableImpl()
		{
			return;
		}

		protected sealed override void OnApplicationFocus(bool i_IsFocused)
		{
			base.OnApplicationFocus(i_IsFocused);
			pauseStatus(!i_IsFocused);
			onApplicationFocusImpl(i_IsFocused);
		}

		protected virtual void onApplicationFocusImpl(bool i_FocusStatus)
		{
			return;
		}

		protected sealed override void OnApplicationPause(bool i_IsPaused)
		{
			base.OnApplicationPause(i_IsPaused);
			pauseStatus(i_IsPaused);
			onApplicationPauseImpl(i_IsPaused);
		}

		protected virtual void onApplicationPauseImpl(bool i_PauseStatus)
		{
			return;
		}

		private void pauseStatus(bool i_PausedStatus)
		{
			if (m_PauseStatus == i_PausedStatus) return;
			if (StorageManager.Instance == null || StorageManager.Instance.DefaultStartlevel != 1)
			{
				m_PauseStatus = i_PausedStatus;
				return;
			}
			if (i_PausedStatus)
			{
				addComulativeTime();
				LevelData data = CurrentLevelData;
				if (data != null)
				{
					data.SetMandatoryDataOnEnd(m_LevelCumulativeDuration, Math.Max(0, Math.Min(100, m_CurrentLevelProgress)), m_ContinueAmount);
					if (StorageManager.Instance != null) StorageManager.Instance.OnGameClosedLevelData = data;
				}
			}
			else
			{
				if (StorageManager.Instance != null) StorageManager.Instance.OnGameClosedLevelData = null;
				m_StartLevelTime = DateTime.Now;
			}
			m_PauseStatus = i_PausedStatus;
		}

		private void gameManager_OnLevelLoadStarted()
		{
			m_ContinueAmount = 0;
			m_LevelCumulativeDuration = 0;
			setNextLevelData();
			loadLevelImpl();
		}

		private void gameManager_OnLevelStart(LevelData i_LevelData)
		{
			m_StartLevelTime = DateTime.Now;
		}

		private void gameManager_OnLevelFailedEvent(LevelData i_LevelData)
		{
			addComulativeTime();
		}

		private void gameManager_OnLevelFailedNoContinueEvent(LevelData i_LevelData)
		{
			addComulativeTime();
			LevelData data = CurrentLevelData;
			data?.SetMandatoryDataOnEnd(m_LevelCumulativeDuration, Math.Max(0, Math.Min(100, m_CurrentLevelProgress)), m_ContinueAmount);
		}

		private void gameManager_OnLevelContinueEvent(LevelData i_LevelData)
		{
			m_ContinueAmount++;
			m_StartLevelTime = DateTime.Now;
		}

		private void gameManager_OnLevelCompletedEvent(LevelData i_LevelData)
		{
			addComulativeTime();
			CurrentLevelData?.SetMandatoryDataOnEnd(m_LevelCumulativeDuration, 100, m_ContinueAmount);
		}

		private void loadLevel()
		{
			m_ContinueAmount = 0;
			m_LevelCumulativeDuration = 0;
			setNextLevelData();
			loadLevelImpl();
		}

		private void setNextLevelData()
		{
			setNextLevelAndDataImpl();
			LevelData data = CurrentLevelData;
			if (data == null) throw new NullReferenceException(nameof(CurrentLevelData));
			data.SetMandatoryDataOnStart(m_GameMode, StorageManager.Instance.CurrentLevel);
		}

		protected abstract void setNextLevelAndDataImpl();

		protected virtual void loadLevelImpl()
		{
			return;
		}

		private void addComulativeTime()
		{
			TimeSpan elapsed = DateTime.Now - m_StartLevelTime;
			m_LevelCumulativeDuration += (int)elapsed.TotalSeconds;
		}

		public LevelManagerBase()
		{
			m_GameMode = "normal";
		}
	}
}
