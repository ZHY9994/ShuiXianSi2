using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public class SplashScreenManager : Singleton<SplashScreenManager>
	{
		[HideInInspector]
		public Action OnSplashScreenShownStart = delegate { };

		[HideInInspector]
		public Action OnSplashScreenShownFinished = delegate { };

		public bool ShowSplash;

		public bool ShowSplashOnEditor;

		public bool IsFinished = true;

		public float LogoFadeDuration = 1.75f;

		public float LogoFadeDelay = 1f;

		public float BackgroundFadeDuration = 1.25f;

		public float BackgroundFadeDelay;

		private Canvas m_SplashCanvas;

		private Image m_SplashBackground;

		private Image m_SplashLogo;

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			initRefs();
			if ((!Application.isEditor || ShowSplashOnEditor) && (Application.isEditor || ShowSplash)) showSplash();
		}

		private void showSplash()
		{
			IsFinished = false;
			if (m_SplashBackground != null) m_SplashBackground.gameObject.SetActive(true);
			var sequence = DOTween.Sequence();
			sequence.InsertCallback(0.025f, () => OnSplashScreenShownStart?.Invoke());
			sequence.AppendInterval(LogoFadeDelay);
			if (m_SplashLogo != null) sequence.Append(m_SplashLogo.DOFade(0f, LogoFadeDuration));
			sequence.AppendInterval(BackgroundFadeDelay);
			sequence.InsertCallback(LogoFadeDelay + LogoFadeDuration + BackgroundFadeDelay + BackgroundFadeDuration * 0.5f, setIsFinished);
			if (m_SplashBackground != null) sequence.Append(m_SplashBackground.DOFade(0f, BackgroundFadeDuration));
			sequence.OnComplete(onSequenceCompleted).OnKill(onSequenceCompleted);
		}

		private void onSequenceCompleted()
		{
			if (m_SplashCanvas != null) m_SplashCanvas.gameObject.SetActive(false);
			OnSplashScreenShownFinished?.Invoke();
		}

		private void setIsFinished()
		{
			IsFinished = true;
		}

		private void Reset()
		{
			initRefs();
		}

		private void OnValidate()
		{
			if (!m_IsDestroyed) initRefs();
		}

		private void initRefs()
		{
			if (m_SplashCanvas == null) m_SplashCanvas = GetComponentInChildren<Canvas>();
			if (m_SplashBackground == null) m_SplashBackground = transform.FindDeepChild<Image>("SplashBackground");
			if (m_SplashLogo == null) m_SplashLogo = transform.FindDeepChild<Image>("SplashLogo");
		}
	}
}
