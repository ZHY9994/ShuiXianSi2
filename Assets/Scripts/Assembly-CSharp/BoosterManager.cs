using System.Collections.Generic;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Collectable;
using Wayne.Game.Shops;

public class BoosterManager : Singleton<BoosterManager>
{
	public enum eBoosterOpenType
	{
		Level = 0,
		Shop = 1
	}

	private List<WeaponItemData> m_AvailableItemList = new List<WeaponItemData>();

	private List<WeaponItemData> m_MaxedOutList;

	public eBoosterOpenType OpenType { get; private set; }

	private BoosterData m_BoosterData => GameConfig.Instance.GamePlay.BoosterData;

	public override void OnAwakeEvent()
	{
		base.OnAwakeEvent();
		if (StorageManager.Instance.RewardWaiting)
		{
			giveCardsOnQuit();
			StorageManager.Instance.RewardWaiting = false;
		}
	}

	public void SetOpenType(eBoosterOpenType i_Type)
	{
		OpenType = i_Type;
	}

	private void giveCardsOnQuit()
	{
		StorageManager.Instance.AddCollectable(eCollectableType.Coin, 150);
		updateLists();
		if (m_AvailableItemList.Count < 1)
		{
			return;
		}
		if (m_BoosterData.IsThereAnOverrideAvailable(StorageManager.Instance.CurrentLevel - 1) && StorageManager.Instance.DefaultStartlevel == 0)
		{
			setOverridenSlot();
		}
		else
		{
			RandomizeAllSlots();
		}
	}

	private void updateLists()
	{
		WeaponItemData[] items = WeaponShopManager.Instance.WeaponItems;
		m_AvailableItemList = new List<WeaponItemData>();
		foreach (WeaponItemData item in items)
		{
			if (item.IsUnlocked && item.RequiredLevel < StorageManager.Instance.CurrentLevel)
			{
				m_AvailableItemList.Add(item);
			}
		}
		m_MaxedOutList = new List<WeaponItemData>();
		foreach (WeaponItemData item in m_AvailableItemList)
		{
			if (item.IsMaxed)
			{
				m_MaxedOutList.Add(item);
			}
		}
		for (int i = 0; i < 5 && m_MaxedOutList.Count > 0 && m_AvailableItemList.Count <= 4; i++)
		{
			WeaponItemData item = m_MaxedOutList[UnityEngine.Random.Range(0, m_MaxedOutList.Count)];
			m_AvailableItemList.Add(item);
			m_MaxedOutList.Remove(item);
		}
	}

	private void setOverridenSlot()
	{
		BoosterData.SlotDatas slots = m_BoosterData.GetOverrideSlotData();
		foreach (BoosterData.SlotData slot in slots.Slots)
		{
			WeaponItemData item = getItemFromList(slot.WeaponType);
			if (item != null && !slot.Random)
			{
				item.AddCard(slot.AddCount);
				m_AvailableItemList.Remove(item);
			}
		}
	}

	private void RandomizeAllSlots()
	{
		for (int i = 0; i < 5; i++)
		{
			if (m_AvailableItemList.Count < 1)
			{
				return;
			}
			WeaponItemData item = m_AvailableItemList[UnityEngine.Random.Range(0, m_AvailableItemList.Count)];
			item.AddCard(UnityEngine.Random.Range(2, 20));
			m_AvailableItemList.Remove(item);
		}
	}

	private WeaponItemData getItemFromList(eWeaponType i_Type)
	{
		foreach (WeaponItemData item in m_AvailableItemList)
		{
			if (item.WeaponType == i_Type)
			{
				return item;
			}
		}
		return null;
	}
}
