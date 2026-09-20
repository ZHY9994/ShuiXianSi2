using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim.Collectable
{
	public class CollectableUI : MonoBehaviour
	{
		[SerializeField]
		private RectTransform m_RectTransform;

		[SerializeField]
		private Image m_Image;

		private eCollectableType m_CollectableType;

		private Action<CollectableUI> m_MoveCompleteCallback;

		private Sequence m_AnimSequence;

		public virtual eCollectableType CollectableType => eCollectableType.Coin;

		public int Value { get; set; }

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			return;
		}

		public virtual void Initialize(RectTransform i_OriginalTransform, eCollectableType i_Type)
		{
			m_AnimSequence?.Kill();
			m_RectTransform.DOKill();
			m_Image.DOKill();
			if (i_OriginalTransform != null)
			{
				transform.position = i_OriginalTransform.position;
				m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, i_OriginalTransform.rect.width);
				m_RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, i_OriginalTransform.rect.height);
				transform.localScale = i_OriginalTransform.localScale;
			}
			m_RectTransform.localRotation = Quaternion.identity;
			m_RectTransform.localScale = Vector3.one;
			Color color = m_Image.color;
			color.a = 1f;
			m_Image.color = color;
			setType(i_Type);
		}

		protected virtual void setType(eCollectableType i_Type)
		{
			m_CollectableType = i_Type;
			m_Image.sprite = GameConfig.Instance.HUD.CollectableData[i_Type].CollectableSprite;
		}

		public virtual void Send(RectTransform i_TargetTransform, AnimationData.SendAnimData.eAnimMode i_AnimMode, AnimationData.SendAnimData.BaseAnimData i_AnimData, Action<CollectableUI> i_CompleteMoveCallback)
		{
			m_MoveCompleteCallback = i_CompleteMoveCallback;
			if (i_AnimMode == AnimationData.SendAnimData.eAnimMode.Burst)
				sendBurst(i_TargetTransform, i_AnimData as AnimationData.SendAnimData.BurstAnimData);
			else if (i_AnimMode == AnimationData.SendAnimData.eAnimMode.Linear)
				sendLinear(i_TargetTransform, i_AnimData as AnimationData.SendAnimData.LinearAnimData);
		}

		private void sendLinear(RectTransform i_TargetTransform, AnimationData.SendAnimData.LinearAnimData i_AnimData)
		{
			if (i_AnimData == null || i_TargetTransform == null) { OnSendComplete(); return; }
			m_AnimSequence?.Kill();
			m_AnimSequence = DOTween.Sequence();
			m_AnimSequence.AppendCallback(() => m_RectTransform.DOAnchorPos(i_AnimData.InitialMoveOffset, i_AnimData.InitialSizeDeltaDuration).SetEase(i_AnimData.InitialMoveEase).SetRelative());
			m_AnimSequence.Join(m_RectTransform.DOSizeDelta(Vector2.one * i_AnimData.InitialSizeDeltaAmount, i_AnimData.InitialSizeDeltaDuration).SetEase(i_AnimData.InitialSizeDeltaEase));
			m_AnimSequence.Append(m_RectTransform.DOSizeDelta(Vector2.one * i_AnimData.FinalSizeDeltaAmount, i_AnimData.FinalSizeDeltaDuration).SetEase(i_AnimData.FinalSizeDeltaEase));
			m_AnimSequence.Append(m_RectTransform.DOMove(i_TargetTransform.position, i_AnimData.FinalAnimData.Duration).SetEase(i_AnimData.FinalAnimData.Ease).SetDelay(i_AnimData.FinalAnimData.Delay));
			m_AnimSequence.OnComplete(OnSendComplete);
		}

		private void sendBurst(RectTransform i_TargetTransform, AnimationData.SendAnimData.BurstAnimData i_AnimData)
		{
			if (i_AnimData == null || i_TargetTransform == null) { OnSendComplete(); return; }
			m_AnimSequence?.Kill();
			m_AnimSequence = DOTween.Sequence();
			float angle = i_AnimData.IsLimitAngle ? UnityEngine.Random.Range(i_AnimData.AngleLimitMin, i_AnimData.AngleLimitMax) : UnityEngine.Random.Range(0f, 360f);
			float radius = UnityEngine.Random.Range(i_AnimData.BurstRadius * (1f - i_AnimData.RadiusThickness), i_AnimData.BurstRadius);
			Vector3 burstVector = Quaternion.Euler(0f, 0f, angle) * Vector2.right * radius;
			Vector2 burst = new Vector2(burstVector.x, burstVector.y) + i_AnimData.BurstOffset;
			m_AnimSequence.Append(m_RectTransform.DOAnchorPos(burst, i_AnimData.BurstDuration).SetEase(i_AnimData.BurstMoveEase).SetRelative());
			m_AnimSequence.Join(m_RectTransform.DOSizeDelta(Vector2.one * i_AnimData.BurstSizeDeltaAmount, i_AnimData.BurstDuration).SetEase(i_AnimData.BurstSizeDeltaEase));
			m_AnimSequence.Append(m_RectTransform.DOSizeDelta(Vector2.one * i_AnimData.WaitSizeDeltaAmount, i_AnimData.WaitSizeDeltaDuration).SetEase(i_AnimData.WaitSizeDeltaEase));
			m_AnimSequence.Append(m_RectTransform.DOMove(i_TargetTransform.position, i_AnimData.FinalAnimData.Duration).SetEase(i_AnimData.FinalAnimData.Ease).SetDelay(i_AnimData.FinalAnimData.Delay));
			m_AnimSequence.OnComplete(OnSendComplete);
		}

		protected virtual void OnSendComplete()
		{
			StorageManager.Instance.AddCollectable(m_CollectableType, Value);
			PoolManager.Instance.Queue(ePoolType.CollectableUI, gameObject);
			m_MoveCompleteCallback?.Invoke(this);
		}

		public virtual void Receive(AnimationData.ReceiveAnimData.eAnimMode i_AnimMode, AnimationData.ReceiveAnimData.BaseAnimData i_AnimData)
		{
			if (i_AnimMode == AnimationData.ReceiveAnimData.eAnimMode.Burst)
				receiveBurst(i_AnimData as AnimationData.ReceiveAnimData.BurstAnimData);
		}

		private void receiveBurst(AnimationData.ReceiveAnimData.BurstAnimData i_AnimData)
		{
			if (i_AnimData == null) { OnReceiveComplete(); return; }
			m_AnimSequence?.Kill();
			m_AnimSequence = DOTween.Sequence();
			float angle = i_AnimData.IsLimitAngle ? UnityEngine.Random.Range(i_AnimData.AngleLimitMin, i_AnimData.AngleLimitMax) : UnityEngine.Random.Range(0f, 360f);
			float radius = UnityEngine.Random.Range(i_AnimData.BurstRadius * (1f - i_AnimData.RadiusThickness), i_AnimData.BurstRadius);
			Vector3 offsetVector = Quaternion.Euler(0f, 0f, angle) * Vector2.right * radius;
			Vector2 offset = new Vector2(offsetVector.x, offsetVector.y) + i_AnimData.BurstOffset;
			m_AnimSequence.Append(m_RectTransform.DOAnchorPos(offset, i_AnimData.MoveDuration).SetEase(i_AnimData.MoveEase).SetRelative());
			if (i_AnimData.IsScale) m_AnimSequence.Join(m_RectTransform.DOScale(i_AnimData.FinalScale, i_AnimData.MoveDuration).SetEase(i_AnimData.ScaleEase));
			if (i_AnimData.IsFade) m_AnimSequence.Join(m_Image.DOFade(i_AnimData.FinalFadeValue, i_AnimData.MoveDuration).SetEase(i_AnimData.FadeEase));
			m_AnimSequence.OnComplete(OnReceiveComplete);
		}

		protected virtual void OnReceiveComplete()
		{
			PoolManager.Instance.Queue(ePoolType.CollectableUI, gameObject);
		}

		protected virtual void SetRefs()
		{
			m_RectTransform = GetComponent<RectTransform>();
			m_Image = GetComponent<Image>();
		}
	}
}
