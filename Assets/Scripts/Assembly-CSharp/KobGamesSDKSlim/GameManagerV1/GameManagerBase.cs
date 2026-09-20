using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KobGamesSDKSlim.GameManagerV1
{
	[ExecutionOrder(-70)]
	public class GameManagerBase : Singleton<GameManager>
	{
		public delegate void GameEvent();

		public delegate void LevelEvent(LevelData i_LevelData);

		[Obsolete("Use LevelEvent instead")]
		public delegate void LevelEventObsolete();

		public delegate void OnScreenChangedEvent(int i_Width, int i_Height);

		private static GameEvent m_OnGameReset = delegate { };
		private static GameEvent m_OnGamePause = delegate { };
		private static GameEvent m_OnGameUnPause = delegate { };
		private static GameEvent m_OnLevelReset = delegate { };
		private static LevelEvent m_OnGameAwakeAfterGameClosed = delegate { };
		private static LevelEvent m_OnLevelContinueEvent = delegate { };
		private static LevelEvent m_OnLevelStartedEvent = delegate { };
		private static LevelEvent m_OnLevelCompletedEvent = delegate { };
		private static LevelEvent m_OnLevelLoadedEvent = delegate { };
		private static LevelEvent m_OnLevelFailedEvent = delegate { };
		private static LevelEvent m_OnLevelFailedNoContinueEvent = delegate { };
		private static LevelEventObsolete m_OnLevelLoadStarted = delegate { };
		private static LevelEventObsolete m_OnLevelContinue = delegate { };
		private static LevelEventObsolete m_OnLevelStarted = delegate { };
		private static LevelEventObsolete m_OnLevelCompleted = delegate { };
		private static LevelEventObsolete m_OnLevelLoaded = delegate { };
		private static LevelEventObsolete m_OnLevelFailed = delegate { };
		private static LevelEventObsolete m_OnLevelFailedNoContinue = delegate { };
		private static OnScreenChangedEvent m_OnScreenChanged = delegate { };

		public eGameState GameState;

		public int CurrContinueAmount;

		private Vector2Int m_OriginalRes;

		public bool ShowLogs;

		public int MaxContinueAmount
		{
			get
			{
				return Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.MaxContinueAmount?.Value ?? 0;
			}
			set
			{
				Managers.Instance?.RemoteSettings?.FirebaseRemoteConfig?.MaxContinueAmount?.SetToDefaultValue(value, true);
			}
		}

		public static event GameEvent OnGameReset
		{
			[CompilerGenerated]
			add
			{
				GameEvent current; GameEvent updated;
				do { current = m_OnGameReset; updated = (GameEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnGameReset, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				GameEvent current; GameEvent updated;
				do { current = m_OnGameReset; updated = (GameEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnGameReset, updated, current) != current);
			}
		}

		public static event GameEvent OnGamePause
		{
			[CompilerGenerated]
			add
			{
				GameEvent current; GameEvent updated;
				do { current = m_OnGamePause; updated = (GameEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnGamePause, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				GameEvent current; GameEvent updated;
				do { current = m_OnGamePause; updated = (GameEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnGamePause, updated, current) != current);
			}
		}

		public static event GameEvent OnGameUnPause
		{
			[CompilerGenerated]
			add
			{
				GameEvent current; GameEvent updated;
				do { current = m_OnGameUnPause; updated = (GameEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnGameUnPause, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				GameEvent current; GameEvent updated;
				do { current = m_OnGameUnPause; updated = (GameEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnGameUnPause, updated, current) != current);
			}
		}

		public static event GameEvent OnLevelReset
		{
			[CompilerGenerated]
			add
			{
				GameEvent current; GameEvent updated;
				do { current = m_OnLevelReset; updated = (GameEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelReset, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				GameEvent current; GameEvent updated;
				do { current = m_OnLevelReset; updated = (GameEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelReset, updated, current) != current);
			}
		}

		public static event LevelEvent OnGameAwakeAfterGameClosed
		{
			[CompilerGenerated]
			add
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnGameAwakeAfterGameClosed; updated = (LevelEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnGameAwakeAfterGameClosed, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnGameAwakeAfterGameClosed; updated = (LevelEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnGameAwakeAfterGameClosed, updated, current) != current);
			}
		}

		public static event LevelEvent OnLevelContinueEvent
		{
			[CompilerGenerated]
			add
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelContinueEvent; updated = (LevelEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelContinueEvent, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelContinueEvent; updated = (LevelEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelContinueEvent, updated, current) != current);
			}
		}

		public static event LevelEvent OnLevelStartedEvent
		{
			[CompilerGenerated]
			add
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelStartedEvent; updated = (LevelEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelStartedEvent, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelStartedEvent; updated = (LevelEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelStartedEvent, updated, current) != current);
			}
		}

		public static event LevelEvent OnLevelCompletedEvent
		{
			[CompilerGenerated]
			add
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelCompletedEvent; updated = (LevelEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelCompletedEvent, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelCompletedEvent; updated = (LevelEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelCompletedEvent, updated, current) != current);
			}
		}

		public static event LevelEvent OnLevelLoadedEvent
		{
			[CompilerGenerated]
			add
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelLoadedEvent; updated = (LevelEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelLoadedEvent, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelLoadedEvent; updated = (LevelEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelLoadedEvent, updated, current) != current);
			}
		}

		public static event LevelEvent OnLevelFailedEvent
		{
			[CompilerGenerated]
			add
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelFailedEvent; updated = (LevelEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelFailedEvent, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelFailedEvent; updated = (LevelEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelFailedEvent, updated, current) != current);
			}
		}

		public static event LevelEvent OnLevelFailedNoContinueEvent
		{
			[CompilerGenerated]
			add
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelFailedNoContinueEvent; updated = (LevelEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelFailedNoContinueEvent, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEvent current; LevelEvent updated;
				do { current = m_OnLevelFailedNoContinueEvent; updated = (LevelEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelFailedNoContinueEvent, updated, current) != current);
			}
		}

		public static event LevelEventObsolete OnLevelLoadStarted
		{
			[CompilerGenerated]
			add
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelLoadStarted; updated = (LevelEventObsolete)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelLoadStarted, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelLoadStarted; updated = (LevelEventObsolete)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelLoadStarted, updated, current) != current);
			}
		}

		[Obsolete("Use LevelEvent instead")]
		public static event LevelEventObsolete OnLevelContinue
		{
			[CompilerGenerated]
			add
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelContinue; updated = (LevelEventObsolete)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelContinue, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelContinue; updated = (LevelEventObsolete)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelContinue, updated, current) != current);
			}
		}

		[Obsolete("Use LevelEvent instead")]
		public static event LevelEventObsolete OnLevelStarted
		{
			[CompilerGenerated]
			add
			{
				LevelEventObsolete current;
				LevelEventObsolete updated;
				do { current = m_OnLevelStarted; updated = (LevelEventObsolete)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelStarted, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEventObsolete current;
				LevelEventObsolete updated;
				do { current = m_OnLevelStarted; updated = (LevelEventObsolete)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelStarted, updated, current) != current);
			}
		}

		[Obsolete("Use LevelEvent instead")]
		public static event LevelEventObsolete OnLevelCompleted
		{
			[CompilerGenerated]
			add
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelCompleted; updated = (LevelEventObsolete)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelCompleted, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelCompleted; updated = (LevelEventObsolete)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelCompleted, updated, current) != current);
			}
		}

		[Obsolete("Use LevelEvent instead")]
		public static event LevelEventObsolete OnLevelLoaded
		{
			[CompilerGenerated]
			add
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelLoaded; updated = (LevelEventObsolete)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelLoaded, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelLoaded; updated = (LevelEventObsolete)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelLoaded, updated, current) != current);
			}
		}

		[Obsolete("Use LevelEvent instead")]
		public static event LevelEventObsolete OnLevelFailed
		{
			[CompilerGenerated]
			add
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelFailed; updated = (LevelEventObsolete)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelFailed, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelFailed; updated = (LevelEventObsolete)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelFailed, updated, current) != current);
			}
		}

		[Obsolete("Use LevelEvent instead")]
		public static event LevelEventObsolete OnLevelFailedNoContinue
		{
			[CompilerGenerated]
			add
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelFailedNoContinue; updated = (LevelEventObsolete)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelFailedNoContinue, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				LevelEventObsolete current; LevelEventObsolete updated;
				do { current = m_OnLevelFailedNoContinue; updated = (LevelEventObsolete)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnLevelFailedNoContinue, updated, current) != current);
			}
		}

		public static event OnScreenChangedEvent OnScreenChanged
		{
			[CompilerGenerated]
			add
			{
				OnScreenChangedEvent current;
				OnScreenChangedEvent updated;
				do { current = m_OnScreenChanged; updated = (OnScreenChangedEvent)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnScreenChanged, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				OnScreenChangedEvent current;
				OnScreenChangedEvent updated;
				do { current = m_OnScreenChanged; updated = (OnScreenChangedEvent)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OnScreenChanged, updated, current) != current);
			}
		}

		[Obsolete]
		public void RaiseOnLevelCompleted()
		{
			m_OnLevelCompleted?.Invoke();
		}

		[Obsolete]
		public void RaiseOnLevelContinue()
		{
			m_OnLevelContinue?.Invoke();
		}

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			m_OriginalRes = Vector2Int.zero;
			SetDefaultFPS();
			GameState = eGameState.Idle;
			CurrContinueAmount = 0;
			KillAllTweensWithExclusionsList();
			Time.timeScale = 1f;
		}

		public void SetDefaultFPS()
		{
			Application.targetFrameRate = 60;
		}

		public new virtual void OnEnable()
		{
			base.OnEnable();
			RemoteSettingsManager.OnFirebaseRemotConfigUpdated += OnFirebaseRemotConfigUpdated;
			if (ShortcutsManager.Instance != null)
				ShortcutsManager.Instance.SetGameManagerShortcutsEvents(SimulateLevelCompleted, SimulateLevelFailedWithRevive, SimulateLevelFailed, SimulatePrevLevel, SimulateNextLevel, SimulateResetLevel, SimulateResetGame);
		}

		public override void OnDisable()
		{
			RemoteSettingsManager.OnFirebaseRemotConfigUpdated -= OnFirebaseRemotConfigUpdated;
			base.OnDisable();
		}

		public override void Start()
		{
			base.Start();
			LevelData closedLevel = StorageManager.Instance != null ? StorageManager.Instance.OnGameClosedLevelData : null;
			if (closedLevel != null && StorageManager.Instance != null)
			{
				StorageManager.Instance.OnGameClosedLevelData = null;
				m_OnGameAwakeAfterGameClosed?.Invoke(closedLevel);
				StorageManager.Instance.TotalAttempts++;
			}
			LoadLevel();
		}

		protected virtual void Update()
		{
			Vector2Int resolution = new Vector2Int(Screen.width, Screen.height);
			if (resolution != m_OriginalRes)
			{
				m_OriginalRes = resolution;
				m_OnScreenChanged?.Invoke(resolution.x, resolution.y);
			}
		}

		protected virtual void OnFirebaseRemotConfigUpdated(bool i_IsUpdated)
		{
			return;
		}

		public virtual void GameOver(bool i_ForceNoContinue = false)
		{
			if (!i_ForceNoContinue && CurrContinueAmount < MaxContinueAmount)
				LevelFailed();
			else
				LevelFailedNoContinue();
		}

		public virtual void ResetGame()
		{
			GameState = eGameState.Idle;
			CurrContinueAmount = 0;
			KillAllTweensWithExclusionsList();
			Time.timeScale = 1f;
			m_OnGameReset?.Invoke();
			LoadLevel();
		}

		public virtual void ResetLevel()
		{
			GameState = eGameState.Idle;
			CurrContinueAmount = 0;
			KillAllTweensWithExclusionsList();
			Time.timeScale = 1f;
			m_OnLevelReset?.Invoke();
			LoadLevel();
		}

		protected void Reset()
		{
			GameState = eGameState.Idle;
			CurrContinueAmount = 0;
			KillAllTweensWithExclusionsList();
			Time.timeScale = 1f;
		}

		protected virtual void KillAllTweensWithExclusionsList()
		{
			DG.Tweening.DOTween.KillAll(false, DOTWeenIDsBase.IdsToExcludeFromGameReset);
		}

		public virtual void PauseGame()
		{
			if (GameState == eGameState.Playing) GameState = eGameState.Paused;
			m_OnGamePause?.Invoke();
		}

		public virtual void UnPauseGame()
		{
			if (GameState == eGameState.Paused) GameState = eGameState.Playing;
			m_OnGameUnPause?.Invoke();
		}

		public void LoadLevel()
		{
			m_OnLevelLoadStarted?.Invoke();
			LevelLoaded();
		}

		protected virtual void LevelLoaded()
		{
			LevelData data = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevelData : null;
			m_OnLevelLoadedEvent?.Invoke(data);
			m_OnLevelLoaded?.Invoke();
		}

		public virtual void StartGame()
		{
			LevelStarted();
		}

		public virtual void LevelStarted()
		{
			GameState = eGameState.Playing;
			LevelData data = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevelData : null;
			m_OnLevelStartedEvent?.Invoke(data);
			m_OnLevelStarted?.Invoke();
		}

		public virtual void LevelCompleted()
		{
			GameState = eGameState.Finished;
			LevelData data = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevelData : null;
			m_OnLevelCompletedEvent?.Invoke(data);
			m_OnLevelCompleted?.Invoke();
			Managers.Instance.Storage.GameWonSessionAttempts++;
		}

		public virtual void LevelContinue()
		{
			GameState = eGameState.Playing;
			CurrContinueAmount++;
			LevelData data = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevelData : null;
			m_OnLevelContinueEvent?.Invoke(data);
			m_OnLevelContinue?.Invoke();
		}

		protected virtual void LevelFailed()
		{
			GameState = eGameState.GamePreOver;
			LevelData data = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevelData : null;
			m_OnLevelFailedEvent?.Invoke(data);
			m_OnLevelFailed?.Invoke();
		}

		protected virtual void LevelFailedNoContinue()
		{
			GameState = eGameState.GameOver;
			LevelData data = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevelData : null;
			m_OnLevelFailedNoContinueEvent?.Invoke(data);
			m_OnLevelFailedNoContinue?.Invoke();
		}

		protected virtual void SimulateLevelCompleted()
		{
			if (GameState != eGameState.Playing)
			{
				ResetGame();
				StartGame();
			}
			LevelCompleted();
		}

		protected virtual void SimulateLevelFailedWithRevive()
		{
			if (GameState != eGameState.Playing)
			{
				ResetGame();
				StartGame();
			}
			if (MaxContinueAmount == 0) MaxContinueAmount = 1;
			CurrContinueAmount = 0;
			GameOver(false);
		}

		protected virtual void SimulateLevelFailed()
		{
			if (GameState != eGameState.Playing)
			{
				ResetGame();
				StartGame();
			}
			GameOver(true);
		}

		protected virtual void SimulatePrevLevel()
		{
			if (StorageManager.Instance != null)
			{
				StorageManager.Instance.CurrentLevel = Math.Max(0, StorageManager.Instance.CurrentLevel - 1);
				ResetGame();
			}
		}

		protected virtual void SimulateNextLevel()
		{
			if (StorageManager.Instance != null)
			{
				StorageManager.Instance.CurrentLevel++;
				ResetGame();
			}
		}

		protected virtual void SimulateResetLevel()
		{
			ResetLevel();
		}

		protected virtual void SimulateResetGame()
		{
			ResetGame();
		}

		protected void CallEvent_OnGameReset()
		{
			m_OnGameReset?.Invoke();
		}

		protected void CallEvent_OnLevelReset()
		{
			m_OnLevelReset?.Invoke();
		}

		protected void CallEvent_OnGameAwakeAfterGameClosed(LevelData i_LevelData)
		{
			m_OnGameAwakeAfterGameClosed?.Invoke(i_LevelData);
		}
	}
}
