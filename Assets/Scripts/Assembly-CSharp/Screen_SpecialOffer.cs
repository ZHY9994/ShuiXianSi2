using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;
using Wayne.Game.Shops;

public class Screen_SpecialOffer : MenuScreenBase
{
	[SerializeField]
	private ExtendedButton[] m_CloseButtons;

	public bool IsNoAdsPurchased
	{
		get
		{
			return Managers.Instance.Storage.RemovedAds;
		}
		set
		{
			Managers.Instance.Storage.RemovedAds = value;
		}
	}

	public bool IsSpecialOfferPurchased
	{
		get
		{
			return Managers.Instance.Storage.IsSpecialOfferPurchased;
		}
		set
		{
			Managers.Instance.Storage.IsSpecialOfferPurchased = value;
		}
	}

	public bool IsGoldenOfferPurchased
	{
		get
		{
			return Managers.Instance.Storage.IsGoldenOfferPurchased;
		}
		set
		{
			Managers.Instance.Storage.IsGoldenOfferPurchased = value;
		}
	}

	public static event Action OnSpecialOfferOpen = delegate { };

	public static event Action OnSpecialOfferClose = delegate { };

	protected override void OnEnable()
	{
		base.OnEnable();
		foreach (ExtendedButton button in m_CloseButtons)
			button.Setup(onClose);
		iAPManager.OnSpecialOfferPurchased += onClose;
		iAPManager.OnGoldenOfferPurchased += onClose;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		iAPManager.OnSpecialOfferPurchased -= onClose;
		iAPManager.OnGoldenOfferPurchased -= onClose;
	}

	private void onClose()
	{
		Close();
		if (SpecialOfferManager.Instance.OpenByShopItem)
			WeaponShopManager.Instance.UpdateShop();
		MenuManager.Instance.OpenMenuScreen("Screen_Welcome");
	}

	protected override void OnScreenOpenEnd()
	{
		base.OnScreenOpenEnd();
		OnSpecialOfferOpen?.Invoke();
	}

	protected override void OnScreenCloseEnd()
	{
		base.OnScreenCloseEnd();
		OnSpecialOfferClose?.Invoke();
	}

	protected override void SetRefs()
	{
		base.SetRefs();
	}

	public Screen_SpecialOffer()
	{
	}
}
