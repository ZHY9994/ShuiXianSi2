using UnityEngine;
using KobGamesSDKSlim;

namespace Wayne.Game.Shops.Skins
{
	public class SkinRarityItem : TShopRarityItem
	{
		[SerializeField]
		private GameObject m_Selection;

		private SkinItemData m_ItemData;

		public SkinRarityItem() : base()
		{
		}

		protected override bool m_IsInUse => m_ItemData != null && m_ItemData.IsSelected;

		public void Initialize(SkinItemData i_ShopItemData)
		{
			if (i_ShopItemData == null) return;
			m_ItemData = i_ShopItemData;
			updateState();
			for (int i = 0; i < m_Icons.Length; i++)
			{
				RectTransform rectTransform = m_Icons[i].rectTransform;
				rectTransform.anchoredPosition = SkinShopManager.Instance.ShopData.PreviewOffset;
				rectTransform.sizeDelta = SkinShopManager.Instance.ShopData.PreviewSize;
				m_Icons[i].sprite = m_ItemData.ShopSprite;
			}
		}

		protected override void updateState()
		{
			m_State = m_ItemData != null && m_ItemData.IsUnlocked &&
				m_ItemData.RequiredLevel < StorageManager.Instance.CurrentLevel
				? eState.Unlocked : eState.Locked;
			base.updateState();
		}

		public void ShowSelection(bool i_Value)
		{
			m_Selection.SetActive(i_Value);
			updateVisual();
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			m_Selection = transform.FindDeepChild<GameObject>("Selection");
		}
	}
}
