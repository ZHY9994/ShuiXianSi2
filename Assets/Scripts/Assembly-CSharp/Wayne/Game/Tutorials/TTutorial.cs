using UnityEngine;
using KobGamesSDKSlim;

namespace Wayne.Game.Tutorials
{
	public abstract class TTutorial : MonoBehaviour, ITutorialModule
	{
		protected TutorialVariableEditor m_Editor => GameConfig.Instance.GamePlay.Tutorials;

		protected abstract eTutorial m_Type { get; }

		protected TTutorialData m_TutorialData => m_Editor.GetTutorialData(m_Type);

		protected bool IsTutorialActive => m_TutorialData.IsTutorialActive;

		public virtual void OnStartUp()
		{
		}

		public virtual void OnActivate()
		{
		}

		public virtual void OnDeactivate()
		{
		}

		public virtual void OnGameReset()
		{
		}

		public virtual void OnLevelStart()
		{
		}

		public virtual void OnLevelComplete()
		{
		}

		protected void invokeCompletion()
		{
			m_TutorialData.OnComplete();
		}

		protected void reportEvent(string i_Text)
		{
			if (IsTutorialActive)
				TutorialManager.Instance.ReportEvent(i_Text);
		}
	}
}
