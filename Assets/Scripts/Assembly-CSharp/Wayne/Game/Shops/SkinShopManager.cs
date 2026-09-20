using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using Wayne.Game.Shops.Skins;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;

namespace Wayne.Game.Shops
{
	public class SkinShopManager : Singleton<SkinShopManager>
	{
		public delegate void ItemEvent(SkinItemData i_Item);

		public delegate void ShopEvent();

		public eItemRarity ActiveTabRarity { get; private set; }

		public bool IsShopOpened { get; private set; }

		public SkinShopData ShopData => GameConfig.Instance.Shop.Shops.Skins;

		public SkinItemData[] SkinItems => ShopData.Items;

		public SkinItemData SelectedItem => GetSelectedItem();

		public static event ItemEvent OnSelectItem = delegate { };

		public static event ItemEvent OnUnlockItem = delegate { };

		public static event ItemEvent OnClickItem = delegate { };

		public static event ShopEvent OnShopOpen = delegate { };

		public static event ShopEvent OnShopClose = delegate { };

		public static event ShopEvent OnUpdateShop = delegate { };

		public static event ShopEvent ReInitializeShop = delegate { };

		public SkinShopManager()
		{
		}

		private new void OnEnable()
		{
			SkinTabButton.OnTabSelected += onTabSelected;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			SkinTabButton.OnTabSelected -= onTabSelected;
		}

		public void OpenShop()
		{
			IsShopOpened = true;
			ActiveTabRarity = 0;
			MenuManager.Instance.OpenMenuScreen("Screen_SkinShop");
			OnShopOpen?.Invoke();
		}

		public void CloseShop()
		{
			IsShopOpened = false;
			MenuManager.Instance.CloseMenuScreen("Screen_SkinShop");
			clearNotifications();
			OnShopClose?.Invoke();
		}

		private void onTabSelected(eItemRarity i_Rarity)
		{
			ActiveTabRarity = i_Rarity;
			ReInitializeShop?.Invoke();
		}

		private void clearNotifications()
		{
			foreach (SkinItemData item in SkinItems)
			{
				if (!item.HasShownNotification && item.IsUnlocked && StorageManager.Instance.CurrentLevel >= item.RequiredLevel + 1)
					item.ItemHasShownNotification();
			}
		}

		public void SelectItem(SkinItemData i_Item)
		{
			SkinItemData selected = GetSelectedItem();
		if (selected != null && i_Item != null && selected.Name != null && string.Equals(selected.Name, i_Item.Name) && GameConfig.Instance.Shop.ShopSettings.General.IsCloseShopOnClickingSelectedItem)
				CloseShop();
			setSelectedItemIndex(i_Item.Index);
			i_Item.ItemHasShownNotification();
			OnSelectItem?.Invoke(i_Item);
		}

		public void ClickItem(SkinItemData i_Item)
		{
			OnClickItem?.Invoke(i_Item);
		}

		public void UnlockItem(SkinItemData i_Item, bool i_IsReward = false)
		{
			i_Item.UnlockItem(i_IsReward);
			OnUnlockItem?.Invoke(i_Item);
			if (!i_IsReward) SelectItem(i_Item);
		}

		public SkinItemData GetSkin(string i_Name)
		{
			return SkinItems.FirstOrDefault(x => x.Name == i_Name);
		}

		public eItemRarity[] GetUsedSkinRarity(eItemRarity i_Rarity)
		{
			return SkinItems.Where(x => x.ItemRarity == i_Rarity).GroupBy(x => x.ItemRarity).Select(x => x.Key).ToArray();
		}

		public SkinItemData[] GetCurrentItemsByRarity(eItemRarity i_Rarity)
		{
			return GetItemsByRarity(i_Rarity).OrderBy(x => x.Index).ToArray();
		}

		public SkinItemData[] GetItemsByRarity(eItemRarity i_Rarity)
		{
			return ShopData.GetItemsByRarity(i_Rarity);
		}

		private int getSelectedItemIndex()
		{
			return PlayerPrefs.GetInt(((eItemType)0).ToString(), 0);
		}

		public SkinItemData GetSelectedItem()
		{
			return SkinItems[getSelectedItemIndex()];
		}

		private void setSelectedItemIndex(int i_Index)
		{
			PlayerPrefs.SetInt(((eItemType)0).ToString(), i_Index);
		}

		public Color GetItemRarityColorEditor(eItemRarity i_Rarity)
		{
			return GameConfig.Instance.Shop.ShopEditorSettings.GetItemRarityColorEditor(i_Rarity);
		}

		public Color GetUnlockMethodColorEditor(eItemUnlockMethod i_UnlockMethod)
		{
			return GameConfig.Instance.Shop.ShopEditorSettings.GetUnlockMethodColorEditor(i_UnlockMethod);
		}

		public List<string> GetItemNames()
		{
			return SkinItems.Select(x => x.Name).ToList();
		}

		public eItemRarity[] GetUsedRarityCount()
		{
			return SkinItems.GroupBy(x => x.ItemRarity).Select(x => x.Key).ToArray();
		}

		public void LockAllItems()
		{
			foreach (SkinItemData item in SkinItems) item.LockItem();
		}

		public void UnlockAllItems()
		{
			foreach (SkinItemData item in SkinItems) item.UnlockItem();
		}

		/// <summary>
		/// Unlocks the mythic skin set without granting VIP/IAP skins.
		/// </summary>
		public void UnlockAllMythicItemsByReward()
		{
			foreach (SkinItemData item in SkinItems)
			{
				if (item == null || item.ItemRarity != eItemRarity.Legendary || item.UnlockMethod == eItemUnlockMethod.IAP) continue;
				UnlockItem(item, true);
			}
		}

		/// <summary>
		/// Unlocks every skin available as a reward, excluding VIP/IAP skins.
		/// </summary>
		public void UnlockAllItemsByReward()
		{
			foreach (SkinItemData item in SkinItems)
			{
				if (item == null || item.UnlockMethod == eItemUnlockMethod.IAP || item.IsUnlocked) continue;
				UnlockItem(item, true);
			}
		}

		/// <summary>
		/// Legacy alias retained for callers that specifically used the old epic reward name.
		/// </summary>
		public void UnlockAllEpicItemsByReward()
		{
			UnlockAllItemsByReward();
		}

		public void UnlockAllItemsAsIAP()
		{
			foreach (SkinItemData item in SkinItems) item.UnlockItem(false, true);
		}

		public void UnlockAllItemsIAP()
		{
			foreach (SkinItemData item in SkinItems) item.UnlockItemIAP();
		}

		private void debugUnlock()
		{
			UnlockAllItems();
			UnlockAllItemsIAP();
			StorageManager.Instance.CurrentLevel = 33;
		}
	}
}
