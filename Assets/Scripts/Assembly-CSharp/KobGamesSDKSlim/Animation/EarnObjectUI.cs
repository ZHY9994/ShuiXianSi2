using System;
using DG.Tweening;
using UnityEngine;

namespace KobGamesSDKSlim.Animation
{
	public class EarnObjectUI : MonoBehaviour
	{
		[SerializeField]
		private RectTransform m_RectTransform;

		private Action<EarnObjectUI> m_MoveCompleteCallback;

		protected Action<EarnObjectUI> MoveCompleteCallback => m_MoveCompleteCallback;

		private Sequence m_AnimSequence;

		private EarnObjectUIAnimData m_DefaultEarnObjectUIAnimData => GameConfig.Instance.HUD.EarnObject.DefaultEarnObjectUIAnimData;

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			return;
		}

		public void Animate(RectTransform i_OriginalTransform, RectTransform i_TargetTransform, Action<EarnObjectUI> i_CompleteMoveCallback)
		{
			Animate(i_OriginalTransform, i_TargetTransform, m_DefaultEarnObjectUIAnimData, i_CompleteMoveCallback);
		}

		public void Animate(RectTransform i_OriginalTransform, RectTransform i_TargetTransform, eEarnObjectSpawnMode i_Mode, Action<EarnObjectUI> i_CompleteMoveCallback)
		{
			EarnObjectUIAnimData data = m_DefaultEarnObjectUIAnimData;
			if (data == null) throw new NullReferenceException(nameof(m_DefaultEarnObjectUIAnimData));
			EarnObjectUIAnimData.AnimData modeData = i_Mode == eEarnObjectSpawnMode.Burst ? data.BurstMode : data.LinearMode;
			Animate(i_OriginalTransform, i_TargetTransform, i_Mode, modeData, i_CompleteMoveCallback);
		}

		public void Animate(RectTransform i_OriginalTransform, RectTransform i_TargetTransform, EarnObjectUIAnimData i_AnimData, Action<EarnObjectUI> i_CompleteMoveCallback)
		{
			if (i_AnimData == null) throw new NullReferenceException(nameof(i_AnimData));
			EarnObjectUIAnimData.AnimData modeData = i_AnimData.SpawnMode == eEarnObjectSpawnMode.Burst ? i_AnimData.BurstMode : i_AnimData.LinearMode;
			Animate(i_OriginalTransform, i_TargetTransform, i_AnimData.SpawnMode, modeData, i_CompleteMoveCallback);
		}

		public void Animate(RectTransform i_OriginalTransform, RectTransform i_TargetTransform, eEarnObjectSpawnMode i_Mode, EarnObjectUIAnimData.AnimData i_AnimData, Action<EarnObjectUI> i_CompleteMoveCallback)
		{
			m_MoveCompleteCallback = i_CompleteMoveCallback;
			if (i_Mode == eEarnObjectSpawnMode.Linear)
				animateLinear(i_OriginalTransform, i_TargetTransform, i_AnimData as EarnObjectUIAnimData.LinearAnimData);
			else if (i_Mode == eEarnObjectSpawnMode.Burst)
				animateBurst(i_OriginalTransform, i_TargetTransform, i_AnimData as EarnObjectUIAnimData.BurstAnimData);
		}

		private void animateLinear(RectTransform i_OriginalTransform, RectTransform i_TargetTransform, EarnObjectUIAnimData.LinearAnimData i_AnimData)
		{
			if (i_AnimData == null || m_RectTransform == null || i_TargetTransform == null) throw new NullReferenceException();
			m_AnimSequence?.Kill();
			m_AnimSequence = DOTween.Sequence();
			m_AnimSequence.AppendCallback(() => copyTarget(i_OriginalTransform));
			m_AnimSequence.Append(m_RectTransform.DOAnchorPos(i_AnimData.InitialMoveOffset, i_AnimData.InitialSizeDeltaDuration).SetEase(i_AnimData.InitialMoveEase).SetRelative(true));
			m_AnimSequence.Append(m_RectTransform.DOSizeDelta(Vector2.one * i_AnimData.InitialSizeDeltaAmount, i_AnimData.InitialSizeDeltaDuration).SetEase(i_AnimData.InitialSizeDeltaEase));
			m_AnimSequence.Append(m_RectTransform.DOSizeDelta(Vector2.one * i_AnimData.FinalSizeDeltaAmount, i_AnimData.FinalSizeDeltaDuration).SetEase(i_AnimData.FinalSizeDeltaEase));
			m_AnimSequence.AppendInterval(i_AnimData.FinalAnimData.Delay);
			m_AnimSequence.Append(m_RectTransform.DOMove(i_TargetTransform.position, i_AnimData.FinalAnimData.Duration).SetEase(i_AnimData.FinalAnimData.Ease));
			m_AnimSequence.OnComplete(OnAnimComplete);
		}

		private void animateBurst(RectTransform i_OriginalTransform, RectTransform i_TargetTransform, EarnObjectUIAnimData.BurstAnimData i_AnimData)
		{
			if (i_AnimData == null || m_RectTransform == null || i_TargetTransform == null) throw new NullReferenceException();
			m_AnimSequence?.Kill();
			m_AnimSequence = DOTween.Sequence();
			m_AnimSequence.AppendCallback(() => copyTarget(i_OriginalTransform));
			float minAngle = i_AnimData.IsLimitAngle ? i_AnimData.AngleLimitMin : 0f;
			float maxAngle = i_AnimData.IsLimitAngle ? i_AnimData.AngleLimitMax : 360f;
			float radius = UnityEngine.Random.Range(i_AnimData.BurstRadius * (1f - i_AnimData.RadiusThickness), i_AnimData.BurstRadius);
			Vector3 direction = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(minAngle, maxAngle)) * Vector3.right;
			Vector2 burstOffset = i_AnimData.BurstOffset + new Vector2(direction.x, direction.y) * radius;
			m_AnimSequence.Append(m_RectTransform.DOAnchorPos(burstOffset, i_AnimData.BurstDuration).SetEase(i_AnimData.BurstMoveEase).SetRelative(true));
			m_AnimSequence.Append(m_RectTransform.DOSizeDelta(Vector2.one * i_AnimData.BurstSizeDeltaAmount, i_AnimData.BurstDuration).SetEase(i_AnimData.BurstSizeDeltaEase));
			m_AnimSequence.AppendInterval(i_AnimData.WaitSizeDeltaDuration);
			m_AnimSequence.Append(m_RectTransform.DOSizeDelta(Vector2.one * i_AnimData.WaitSizeDeltaAmount, i_AnimData.WaitSizeDeltaDuration).SetEase(i_AnimData.WaitSizeDeltaEase));
			m_AnimSequence.AppendInterval(i_AnimData.FinalAnimData.Delay);
			m_AnimSequence.Append(m_RectTransform.DOMove(i_TargetTransform.position, i_AnimData.FinalAnimData.Duration).SetEase(i_AnimData.FinalAnimData.Ease));
			m_AnimSequence.OnComplete(OnAnimComplete);
		}

		protected virtual void OnAnimComplete()
		{
			m_MoveCompleteCallback?.Invoke(this);
		}

		private void copyTarget(RectTransform i_OriginalTransform)
		{
			transform.position = i_OriginalTransform.position;
			m_RectTransform.sizeDelta = i_OriginalTransform.sizeDelta;
			transform.localScale = i_OriginalTransform.localScale;
			m_RectTransform.sizeDelta = i_OriginalTransform.sizeDelta;
		}

		protected virtual void SetRefs()
		{
			m_RectTransform = GetComponent<RectTransform>();
		}
	}
}
