using KobGamesSDKSlim.Shop;
using Wayne.Game.Shops;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-200)]
	public class AnalyticsManager : AnalyticsManagerBase
	{
		private AchievedLevelEventData m_AchievedLevelData;

		protected override void onAwakeImpl()
		{
			base.onAwakeImpl();
			string saved = StorageManager.Instance.AchievedLevelSave;
			m_AchievedLevelData = string.IsNullOrEmpty(saved) ? new AchievedLevelEventData() : JsonUtility.FromJson<AchievedLevelEventData>(saved);
			if (m_AchievedLevelData == null) m_AchievedLevelData = new AchievedLevelEventData();
			m_AchievedLevelData.Preload();
		}

		public void Tutorial(string i_Step)
		{
			LogEvent("Tutorial", i_Step);
		}

		public void SkinUnlock(TItemData i_ItemData, bool i_IsIAP)
		{
			if (i_ItemData == null) return;
			LogEvent("skin_unlock", i_ItemData.Name, "unlock_type", (long)(i_IsIAP ? eItemUnlockMethod.IAP : i_ItemData.UnlockMethod));
		}

		public void WeaponUnlock(TItemData i_ItemData, bool i_IsIAP)
		{
			if (i_ItemData == null) return;
			LogEvent("weapon_unlock", i_ItemData.Name, "unlock_type", (long)(i_IsIAP ? eItemUnlockMethod.IAP : i_ItemData.UnlockMethod));
		}

		public void WeaponDeck(string i_Value)
		{
			LogEvent("weapon_deck", i_Value, "current_level", CurrentLevel);
		}

		public void WeaponUpgraded(WeaponItemData i_Item)
		{
			if (i_Item == null) return;
			LogEvent("item_upgraded", i_Item.Name, "Item_level", i_Item.Level);
		}

		public void WeaponEquip(TItemData i_Item)
		{
			if (i_Item == null) return;
			LogEvent("item_equipped", i_Item.Name);
		}

		public void WeaponThrow(eWeaponType i_Item, int i_Count)
		{
			string weaponName = i_Item.ToString().ToLowerInvariant().Replace(" ", "_");
			LogEvent("weapon_thrown", weaponName, "weapon_name", i_Count);
		}

		public void AchievedLevel(int i_Level)
		{
			if (m_AchievedLevelData != null && m_AchievedLevelData.Evaluate(i_Level)) LogEvent("level_" + i_Level + "_achieved", "level_" + i_Level + "_achieved");
		}
	}
}
