using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;

public class Screen_ShopCard : MenuScreenBase
{
	[SerializeField]
	private ExtendedButton m_CloseButton;

	[SerializeField]
	private ExtendedButton m_ExtraCloseButton;

	protected override void OnEnable()
	{
		base.OnEnable();
		m_CloseButton.Setup(onClick);
		m_ExtraCloseButton.Setup(onClick);
	}

	private void onClick()
	{
		Wayne.Game.Shops.WeaponShopManager.Instance.UpdateShop();
		Close();
	}
}
