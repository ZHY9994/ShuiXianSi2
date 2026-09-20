using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using UnityEngine;

public class OpenOfferScreenButton : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Root;

	[SerializeField]
	private eOfferType m_OfferType;

	[SerializeField]
	private ExtendedButton m_Button;

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

	public static event Action<eOfferType> onOpenOfferScreen;

	private void OnEnable()
	{
		m_Button.Setup(onClick);
		updateButtonState();
		iAPManager.OnRemoveAdsPurchased += onNoAdsPurchaseSuccess;
		iAPManager.OnSpecialOfferPurchased += onSpecialOfferPurchaseSuccess;
		iAPManager.OnGoldenOfferPurchased += onGoldenOfferPurchaseSuccess;
	}

	private void OnDisable()
	{
		iAPManager.OnRemoveAdsPurchased -= onNoAdsPurchaseSuccess;
		iAPManager.OnSpecialOfferPurchased -= onSpecialOfferPurchaseSuccess;
		iAPManager.OnGoldenOfferPurchased -= onGoldenOfferPurchaseSuccess;
	}

	private void onClick()
	{
		if (m_OfferType == eOfferType.No_Ads)
		{
			if (!IsNoAdsPurchased)
			{
				Managers.Instance.iAPManager.InitiateRemoveAdsPurchase();
			}
		}
		else if (m_OfferType <= eOfferType.Golden)
		{
			onOpenOfferScreen?.Invoke(m_OfferType);
		}
	}

	private void onNoAdsPurchaseSuccess()
	{
		IsNoAdsPurchased = true;
		updateButtonState();
	}

	private void onSpecialOfferPurchaseSuccess()
	{
		IsSpecialOfferPurchased = true;
		updateButtonState();
	}

	private void onGoldenOfferPurchaseSuccess()
	{
		IsGoldenOfferPurchased = true;
		updateButtonState();
	}

	private void updateButtonState()
	{
		bool purchased;
		switch (m_OfferType)
		{
			case eOfferType.Special:
				purchased = IsSpecialOfferPurchased;
				break;
			case eOfferType.Golden:
				purchased = IsGoldenOfferPurchased;
				break;
			case eOfferType.No_Ads:
				purchased = IsNoAdsPurchased;
				break;
			default:
				return;
		}
		m_Root.SetActive(!purchased);
	}

	private void SetRefs()
	{
		m_Button = GetComponent<ExtendedButton>();
	}
}
