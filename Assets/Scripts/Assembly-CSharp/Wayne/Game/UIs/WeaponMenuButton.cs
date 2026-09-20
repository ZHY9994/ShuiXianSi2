using UnityEngine;
using KobGamesSDKSlim;
using Wayne.Game.Shops;

namespace Wayne.Game.UIs
{
	public class WeaponMenuButton : TMenuButtons
	{
		public WeaponMenuButton() { }
		[SerializeField]
		private GameObject m_Notification;

		protected override eButtonType m_Type => eButtonType.Weapon;

		protected override void OnEnable()
		{
			base.OnEnable();
			updateNotification();
			WeaponShopManager.OnShopOpen += updateNotification;
			WeaponShopManager.OnShopClose += updateNotification;
			WeaponShopManager.OnUpdateShop += updateNotification;
			WeaponShopManager.OnSelectItem += _ => updateNotification();
			WeaponShopManager.OnUnlockItem += _ => updateNotification();
			iAPManager.OnGoldenOfferPurchased += updateNotification;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			WeaponShopManager.OnShopOpen -= updateNotification;
			WeaponShopManager.OnShopClose -= updateNotification;
			WeaponShopManager.OnUpdateShop -= updateNotification;
			iAPManager.OnGoldenOfferPurchased -= updateNotification;
		}

		protected override void onActivated()
		{
			if (SkinShopManager.Instance != null && SkinShopManager.Instance.IsShopOpened)
				SkinShopManager.Instance.CloseShop();
			if (WeaponShopManager.Instance == null || WeaponShopManager.Instance.IsShopOpened)
				return;
			WeaponShopManager.Instance.OpenShop();
		}

		private void updateNotification()
		{
			bool active = false;
			if (WeaponShopManager.Instance != null && WeaponShopManager.Instance.WeaponItems != null)
			{
				foreach (WeaponItemData item in WeaponShopManager.Instance.WeaponItems)
				{
					if (item != null && item.IsUnlocked && !item.HasShownNotification &&
						StorageManager.Instance != null && StorageManager.Instance.CurrentLevel >= item.RequiredLevel + 1)
					{ active = true; break; }
				}
			}
			if (m_Notification != null) m_Notification.SetActive(active);
		}

	}
}
