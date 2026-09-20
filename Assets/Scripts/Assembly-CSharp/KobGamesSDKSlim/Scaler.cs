using UnityEngine;
using DG.Tweening;

namespace KobGamesSDKSlim
{
	public class Scaler : AnimatorBase
	{
		private Vector3 m_InitialScale;

		public Vector3 FinalScale;

		public bool SetInitialScaleStart;

		public Vector3 InitialScale;

		protected override void Awake()
		{
			if (RandomStartTime)
				StartTime = Random.Range(0f, 1f);
			m_InitialScale = transform.localScale;
		}

		public override void StartAnimation()
		{
			if (Tween != null)
				Tween.Kill();
			m_IsPlaying = true;
			if (SetInitialScaleStart)
				transform.localScale = InitialScale;
			Tween = transform.DOScale(FinalScale, Duration).SetEase(Ease).SetRelative(IsRelative)
				.SetLoops(LoopCount, LoopType).SetUpdate(UpdateType, UpdateFrameIndependant);
			SetId(Tween);
			if (StartTime > 0f)
				Tween.Goto(Mathf.Clamp01(StartTime) * Duration, true);
		}

		public override void ResetValues()
		{
			StopAnimation();
			transform.localScale = m_InitialScale;
		}
	}
}
