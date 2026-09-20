using System;
using System.Collections;
using MoreMountains.FeedbacksForThirdParty;
using TMPro;
using UnityEngine;

namespace KobGamesSDKSlim.Collectable
{
	public class CollectableSender : MonoBehaviour
	{
		[SerializeField]
		private RectTransform m_RectTransform;

		[SerializeField]
		private TextMeshProUGUI m_CollectableText;

		[SerializeField]
		private RectTransform m_CollectableIcon;

		[SerializeField]
		private eCollectableType m_CollectableType;

		[SerializeField]
		private eCollectableSendAnimType m_AnimType;

		[SerializeField]
		private bool m_IsOverrideTarget;

		[SerializeField]
		private CollectableUpdater m_OverrideTarget;

		[SerializeField]
		private bool m_IsOverrideAnimData;

		[SerializeField]
		private AnimationData.SendAnimData m_OverrideAnimData;

		[SerializeField]
		private bool m_IsDecreaseSendAmount;

		[SerializeField]
		private bool m_IsUseTextFormat;

		[SerializeField]
		private bool m_IsHideBigNumbers = true;

		private Action m_SendCompleteCallback;

		private int m_CollectableAmount;

		private int m_ReceivedCollectableAmount;

		private int m_SentCollectableAmount;

		private string m_TextFormat;

		private bool m_IsHapticsCooldown;

		private AnimationData m_AnimationData => GameConfig.Instance.HUD.AnimationData;

		protected virtual eCollectableType CollectableType => eCollectableType.Coin;

		public RectTransform RectTransform => m_RectTransform;

		private AnimationData.SendAnimDataDictionary m_AnimDataDictionary => m_AnimationData.SendAnim;

		private AnimationData.SendAnimData m_AnimData => m_IsOverrideAnimData ? m_OverrideAnimData : (m_AnimDataDictionary.ContainsKey(m_AnimType) ? m_AnimDataDictionary[m_AnimType] : m_AnimDataDictionary[eCollectableSendAnimType.Default]);

		public virtual void Set(int i_CollectableAmount)
		{
			m_CollectableAmount = i_CollectableAmount;
			updateCoinValueText(i_CollectableAmount);
		}

		private void updateCoinValueText(int i_CollectableValue)
		{
			if (m_CollectableText == null) throw new NullReferenceException(nameof(m_CollectableText));
			string text = m_IsHideBigNumbers ? hideBigNumber(i_CollectableValue) : i_CollectableValue.ToString();
			if (m_IsUseTextFormat)
			{
				if (m_TextFormat == null) m_TextFormat = m_CollectableText.text;
				text = string.Format(m_TextFormat, i_CollectableValue);
			}
			m_CollectableText.SetText(text);
		}

		private string hideBigNumber(int i_Num)
		{
			if (i_Num < 10000) return i_Num.ToString();
			if (i_Num >= 100000000) return (i_Num / 1000000d).ToString() + "M";
			if (i_Num >= 1000000) return (i_Num / 1000000d).ToString() + "M";
			return (i_Num / 1000d).ToString() + "K";
		}

		public virtual void Send(Action i_CompleteCallback = null)
		{
			Send(m_CollectableIcon, i_CompleteCallback);
		}

		public virtual void Send(RectTransform i_SpawnRectTransform, Action i_CompleteCallback = null)
		{
			CollectableUpdater target = m_IsOverrideTarget && m_OverrideTarget != null ? m_OverrideTarget : CollectableManager.Instance.DefaultCollectableTarget(CollectableType);
			if (target == null) throw new NullReferenceException(nameof(target));
			m_SendCompleteCallback = i_CompleteCallback;
			StartCoroutine(sendsCoroutine(i_SpawnRectTransform, target.MoveTarget, m_AnimData));
		}

		protected virtual IEnumerator sendsCoroutine(RectTransform i_SpawnRectTransform, RectTransform i_TargetRectTransform, AnimationData.SendAnimData i_AnimData)
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
				if (sendAmount != 0)
				{
					collectableValue = m_CollectableAmount / sendAmount;
					collectableValueRest = m_CollectableAmount % sendAmount;
				}
				else
				{
					sendAmount = 1;
					collectableValue = m_CollectableAmount;
					collectableValueRest = 0;
				}
			}
			m_ReceivedCollectableAmount = 0;
			m_SentCollectableAmount = 0;
			for (int i = 0; i < sendAmount; i++)
			{
				CollectableUI collectable = PoolManager.Instance.Dequeue(ePoolType.CollectableUI).GetComponent<CollectableUI>();
				collectable.transform.SetParent(i_TargetRectTransform);
				collectable.Value = (i == sendAmount - 1 ? collectableValueRest : 0) + collectableValue;
				collectable.Initialize(i_SpawnRectTransform, CollectableType);
				collectable.Send(i_TargetRectTransform, i_AnimData.AnimMode, animData, collectableMoveComplete);
				float delay = animData.DelayBetween * animData.DelayCurve.Evaluate((float)i / (float)sendAmount);
				m_SentCollectableAmount += collectable.Value;
				if (m_IsDecreaseSendAmount)
					updateCoinValueText(m_CollectableAmount - m_SentCollectableAmount);
				if (delay != 0f)
					yield return new WaitForSeconds(delay);
			}
		}

		protected virtual void collectableMoveComplete(CollectableUI i_SentCollectable)
		{
			if (i_SentCollectable == null) throw new NullReferenceException(nameof(i_SentCollectable));
			m_ReceivedCollectableAmount += i_SentCollectable.Value;
			AnimationData data = m_AnimationData;
			if (m_ReceivedCollectableAmount == m_CollectableAmount)
			{
				m_SendCompleteCallback?.Invoke();
				if (data?.Haptics != null && data.Haptics.IsPlayHapticsOnReceive) playHaptics(data.Haptics.CompleteHaptics);
			}
			else if (data?.Haptics != null && data.Haptics.IsPlayHapticsOnReceive) playHaptics(data.Haptics.ReceivedHaptics);
		}

		private void playHaptics(MMFeedbackHaptics.HapticTypes i_HapticsType)
		{
			if (m_IsHapticsCooldown) return;
			m_IsHapticsCooldown = true;
			if (Managers.Instance == null || Managers.Instance.HapticManager == null) throw new NullReferenceException();
			Managers.Instance.HapticManager.Haptic(i_HapticsType);
			Invoke(nameof(resetHapticsCooldown), m_AnimationData?.Haptics?.HapticsCooldown ?? 0f);
		}

		private void resetHapticsCooldown()
		{
			m_IsHapticsCooldown = false;
		}

		private void sendTest()
		{
			if (Application.isPlaying) Send();
		}

		protected virtual void SetRefs()
		{
			m_CollectableText = transform.FindDeepChild<TextMeshProUGUI>("Collectable Text");
			if (m_CollectableText == null) m_CollectableText = transform.FindDeepChild<TextMeshProUGUI>("Coin Text");
			m_CollectableIcon = transform.FindDeepChild<RectTransform>("Collectable Icon");
			if (m_CollectableIcon == null) m_CollectableIcon = transform.FindDeepChild<RectTransform>("Coin Icon");
			m_RectTransform = GetComponent<RectTransform>();
		}
	}
}
