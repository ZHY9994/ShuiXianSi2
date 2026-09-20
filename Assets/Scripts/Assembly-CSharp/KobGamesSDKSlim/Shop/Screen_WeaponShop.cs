using DG.Tweening;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;
using Wayne.Game.Shops.Weapons;

namespace KobGamesSDKSlim.Shop
{
	public class Screen_WeaponShop : MenuScreenBase
	{
		[SerializeField]
		private ExtendedButton m_CloseButton;

		private WeaponShopItem m_SelectedItem;

		private Tween m_ScrollTween;

		public ShopSettings m_ShopSettings => GameConfig.Instance.Shop.ShopSettings;

		protected override void OnEnable()
		{
			m_CloseButton.Setup(closeShop);
		}

		protected override void OnDisable()
		{
			return;
		}

		private void closeShop()
		{
			EventManager.OnShopCloseClicked.Trigger();
		}
	}
}
