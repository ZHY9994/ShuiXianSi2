using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

public class DeckManager : Singleton<DeckManager>
{
	public DeckSlotData[] Slots => GameConfig.Instance.GamePlay.DeckData.DeckSlots;

	public List<WeaponItemData> Items { get; } = new List<WeaponItemData>();

	public eDeckState State { get; private set; }

	public static event Action OnDeckOpen = delegate { };

	public static event Action OnDeckClose = delegate { };

	public static event Action OnDeckUpdate = delegate { };

	public bool IsWeaponInDeck(eWeaponType i_Type)
	{
		foreach (WeaponItemData item in Items)
		{
			if (item.WeaponType == i_Type)
			{
				return true;
			}
		}
		return false;
	}

	public DeckSlotData GetSlot(int i_Index)
	{
		return Slots[i_Index];
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		Wayne.Game.Shops.WeaponShopManager.OnShopOpen += onShopOpen;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset += onGameReset;
		initializeDeck();
	}

	public override void OnDisable()
	{
		base.OnDisable();
		Wayne.Game.Shops.WeaponShopManager.OnShopOpen -= onShopOpen;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset -= onGameReset;
	}

	private void onShopOpen()
	{
		setState(eDeckState.Idle);
	}

	private void onGameReset()
	{
		setState(eDeckState.Idle);
	}

	private void initializeDeck()
	{
		bool wasModified = false;
		WeaponItemData[] weaponItems = Wayne.Game.Shops.WeaponShopManager.Instance.WeaponItems;
		foreach (DeckSlotData slot in Slots)
		{
			if (slot.HasSavedData)
				continue;

			int slotIndex = slot.Index;
			if (slotIndex < 0 || slotIndex >= weaponItems.Length)
			{
				continue;
			}

			slot.Unlock();
			slot.UpdateSlot(weaponItems[slotIndex].WeaponType);
			wasModified = true;
		}

		if (wasModified)
		{
			global::Ade_Framework.AdeCloudPlayerPrefs.Save();
		}
		cacheDeckItems();
	}

	public void OpenDeck(eDeckState i_State)
	{
		setState(i_State);
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_Deck");
		OnDeckOpen?.Invoke();
	}

	public void CloseDeck()
	{
		setState(eDeckState.Idle);
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_Deck");
		OnDeckClose?.Invoke();
	}

	public void UpdateDeck()
	{
		cacheDeckItems();
		setState(eDeckState.Idle);
		OnDeckUpdate?.Invoke();
	}

	public void Unlock(DeckSlotData i_SlotData)
	{
		i_SlotData.Unlock();
		global::Ade_Framework.AdeCloudPlayerPrefs.Save();
	}

	public void UpdateSlot(DeckSlotData i_SlotData, eWeaponType i_Type)
	{
		i_SlotData.UpdateSlot(i_Type);
		UpdateDeck();
		global::Ade_Framework.AdeCloudPlayerPrefs.Save();
	}

	private void cacheDeckItems()
	{
		Items.Clear();
		foreach (DeckSlotData slot in Slots)
		{
			Items.Add(Wayne.Game.Shops.WeaponShopManager.Instance.GetWeapon(slot.WeaponInSlot));
		}
	}

	private void setState(eDeckState i_State)
	{
		State = i_State;
	}
}
