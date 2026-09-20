using UnityEngine;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using Wayne.Game.Shops;

namespace Wayne.Game.UIs
{
	public class SkinMenuButton : TMenuButtons
	{
		public SkinMenuButton() { }
		[SerializeField]
		private GameObject m_Notification;

		protected override eButtonType m_Type => eButtonType.Skin;

		protected override void OnEnable()
		{
			base.OnEnable();
			updateNotification();
			SkinShopManager.OnShopOpen += updateNotification;
			SkinShopManager.OnShopClose += updateNotification;
			SkinShopManager.OnSelectItem += _ => updateNotification();
			SkinShopManager.OnUnlockItem += _ => updateNotification();
			iAPManager.OnGoldenOfferPurchased += updateNotification;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			SkinShopManager.OnShopOpen -= updateNotification;
			SkinShopManager.OnShopClose -= updateNotification;
			iAPManager.OnGoldenOfferPurchased -= updateNotification;
		}

		protected override void onActivated()
		{
			if (WeaponShopManager.Instance != null && WeaponShopManager.Instance.IsShopOpened)
				WeaponShopManager.Instance.CloseShop();
			if (SkinShopManager.Instance == null || SkinShopManager.Instance.IsShopOpened)
				return;
			SkinShopManager.Instance.OpenShop();
		}

		private void updateNotification()
		{
			bool active = false;
			if (SkinShopManager.Instance != null && SkinShopManager.Instance.SkinItems != null)
			{
				foreach (SkinItemData item in SkinShopManager.Instance.SkinItems)
				{
					if (item != null && item.UnlockMethod == eItemUnlockMethod.IAP && !item.IsUnlocked &&
						StorageManager.Instance != null && StorageManager.Instance.CoinsAmount >= item.RequiredMoney && !item.HasShownNotification)
					{ active = true; break; }
				}
			}
			if (m_Notification != null) m_Notification.SetActive(active);
		}

	}
}
