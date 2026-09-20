using UnityEngine;
using DG.Tweening;

namespace KobGamesSDKSlim
{
	public class Mover : AnimatorBase
	{
		public bool IsLocal;

		public Vector3 m_InitialPosition;

		public Vector3 FinalPosition;

		protected override void Awake()
		{
			if (RandomStartTime)
				StartTime = Random.Range(0f, 1f);
			m_InitialPosition = IsLocal ? transform.localPosition : transform.position;
		}

		public override void StartAnimation()
		{
			if (Tween != null)
				Tween.Kill();
			m_IsPlaying = true;
			ResetValues();
			Tween tween = IsLocal
				? transform.DOLocalMove(FinalPosition, Duration)
				: transform.DOMove(FinalPosition, Duration);
			Tween = tween.SetEase(Ease).SetRelative(IsRelative).SetLoops(LoopCount, LoopType);
			SetId(Tween);
			if (StartTime > 0f)
				Tween.Goto(Mathf.Clamp01(StartTime) * Duration, true);
		}

		public override void ResetValues()
		{
			StopAnimation();
			if (IsLocal)
				transform.localPosition = m_InitialPosition;
			else
				transform.position = m_InitialPosition;
		}
	}
}
