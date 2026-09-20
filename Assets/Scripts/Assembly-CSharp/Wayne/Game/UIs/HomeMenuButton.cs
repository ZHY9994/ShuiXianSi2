using KobGamesSDKSlim;
using Wayne.Game.Shops;
using KobGamesSDKSlim.MenuManagerV1;

namespace Wayne.Game.UIs
{
	public class HomeMenuButton : TMenuButtons
	{
		public HomeMenuButton() { }
		protected override eButtonType m_Type => eButtonType.Home;

		protected override void OnEnable()
		{
			base.OnEnable();
			EventManager.OnShopCloseClicked.Register(onShopButtonCloseClicked);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			EventManager.OnShopCloseClicked.UnRegister(onShopButtonCloseClicked);
		}

		private void onShopButtonCloseClicked()
		{
			onClicked();
		}

		protected override void onActivated()
		{
			if (SkinShopManager.Instance != null && SkinShopManager.Instance.IsShopOpened)
				SkinShopManager.Instance.CloseShop();
			if (WeaponShopManager.Instance != null && WeaponShopManager.Instance.IsShopOpened)
				WeaponShopManager.Instance.CloseShop();
			if (MenuManager.Instance == null)
				return;
			if (!MenuManager.Instance.IsScreenOpened("Screen_Welcome"))
				MenuManager.Instance.OpenMenuScreen("Screen_Welcome");
		}
	}
}
