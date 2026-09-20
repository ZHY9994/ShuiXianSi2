using KobGamesSDKSlim.Shop;
using System.Linq;
using UnityEngine;
using KobGamesSDKSlim;

namespace Wayne.Game.Shops.Weapons
{
	public class WeaponShopTab : TShopTab
	{
		[SerializeField]
		private WeaponShopItem[] m_ShopItems;

		private WeaponItemData[] m_Items;

		public WeaponShopItem[] ShopItems => m_ShopItems;

		private TabData.UniqueTabData m_TabsParameters => WeaponShopManager.Instance.ShopData.ShopTabData.Tabs[TabRarity];

		public WeaponShopTab() { }

		protected override void OnEnable()
		{
			base.OnEnable();
			WeaponShopItem.OnUnlock += onUnlock;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			WeaponShopItem.OnUnlock -= onUnlock;
		}

		private void onUnlock(WeaponShopItem i_Item)
		{
			if (i_Item == null || i_Item.ItemData == null || i_Item.ItemData.ItemRarity != TabRarity)
				return;
			m_UnlockedNumber++;
			m_UnlockedCounter.SetText($"{m_UnlockedNumber}/{m_TotalNumber}");
		}

		public override void Initialize(eItemRarity i_TabRarity)
		{
			GameTraceLog.Log($"[ShopTrace] WeaponShopTab.Initialize tab={name} rarity={i_TabRarity} shopItems={(m_ShopItems == null ? -1 : m_ShopItems.Length)} label={(m_TabLabel == null ? "null" : m_TabLabel.name)}");
			m_UnlockedCounterRoot.gameObject.SetActive(true);
			TabRarity = i_TabRarity;
			m_TabLabel.SetText(LocalizationUtility.GetWordLocalizedString(m_TabsParameters.Label.ToLower()).ToUpper());
			m_Items = WeaponShopManager.Instance.GetCurrentItemsByRarity(TabRarity)
				.Where(x => x.ItemRarity == WeaponShopManager.Instance.ActiveTabRarity).ToArray();
			GameTraceLog.Log($"[ShopTrace] WeaponShopTab data activeRarity={WeaponShopManager.Instance.ActiveTabRarity} items={(m_Items == null ? -1 : m_Items.Length)}");
			m_TotalNumber = 0;
			m_UnlockedNumber = 0;
			int count = System.Math.Min(m_TabsParameters.ItemsNumber, m_Items.Length);
			for (int i = 0; i < m_ShopItems.Length; i++)
			{
				m_ShopItems[i].Initialize(i < count ? m_Items[i] : null);
				if (i >= count) continue;
				m_TotalNumber++;
				if (m_Items[i].IsUnlocked && m_Items[i].RequiredLevel + 1 <= StorageManager.Instance.CurrentLevel)
					m_UnlockedNumber++;
			}
			int comingSoonNumber = m_TabsParameters.ComingSoonNumber;
			while (m_TotalNumber < m_ShopItems.Length && comingSoonNumber > 0)
			{
				m_ShopItems[m_TotalNumber].Initialize(null);
				m_TotalNumber++;
				comingSoonNumber--;
			}
			m_UnlockedCounter.SetText($"{m_UnlockedNumber}/{m_TotalNumber}");
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			m_ShopItems = GetComponentsInChildren<WeaponShopItem>(true);
		}
	}
}
