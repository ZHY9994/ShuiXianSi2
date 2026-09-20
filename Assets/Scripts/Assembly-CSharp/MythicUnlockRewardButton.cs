using KobGamesSDKSlim;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Wayne.Game.Shops;

/// <summary>
/// Three-view rewarded-video button for the rarity offers placed in Main. VIP/IAP
/// items are never included in the granted reward.
/// </summary>
public sealed class MythicUnlockRewardButton : MonoBehaviour, IPointerClickHandler
{
	public enum RewardType
	{
		Skins = 0,
		MythicWeapons = 1
	}

	[SerializeField] private RewardType m_RewardType;
	[SerializeField] private ExtendedButton_RV m_RewardVideoButton;
	[SerializeField] private ExtendedButton m_Button;
	[SerializeField] private Text m_WatchCountText;

	private const int RequiredWatchCount = 3;

	private bool m_IsShowingAd;

	private bool IsSkin => m_RewardType == RewardType.Skins;

	private bool IsClaimed => IsSkin
		? StorageManager.Instance != null && StorageManager.Instance.IsEpicSkinRewardClaimed
		: StorageManager.Instance != null && StorageManager.Instance.IsMythicWeaponRewardClaimed;

	private int WatchCount => StorageManager.Instance == null
		? 0
		: IsSkin
		? StorageManager.Instance.SkinRewardVideoCount
			: StorageManager.Instance.MythicWeaponRewardVideoCount;

	private void Awake()
	{
		if (m_RewardVideoButton == null) m_RewardVideoButton = GetComponentInChildren<ExtendedButton_RV>(true);
		if (m_Button == null) m_Button = GetComponentInChildren<ExtendedButton>(true);
		if (m_RewardVideoButton != null) m_RewardVideoButton.DisableButtonAfterAdShown = false;
		if (m_WatchCountText == null)
		{
			string textName = IsSkin ? "皮肤观看次数" : "武器观看次数";
			m_WatchCountText = transform.FindDeepChild<Text>(textName);
			if (m_WatchCountText == null) m_WatchCountText = GetComponentInChildren<Text>(true);
		}
	}

	private void OnEnable()
	{
		if (IsClaimed)
		{
			gameObject.SetActive(false);
			return;
		}
		UpdateWatchCountText();

#if UNITY_EDITOR || ADE_NO_ADS
		// Editor and no-ads builds treat the reward video as immediately successful.
		// Use the same callback path as the runtime fallback so the counter updates.
		if (m_Button != null) m_Button.Setup(ShowRewardVideo);
		else if (m_RewardVideoButton != null) m_RewardVideoButton.Setup(
			IsSkin ? eRewardVideoPlacements.Shop_Unlock_Skin : eRewardVideoPlacements.Shop_Unlock_Weapon_MYTHIC,
			null,
			Claim);
#else
		if (m_RewardVideoButton != null)
		{
			m_RewardVideoButton.Setup(
				IsSkin ? eRewardVideoPlacements.Shop_Unlock_Skin : eRewardVideoPlacements.Shop_Unlock_Weapon_MYTHIC,
				null,
				Claim);
		}
		else if (m_Button != null)
		{
			m_Button.Setup(ShowRewardVideo);
		}
#endif
	}

	private void ShowRewardVideo()
	{
		if (m_IsShowingAd || IsClaimed) return;
		m_IsShowingAd = true;
		ADManager.Instance.ShowRewardAD(Claim, () => m_IsShowingAd = false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (m_RewardVideoButton == null && m_Button == null)
			ShowRewardVideo();
	}

	private void Claim()
	{
		if (IsClaimed) return;
		m_IsShowingAd = false;
		StorageManager storage = StorageManager.Instance;
		if (storage == null) return;

		int watchCount = Mathf.Min(WatchCount + 1, RequiredWatchCount);
		if (IsSkin) storage.SkinRewardVideoCount = watchCount;
		else storage.MythicWeaponRewardVideoCount = watchCount;
		UpdateWatchCountText();

		if (watchCount < RequiredWatchCount)
		{
			if (m_RewardVideoButton != null) m_RewardVideoButton.SetInteractable(true);
			if (m_Button != null) m_Button.SetInteractable(true);
			return;
		}

		if (IsSkin)
		{
			if (SkinShopManager.Instance == null) return;
			SkinShopManager.Instance.UnlockAllItemsByReward();
			storage.IsEpicSkinRewardClaimed = true;
		}
		else
		{
			if (WeaponShopManager.Instance == null) return;
			WeaponShopManager.Instance.UnlockAllMythicItemsByReward();
			storage.IsMythicWeaponRewardClaimed = true;
		}
		gameObject.SetActive(false);
	}

	private void UpdateWatchCountText()
	{
		if (m_WatchCountText != null)
			m_WatchCountText.text = $"观看{WatchCount}/{RequiredWatchCount}次获取";
	}
}
