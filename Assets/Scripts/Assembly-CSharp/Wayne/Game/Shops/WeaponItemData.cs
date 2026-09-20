using System;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using MrWayne;

namespace Wayne.Game.Shops
{
	[Serializable]
	public class WeaponItemData : TItemData
	{
		public eWeaponType WeaponType;

		public override eItemType ItemType => eItemType.Weapon;

		public override bool IsSelected => WeaponShopManager.Instance != null && WeaponShopManager.Instance.GetSelectedItem() != null && WeaponShopManager.Instance.GetSelectedItem().Index == Index;

		public bool IsItemNew
		{
			get
			{
				return PlayerPrefsBool.GetBool("IsItemNew" + Identifier, UnlockMethod == eItemUnlockMethod.Unlocked && RequiredLevel >= 1 && StorageManager.Instance.CurrentLevel >= RequiredLevel);
			}
			set
			{
				PlayerPrefsBool.SetBool("IsItemNew" + Identifier, value);
			}
		}

		public WeaponData WeaponData => WeaponShopManager.Instance.GetWeaponData(WeaponType);

		public UpgradeDataEditor.UpgradeData UpgradeData => WeaponShopManager.Instance.GetUpgradeData(Level + 1);

		public int Level => WeaponData.Level;

		public int CardAmount => WeaponData.CardAmount;

		public bool HasUpgradeData => UpgradeData != null;

		public bool IsMaxed => Level >= GameConfig.Instance.GamePlay.WeaponData.Upgrade.MaxUpgrade || !HasUpgradeData;

		public bool IsUpgradeable
		{
			get
			{
				UpgradeDataEditor.UpgradeData upgradeData = UpgradeData;
				return !IsMaxed && upgradeData != null && CardAmount >= upgradeData.RequiredNumberOfCards;
			}
		}

		public bool IsUpgradePurchasable
		{
			get
			{
				UpgradeDataEditor.UpgradeData upgradeData = UpgradeData;
				return upgradeData != null && StorageManager.Instance.CoinsAmount >= upgradeData.Price;
			}
		}

		protected override void unlock(bool i_IsVIP = false)
		{
			IsItemNew = true;
		}

		public void Upgrade(bool i_WithRV)
		{
			AnalyticsManager.Instance.WeaponUpgraded(this);
		}

		public void AddCard(int i_Amount)
		{
			WeaponShopManager.Instance.AddCard(WeaponType, i_Amount);
		}

		public void ItemUsed()
		{
			IsItemNew = false;
		}

		public WeaponItemData()
		{
		}
	}
}
