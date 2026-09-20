using TMPro;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	public abstract class TShopTab : MonoBehaviour
	{
		[SerializeField]
		protected TextMeshProUGUI m_TabLabel;

		[SerializeField]
		protected TextMeshProUGUI m_UnlockedCounter;

		[SerializeField]
		protected RectTransform m_Grid;

		[SerializeField]
		protected GameObject m_UnlockedCounterRoot;

		protected int m_UnlockedNumber;

		protected int m_TotalNumber;

		public bool IsShowNotification { get; private set; }

		public bool IsActive { get; private set; }

		public eItemRarity TabRarity { get; protected set; }

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			return;
		}

		public abstract void Initialize(eItemRarity i_TabRarity);

		public void TabVisibility(bool i_Value)
		{
			gameObject.SetActive(i_Value);
		}

		protected virtual void SetRefs()
		{
			m_TabLabel = GetComponentInChildren<TextMeshProUGUI>();
		}
	}
}
