using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim.MenuManagerV1
{
	public class MenuScreenBase : MonoBehaviour
	{
		[SerializeField]
		protected bool DefaultAnimateOnOpen;

		[SerializeField]
		protected bool DefaultAnimateOnClose;

		private bool m_AnimateOnOpen;

		private bool m_AnimateOnClose;

		[SerializeField]
		private bool m_HideOnReset = true;

		[SerializeField]
		protected CanvasGroup m_CanvasGroup;

		[SerializeField]
		private Button[] m_AllUnityButtons = new Button[0];

		[SerializeField]
		private bool[] m_AllUnityButtonsOriginalInteractivity = new bool[0];

		[SerializeField]
		private ExtendedButton[] m_AllExtendedButtons = new ExtendedButton[0];

		protected Tween m_FadeTween;

		protected MenuVariablesEditor m_MenuVars => GameConfig.Instance.Menus;

		protected virtual void SetRefs()
		{
			m_CanvasGroup = transform.FindDeepChild<CanvasGroup>("Canvas Group");
			m_AllExtendedButtons = GetComponentsInChildren<ExtendedButton>(true);
			m_AllUnityButtons = GetComponentsInChildren<Button>(true).Except(m_AllExtendedButtons).ToArray();
			m_AllUnityButtonsOriginalInteractivity = m_AllUnityButtons?.Select(x => x != null && x.interactable).ToArray();
		}

		protected virtual void OnValidate()
		{
			SetRefs();
		}

		protected virtual void Awake()
		{
			return;
		}

		protected virtual void Start()
		{
			return;
		}

		protected virtual void Update()
		{
			return;
		}

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			return;
		}

		public virtual void Reset()
		{
			gameObject.SetActive(!m_HideOnReset);
			if (m_CanvasGroup != null) m_CanvasGroup.alpha = m_HideOnReset ? 0f : 1f;
		}

		public virtual void Open()
		{
			Open(DefaultAnimateOnOpen);
		}

		public virtual void Open(bool i_Animate)
		{
			m_AnimateOnOpen = i_Animate;
			OnScreenOpenStart();
			ResetUnityButtonsToOriginalInteractivity();
		}

	private void OnScreenOpenStart()
	{
		MenuManager.Instance.OnMenu_ScreenOpenStart(this);
		OpenAnim();
	}

		protected virtual void OpenAnim()
		{
			gameObject.SetActive(true);
			m_CanvasGroup.alpha = 0f;
			if (m_FadeTween != null) m_FadeTween.Kill();
			float duration = m_AnimateOnOpen && m_MenuVars != null ? m_MenuVars.ScreenFadeInDuration : 0f;
			m_FadeTween = m_CanvasGroup.DOFade(1f, duration).OnComplete(OnScreenOpenEnd);
		}

	protected virtual void OnScreenOpenEnd()
	{
		MenuManager.Instance.OnMenu_ScreenOpenEnd(this);
	}

		public virtual void Close()
		{
			Close(DefaultAnimateOnClose);
		}

		public virtual void Close(bool i_Animate)
		{
			m_AnimateOnClose = i_Animate;
			OnScreenCloseStart();
		}

	private void OnScreenCloseStart()
	{
		MenuManager.Instance.OnMenu_ScreenCloseStart(this);
		ResetButtonsState();
		CloseAnim();
	}

		protected virtual void CloseAnim()
		{
			if (m_FadeTween != null) m_FadeTween.Kill();
			float duration = m_AnimateOnClose && m_MenuVars != null ? m_MenuVars.ScreenFadeOutDuration : 0f;
			m_FadeTween = m_CanvasGroup.DOFade(0f, duration).OnComplete(OnScreenCloseEnd);
		}

	protected virtual void OnScreenCloseEnd()
	{
		MenuManager.Instance.OnMenu_ScreenCloseEnd(this);
		gameObject.SetActive(false);
	}

		protected void SetButtonsInteractivityOn()
		{
			SetButtonsInteractivity(true);
		}

		protected void SetButtonsInteractivityOff(bool i_TurnOnWithTimer = false)
		{
			SetButtonsInteractivity(false);
			if (i_TurnOnWithTimer) Invoke(nameof(SetButtonsInteractivityOn), 0.1f);
		}

		private void SetButtonsInteractivity(bool i_Value)
		{
			if (m_AllExtendedButtons != null)
				foreach (var button in m_AllExtendedButtons) if (button != null) button.SetInteractable(i_Value);
			if (m_AllUnityButtons != null)
				foreach (var button in m_AllUnityButtons) if (button != null) button.interactable = i_Value;
		}

		protected virtual void ResetButtonsState()
		{
			if (m_AllExtendedButtons != null)
				foreach (var button in m_AllExtendedButtons) if (button != null) button.ResetState();
			if (m_AllUnityButtons != null)
				foreach (var button in m_AllUnityButtons) if (button != null) button.interactable = false;
		}

		protected virtual void ResetUnityButtonsToOriginalInteractivity()
		{
			if (m_AllUnityButtons == null || m_AllUnityButtonsOriginalInteractivity == null) return;
			for (int i = 0; i < m_AllUnityButtons.Length && i < m_AllUnityButtonsOriginalInteractivity.Length; i++)
				if (m_AllUnityButtons[i] != null) m_AllUnityButtons[i].interactable = m_AllUnityButtonsOriginalInteractivity[i];
		}

		public virtual void OpenDebug()
		{
			if (Application.isPlaying) Open();
		}

		public virtual void CloseDebug()
		{
			if (Application.isPlaying) Close();
		}
	}
}
