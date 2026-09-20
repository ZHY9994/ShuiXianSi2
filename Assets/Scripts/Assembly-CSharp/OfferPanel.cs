using KobGamesSDKSlim;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wayne.Game.Shops;

public class OfferPanel : MonoBehaviour
{
	[SerializeField]
	private eOfferType m_OfferType;

	[SerializeField]
	private ExtendedButton m_OfferButton;

	[SerializeField]
	private TextMeshProUGUI m_PriceText;

	[SerializeField]
	private ScaleAnimator m_Scaler;

	[SerializeField]
	private Text m_RewardCountText;

	private const int RequiredWatchCount = 3;

	private bool m_IsRewardAdShowing;

	public bool IsNoAdsPurchased
	{
		get
		{
			return StorageManager.Instance.RemovedAds;
		}
		set
		{
			StorageManager.Instance.RemovedAds = value;
		}
	}

	public bool IsSpecialOfferPurchased
	{
		get
		{
			return StorageManager.Instance.IsSpecialOfferPurchased;
		}
		set
		{
			StorageManager.Instance.IsSpecialOfferPurchased = value;
		}
	}

	public bool IsGoldenOfferPurchased
	{
		get
		{
			return StorageManager.Instance.IsGoldenOfferPurchased;
		}
		set
		{
			StorageManager.Instance.IsGoldenOfferPurchased = value;
		}
	}

	public eOfferType OfferType => m_OfferType;

	private void Awake()
	{
		if (m_PriceText != null)
			m_PriceText.text = "免费";
		if (m_RewardCountText == null)
		{
			string textName = m_OfferType == eOfferType.Mythic_Skin_Reward ? "皮肤观看次数" : "武器观看次数";
			m_RewardCountText = transform.FindDeepChild<Text>(textName);
		}
	}

	private void OnEnable()
	{
		m_OfferButton.Setup(onClick);
		UpdateRewardState();
	}

	private void onClick()
	{
		if (m_OfferType == eOfferType.Mythic_Skin_Reward)
			ShowMythicReward(true);
		else if (m_OfferType == eOfferType.Mythic_Weapon_Reward)
			ShowMythicReward(false);
		else if (m_OfferType == eOfferType.Golden)
			GoldenOffer();
		else if (m_OfferType == eOfferType.Special)
			SpecialOffer();
	}

	public void PanelVisibility(bool i_Visibility)
	{
		gameObject.SetActive(i_Visibility);
		if (i_Visibility)
			m_Scaler.Animate();
	}

	private void GoldenOffer()
	{
		if (IsGoldenOfferPurchased || m_IsRewardAdShowing)
			return;

		m_IsRewardAdShowing = true;
		ADManager.Instance.ShowRewardAD(() =>
		{
			m_IsRewardAdShowing = false;
			Managers.Instance.iAPManager.InitiateGoldenOfferPurchase();
		}, () => m_IsRewardAdShowing = false);
	}

	private void ShowMythicReward(bool i_IsSkin)
	{
		if (m_IsRewardAdShowing || IsMythicRewardClaimed(i_IsSkin)) return;

		m_IsRewardAdShowing = true;
		ADManager.Instance.ShowRewardAD(
			() =>
			{
				m_IsRewardAdShowing = false;
				if (IsMythicRewardClaimed(i_IsSkin)) return;
				StorageManager storage = StorageManager.Instance;
				if (storage == null) return;
				int watchCount = i_IsSkin
					? Mathf.Min(storage.SkinRewardVideoCount + 1, RequiredWatchCount)
					: Mathf.Min(storage.MythicWeaponRewardVideoCount + 1, RequiredWatchCount);
				if (i_IsSkin) storage.SkinRewardVideoCount = watchCount;
				else storage.MythicWeaponRewardVideoCount = watchCount;
				UpdateRewardCountText(watchCount);
				if (watchCount < RequiredWatchCount) return;
				if (i_IsSkin)
				{
					SkinShopManager.Instance.UnlockAllItemsByReward();
					StorageManager.Instance.IsEpicSkinRewardClaimed = true;
				}
				else
				{
					WeaponShopManager.Instance.UnlockAllMythicItemsByReward();
					StorageManager.Instance.IsMythicWeaponRewardClaimed = true;
				}
				UpdateRewardState();
			},
			() => m_IsRewardAdShowing = false);
	}

	private bool IsMythicRewardClaimed(bool i_IsSkin)
	{
		return i_IsSkin
			? StorageManager.Instance.IsEpicSkinRewardClaimed
			: StorageManager.Instance.IsMythicWeaponRewardClaimed;
	}

	private void UpdateRewardState()
	{
		if (m_OfferType != eOfferType.Mythic_Skin_Reward && m_OfferType != eOfferType.Mythic_Weapon_Reward)
			return;
		bool isSkin = m_OfferType == eOfferType.Mythic_Skin_Reward;
		if (IsMythicRewardClaimed(isSkin))
			gameObject.SetActive(false);
		else
			UpdateRewardCountText(isSkin ? StorageManager.Instance.SkinRewardVideoCount : StorageManager.Instance.MythicWeaponRewardVideoCount);
	}

	private void UpdateRewardCountText(int watchCount)
	{
		if (m_RewardCountText != null)
			m_RewardCountText.text = $"观看{Mathf.Clamp(watchCount, 0, RequiredWatchCount)}/{RequiredWatchCount}次获取";
	}

	private void SpecialOffer()
	{
		if (!IsSpecialOfferPurchased)
			Managers.Instance.iAPManager.InitiateSpecialOfferPurchase();
	}

	private void SetRefs()
	{
		m_PriceText = transform.FindDeepChild<TextMeshProUGUI>("Offer Price Text");
		m_OfferButton = GetComponentInChildren<ExtendedButton>();
	}
}
