using System.Collections.Generic;
using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Collectable;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;
using Wayne.Game.Shops;

public class Screen_Booster : MenuScreenBase
{
	[SerializeField]
	private BoosterSlot[] m_Slots;

	[SerializeField]
	private Transform m_Panel;

	[SerializeField]
	private ExtendedButton m_CloseButton;

	[SerializeField]
	private CollectableSender m_CoinSender;

	private List<WeaponItemData> m_AvailableItemList;

	private List<WeaponItemData> m_MaxedOutList;

	private Sequence m_RevealSequence;

	private int m_Index;

	private BoosterData m_BoosterData => GameConfig.Instance.GamePlay.BoosterData;

	protected override void OnEnable()
	{
		base.OnEnable();
		m_CloseButton?.Setup(Close);
		m_CoinSender?.Set(150);
		Initialize();
		StorageManager.Instance.AddCollectable(eCollectableType.Coin, 150);
		StorageManager.Instance.RewardWaiting = false;
		WeaponShopManager.Instance.UpdateShop();
	}

	private void Initialize()
	{
		updateLists();
		if (m_AvailableItemList == null || m_AvailableItemList.Count < 1)
			return;
		m_Index = 0;
		if (m_BoosterData.IsThereAnOverrideAvailable(StorageManager.Instance.CurrentLevel - 1) && !BoosterManager.Instance.OpenType.Equals(BoosterManager.eBoosterOpenType.Shop))
			setOverridenSlot();
		else
			RandomizeAllSlots();
	}

	private void scalePanelAnim()
	{
		m_RevealSequence?.Kill();
		m_RevealSequence = DOTween.Sequence();
		m_Panel.transform.localScale = Vector3.zero;
		m_RevealSequence.Append(m_Panel.DOScale(1f, m_BoosterData.RevealAnimData.Duration).SetEase(m_BoosterData.RevealAnimData.Ease));
		m_RevealSequence.AppendInterval(m_BoosterData.RevealAnimData.DelayBeforeSlotReveal);
		m_RevealSequence.OnComplete(() => { if (m_BoosterData.IsThereAnOverrideAvailable(StorageManager.Instance.CurrentLevel - 1) && BoosterManager.Instance.OpenType == BoosterManager.eBoosterOpenType.Level) setOverridenSlot(); else RandomizeAllSlots(); });
	}

	private void updateLists()
	{
		m_AvailableItemList = new List<WeaponItemData>();
		foreach (WeaponItemData item in WeaponShopManager.Instance.WeaponItems)
			if (item.IsUnlocked && item.RequiredLevel < StorageManager.Instance.CurrentLevel) m_AvailableItemList.Add(item);
		m_MaxedOutList = new List<WeaponItemData>();
		foreach (WeaponItemData item in m_AvailableItemList)
			if (item.IsMaxed) m_MaxedOutList.Add(item);
		for (int i = 0; i < 8 && m_MaxedOutList.Count > 0 && m_AvailableItemList.Count < m_Slots.Length; i++) {
			WeaponItemData item = m_MaxedOutList[Random.Range(0, m_MaxedOutList.Count)];
			m_AvailableItemList.Add(item); m_MaxedOutList.Remove(item);
		}
	}

	private void setOverridenSlot()
	{
		List<BoosterData.SlotData> slots = new List<BoosterData.SlotData>(m_BoosterData.GetOverrideSlotData().Slots);
		for (int i = 0; i < slots.Count; i++) {
			WeaponItemData item = getItemFromList(slots[i].WeaponType);
			if (item != null && !slots[i].Random) {
				m_Slots[i].Initialize(item, slots[i].AddCount, m_Index);
				m_Slots[i].SetSlot(); m_AvailableItemList.Remove(item); m_Index++;
			}
		}
		RandomizeAllSlots();
	}

	private void RandomizeAllSlots()
	{
		for (int i = 0; i < m_Slots.Length; i++) {
			if (m_AvailableItemList.Count < 1) break;
			if (!m_Slots[i].IsSlotSet) {
				WeaponItemData item = m_AvailableItemList[Random.Range(0, m_AvailableItemList.Count)];
				m_Slots[i].Initialize(item, Random.Range(1, 20), m_Index); m_Slots[i].SetSlot();
				m_AvailableItemList.Remove(item); m_Index++;
			}
		}
	}

	private WeaponItemData getItemFromList(eWeaponType i_Type)
	{
		foreach (WeaponItemData item in m_AvailableItemList)
		{
			if (item.WeaponType == i_Type)
				return item;
		}
		return null;
	}

	protected override void OnScreenCloseEnd()
	{
		base.OnScreenCloseEnd();
		if (BoosterManager.Instance.OpenType == BoosterManager.eBoosterOpenType.Level)
			MenuManager.Instance.OpenMenuScreen("Screen_LevelCompletedReward_V2");
	}

	private void debugReveal()
	{
		scalePanelAnim();
	}

	private new void SetRefs()
	{
		m_Slots = GetComponentsInChildren<BoosterSlot>();
		m_CloseButton = transform.FindDeepChild<ExtendedButton>("Close Button");
		m_CoinSender = transform.FindDeepChild<CollectableSender>("Reward Button");
		m_Panel = transform.FindDeepChild<Transform>("Panel");
	}

	public Screen_Booster()
	{
	}
}
