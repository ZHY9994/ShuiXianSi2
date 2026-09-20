using UnityEngine;
using DG.Tweening;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-10)]
	public class Rotator : AnimatorBase
	{
		public bool IsLocal = true;

		private Quaternion m_InitialRotation;

		public Vector3 FinalLocalRotation;

		protected override void Awake()
		{
			if (RandomStartTime)
				StartTime = Random.Range(0f, 1f);
			SetInitialValues();
		}

		public void SetInitialValues()
		{
			m_InitialRotation = IsLocal ? transform.localRotation : transform.rotation;
		}

		public override void StartAnimation()
		{
			if (Tween != null)
				Tween.Kill();
			m_IsPlaying = true;
			Tween = IsLocal
				? transform.DOLocalRotate(FinalLocalRotation, Duration, RotateMode.LocalAxisAdd)
				: transform.DORotate(FinalLocalRotation, Duration, RotateMode.LocalAxisAdd);
			Tween = Tween.SetEase(Ease).SetRelative(IsRelative).SetLoops(LoopCount, LoopType);
			SetId(Tween);
			if (StartTime > 0f)
				Tween.Goto(Mathf.Clamp01(StartTime) * Duration, true);
		}

		public override void ResetValues()
		{
			StopAnimation();
			if (IsLocal)
				transform.localRotation = m_InitialRotation;
			else
				transform.rotation = m_InitialRotation;
		}
	}
}
