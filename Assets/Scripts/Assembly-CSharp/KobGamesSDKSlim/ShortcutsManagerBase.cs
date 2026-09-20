using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class ShortcutsManagerBase : Singleton<ShortcutsManager>
	{
		[SerializeField]
		private bool m_IsShortcutsEnable = true;

		private Action m_OnSimulateLevelCompleted;

		private Action m_OnSimulateLevelFailedWithRevive;

		private Action m_OnSimulateLevelFailed;

		private Action m_OnSimulatePrevLevel;

		private Action m_OnSimulateNextLevel;

		private Action m_OnSimulateResetLevel;

		private Action m_OnSimulateResetGame;

		public static bool IsShortcutsEnable
		{
			get
			{
				return ShortcutsManager.Instance.m_IsShortcutsEnable;
			}
			set
			{
				ShortcutsManager.Instance.m_IsShortcutsEnable = value;
			}
		}

		public static event Action OnEditorPauseToggle;

		public void SetGameManagerShortcutsEvents(Action i_LevelCompleted, Action i_LevelFailedWithRevive, Action i_LevelFailed, Action i_PrevLevel, Action i_NextLevel, Action i_ResetLevel, Action i_ResetGame)
		{
			m_OnSimulateLevelCompleted = i_LevelCompleted;
			m_OnSimulateLevelFailedWithRevive = i_LevelFailedWithRevive;
			m_OnSimulateLevelFailed = i_LevelFailed;
			m_OnSimulatePrevLevel = i_PrevLevel;
			m_OnSimulateNextLevel = i_NextLevel;
			m_OnSimulateResetLevel = i_ResetLevel;
			m_OnSimulateResetGame = i_ResetGame;
		}

		public override void OnDisable()
		{
			m_OnSimulateResetGame = null;
			m_OnSimulateResetLevel = null;
			m_OnSimulateNextLevel = null;
			m_OnSimulatePrevLevel = null;
			m_OnSimulateLevelFailed = null;
			m_OnSimulateLevelFailedWithRevive = null;
			m_OnSimulateLevelCompleted = null;
		}

		public void TriggerShortcut(string i_Type)
		{
			if (GameConfig.Instance.Debug.DebugMode)
				triggerShortcut(i_Type);
		}

		protected virtual void triggerShortcut(string i_Type)
		{
			switch (i_Type)
			{
				case ShortcutsActionsBase.LevelComplete: m_OnSimulateLevelCompleted?.Invoke(); break;
				case ShortcutsActionsBase.LevelFailedWithRevive: m_OnSimulateLevelFailedWithRevive?.Invoke(); break;
				case ShortcutsActionsBase.LevelFailed: m_OnSimulateLevelFailed?.Invoke(); break;
				case ShortcutsActionsBase.PrevLevel: m_OnSimulatePrevLevel?.Invoke(); break;
				case ShortcutsActionsBase.NextLevel: m_OnSimulateNextLevel?.Invoke(); break;
				case ShortcutsActionsBase.ResetLevel: m_OnSimulateResetLevel?.Invoke(); break;
				case ShortcutsActionsBase.ResetGame: m_OnSimulateResetGame?.Invoke(); break;
				case ShortcutsActionsBase.EditorPauseToggle: OnEditorPauseToggle?.Invoke(); break;
				case ShortcutsActionsBase.ShowBanner: break;
				case ShortcutsActionsBase.HideBanner: break;
				case ShortcutsActionsBase.ToggleGameLoopButtons: GameConfig.Instance.Debug.ShowGameLoopButtons = !GameConfig.Instance.Debug.ShowGameLoopButtons; break;
			}
		}
	}
}
