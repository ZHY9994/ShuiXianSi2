using System;
using KobGamesSDKSlim;
using UnityEngine.Purchasing;
using Wayne.Game.Shops;

public class iAPManager : iAPManagerBase
{
	public const string k_NoAds = "no_ads_who_dies_first";

	public const string k_SpecialOffer = "special_offer_who_dies_first";

	public const string k_GoldenOffer = "golden_offer_who_dies_first";

	public static Action OnRemoveAdsPurchased = delegate { };

	public static Action OnSpecialOfferPurchased = delegate { };

	public static Action OnGoldenOfferPurchased = delegate { };

	public void IsRestore()
	{
		m_IsRestore = true;
	}

	protected override void ProcessPurchasedProduct(Product i_Product, bool i_IsRestore)
	{
		base.ProcessPurchasedProduct(i_Product, i_IsRestore);
		string id = i_Product.definition.id;
		if (id == k_NoAds)
			noAds();
		else if (id == k_SpecialOffer)
			specialOffer();
		else if (id == k_GoldenOffer)
			goldenOffer();
	}

	private void specialOffer()
	{
		noAds();
		Managers.Instance.Storage.IsSpecialOfferPurchased = true;
		SkinShopManager.Instance.UnlockAllItemsAsIAP();
		OnSpecialOfferPurchased?.Invoke();
	}

	private void goldenOffer()
	{
		Managers.Instance.Storage.IsGoldenOfferPurchased = true;
		SkinShopManager.Instance.UnlockAllItemsIAP();
		WeaponShopManager.Instance.UnlockAllItemsIAP();
		OnGoldenOfferPurchased?.Invoke();
	}

	private void noAds()
	{
		if (Managers.Instance != null && Managers.Instance.Storage != null)
			Managers.Instance.Storage.RemovedAds = true;
		OnRemoveAdsPurchased?.Invoke();
	}

	public void InitiateRemoveAdsPurchase()
	{
		noAds();
	}

	public void InitiateSpecialOfferPurchase()
	{
		specialOffer();
	}

	public void InitiateGoldenOfferPurchase()
	{
		goldenOffer();
	}

	public Product GetProduct(string i_ProductId)
	{
		return CodelessIAPStoreListener.Instance.GetProduct(i_ProductId);
	}

	public Product GetProduct(eOfferType i_ProductType)
	{
		return GetProduct(GetProductId(i_ProductType));
	}

	public string GetProductId(eOfferType i_Type)
	{
		switch (i_Type)
		{
			case eOfferType.Special:
				return k_SpecialOffer;
			case eOfferType.Golden:
				return k_GoldenOffer;
		case eOfferType.No_Ads:
				return k_NoAds;
			default:
				return string.Empty;
		}
	}
}
