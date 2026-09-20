using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using UnityEngine;
using UnityEngine.UI;
using Wayne.Game.Shops;
using Wayne.Game.Shops.Weapons;

public class DeckItem : MonoBehaviour
{
	public DeckItem()
	{
	}

	[SerializeField]
	private Button m_Button;

	[SerializeField]
	private Rotator m_Rotator;

	[SerializeField]
	private ShopItemContainer m_CounterContainer;

	[SerializeField]
	private ShopItemContainer m_UpgradeContainer;

	[SerializeField]
	private ShopItemContainer m_MaxContainer;

	[SerializeField]
	private WeaponRarityItemHandler m_RarityController;

	public RectTransform TutorialTarget;

	private DeckSlotData m_SlotData;

	public ShopSettings m_ShopSettings => GameConfig.Instance.Shop.ShopSettings;

	public WeaponItemData ItemData { get; private set; }

	private void OnEnable()
	{
		m_Button.onClick.AddListener(onButtonClick);
	}

	private void OnDisable()
	{
		m_Button.onClick.RemoveAllListeners();
	}

	private void onButtonClick()
	{
		playSelectAnim();
		if (DeckManager.Instance.State == eDeckState.Selection)
		{
			DeckManager.Instance.UpdateSlot(m_SlotData, Wayne.Game.Shops.WeaponShopManager.Instance.SelectedItem.WeaponType);
			Wayne.Game.Shops.WeaponShopManager.Instance.UpdateShop();
		}
		else if (Wayne.Game.Shops.WeaponShopManager.Instance.ShopData.IsSelectable)
		{
			Wayne.Game.Shops.WeaponShopManager.Instance.ShopSelectItem(ItemData);
		}
	}

	public void Initialize(DeckSlotData i_SlotData)
	{
		m_SlotData = i_SlotData;
		ItemData = Wayne.Game.Shops.WeaponShopManager.Instance.GetWeapon(i_SlotData.WeaponInSlot);
		m_RarityController.Initialize(ItemData);
		updateVisual();
	}

	private void updateVisual()
	{
		WeaponShopItem.eUnlockState state = ItemData.GetUnlockState();
		var upgradeData = ItemData.UpgradeData;
		int requiredCards = upgradeData?.RequiredNumberOfCards ?? 0;
		string amount = ItemData.CardAmount + "/" + requiredCards;
		m_CounterContainer.Visibility(state == WeaponShopItem.eUnlockState.NotUpgradeable);
		m_CounterContainer.SetValue(amount);
		m_CounterContainer.UpdateFiller(requiredCards > 0 ? (float)ItemData.CardAmount / requiredCards : 0f);
		m_UpgradeContainer.Visibility(state == WeaponShopItem.eUnlockState.UpgradableByCash || state == WeaponShopItem.eUnlockState.UpgradableByRV);
		m_UpgradeContainer.SetValue(amount);
		m_MaxContainer.Visibility(state == WeaponShopItem.eUnlockState.Maxed);
	}

	public void StartShake()
	{
		m_Rotator.StartAnimation();
	}

	public void StopShake()
	{
		m_Rotator.StopAnimation();
		m_Rotator.ResetValues();
	}

	private void playSelectAnim()
	{
		m_CounterContainer.transform.DOPunchScale(Vector3.one * m_ShopSettings.Tweens.PunchOnSelect.Power, m_ShopSettings.Tweens.PunchOnSelect.Duration, m_ShopSettings.Tweens.PunchOnSelect.Vibration, m_ShopSettings.Tweens.PunchOnSelect.Elasticity);
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
