using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim.Shop;

namespace Wayne.Game.Shops
{
	public class SkinShopTabController : TShopTabController
	{
		public static event Action OnTabInitialized = delegate { };

		protected override void OnEnable()
		{
			base.OnEnable();
			SkinShopManager.OnShopOpen += onShopOpen;
			SkinShopManager.OnUpdateShop += onUpdateShop;
			SkinShopManager.ReInitializeShop += onReInitializeShop;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			SkinShopManager.OnShopOpen -= onShopOpen;
			SkinShopManager.OnUpdateShop -= onUpdateShop;
			SkinShopManager.ReInitializeShop -= onReInitializeShop;
		}

		protected override void initialize(bool i_InvokEvent = false)
		{
			clearTabs();
			base.initialize(false);
			eItemRarity[] usedSkinRarity = SkinShopManager.Instance.GetUsedSkinRarity(SkinShopManager.Instance.ActiveTabRarity);
			if (usedSkinRarity == null || m_AllTabs == null)
				return;
			if (usedSkinRarity.Length > m_AllTabs.Length)
				return;
			for (int i = 0; i < usedSkinRarity.Length; i++)
			{
				TShopTab tab = m_AllTabs[i];
				if (tab == null)
					return;
				tab.gameObject.SetActive(true);
				tab.Initialize(usedSkinRarity[i]);
				m_Tabs.Add(usedSkinRarity[i], tab);
			}
			for (int i = 0; i < m_AllTabs.Length; i++)
				selectTab(m_AllTabs[i]);
			if (i_InvokEvent)
				OnTabInitialized?.Invoke();
		}

		public SkinShopTabController()
		{
		}
	}
}
