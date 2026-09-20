using System;
using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using TMPro;
using UnityEngine;
using KobGamesSDKSlim.Animation;
using KobGamesSDKSlim.Collectable;
using UnityEngine.UI;

namespace Wayne.Game.Shops.Skins
{
	public class SkinShopItem : MonoBehaviour
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

		public static Action<SkinShopItem> OnUnlock = delegate { };

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
		private GameObject m_InUse;

		[SerializeField]
		private SkinRarityItemHandler m_RarityHandler;

		[SerializeField]
		private ExtendedButton_RV m_RvButton;

		[SerializeField]
		private Button m_SelectButton;

		private eState m_State;

		public ShopSettings m_ShopSettings => GameConfig.Instance.Shop.ShopSettings;

		public SkinItemData ItemData { get; private set; }

		private void OnEnable()
		{
			m_SelectButton.onClick.AddListener(onClick);
			SkinShopManager.OnSelectItem += onSelectionUpdated;
			m_RvButton.Setup(eRewardVideoPlacements.Shop_Unlock_Skin, null, rvSuccess);
		}

		private void OnDisable()
		{
			m_SelectButton.onClick.RemoveAllListeners();
			SkinShopManager.OnSelectItem -= onSelectionUpdated;
		}

		private void onSelectionUpdated(SkinItemData i_Item)
		{
			if (i_Item == null || ItemData == null) return;
			bool selected = i_Item.Name == ItemData.Name;
			m_RarityHandler.ShowSelection(selected);
			m_InUse.SetActive(selected);
			m_Notification.SetActive(!ItemData.IsUnlocked && !ItemData.HasShownNotification &&
				StorageManager.Instance.CoinsAmount >= ItemData.RequiredMoney && (int)ItemData.UnlockMethod == 2);
		}

		public void Initialize(SkinItemData i_Item)
		{
			m_Notification.SetActive(false);
			gameObject.SetActive(i_Item != null);
			if (i_Item == null) return;
			ItemData = i_Item;
			updateState();
			m_RarityHandler.Initialize(ItemData);
			onSelectionUpdated(SkinShopManager.Instance.SelectedItem);
			string color = SkinShopManager.Instance.GetItemRarityColorEditor(ItemData.ItemRarity).ColorToHex();
			m_RequiredLevelText.text = $"<color={color}>{LocalizationUtility.GetWordLocalizedString("level")} {ItemData.RequiredLevel + 1}</color>";
			m_PriceText.SetText(ItemData.RequiredMoney.ToString());
			m_Notification.SetActive(!ItemData.IsUnlocked && !ItemData.HasShownNotification &&
				StorageManager.Instance.CoinsAmount >= ItemData.RequiredMoney && (int)ItemData.UnlockMethod == 2);
		}

		private void onClick()
		{
			playSelectAnim();
			if (DeckManager.Instance.State == eDeckState.Selection) return;
			switch (m_State)
			{
				case eState.Unlocked: onSelect(); break;
				case eState.Ads: onRV(); break;
				case eState.Money: onBuy(); break;
				case eState.IAP: onIAP(); return;
			}
			SkinShopManager.Instance.ClickItem(ItemData);
		}

		private void playSelectAnim()
		{
			var p = m_ShopSettings.Tweens.PunchOnSelect;
			PunchScale(1f, p.Power, p.Duration, p.Vibration, p.Elasticity, 0f);
		}

		private void updateState()
		{
			if (ItemData == null) return;
			int level = StorageManager.Instance.CurrentLevel;
			if (ItemData.RequiredLevel < level && ItemData.IsUnlocked) m_State = eState.Unlocked;
			else if (ItemData.RequiredLevel < level && (int)ItemData.UnlockMethod == 1) m_State = eState.Ads;
			else if ((int)ItemData.UnlockMethod == 2) m_State = eState.Money;
			else if ((int)ItemData.UnlockMethod == 3) m_State = eState.IAP;
			else m_State = eState.Locked;
			m_AdsRoot.SetActive(m_State == eState.Ads);
			m_MoneyRoot.SetActive(m_State == eState.Money);
			m_IAPRoot.SetActive(m_State == eState.IAP);
			m_LockedButtonRoot.SetActive(m_State == eState.Locked);
			m_RarityHandler.UpdateController();
		}

		private void onSelect()
		{
			playSelectAnim();
			if (m_State == eState.Unlocked && SkinShopManager.Instance.IsShopOpened)
			{
				SkinShopManager.Instance.SelectItem(ItemData);
				m_Notification.SetActive(false);
			}
		}

		private void onBuy()
		{
			playSelectAnim();
			if (StorageManager.Instance.GetCollectable(eCollectableType.Coin) >= ItemData.RequiredMoney)
				unlockSkin();
			SkinShopManager.Instance.ClickItem(ItemData);
		}

		private void onIAP()
		{
			SpecialOfferManager.Instance.OpenGoldenOfferFromShopItem();
		}

		private void unlockSkin(bool withRV = false)
		{
			if (!withRV) StorageManager.Instance.SubtractCollectable(eCollectableType.Coin, ItemData.RequiredMoney);
			SkinShopManager.Instance.UnlockItem(ItemData, withRV);
			updateState();
			OnUnlock?.Invoke(this);
		}

		private void onRV()
		{
			playSelectAnim();
			ADManager.Instance.ShowRewardAD(rvSuccess);
		}

		private void rvSuccess()
		{
			unlockSkin(true);
			SkinShopManager.Instance.ClickItem(ItemData);
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
}
