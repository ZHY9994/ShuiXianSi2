using UnityEngine;

namespace Wayne.Game.Shops.Weapons
{
	public class WeaponRarityItemHandler : MonoBehaviour
	{
		[SerializeField]
		private WeaponRarityItem[] m_RarityContainers;

		private WeaponRarityItem m_SelectedItem;

		private WeaponItemData m_ItemData;

		public WeaponRarityItemHandler()
		{
		}

		public void Initialize(WeaponItemData i_ItemData)
		{
			hideAllContainers();
			if (i_ItemData == null) return;
			m_ItemData = i_ItemData;
			if (m_RarityContainers == null) return;
			m_SelectedItem = m_RarityContainers[(int)m_ItemData.ItemRarity];
			if (m_SelectedItem == null) return;
			m_SelectedItem.Show();
			GameTraceLog.Log($"[RarityTrace] item={m_ItemData.Name} rarity={m_ItemData.ItemRarity} index={(int)m_ItemData.ItemRarity} selected={m_SelectedItem.name} active={m_SelectedItem.gameObject.activeSelf} containers={string.Join(",", System.Array.ConvertAll(m_RarityContainers, item => item == null ? "NULL" : item.name))}");
			m_SelectedItem.Initialize(m_ItemData);
		}

		public void UpdateController()
		{
			if (m_SelectedItem != null) m_SelectedItem.UpdateItem();
		}

		private void hideAllContainers()
		{
			if (m_RarityContainers == null) return;
			for (int i = 0; i < m_RarityContainers.Length; i++)
			{
				if (m_RarityContainers[i] != null) m_RarityContainers[i].Hide();
			}
		}

		private void SetRefs()
		{
			m_RarityContainers = GetComponentsInChildren<WeaponRarityItem>(true);
		}
	}
}
