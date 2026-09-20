using System;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	[Serializable]
	public abstract class TItemData
	{
		[HideInInspector]
		public int Index;

		public int Order;

		public string Name;

		public eItemRarity ItemRarity;

		public eItemUnlockMethod UnlockMethod;

		[HideInInspector]
		public int RequiredAdsToWatch;

		public int RequiredMoney;

		public int RequiredLevel;

		public Sprite ShopSprite;

		public Sprite RewardSprite;

		public abstract eItemType ItemType { get; }

		public bool IsUnlocked
		{
			get
			{
				if (Index == 0)
				{
					PlayerPrefs.SetInt("IsUnlocked" + Identifier, 1);
					return PlayerPrefs.GetInt("IsUnlocked" + Identifier, 0) == 1;
				}
				if ((int)UnlockMethod == 0 || ((int)UnlockMethod == 2 && RequiredMoney == 0))
				{
					if (StorageManager.Instance.CurrentLevel >= RequiredLevel)
					{
						PlayerPrefs.SetInt("IsUnlocked" + Identifier, 1);
						return PlayerPrefs.GetInt("IsUnlocked" + Identifier, 0) == 1;
					}
				}
				return PlayerPrefs.GetInt("IsUnlocked" + Identifier, 0) == 1;
			}
		}

		public abstract bool IsSelected { get; }

		public int WatchedAds
		{
			get
			{
				return PlayerPrefs.GetInt("WatchedAds" + Identifier, 0);
			}
			set
			{
				PlayerPrefs.SetInt("WatchedAds" + Identifier, value);
			}
		}

		public bool HasShownNotification
		{
			get
			{
				return PlayerPrefsBool.GetBool("HasShownNotification" + Identifier, (int)UnlockMethod == 0 && RequiredLevel < 1);
			}
			set
			{
				PlayerPrefsBool.SetBool("HasShownNotification" + Identifier, value);
			}
		}

		public string Identifier => "Shop_" + ItemType + "_" + Name + "_" + Index;

		public bool OnAdWatched()
		{
			WatchedAds++;
			return WatchedAds >= RequiredAdsToWatch;
		}

		public void UnlockItem(bool i_IsIAPIncluded = false, bool i_IsVIP = false)
		{
			if (i_IsIAPIncluded || (int)UnlockMethod != 3)
			{
				PlayerPrefs.SetInt("IsUnlocked" + Identifier, 1);
				unlock(i_IsVIP);
			}
		}

		protected virtual void unlock(bool i_IsVIP = false)
		{
			return;
		}

		public void UnlockItemIAP()
		{
			if ((int)UnlockMethod == 3) UnlockItem(true, false);
		}

		public void LockItem()
		{
			PlayerPrefs.SetInt("IsUnlocked" + Identifier, 0);
		}

		public void ItemHasShownNotification()
		{
			HasShownNotification = true;
		}

		public Color EditorColorRarity()
		{
			return Wayne.Game.Utils.ProjectUtils.GetRarityColor(ItemRarity);
		}

		public Color EditorColorUnlockMethod()
		{
			return Wayne.Game.Utils.ProjectUtils.GetUnlockMethodColor(UnlockMethod);
		}
	}
}
