using System;
using System.Collections;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;

namespace KobGamesSDKSlim.Collectable
{
	public class UniversalCollectableSender : MonoBehaviour
	{
		[SerializeField]
		private RectTransform m_RectTransform;

		private Action m_SendCompleteCallback;

		private int m_CollectableAmount;

		private int m_ReceivedCollectableAmount;

		private int m_SentCollectableAmount;

		private bool m_IsHapticsCooldown;

		private AnimationData m_AnimationData => GameConfig.Instance.HUD.AnimationData;

		public void Send(eCollectableType i_CollectableType, int i_CollectableAmount, eCollectableSendAnimType i_AnimType, Vector2 i_ScreenPosition, Action i_CompleteCallback = null)
		{
			m_SendCompleteCallback = i_CompleteCallback;
			m_CollectableAmount = i_CollectableAmount;
			CollectableUpdater target = CollectableManager.Instance.DefaultCollectableTarget(i_CollectableType);
			if (target == null || m_AnimationData == null || m_AnimationData.SendAnim == null) throw new NullReferenceException();
			var data = m_AnimationData.SendAnim;
			if (!data.ContainsKey(i_AnimType)) i_AnimType = eCollectableSendAnimType.Default;
			StartCoroutine(sendsCoroutine(i_CollectableType, i_CollectableAmount, i_AnimType, i_ScreenPosition, target.MoveTarget, data[i_AnimType]));
		}

		private IEnumerator sendsCoroutine(eCollectableType i_CollectableType, int i_CollectableAmount, eCollectableSendAnimType i_AnimType, Vector2 i_ScreenPosition, RectTransform i_TargetRectTransform, AnimationData.SendAnimData i_AnimData)
		{
			AnimationData.SendAnimData.BaseAnimData animData = null;
			switch (i_AnimData.AnimMode)
			{
				case AnimationData.SendAnimData.eAnimMode.Linear:
					animData = i_AnimData.LinearAnimMode;
					break;
				case AnimationData.SendAnimData.eAnimMode.Burst:
					animData = i_AnimData.BurstAnimMode;
					break;
			}
			float scaleFactor = CollectableManager.Instance.HUDCanvas.scaleFactor;
			m_RectTransform.anchoredPosition = new Vector2(i_ScreenPosition.x / scaleFactor, i_ScreenPosition.y / scaleFactor);
			int sendAmount;
			int collectableValue = 0;
			int collectableValueRest = 0;
			if (animData.IsSpecificAmount)
			{
				sendAmount = animData.SendAmount;
				if (sendAmount != 0)
				{
					collectableValue = m_CollectableAmount / sendAmount;
					collectableValueRest = m_CollectableAmount % sendAmount;
				}
			}
			else
			{
				sendAmount = animData.MaxAmount;
				int amount = m_CollectableAmount / animData.ItemCost;
				if (amount < sendAmount)
					sendAmount = amount;
				if (sendAmount == 0)
				{
					sendAmount = 1;
					collectableValue = m_CollectableAmount;
					collectableValueRest = 0;
				}
				else
				{
					collectableValue = m_CollectableAmount / sendAmount;
					collectableValueRest = m_CollectableAmount % sendAmount;
				}
			}
			m_ReceivedCollectableAmount = 0;
			m_SentCollectableAmount = 0;
			for (int i = 0; i < sendAmount; i++)
			{
				CollectableUI collectable = PoolManager.Instance.Dequeue(ePoolType.CollectableUI).GetComponent<CollectableUI>();
				collectable.transform.SetParent(i_TargetRectTransform);
				collectable.Value = (i == sendAmount - 1 ? collectableValueRest : 0) + collectableValue;
				collectable.Initialize(m_RectTransform, i_CollectableType);
				collectable.Send(i_TargetRectTransform, i_AnimData.AnimMode, animData, collectableMoveComplete);
				m_SentCollectableAmount += collectable.Value;
				float delay = animData.DelayBetween * animData.DelayCurve.Evaluate((float)i / (float)sendAmount);
				if (delay != 0f)
					yield return new WaitForSeconds(delay);
			}
		}

		private void collectableMoveComplete(CollectableUI i_SentCollectable)
		{
			if (i_SentCollectable == null) throw new NullReferenceException(nameof(i_SentCollectable));
			m_ReceivedCollectableAmount += i_SentCollectable.Value;
			AnimationData data = m_AnimationData;
			if (m_ReceivedCollectableAmount == m_CollectableAmount)
			{
				m_SendCompleteCallback?.Invoke();
				if (data != null && data.Haptics.IsPlayHapticsOnReceive)
					playHaptics(data.Haptics.CompleteHaptics, data.Haptics.HapticsCooldown);
			}
			else if (data != null && data.Haptics.IsPlayHapticsOnReceive)
				playHaptics(data.Haptics.ReceivedHaptics, data.Haptics.HapticsCooldown);
		}

		private void playHaptics(MMFeedbackHaptics.HapticTypes i_HapticsType, float i_HapticsCooldown)
		{
			if (m_IsHapticsCooldown) return;
			m_IsHapticsCooldown = true;
			if (Managers.Instance == null || Managers.Instance.HapticManager == null) throw new NullReferenceException();
			Managers.Instance.HapticManager.Haptic(i_HapticsType);
			Invoke(nameof(resetHapticsCooldown), i_HapticsCooldown);
		}

		private void resetHapticsCooldown()
		{
			m_IsHapticsCooldown = false;
		}

		private void SetRefs()
		{
			m_RectTransform = GetComponent<RectTransform>();
		}
	}
}
