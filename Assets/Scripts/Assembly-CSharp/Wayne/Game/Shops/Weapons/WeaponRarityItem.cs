using TMPro;
using UnityEngine;
using KobGamesSDKSlim;

namespace Wayne.Game.Shops.Weapons
{
	public class WeaponRarityItem : TShopRarityItem
	{
		[SerializeField]
		private GameObject m_LevelRoot;

		[SerializeField]
		private TextMeshProUGUI m_LevelText;

		private WeaponItemData m_ItemData;

		protected override bool m_IsInUse => m_ItemData != null && DeckManager.Instance.IsWeaponInDeck(m_ItemData.WeaponType);

		public WeaponRarityItem()
		{
		}

		public void Initialize(WeaponItemData i_ShopItemData)
		{
			if (i_ShopItemData == null) return;
			m_ItemData = i_ShopItemData;
			updateState();

			if (WeaponShopManager.Instance == null || WeaponShopManager.Instance.ShopData == null || m_Icons == null) return;
			for (int i = 0; i < m_Icons.Length; i++)
			{
				if (m_Icons[i] == null) continue;
				m_Icons[i].rectTransform.anchoredPosition = WeaponShopManager.Instance.ShopData.PreviewOffset;
				m_Icons[i].rectTransform.sizeDelta = WeaponShopManager.Instance.ShopData.PreviewSize;
				m_Icons[i].sprite = m_ItemData.ShopSprite;
				GameTraceLog.Log($"[ShopTrace] WeaponRarityItem.Initialize card={transform.parent?.name ?? name} item={m_ItemData.Name} icon={m_Icons[i].name} sprite={(m_Icons[i].sprite == null ? "NULL" : m_Icons[i].sprite.name)}");
			}
		}

		protected override void updateState()
		{
			if (m_ItemData != null && m_ItemData.IsUnlocked)
			{
				m_State = m_ItemData.RequiredLevel < StorageManager.Instance.CurrentLevel ? eState.Unlocked : eState.Locked;
			}
			else
			{
				m_State = eState.Locked;
			}
			base.updateState();
		}

		protected override void updateVisual()
		{
			base.updateVisual();
			string levelWord = LocalizationUtility.GetWordLocalizedString("lvl").ToLower();
			if (m_LevelText == null) return;
			m_LevelText.fontSizeMin = LocalizationUtility.GetLocalType() == eLocaleType.EN ? 10f : 32.8f;
			if (m_ItemData == null || m_ItemData.WeaponData == null) return;
			m_LevelText.text = string.Concat(levelWord, " ", m_ItemData.WeaponData.Level.ToString());
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			Transform levelRoot = transform.FindDeepChild<Transform>("Level Root");
			m_LevelRoot = levelRoot == null ? null : levelRoot.gameObject;
			m_LevelText = transform.FindDeepChild<TextMeshProUGUI>("Level Text");
		}
	}
}
