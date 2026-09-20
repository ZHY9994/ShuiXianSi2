using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wayne.Game.Shops.Weapons
{
	public class WeaponPreviewItem : MonoBehaviour
	{
		[SerializeField]
		private WeaponRarityItemHandler m_RarityHandler;

		[SerializeField]
		private bool m_UseCounter;

		[SerializeField]
		private GameObject m_CounterRoot;

		[SerializeField]
		private GameObject m_MaxRoot;

		[SerializeField]
		private TextMeshProUGUI m_Counter;

		[SerializeField]
		private Image m_Fill;

		public WeaponItemData ItemData { get; private set; }

		public void Initialize(WeaponItemData i_ItemData)
		{
			if (i_ItemData != null)
			{
				ItemData = i_ItemData;
				updateVisuals();
			}
		}

		private void updateVisuals()
		{
			if (m_RarityHandler == null || ItemData == null)
				return;
			m_RarityHandler.Initialize(ItemData);
			bool isUnlocked = m_UseCounter && ItemData.IsUnlocked;
			bool isMaxed = ItemData.IsMaxed;
			if (m_CounterRoot != null)
				m_CounterRoot.SetActive(isUnlocked && !isMaxed);
			if (m_MaxRoot != null)
				m_MaxRoot.SetActive(isUnlocked && isMaxed);
			if (!isUnlocked)
				return;
			var upgradeData = ItemData.UpgradeData;
			int requiredCards = upgradeData?.RequiredNumberOfCards ?? 0;
			if (m_Counter != null)
				m_Counter.text = string.Format("{0}/{1}", ItemData.CardAmount, requiredCards);
			if (m_Fill != null)
				m_Fill.fillAmount = requiredCards > 0 ? (float)ItemData.CardAmount / requiredCards : 0f;
		}
	}
}
