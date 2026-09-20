using System;
using System.Linq;
using System.Runtime.CompilerServices;
using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;

namespace Wayne.Game.Shops.Skins
{
	public class SkinTabButton : TTabButton
	{
		public static event Action<eItemRarity> OnTabSelected = delegate { };

		public SkinTabButton()
		{
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			SkinShopTabButtonController.OnTabUpdate += onTabUpdate;
			SkinShopManager.OnSelectItem += onSelectItem;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			SkinShopTabButtonController.OnTabUpdate -= onTabUpdate;
			SkinShopManager.OnSelectItem -= onSelectItem;
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
			bool visible = SkinShopManager.Instance != null && SkinShopManager.Instance.SkinItems.Any(item =>
				item != null && !item.IsUnlocked && !item.HasShownNotification && item.ItemRarity == Rarity &&
				StorageManager.Instance.CoinsAmount >= item.RequiredMoney && (int)item.UnlockMethod == 2);
			notificationVisibility(visible);
		}

	}
}
