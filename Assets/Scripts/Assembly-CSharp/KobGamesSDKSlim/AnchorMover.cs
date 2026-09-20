using DG.Tweening;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class AnchorMover : AnimatorBase
	{
		public bool IsLocal;

		public Vector3 m_InitialPosition;

		public Vector3 FinalPosition;

		private RectTransform m_rectTransform;

		private bool m_WasInit;

		protected override void Awake()
		{
			if (RandomStartTime)
				StartTime = Random.Range(0f, 1f);
			initIfNeed();
		}

		public override void StartAnimation()
		{
			initIfNeed();
			if (Tween != null)
				Tween.Kill(true);
			m_IsPlaying = true;
			ResetValues();
			Tween = m_rectTransform.DOAnchorPos3D(FinalPosition, Duration, false)
				.SetEase(Ease)
				.SetRelative(IsRelative)
				.SetLoops(LoopCount, LoopType);
			SetId(Tween);
			if (StartTime > 0f)
				Tween.Goto(Mathf.Clamp01(StartTime) * Duration, true);
		}

		public override void ResetValues()
		{
			initIfNeed();
			StopAnimation();
			m_rectTransform.anchoredPosition3D = m_InitialPosition;
		}

		private void initIfNeed()
		{
			if (m_WasInit)
				return;
			m_WasInit = true;
			m_rectTransform = GetComponent<RectTransform>();
			if (m_rectTransform == null)
				throw new MissingComponentException();
			m_InitialPosition = m_rectTransform.anchoredPosition3D;
		}
	}
}
