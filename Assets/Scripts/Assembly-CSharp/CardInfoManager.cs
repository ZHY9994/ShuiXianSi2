using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using KobGamesSDKSlim.Collectable;
using KobGamesSDKSlim.Shop;
using MrWayne;
using TMPro;
using UnityEngine;
using Wayne.Game.Helpers;
using Wayne.Game.Shops;

public class CardInfoManager : MonoBehaviour
{
	public CardInfoManager()
	{
	}

	[SerializeField]
	private CardItem m_CardItem;

	[SerializeField]
	private CardStats m_DamageStat;

	[SerializeField]
	private CardStats m_PushForceStats;

	[SerializeField]
	private CardStats m_DamageRadiusStats;

	[SerializeField]
	private CardInfoDetails m_CardDetails;

	[SerializeField]
	private ExtendedButton m_EquipButton;

	[SerializeField]
	private ExtendedButton m_UpgradeCashButton;

	[SerializeField]
	private TextMeshProUGUI m_UpgradeCostText;

	[SerializeField]
	private ExtendedButton_RV m_UpgradeRVButton;

	[SerializeField]
	private GameObject m_CloseButton;

	[SerializeField]
	private GameObject m_ExtraCloseButton;

	[SerializeField]
	private TextMeshProUGUI m_CardTitle;

	[SerializeField]
	private TextMeshProUGUI m_CardLevel;

	[SerializeField]
	private RaritySelector m_RaritySelector;

	private WeaponItemData m_ItemData;

	private WeaponData m_WeaponData;

	private bool m_IsRVUpgradePending;

	public ShopSettings m_ShopSettings => GameConfig.Instance.Shop.ShopSettings;

	public bool IsInDeck => DeckManager.Instance.IsWeaponInDeck(m_ItemData.WeaponType);

	public static event Action OnCardInfoOpen = delegate { };

	private void OnEnable()
	{
		m_CloseButton.SetActive(true);
		m_ExtraCloseButton.SetActive(true);
		m_EquipButton.Setup(onEquipClick);
		m_UpgradeCashButton.Setup(onUpgradeCash);
		m_UpgradeRVButton.Setup(onUpgradeRV);
		initialize();
	}

	private void OnDisable()
	{
	}

	private void onUseWildCard()
	{
	}

	private void onEquipClick()
	{
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_ShopCard");
		DeckManager.Instance.OpenDeck(eDeckState.Selection);
	}

	private void onUpgradeCash()
	{
		var upgradeData = m_ItemData?.UpgradeData;
		if (upgradeData == null)
			return;
		StorageManager.Instance.SubtractCollectable(eCollectableType.Coin, upgradeData.Price);
		playSelectAnim();
		upgradeItem(false);
		updateInfo();
		updateStats();
		updateButtons();
		m_CardItem.RarityController.UpdateController();
	}

	private void onUpgradeRV()
	{
		if (m_IsRVUpgradePending || !m_UpgradeRVButton.interactable || m_ItemData == null || m_ItemData.IsMaxed || m_ItemData.UpgradeData == null)
			return;
		m_IsRVUpgradePending = true;
		ADManager.Instance.ShowRewardAD(rvSuccess);
	}

	private void initialize()
	{
		m_ItemData = WeaponShopManager.Instance.SelectedItem;
		m_CardItem.Initialize(m_ItemData);
		m_RaritySelector.UpdateSelection(m_ItemData.ItemRarity);
		updateInfo();
		updateStats();
		updateButtons();
		OnCardInfoOpen?.Invoke();
	}

	private void updateButtons()
	{
		m_EquipButton.gameObject.SetActive(!IsInDeck);
		var upgradeData = m_ItemData.UpgradeData;
		m_UpgradeCostText.SetText("{0}", upgradeData?.Price ?? 0);
		bool cash = !m_ItemData.IsMaxed && m_ItemData.IsUpgradeable && m_ItemData.IsUpgradePurchasable;
		m_UpgradeCashButton.gameObject.SetActive(cash);
		bool rv = !m_ItemData.IsMaxed && upgradeData != null && (!m_ItemData.IsUpgradeable || !m_ItemData.IsUpgradePurchasable);
		m_UpgradeRVButton.gameObject.SetActive(rv);
	}

	private void upgradeItem(bool i_WithRV)
	{
		WeaponShopManager.Instance.UpgradeItem(m_ItemData, i_WithRV);
	}

	private void updateInfo()
	{
		m_CardTitle.SetText(LocalizationUtility.GetWeaponLocalizedString(m_ItemData.Name).ToUpper());
		string level = LocalizationUtility.GetLocalizedString(eLocalKeyType.Word, "level");
		m_CardLevel.SetText(string.Format("{0} {1}", level, m_ItemData.Level));
	}

	private void updateStats()
	{
		WeaponData.WeaponStatsBase stats = m_ItemData.WeaponData.StatsData;
		float damage = (float)Math.Round(stats.HitDamageIncrease);
		float pushForce = getPushForce();
		float radius = (float)Math.Round(stats.RadiusIncrease);
		m_DamageStat.InitializeStat(true, LocalizationUtility.GetLocalizedString(eLocalKeyType.Word, "damage"), damage.ToString());
		m_PushForceStats.InitializeStat(true, LocalizationUtility.GetLocalizedString(eLocalKeyType.Word, "push_force"), pushForce.ToString());
		m_DamageRadiusStats.InitializeStat(radius > 0f, LocalizationUtility.GetLocalizedString(eLocalKeyType.Word, "damage_radius"), radius.ToString());
		m_CardDetails.Visibility(!m_ItemData.IsMaxed);
		m_CardDetails.UpdateDetails(m_ItemData);
	}

	private float getPushForce()
	{
		WeaponData.WeaponStatsBase stats = m_ItemData.WeaponData.StatsData;
		float pushForce = stats.PushForceIncrease;
		if (pushForce >= 100f && pushForce < 1000f && stats.PushForce >= 100f)
			pushForce *= 0.5f;
		else if (pushForce >= 1000f)
			pushForce /= 100f;
		return (float)Math.Round(pushForce);
	}

	private void rvSuccess()
	{
		if (!m_IsRVUpgradePending)
			return;
		m_IsRVUpgradePending = false;
		if (m_ItemData == null || m_ItemData.IsMaxed || m_ItemData.UpgradeData == null)
		{
			updateButtons();
			return;
		}
		playSelectAnim();
		upgradeItem(true);
		updateInfo();
		updateStats();
		updateButtons();
		m_CardItem.RarityController.UpdateController();
	}

	private void playSelectAnim()
	{
		ShopSettings.PunchTweenParameters punch = m_ShopSettings.Tweens.PunchOnSelect;
		PunchScale(1f, punch.Power, punch.Duration, punch.Vibration, punch.Elasticity, 0f);
	}

	public Tween PunchScale(float i_FromScale, float i_ScalePower, float i_ScaleDuration, int i_Vibration, float i_Elasticity, float i_Delay)
	{
		transform.DOKill();
		return transform.DOPunchScale(Vector3.one * i_ScalePower, i_ScaleDuration, i_Vibration, i_Elasticity)
			.OnStart(() => transform.localScale = Vector3.one * i_FromScale)
			.SetDelay(i_Delay)
			.OnKill(() => transform.localScale = Vector3.one);
	}
}
