using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim.Shop;
using UnityEngine;

namespace Wayne.Game.Shops
{
	public class WeaponShopTabController : TShopTabController
	{
		public static event Action OnTabInitialized = delegate { };

		protected override void OnEnable()
		{
			GameTraceLog.Log($"[ShopTrace] WeaponShopTabController.OnEnable go={name} active={gameObject.activeInHierarchy} all={(m_AllTabs == null ? -1 : m_AllTabs.Length)}");
			WeaponShopManager.OnShopOpen += onShopOpen;
			WeaponShopManager.OnUpdateShop += onUpdateShop;
			WeaponShopManager.ReInitializeShop += onReInitializeShop;
		}

		protected override void OnDisable()
		{
			WeaponShopManager.OnShopOpen -= onShopOpen;
			WeaponShopManager.OnUpdateShop -= onUpdateShop;
			WeaponShopManager.ReInitializeShop -= onReInitializeShop;
		}

		protected override void initialize(bool i_InvokEvent = false)
		{
			GameTraceLog.Log($"[ShopTrace] WeaponShopTabController.initialize invoke={i_InvokEvent} all={(m_AllTabs == null ? -1 : m_AllTabs.Length)} manager={(WeaponShopManager.Instance == null ? 0 : 1)}");
			clearTabs();
			WeaponShopManager manager = WeaponShopManager.Instance;
			if (manager == null || m_AllTabs == null)
				return;

			eItemRarity[] usedRarities = manager.GetUsedWeaponRarity(manager.ActiveTabRarity);
			if (usedRarities == null || usedRarities.Length > m_AllTabs.Length)
			{
				GameTraceLog.Log($"[ShopTrace] WeaponShopTabController.initialize early used={(usedRarities == null ? -1 : usedRarities.Length)} all={m_AllTabs.Length}");
				return;
			}

			for (int i = 0; i < usedRarities.Length; i++)
			{
				TShopTab tab = m_AllTabs[i];
				if (tab == null)
					return;
				tab.gameObject.SetActive(true);
				tab.Initialize(usedRarities[i]);
				GameTraceLog.Log($"[ShopTrace] WeaponShopTabController.tab index={i} rarity={usedRarities[i]} tab={tab.name}");
				m_Tabs.Add(usedRarities[i], tab);
			}

			for (int i = 0; i < m_AllTabs.Length; i++)
				selectTab(m_AllTabs[i]);

			if (i_InvokEvent)
				OnTabInitialized?.Invoke();
		}
	}
}
