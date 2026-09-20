using KobGamesSDKSlim.Shop;
using System.Linq;
using UnityEngine;
using KobGamesSDKSlim;

namespace Wayne.Game.Shops.Skins
{
	public class SkinShopTab : TShopTab
	{
		[SerializeField]
		private SkinShopItem[] m_ShopItems;

		private SkinItemData[] m_Items;

		public SkinShopItem[] ShopItems => m_ShopItems;

		private TabData.UniqueTabData m_TabsParameters => SkinShopManager.Instance.ShopData.ShopTabData.Tabs[TabRarity];

		public SkinShopTab() { }

		protected override void OnEnable()
		{
			base.OnEnable();
			SkinShopItem.OnUnlock += onUnlock;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			SkinShopItem.OnUnlock -= onUnlock;
		}

		private void onUnlock(SkinShopItem i_Item)
		{
			if (i_Item == null || i_Item.ItemData == null || i_Item.ItemData.ItemRarity != TabRarity)
				return;
			m_UnlockedNumber++;
			m_UnlockedCounter.SetText($"{m_UnlockedNumber}/{m_TotalNumber}");
		}

		public override void Initialize(eItemRarity i_TabRarity)
		{
			m_UnlockedCounterRoot.gameObject.SetActive(true);
			TabRarity = i_TabRarity;
			m_TabLabel.SetText(LocalizationUtility.GetWordLocalizedString(m_TabsParameters.Label.ToLower()).ToUpper());
			m_Items = SkinShopManager.Instance.GetCurrentItemsByRarity(TabRarity)
				.Where(x => x.ItemRarity == SkinShopManager.Instance.ActiveTabRarity)
				.OrderByDescending(x => (int)x.UnlockMethod == 3)
				.ThenByDescending(x => (int)x.UnlockMethod == 1)
				.ThenBy(x => x.RequiredMoney).ToArray();
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
			m_ShopItems = GetComponentsInChildren<SkinShopItem>(true);
		}
	}
}
