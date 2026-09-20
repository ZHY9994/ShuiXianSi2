using System;
using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using KobGamesSDKSlim.Collectable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Wayne.Game.Shops.Weapons
{
	public class WeaponShopItem : MonoBehaviour
	{
		public enum eState
		{
			None = 0,
			Unlocked = 1,
			Locked = 2,
			Ads = 3,
			Money = 4,
			IAP = 5,
			ComingSoon = 6
		}

		public enum eUnlockState
		{
			None = 0,
			NotUpgradeable = 1,
			UpgradableByCash = 2,
			UpgradableByRV = 3,
			Maxed = 4,
			InDeck = 5
		}

		public static Action<WeaponShopItem> OnUnlock = delegate { };

		[SerializeField]
		private TextMeshProUGUI m_RequiredLevelText;

		[SerializeField]
		private TextMeshProUGUI m_PriceText;

		[SerializeField]
		private GameObject m_AdsRoot;

		[SerializeField]
		private GameObject m_MoneyRoot;

		[SerializeField]
		private GameObject m_IAPRoot;

		[SerializeField]
		private GameObject m_LockedButtonRoot;

		[SerializeField]
		private GameObject m_Notification;

		[SerializeField]
		private WeaponRarityItemHandler m_RarityHandler;

		[SerializeField]
		private RectTransform m_Body;

		[SerializeField]
		private ShopItemContainer m_CounterContainer;

		[SerializeField]
		private ShopItemContainer m_UpgradeContainer;

		[SerializeField]
		private ShopItemContainer m_MaxContainer;

		[SerializeField]
		private ExtendedButton_RV m_RvButton;

		[SerializeField]
		private ExtendedButton_RV m_UpgradeRVButton;

		[SerializeField]
		private Button m_SelectButton;

		private eState m_State;

		private eUnlockState m_UnlockState;

		public ShopSettings m_ShopSettings => GameConfig.Instance.Shop.ShopSettings;

		public RectTransform Body => m_Body;

		public WeaponItemData ItemData { get; private set; }

		private void OnEnable()
		{
			m_SelectButton?.onClick.AddListener(onClick);
			WeaponShopManager.OnWeaponUpgrade += onWeaponUpgrade;
			WeaponShopManager.OnSelectItem += onSelected;
			m_RvButton?.Setup(eRewardVideoPlacements.Shop_Unlock_Skin, result => rvSuccess());
			m_UpgradeRVButton?.Setup(eRewardVideoPlacements.Shop_Upgrade_Weapon, result => upgradeItem());
		}

		private void OnDisable()
		{
			m_SelectButton?.onClick.RemoveAllListeners();
			WeaponShopManager.OnWeaponUpgrade -= onWeaponUpgrade;
			WeaponShopManager.OnSelectItem -= onSelected;
		}

		private void onWeaponUpgrade(WeaponItemData i_Item)
		{
			if (i_Item != null && ItemData != null && i_Item.Name.Equals(ItemData.Name))
				Initialize(i_Item);
		}

		private void onSelected(WeaponItemData i_Item)
		{
			if (i_Item != null && ItemData != null && i_Item.Name.Equals(ItemData.Name))
				m_Notification?.SetActive(false);
		}

		public void Initialize(WeaponItemData i_Item)
		{
			m_Notification?.SetActive(false);
			if (i_Item == null) { gameObject.SetActive(false); return; }
			gameObject.SetActive(true);
			ItemData = i_Item;
			GameTraceLog.Log($"[ShopTrace] WeaponShopItem.Initialize card={name} item={ItemData.Name} shopSprite={(ItemData.ShopSprite == null ? "NULL" : ItemData.ShopSprite.name)} rarityHandler={(m_RarityHandler == null ? "NULL" : "OK")}");
			updateState();
			updateUnlockState();
			m_RarityHandler?.Initialize(ItemData);
			if (m_RequiredLevelText != null)
			{
				Color color = WeaponShopManager.Instance.GetItemRarityColor(ItemData.ItemRarity);
				string level = LocalizationUtility.GetWordLocalizedString("level") + " " + (ItemData.RequiredLevel + 1);
				m_RequiredLevelText.text = $"<color={WayneStaticFunctions.ColorToHex(color)}>{level}</color>";
			}
			if (m_PriceText != null) m_PriceText.SetText(ItemData.RequiredMoney.ToString());
			var upgradeData = ItemData.UpgradeData;
			int requiredCards = upgradeData?.RequiredNumberOfCards ?? 0;
			string amount = $"{ItemData.CardAmount}/{requiredCards}";
			m_CounterContainer?.SetValue(amount);
			m_CounterContainer?.UpdateFiller(requiredCards > 0 ? (float)ItemData.CardAmount / requiredCards : 0f);
			m_UpgradeContainer?.SetValue(amount);
			if (ItemData.IsUnlocked && !ItemData.HasShownNotification && StorageManager.Instance.CurrentLevel >= ItemData.RequiredLevel + 1)
				m_Notification?.SetActive(true);
		}

		private void onClick()
		{
			playSelectAnim();
			if (DeckManager.Instance == null) return;
			switch (m_State) { case eState.Unlocked: onSelect(); break; case eState.Ads: onRV(); break; case eState.Money: onBuy(); break; case eState.IAP: onIAP(); return; }
			WeaponShopManager.Instance.ClickItem(ItemData);
		}

		private void playSelectAnim()
		{
			ShopSettings.PunchTweenParameters p = m_ShopSettings.Tweens.PunchOnSelect;
			PunchScale(1f, p.Power, p.Duration, p.Vibration, p.Elasticity, 0f);
		}

		private void updateState()
		{
			if (ItemData == null) return;
			int currentLevel = StorageManager.Instance.CurrentLevel;
			if (ItemData.RequiredLevel < currentLevel && ItemData.IsUnlocked)
				m_State = eState.Unlocked;
			else if (ItemData.RequiredLevel < currentLevel && ItemData.UnlockMethod == eItemUnlockMethod.AD)
				m_State = eState.Ads;
			else if (ItemData.UnlockMethod == eItemUnlockMethod.Cash)
				m_State = eState.Money;
			else if (ItemData.UnlockMethod == eItemUnlockMethod.IAP)
				m_State = eState.IAP;
			else
				m_State = eState.Locked;
			m_AdsRoot?.SetActive(m_State == eState.Ads);
			m_MoneyRoot?.SetActive(m_State == eState.Money);
			m_IAPRoot?.SetActive(m_State == eState.IAP);
			m_LockedButtonRoot?.SetActive(m_State == eState.Locked);
			m_RarityHandler?.UpdateController();
		}

		private void updateUnlockState()
		{
			if (ItemData == null) return;
			var upgradeData = ItemData.UpgradeData;
			if (ItemData.IsMaxed || upgradeData == null || m_State != eState.Unlocked) m_UnlockState = ItemData.IsMaxed && m_State == eState.Unlocked ? eUnlockState.Maxed : eUnlockState.None;
			else if (ItemData.CardAmount < upgradeData.RequiredNumberOfCards) m_UnlockState = eUnlockState.NotUpgradeable;
			else if (StorageManager.Instance.CoinsAmount >= upgradeData.Price) m_UnlockState = eUnlockState.UpgradableByCash;
			else if (ItemData.CardAmount >= upgradeData.RequiredNumberOfCards) m_UnlockState = eUnlockState.UpgradableByRV;
			bool showUpgrade = m_UnlockState == eUnlockState.UpgradableByCash || m_UnlockState == eUnlockState.UpgradableByRV;
			m_CounterContainer?.Visibility(m_UnlockState == eUnlockState.NotUpgradeable);
			m_UpgradeContainer?.Visibility(showUpgrade);
			m_MaxContainer?.Visibility(m_UnlockState == eUnlockState.Maxed);
		}

		private void onSelect()
		{
			playSelectAnim();
			if (m_State == eState.Unlocked && WeaponShopManager.Instance.ShopData.IsSelectable)
			{
				WeaponShopManager.Instance.ShopSelectItem(ItemData);
				m_Notification?.SetActive(false);
			}
		}

		private void onBuy()
		{
			playSelectAnim();
			if (StorageManager.Instance.GetCollectable(eCollectableType.Coin) >= ItemData.RequiredMoney) unlockWeapon();
			WeaponShopManager.Instance.ClickItem(ItemData);
		}

		private void onIAP()
		{
			SpecialOfferManager.Instance.OpenGoldenOfferFromShopItem();
		}

		private void unlockWeapon(bool i_WithRV = false)
		{
			if (!i_WithRV) StorageManager.Instance.SubtractCollectable(eCollectableType.Coin, ItemData.RequiredMoney);
			WeaponShopManager.Instance.UnlockItem(ItemData, i_WithRV);
			updateState();
			OnUnlock?.Invoke(this);
		}

		private void upgradeItem()
		{
			if (ItemData == null) return;
			WeaponShopManager.Instance.UpgradeItem(ItemData, true);
			Initialize(ItemData);
		}

		private void onRV()
		{
			// IDA first checks ExtendedButton_RV's interactability before showing an ad.
			if (m_RvButton == null || !m_RvButton.interactable) return;
			playSelectAnim();
			if (ItemData == null) return;
			ADManager.Instance.ShowRewardAD(rvSuccess);
		}

		private void rvSuccess()
		{
			unlockWeapon(true);
			WeaponShopManager.Instance.ClickItem(ItemData);
		}

		public Tween PunchScale(float i_FromScale, float i_ScalePower, float i_ScaleDuration, int i_Vibration, float i_Elasticity, float i_Delay)
		{
			transform.DOKill();
			return transform.DOPunchScale(Vector3.one * i_ScalePower, i_ScaleDuration, i_Vibration, i_Elasticity)
				.OnStart(() => transform.localScale = Vector3.one * i_FromScale).SetDelay(i_Delay).OnKill(() => transform.localScale = Vector3.one);
		}

		private void SetRefs()
		{
			// IDA confirms this native method is a 4-byte no-op.
		}
	}
}
