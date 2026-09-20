using System;
using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;

public class Screen_Transition : MenuScreenBase
{
	[Serializable]
	public class AnimationData
	{
		public float ShowCircleSize;

		public float EnterDuration;

		public float EnterFadeDuration;

		public float EnterBlackScreenDuration;

		public Ease EnterEase;

		public Ease EnterFadeEase;

		public float ExitDuration;

		public float ExitFadeDuration;

		public Ease ExitEase;

		public Ease ExitFadeEase;

		public float ExitFadeDuration2;

		public Ease ExitFadeEase2;
	}

	public enum eTransitionMode
	{
		Hide = 0,
		Show = 1,
		HideAndShow = 2
	}

	public static Action OnHideComplete;

	public static Action OnShowComplete;

	public static Action OnStartGame;

	public static eTransitionMode TransitionMode = eTransitionMode.HideAndShow;

	[SerializeField]
	private RectTransform m_Circle;

	[SerializeField]
	private CanvasGroup m_CanvasAlpha;

	[SerializeField]
	private AnimationData m_Animation;

	protected override void OnEnable()
	{
		base.OnEnable();
		OnShowComplete += startGame;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		OnShowComplete -= startGame;
	}

	protected override void OnScreenOpenEnd()
	{
		base.OnScreenOpenEnd();
		switch (TransitionMode)
		{
		case eTransitionMode.Hide:
			hide();
			break;
		case eTransitionMode.Show:
			show();
			break;
		case eTransitionMode.HideAndShow:
			hide(() => show());
			break;
		}
	}

	private void startGame()
	{
		GameTraceLog.LogError($"[NextLevelTrace] Transition.startGame frame={Time.frameCount} gameplayOpened={MenuManager.Instance.IsScreenOpened("Screen_GamePlay")}");
		SoundManager.Instance.PlayGeneralSFXByType(eSFXType.LevelStart);
		Close();
		OnStartGame?.Invoke();
		MenuManager.Instance.OpenMenuScreen("Screen_GamePlay");
	}

	private void hide(Action i_OnComplete = null)
	{
		m_Circle.sizeDelta = Vector2.one * m_Animation.ShowCircleSize;
		m_CanvasAlpha.DOFade(1f, m_Animation.EnterFadeDuration)
			.SetEase(m_Animation.EnterFadeEase).SetId(-90).From(0f);
		m_Circle.DOSizeDelta(Vector2.zero, m_Animation.EnterDuration)
			.SetId(-90).SetEase(m_Animation.EnterEase)
			.OnComplete(() =>
			{
				OnHideComplete?.Invoke();
				i_OnComplete?.Invoke();
			});
	}

	private void show(Action i_OnComplete = null)
	{
		DOVirtual.DelayedCall(m_Animation.EnterBlackScreenDuration, () =>
		{
			m_CanvasAlpha.DOFade(0f, m_Animation.ExitFadeDuration)
				.SetEase(m_Animation.ExitFadeEase).SetId(-90).From(1f);
			m_Circle.DOSizeDelta(Vector2.one * m_Animation.ShowCircleSize, m_Animation.ExitDuration)
				.SetId(-90).SetEase(m_Animation.ExitEase)
				.OnComplete(() =>
				{
					OnShowComplete?.Invoke();
					i_OnComplete?.Invoke();
				});
		}).SetId(-90);
	}
}
