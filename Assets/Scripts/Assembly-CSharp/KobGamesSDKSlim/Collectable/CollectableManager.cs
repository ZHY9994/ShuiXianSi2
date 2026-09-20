using System;
using UnityEngine;

namespace KobGamesSDKSlim.Collectable
{
	public class CollectableManager : Singleton<CollectableManager>
	{
		[Serializable]
		public class TypeCollectableUpdaterDictionary : UnitySerializedDictionary<eCollectableType, CollectableUpdater>
		{
		}

		[SerializeField]
		private Canvas m_HUDCanvas;

		[SerializeField]
		private Canvas m_WorldCanvas;

		[SerializeField]
		private TypeCollectableUpdaterDictionary m_DefaultCollectableTargetDictionary;

		[SerializeField]
		private UniversalCollectableSender m_CollectableSender;

		[SerializeField]
		private bool m_IsOverrideCollectableParent;

		[SerializeField]
		private Transform m_DefaultCollectableParent;

		public Canvas HUDCanvas => m_HUDCanvas;

		public CollectableUpdater DefaultCollectableTarget(eCollectableType i_Type)
		{
			return m_DefaultCollectableTargetDictionary[i_Type];
		}

		private void SetRefs()
		{
			m_WorldCanvas = GetComponent<Canvas>();
			if (m_WorldCanvas != null) m_WorldCanvas.worldCamera = Camera.main;
			Transform parent = transform.parent;
			if (parent == null) return;
			HUDManager hud = parent.GetComponentInChildren<HUDManager>();
			if (hud == null) return;
			m_HUDCanvas = hud.GetComponent<Canvas>();
			if (m_HUDCanvas == null) return;
			m_CollectableSender = m_HUDCanvas.transform.FindDeepChild<UniversalCollectableSender>("Collectable Sender");
			CollectableUpdater[] updaters = m_IsOverrideCollectableParent && m_DefaultCollectableParent != null
				? m_DefaultCollectableParent.GetComponentsInChildren<CollectableUpdater>(true)
				: m_HUDCanvas.GetComponentsInChildren<CollectableUpdater>();
			m_DefaultCollectableTargetDictionary = new TypeCollectableUpdaterDictionary();
			foreach (CollectableUpdater updater in updaters)
				if (updater != null && !m_DefaultCollectableTargetDictionary.ContainsKey(updater.CollectableType))
					m_DefaultCollectableTargetDictionary.Add(updater.CollectableType, updater);
		}

		private void OnValidate()
		{
			SetRefs();
		}

		private void sendCollectables(eCollectableType i_CollectableType, int i_SendAmount, eCollectableSendAnimType i_SendType, Vector2 i_ScreenPosition)
		{
			m_CollectableSender.Send(i_CollectableType, i_SendAmount, i_SendType, i_ScreenPosition);
		}

		public void SendCollectables(eCollectableType i_CollectableType, int i_SendAmount, eCollectableSendAnimType i_AnimType, Vector3 i_Position, bool i_IsScreenSpace = false)
		{
			Vector3 position = i_IsScreenSpace ? i_Position : Camera.main.WorldToScreenPoint(i_Position);
			sendCollectables(i_CollectableType, i_SendAmount, i_AnimType, new Vector2(position.x, position.y));
		}

		public void ShowEarnMessage(eCollectableType i_CollectableType, int i_EarnAmount, eCollectableEarnMessageAnimType i_AnimType, Vector3 i_Position)
		{
			GameObject obj = PoolManager.Instance.Dequeue(ePoolType.CollectableEarnMessage);
			CollectableEarnMessage message = obj.GetComponent<CollectableEarnMessage>();
			message.transform.SetParent(m_WorldCanvas.transform);
			message.ShowMessage(i_CollectableType, i_EarnAmount, i_AnimType, i_Position);
		}

		public CollectableManager()
		{
		}
	}
}
