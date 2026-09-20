using System.Collections.Generic;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using Wayne.Game.Shops;

namespace Wayne.Game.Utils
{
	public static class ShopUtils
	{
		public static List<TItemData> GetAllShopsItems()
		{
			List<TItemData> items = new List<TItemData>();
			items.AddRange(WeaponShopManager.Instance.WeaponItems);
			items.AddRange(SkinShopManager.Instance.SkinItems);
			return items;
		}

		public static TItemData GetItemToFill(int i_LevelNumber)
		{
			TItemData result = null;
			foreach (TItemData item in GetAllShopsItems())
			{
				if (item.RequiredLevel < i_LevelNumber) continue;
				if (result == null || item.RequiredLevel - i_LevelNumber < result.RequiredLevel - i_LevelNumber) result = item;
			}
			return result;
		}

		public static TItemData GetPreviousItemToFill(int i_LevelNumber)
		{
			TItemData result = null;
			foreach (TItemData item in GetAllShopsItems())
			{
				if (item.RequiredLevel >= i_LevelNumber)
					continue;
				if (result == null || i_LevelNumber - item.RequiredLevel < i_LevelNumber - result.RequiredLevel)
					result = item;
			}
			return result;
		}
		public static TItemData GetCurrentItemToFill() => GetItemToFill(StorageManager.Instance.CurrentLevel - 1);
		public static TItemData GetPreviousItemToFill() => GetPreviousItemToFill(StorageManager.Instance.CurrentLevel - 1);

		public static bool IsAnyItemBeingUnlocked()
		{
			return IsAnyItemBeingUnlocked(StorageManager.Instance.CurrentLevel);
		}

		// [已验证] IDA 0x22b8250 确认真实逻辑: 遍历 GetAllShopsItems, 命中 RequiredLevel == i_Level 即返回 true
		public static bool IsAnyItemBeingUnlocked(int i_Level)
		{
			foreach (TItemData item in GetAllShopsItems())
				if (item.RequiredLevel == i_Level) return true;
			return false;
		}

		// [已修正] IDA 0x22b8394 真实逻辑: 按物品类型分发到对应 ShopManager.UnlockItem(item, i_IsReward=true),
		// 而非直接调 i_ItemData.UnlockItem()。原还原遗漏了 OnUnlockItem 事件触发与 ShopManager 流程,
		// 导致解锁后 UI/存档状态不同步, 是 Reward/V2 循环的诱因之一。
		public static void UnlockItem(TItemData i_ItemData)
		{
			if (i_ItemData == null) return;
			if (i_ItemData is SkinItemData skinItem)
				SkinShopManager.Instance.UnlockItem(skinItem, true);
			else if (i_ItemData is WeaponItemData weaponItem)
				WeaponShopManager.Instance.UnlockItem(weaponItem, true);
		}
	}
}
