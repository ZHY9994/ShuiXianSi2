using DG.Tweening;
using KobGamesSDKSlim;
using UnityEngine;

namespace Wayne.Game.Shops.Skins
{
	public class SkinRarityItemHandler : MonoBehaviour
	{
		[SerializeField]
		private Scaler m_Scaler;

		[SerializeField]
		private SkinRarityItem[] m_RarityContainers;

		private SkinRarityItem m_SelectedItem;

		private Tween m_Tween;

		private SkinItemData m_ItemData;

		public SkinRarityItemHandler() : base()
		{
		}

		public void Initialize(SkinItemData i_ItemData)
		{
			hideAllContainers();
			if (i_ItemData == null) return;
			m_ItemData = i_ItemData;
			m_SelectedItem = m_RarityContainers[(int)i_ItemData.ItemRarity];
			m_SelectedItem.Show();
			m_SelectedItem.Initialize(m_ItemData);
		}

		public void UpdateController()
		{
			if (m_SelectedItem != null) m_SelectedItem.UpdateItem();
		}

		private void hideAllContainers()
		{
			for (int i = 0; i < m_RarityContainers.Length; i++)
				m_RarityContainers[i].Hide();
		}

		public void ShowSelection(bool i_Value)
		{
			if (i_Value)
			{
				m_Tween = DOVirtual.DelayedCall(0.375f, () => m_Scaler?.StartAnimation());
			}
			else
			{
				m_Tween?.Kill();
				m_Scaler?.ResetValues();
				m_Scaler?.StopAnimation();
			}
			if (m_SelectedItem != null) m_SelectedItem.ShowSelection(i_Value);
		}

		private void SetRefs()
		{
			m_RarityContainers = GetComponentsInChildren<SkinRarityItem>(true);
			m_Scaler = GetComponentInChildren<Scaler>();
		}
	}
}
