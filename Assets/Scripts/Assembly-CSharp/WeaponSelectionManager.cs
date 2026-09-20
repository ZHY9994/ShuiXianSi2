using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using KobGamesSDKSlim;
using MrWayne;
using UnityEngine;
using Wayne.Game.Shops;

public class WeaponSelectionManager : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Holder;

	[SerializeField]
	private WeaponButton[] m_Buttons;

	private List<WeaponItemData> m_ActiveItems;

	private int m_ThrownCount;

	private RectTransform m_Rect;

	private bool m_StopWeaponSet;

	private List<WeaponItemData> m_DeckItems => DeckManager.Instance.Items;

	public int ThrownCount => m_ThrownCount;

	public static event Action<WeaponItemData> OnSetWeapon = delegate { };

	public static event Action<WeaponItemData> PlayerSetWeapon = delegate { };

	public static event Action OnWeaponDeckOpened = delegate { };

	private void Awake()
	{
		m_Rect = m_Holder.GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		GameTraceLog.LogError($"[NextLevelTrace] WeaponSelection.OnEnable frame={Time.frameCount} go={name} holderSelf={(m_Holder != null && m_Holder.activeSelf)} stop={m_StopWeaponSet}");
		completeTutorialIfPastFirstLevel();
		initializeWeapon();
		weaponManagerVisibility(true);
		PlayerAimingState.OnPlayerThrown += onPlayerThrown;
		GameplayController.OnStateChange += onStateChange;
		foreach (WeaponButton button in m_Buttons)
			button.OnWeaponSelected += setWeapon;
	}

	private void OnDisable()
	{
		GameTraceLog.LogError($"[NextLevelTrace] WeaponSelection.OnDisable frame={Time.frameCount} go={name} holderSelf={(m_Holder != null && m_Holder.activeSelf)} stop={m_StopWeaponSet}");
		PlayerAimingState.OnPlayerThrown -= onPlayerThrown;
		GameplayController.OnStateChange -= onStateChange;
		foreach (WeaponButton button in m_Buttons)
			button.OnWeaponSelected -= setWeapon;
	}

	private void onPlayerThrown(eWeaponType i_Type)
	{
		if (StorageManager.Instance.WeaponSelectionTutorial)
			m_ThrownCount++;
		if (m_ThrownCount >= 2)
		{
			completeTutorial();
		}
	}

	private void completeTutorialIfPastFirstLevel()
	{
		if (StorageManager.Instance.CurrentLevel > 1 || !StorageManager.Instance.IsFirstLaunch)
			completeTutorial();
	}

	private void completeTutorial()
	{
		if (!StorageManager.Instance.WeaponSelectionTutorial)
			return;
		StorageManager.Instance.WeaponSelectionTutorial = false;
		global::Ade_Framework.AdeCloudPlayerPrefs.Save();
	}

	private void onStateChange(eTurnState i_State)
	{
		GameTraceLog.LogError($"[NextLevelTrace] WeaponSelection.onStateChange frame={Time.frameCount} state={i_State} active={gameObject.activeInHierarchy} holderSelf={m_Holder.activeSelf} stop={m_StopWeaponSet}");
		if (i_State == eTurnState.SelectingWeapon)
			weaponManagerVisibility(true);
	}

	private void setWeapon(WeaponItemData i_ItemData, int i_Index)
	{
		if (m_StopWeaponSet)
			return;
		swapWeapon(i_ItemData, i_Index);
		weaponManagerVisibility(false);
		m_StopWeaponSet = true;
		PlayerSetWeapon?.Invoke(i_ItemData);
		OnSetWeapon?.Invoke(i_ItemData);
	}

	private void initializeWeapon()
	{
		m_ActiveItems.Clear();
		if (StorageManager.Instance.WeaponSelectionTutorial && StorageManager.Instance.IsFirstLaunch && StorageManager.Instance.CurrentLevel == 1)
			setTutorialDeck();
		else if (GameConfig.Instance.Debug.DebugWeapon)
			setDebugDeck();
		else
			setStandardDeck();
	}

	private void weaponManagerVisibility(bool i_Value)
	{
		GameTraceLog.LogError($"[NextLevelTrace] WeaponSelection.visibility frame={Time.frameCount} value={i_Value} active={gameObject.activeInHierarchy} holderSelf={m_Holder.activeSelf} stopBefore={m_StopWeaponSet}");
		MenuVariablesEditor.DeckParameter parameters = GameConfig.Instance.Menus.DeckParameters;
		if (!GameConfig.Instance.Menus.UseScreenSettings)
		{
			m_Holder.SetActive(i_Value);
			if (!i_Value)
				return;
		}
		if (!i_Value)
		{
			m_Rect.DOAnchorPos(Vector2.down * parameters.HideDistance, parameters.HideDuration)
				.SetEase(parameters.HideEase).OnComplete(() => m_Holder.SetActive(false));
			return;
		}
		m_Holder.SetActive(true);
		m_Rect.DOAnchorPos(Vector2.zero, parameters.ShowDuration)
			.SetEase(parameters.ShowEase).From(Vector2.down * parameters.HideDistance);
		m_StopWeaponSet = false;
		OnWeaponDeckOpened?.Invoke();
	}

	private void setStandardDeck()
	{
		m_ActiveItems.AddRange(m_DeckItems.Shuffle().OrderByDescending(x => x.IsItemNew));
		InitializeButtons();
	}

	private void setTutorialDeck()
	{
		m_ActiveItems.AddRange(m_DeckItems.Where(x => (int)x.WeaponType > 1));
		m_ActiveItems.Insert(1, m_DeckItems[6]);
		m_ActiveItems.Insert(1, m_DeckItems[0]);
		InitializeButtons();
		WayneStaticFunctions.Shuffle2(m_ActiveItems);
	}

	private void setDebugDeck()
	{
		foreach (eWeaponType type in GameConfig.Instance.Debug.WeaponType)
			m_ActiveItems.Add(WeaponShopManager.Instance.WeaponItems.First(x => x.WeaponType == type));
		InitializeButtons();
	}

	private void swapWeapon(WeaponItemData i_ItemData, int i_Index)
	{
		if (GameConfig.Instance.Debug.DebugWeapon)
			return;
		updateButton(m_ActiveItems[0], i_Index);
		m_ActiveItems.RemoveAt(0);
		m_ActiveItems.Add(i_ItemData);
	}

	private void InitializeButtons()
	{
		for (int i = 0; i < m_Buttons.Length; i++)
			updateButton(m_ActiveItems[i], i);
		m_ActiveItems.RemoveRange(0, 3);
	}

	private void updateButton(WeaponItemData i_Item, int i_Index)
	{
		m_Buttons[i_Index].UpdateButton(i_Item, i_Index);
	}

	private void SetRefs()
	{
		m_Holder = transform.FindDeepChild<GameObject>("Holder");
		m_Buttons = GetComponentsInChildren<WeaponButton>(true);
	}

	private void debug()
	{
		completeTutorial();
	}

	public WeaponSelectionManager()
	{
		m_ActiveItems = new List<WeaponItemData>();
	}
}
