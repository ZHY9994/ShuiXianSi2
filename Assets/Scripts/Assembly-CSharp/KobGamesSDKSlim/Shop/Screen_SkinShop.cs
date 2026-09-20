using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	public class Screen_SkinShop : MenuScreenBase
	{
		[SerializeField]
		private ExtendedButton m_CloseButton;

		protected override void OnEnable()
		{
			m_CloseButton.Setup(onClick);
		}

		private void onClick()
		{
			EventManager.OnShopCloseClicked.Trigger();
		}

		public Screen_SkinShop()
		{
		}
	}
}
