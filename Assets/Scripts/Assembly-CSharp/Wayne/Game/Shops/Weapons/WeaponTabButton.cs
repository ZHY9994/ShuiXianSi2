using System;
using System.Runtime.CompilerServices;
using System.Linq;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;

namespace Wayne.Game.Shops.Weapons
{
	public class WeaponTabButton : TTabButton
	{
		public static event Action<eItemRarity> OnTabSelected = delegate { };

		protected override void OnEnable()
		{
			base.OnEnable();
			WeaponShopTabButtonController.OnTabUpdate += onTabUpdate;
			WeaponShopManager.OnSelectItem += onSelectItem;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			WeaponShopTabButtonController.OnTabUpdate -= onTabUpdate;
			WeaponShopManager.OnSelectItem -= onSelectItem;
		}

		protected override void onClick()
		{
			if (m_State != eState.On)
				OnTabSelected?.Invoke(Rarity);
		}

		private void onSelectItem(TItemData i_Item)
		{
			updateNotification();
		}

		protected override void updateNotification()
		{
			bool active = false;
			WeaponShopManager manager = WeaponShopManager.Instance;
			if (manager != null)
				active = manager.WeaponItems.Any(x => x != null && x.IsUnlocked && !x.HasShownNotification && x.ItemRarity == Rarity && StorageManager.Instance != null && StorageManager.Instance.CurrentLevel >= x.RequiredLevel + 1);
			notificationVisibility(active);
		}

	}
}
