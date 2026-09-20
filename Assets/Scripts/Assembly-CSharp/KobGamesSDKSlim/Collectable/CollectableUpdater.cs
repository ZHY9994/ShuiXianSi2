using System;
using System.Collections;
using UnityEngine;

namespace KobGamesSDKSlim.Collectable
{
	public class CollectableUpdater : CollectableContainer
	{
		[SerializeField]
		private eCollectableType m_CollectableType;

		[SerializeField]
		private eCollectableReceiveAnimType m_ReceiverType;

		[SerializeField]
		private bool m_IsOverrideAnimData;

		[SerializeField]
		private AnimationData.ReceiveAnimData m_OverrideAnimData;

		private Coroutine m_RecieveCoroutine;

		public eCollectableType CollectableType => m_CollectableType;

		private AnimationData m_AnimationData => GameConfig.Instance.HUD.AnimationData;

		private AnimationData.ReceiveAnimDataDictionary m_AnimDataDictionary => m_AnimationData.ReceiveAnim;

		private AnimationData.ReceiveAnimData m_AnimData => m_IsOverrideAnimData ? m_OverrideAnimData : (m_AnimDataDictionary.ContainsKey(m_ReceiverType) ? m_AnimDataDictionary[m_ReceiverType] : m_AnimDataDictionary[eCollectableReceiveAnimType.Default]);

		protected override void OnEnable()
		{
			base.OnEnable();
			StorageManagerBase.OnCollectableAmountChanged += collectableValueChanged;
			SetCollectableValue(StorageManager.Instance.GetCollectable(m_CollectableType), false);
		}

		protected override void OnDisable()
		{
			StorageManagerBase.OnCollectableAmountChanged -= collectableValueChanged;
			if (m_RecieveCoroutine != null) { StopCoroutine(m_RecieveCoroutine); m_RecieveCoroutine = null; }
			base.OnDisable();
		}

		protected virtual void collectableValueChanged(eCollectableType i_CollectableType, int i_EarnAmount, int i_ResultAmount)
		{
			if (m_CollectableType != i_CollectableType || m_AnimData == null) return;
			SetCollectableValue(i_ResultAmount, m_AnimData.IsPunchIconScale);
			if (i_EarnAmount < 1 || m_AnimData.ShowMode == AnimationData.ReceiveAnimData.eShowMode.None || m_AnimData.ShowMode == AnimationData.ReceiveAnimData.eShowMode.OnDecreaseAmount) return;
			if (m_RecieveCoroutine != null) StopCoroutine(m_RecieveCoroutine);
			m_RecieveCoroutine = StartCoroutine(receiveCoroutine(m_MoveTarget, m_IsOverrideAnimData ? m_OverrideAnimData : m_AnimData, i_EarnAmount, true));
		}

		protected virtual IEnumerator receiveCoroutine(RectTransform i_SpawnRectTransform, AnimationData.ReceiveAnimData i_AnimData, int i_ReceiveAmount, bool i_IsIncrease)
		{
			AnimationData.ReceiveAnimData.BaseAnimData animData = null;
			if (i_AnimData.AnimMode == AnimationData.ReceiveAnimData.eAnimMode.Burst)
				animData = i_IsIncrease ? i_AnimData.IncreaseAnimMode : i_AnimData.DecreaseAnimMode;
			int amount = i_ReceiveAmount / animData.ItemCost;
			int sendAmount = animData.MaxAmount;
			if (amount < sendAmount)
				sendAmount = amount;
			if (amount < 0)
				sendAmount = 0;
			for (int i = 0; i < sendAmount; i++)
			{
				CollectableUI collectable = PoolManager.Instance.Dequeue(ePoolType.CollectableUI).GetComponent<CollectableUI>();
				collectable.transform.SetParent(i_SpawnRectTransform);
				collectable.Initialize(i_SpawnRectTransform, m_CollectableType);
				collectable.Receive(m_AnimData.AnimMode, animData);
				float delay = animData.DelayBetween * animData.DelayCurve.Evaluate((float)i / (float)sendAmount);
				if (delay != 0f)
					yield return new WaitForSeconds(delay);
			}
		}
	}
}
