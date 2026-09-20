using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using MrWayne;
using UnityEngine;
using KobGamesSDKSlim.MenuManagerV1;
using Wayne.Game.Shops.Weapons;

namespace Wayne.Game.Shops
{
	public class WeaponShopManager : Singleton<WeaponShopManager>
	{
		public delegate void ItemEvent(WeaponItemData i_Item);

		public delegate void ShopEvent();

		private UpgradeDataEditor m_UpgradeData => GameConfig.Instance.GamePlay.WeaponData.Upgrade;

		public WeaponShopData ShopData => GameConfig.Instance.Shop.Shops.Weapons;

		public eItemRarity ActiveTabRarity { get; private set; }

		public bool IsShopOpened { get; private set; }

		public WeaponItemData[] WeaponItems => ShopData.Items;

		public WeaponItemData SelectedItem => GetSelectedItem();

		public static event ItemEvent OnSelectItem = delegate { };

		public static event ItemEvent OnUnlockItem = delegate { };

		public static event ItemEvent OnClickItem = delegate { };

		public static event ItemEvent OnWeaponUpgrade = delegate { };

		public static event ItemEvent OnCardScreenOpen = delegate { };

		public static event ShopEvent OnShopOpen = delegate { };

		public static event ShopEvent OnShopClose = delegate { };

		public static event ShopEvent OnUpdateShop = delegate { };

		public static event ShopEvent ReInitializeShop = delegate { };


		protected override void OnEnable()
		{
			GameTraceLog.Log($"[ShopTrace] WeaponShopManager.OnEnable begin instance={GetInstanceID()} active={gameObject.activeInHierarchy}");
			base.OnEnable();
			initialize();
			var weapons = GameConfig.Instance != null && GameConfig.Instance.GamePlay != null && GameConfig.Instance.GamePlay.WeaponData != null ? GameConfig.Instance.GamePlay.WeaponData.Weapons : null;
			GameTraceLog.Log($"[ShopTrace] WeaponShopManager.OnEnable afterInitialize weapons={(weapons == null ? "NULL" : "OK")} dictionary={(weapons == null || weapons.WeaponDictionary == null ? "NULL" : weapons.WeaponDictionary.Count.ToString())}");
			WeaponTabButton.OnTabSelected += onTabSelected;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			WeaponTabButton.OnTabSelected -= onTabSelected;
		}

		private void onTabSelected(eItemRarity i_Rarity)
		{
			ActiveTabRarity = i_Rarity;
			ReInitializeShop?.Invoke();
		}

		private void initialize()
		{
			var weapons = GameConfig.Instance.GamePlay.WeaponData.Weapons;
			GameTraceLog.Log($"[ShopTrace] WeaponShopManager.initialize weapons={(weapons == null ? "NULL" : "OK")}");
			weapons.Initialize();
			GameTraceLog.Log($"[ShopTrace] WeaponShopManager.initialize complete dictionary={(weapons.WeaponDictionary == null ? "NULL" : weapons.WeaponDictionary.Count.ToString())}");
		}

		public void OpenShop()
		{
			GameTraceLog.Log($"[ShopTrace] WeaponShopManager.OpenShop instance={GetInstanceID()} hasMenuRefs={MenuManager.Instance?.MenuScreensRefs != null}");
			IsShopOpened = true;
			ActiveTabRarity = 0;
			MenuManager.Instance.OpenMenuScreen("Screen_WeaponShop");
			OnShopOpen?.Invoke();
			GameTraceLog.Log($"[ShopTrace] WeaponShopManager.OpenShop eventDone opened={IsShopOpened} activeRarity={ActiveTabRarity}");
		}

		public void CloseShop()
		{
			IsShopOpened = false;
			MenuManager.Instance.CloseMenuScreen("Screen_WeaponShop");
			clearNotification();
			OnShopClose?.Invoke();
		}

		public void UpdateShop()
		{
			OnUpdateShop?.Invoke();
		}

		public void SelectItem(WeaponItemData i_Item)
		{
			setSelectedItemIndex(i_Item.Index);
		}

		public void ShopSelectItem(WeaponItemData i_Item)
		{
			if (GetSelectedItem() != null && i_Item != null && GetSelectedItem().Name != null && string.Equals(GetSelectedItem().Name, i_Item.Name))
			{
				if (GameConfig.Instance.Shop.ShopSettings.General.IsCloseShopOnClickingSelectedItem)
					CloseShop();
			}
			setSelectedItemIndex(i_Item.Index);
			i_Item.ItemHasShownNotification();
			OnSelectItem?.Invoke(i_Item);
			MenuManager.Instance.OpenMenuScreen("Screen_ShopCard");
			OnCardScreenOpen?.Invoke(i_Item);
		}

		public void ClickItem(WeaponItemData i_Item)
		{
			OnClickItem?.Invoke(i_Item);
		}

		public void UnlockItem(WeaponItemData i_Item, bool i_IsReward = false)
		{
			i_Item.UnlockItem(i_IsReward);
			OnUnlockItem?.Invoke(i_Item);
			UpdateShop();
			if (!i_IsReward) ShopSelectItem(i_Item);
		}

