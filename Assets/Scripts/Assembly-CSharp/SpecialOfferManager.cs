using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;

public class SpecialOfferManager : Singleton<SpecialOfferManager>
{
	public eOfferType OfferType { get; private set; }

	public bool OpenByShopItem { get; private set; }

	private new void OnEnable()
	{
		OpenOfferScreenButton.onOpenOfferScreen += onOpenOfferScreen;
		Wayne.Game.Shops.SkinShopManager.OnClickItem += onItemSelect;
		Wayne.Game.Shops.WeaponShopManager.OnClickItem += onItemSelect;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		OpenOfferScreenButton.onOpenOfferScreen -= onOpenOfferScreen;
		Wayne.Game.Shops.SkinShopManager.OnClickItem -= onItemSelect;
		Wayne.Game.Shops.WeaponShopManager.OnClickItem -= onItemSelect;
	}

	private void onItemSelect(TItemData i_Item)
	{
		if (i_Item != null && i_Item.UnlockMethod == eItemUnlockMethod.IAP && !i_Item.IsUnlocked)
		{
			OpenGoldenOfferFromShopItem();
		}
	}

	public void OpenGoldenOfferFromShopItem()
	{
		onOpenOfferScreenThroughShop(eOfferType.Golden);
	}

	private void onOpenOfferScreenThroughShop(eOfferType i_Type)
	{
		OfferType = i_Type;
		OpenByShopItem = true;
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_SpecialOffer");
	}

	private void onOpenOfferScreen(eOfferType i_Type)
	{
		OfferType = i_Type;
		OpenByShopItem = false;
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_Welcome");
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_SpecialOffer");
	}
}