		public void UpgradeItem(WeaponItemData i_Item, bool i_WithRV)
		{
			if (i_Item == null) return;
			if (i_Item.IsMaxed || i_Item.UpgradeData == null) return;
			upgradeWeapon(i_Item, i_WithRV);
			i_Item.Upgrade(i_WithRV);
			OnWeaponUpgrade?.Invoke(i_Item);
			UpdateShop();
			DeckManager.Instance.UpdateDeck();
		}

		private void clearNotification()
		{
			foreach (WeaponItemData item in WeaponItems)
			{
				if (!item.HasShownNotification && item.IsUnlocked && StorageManager.Instance.CurrentLevel >= item.RequiredLevel + 1)
					item.ItemHasShownNotification();
			}
		}

		public UpgradeDataEditor.UpgradeData GetUpgradeData(int i_Level)
		{
			return m_UpgradeData.GetUpgradeData(i_Level);
		}

		public WeaponData GetWeaponData(eWeaponType i_Type)
		{
			return GameConfig.Instance.GamePlay.WeaponData.Weapons.WeaponDictionary[i_Type];

		}

		public WeaponItemData[] GetCurrentItemsByRarity(eItemRarity i_Rarity)
		{
			return GetItemsByRarity(i_Rarity).OrderBy(x => x.Order).ToArray();
		}

		public WeaponItemData[] GetItemsByRarity(eItemRarity i_Rarity)
		{
			return ShopData.GetItemsByRarity(i_Rarity);

		}

		public WeaponItemData GetWeapon(eWeaponType i_Type)
		{
			return WeaponItems.FirstOrDefault(x => x.WeaponType == i_Type);

		}

		public WeaponItemData GetSelectedItem()
		{
			return WeaponItems[getSelectedItemIndex()];

		}

		private int getSelectedItemIndex()
		{
			return PlayerPrefs.GetInt(((eItemType)1).ToString(), 0);

		}

		private void setSelectedItemIndex(int i_Index)
		{
			PlayerPrefs.SetInt(((eItemType)1).ToString(), i_Index);
		}

		public Color GetItemRarityColor(eItemRarity i_Rarity)
		{
			return GameConfig.Instance.Shop.ShopEditorSettings.GetItemRarityColorEditor(i_Rarity);
		}

		public eItemRarity[] GetUsedRarityCount()
		{
			return WeaponItems.GroupBy(x => x.ItemRarity).Select(x => x.Key).ToArray();
		}

		public eItemRarity[] GetUsedWeaponRarity(eItemRarity i_Rarity)
		{
			return WeaponItems.Where(x => x.ItemRarity == i_Rarity).GroupBy(x => x.ItemRarity).Select(x => x.Key).ToArray();
		}

		public List<string> GetWeaponNames()
		{
			return WeaponItems.Select(x => x.Name).ToList();

		}

		private void upgradeWeapon(WeaponItemData i_Item, bool i_WithRV)
		{
			if (i_Item == null) return;
			WeaponData weapon = GetWeaponData(i_Item.WeaponType);
			if (weapon == null || m_UpgradeData == null || weapon.Level >= m_UpgradeData.MaxUpgrade) return;
			UpgradeDataEditor.UpgradeData upgrade = GetUpgradeData(weapon.Level + 1);
			if (upgrade == null) return;
			if (i_WithRV)
			{
				weapon.UpgradeLevel();
				weapon.AddCard(0);
			}
			else if (upgrade != null)
			{
				weapon.UpgradeLevel();
				weapon.AddCard(-upgrade.RequiredNumberOfCards);
			}
		}

		public void AddCard(eWeaponType i_Type, int i_Amount)
		{
			GetWeaponData(i_Type).AddCard(i_Amount < 0 ? 0 : i_Amount);
		}

		public void LockAllItems()
		{
			foreach (WeaponItemData item in WeaponItems) item.LockItem();
		}

		public void UnlockAllItems()
		{
			foreach (WeaponItemData item in WeaponItems) item.UnlockItem();
		}

		/// <summary>
		/// Unlocks every mythic weapon available as a reward, excluding VIP/IAP weapons.
		/// </summary>
		public int UnlockAllMythicItemsByReward()
		{
			if (WeaponItems == null) return 0;

			int unlockCount = 0;
			foreach (WeaponItemData item in WeaponItems)
			{
				if (item == null
					|| item.ItemRarity != eItemRarity.Legendary
					|| item.UnlockMethod == eItemUnlockMethod.IAP
					|| item.IsUnlocked) continue;

				UnlockItem(item, true);
				unlockCount++;
			}

			return unlockCount;
		}

		public void UnlockAllItemsAsIAP()
		{
			foreach (WeaponItemData item in WeaponItems) item.UnlockItem(false, true);
		}

		public void UnlockAllItemsIAP()
		{
			foreach (WeaponItemData item in WeaponItems) item.UnlockItemIAP();
		}

		private void addCardDebug(eWeaponType i_Type, int i_Amount)
		{
			AddCard(i_Type, i_Amount);
		}

		private void debugUnlock()
		{
			UnlockAllItems();
			UnlockAllItemsIAP();
			StorageManager.Instance.CurrentLevel = 33;
		}
	}
}
