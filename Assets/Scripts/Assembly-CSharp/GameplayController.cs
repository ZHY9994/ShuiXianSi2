using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using MrWayne;
using UnityEngine;

public class GameplayController : Singleton<GameplayController>
{
	private eTurnState m_CurrentTurnState;

	private bool m_IsPlayerTurn;

	private int m_PlayerUnitIndex;

	private int m_EnemyUnitIndex;

	private UnitBase m_CurrentUnit;

	private List<UnitBase> m_AllUnits => UnitManager.Instance.ActiveUnits;

	public MatchMakingItem CurrentMatchMakingItem { get; private set; }

	public static event Action<eTurnState> OnStateChange = delegate { };

	protected override void OnEnable()
	{
		base.OnEnable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelLoaded += onLevelLoaded;
		MrWayne.UnitWeaponHandler.OnUnitWeaponThrown += onUnitWeaponThrown;
		Screen_Transition.OnStartGame += onMatchMakingOpen;
		WeaponSelectionManager.OnSetWeapon += onSetWeapon;
		MrWayne.WeaponBase.OnWeaponFinished += onWeaponFinished;
		MrWayne.UnitHealth.OnUnitDeath += onUnitDeath;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelLoaded -= onLevelLoaded;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
		MrWayne.UnitWeaponHandler.OnUnitWeaponThrown -= onUnitWeaponThrown;
		Screen_Transition.OnStartGame -= onMatchMakingOpen;
		WeaponSelectionManager.OnSetWeapon -= onSetWeapon;
		MrWayne.WeaponBase.OnWeaponFinished -= onWeaponFinished;
		MrWayne.UnitHealth.OnUnitDeath -= onUnitDeath;
	}

	private void onLevelLoaded()
	{
		m_PlayerUnitIndex = 0;
		m_EnemyUnitIndex = 0;
		m_IsPlayerTurn = true;
		m_CurrentTurnState = eTurnState.Start;
		GameTraceLog.LogError($"[NextLevelTrace] Gameplay.onLevelLoaded frame={Time.frameCount} playerTurn={m_IsPlayerTurn} state={m_CurrentTurnState}");
	}

	private void onLevelStarted()
	{
		GameTraceLog.LogError($"[NextLevelTrace] Gameplay.onLevelStarted frame={Time.frameCount} players={UnitManager.Instance.PlayerUnits.Count} enemies={UnitManager.Instance.EnemyUnits.Count}");
		setNextTurn(eTurnState.Start);
	}

	private void onUnitWeaponThrown(IUnit i_Character)
	{
		setNextTurn(eTurnState.Waiting);
	}

	private void onWeaponFinished(IWeapon i_Weapon)
	{
		SetNextTurnDelay();
	}

	private void onMatchMakingOpen()
	{
		GameTraceLog.LogError($"[NextLevelTrace] Gameplay.onMatchMakingOpen frame={Time.frameCount} playerTurn={m_IsPlayerTurn} stateBefore={m_CurrentTurnState} players={UnitManager.Instance.PlayerUnits.Count} enemies={UnitManager.Instance.EnemyUnits.Count}");
		setNextTurn(eTurnState.SelectingWeapon);
	}

	private void onSetWeapon(TItemData i_Item)
	{
		setNextTurn(eTurnState.UnitTurn);
	}

	private void onUnitDeath(UnitBase i_Unit)
	{
		if (i_Unit != null && m_CurrentUnit != null && !UnitManager.Instance.ArePlayerUnitsDead && !UnitManager.Instance.AreEnemyUnitsDead && m_CurrentUnit == i_Unit)
		{
			m_IsPlayerTurn = i_Unit.UnitType == eUnityType.Player;
			SetNextTurnDelay();
		}
	}

	private void setNextTurn(eTurnState i_State)
	{
		if (UnitManager.Instance.ArePlayerUnitsDead || UnitManager.Instance.AreEnemyUnitsDead)
			return;
		if (i_State == eTurnState.UnitTurn)
		{
			UnitManager.Instance.StopAllUnitTurns();
			UnitBase unit = m_IsPlayerTurn ? getNextAlivePlayerUnit() : getNextAliveEnemyUnit();
			if (m_CurrentTurnState != eTurnState.SelectingWeapon && m_IsPlayerTurn)
			{
				m_CurrentUnit = unit;
				setState(eTurnState.SelectingWeapon);
				return;
			}
			m_IsPlayerTurn = !m_IsPlayerTurn;
			if (unit != null)
			{
				unit.StartTurn();
				m_CurrentUnit = unit;
				setState(eTurnState.UnitTurn);
				return;
			}
		}
		setState(i_State);
	}

	private UnitBase getNextAlivePlayerUnit()
	{
		UnitBase unit = null;
		for (int i = 0; i < 30; i++)
		{
			unit = UnitManager.Instance.PlayerUnits[m_PlayerUnitIndex];
			m_PlayerUnitIndex = (m_PlayerUnitIndex + 1) % UnitManager.Instance.PlayerUnits.Count;
			if (unit != null && !unit.IsUnitDead)
				break;
		}
		return unit;
	}

	private UnitBase getNextAliveEnemyUnit()
	{
		UnitBase unit = null;
		for (int i = 0; i < 30; i++)
		{
			unit = UnitManager.Instance.EnemyUnits[m_EnemyUnitIndex];
			m_EnemyUnitIndex = (m_EnemyUnitIndex + 1) % UnitManager.Instance.EnemyUnits.Count;
			if (unit != null && !unit.IsUnitDead)
				break;
		}
		return unit;
	}

	private void setTurnState()
	{
		setNextTurn(eTurnState.UnitTurn);
	}

	public void SetNextTurnDelay()
	{
		Invoke(nameof(setTurnState), 0.3f);
	}

	private void setState(eTurnState i_State)
	{
		m_CurrentTurnState = i_State;
		GameTraceLog.LogError($"[NextLevelTrace] Gameplay.setState frame={Time.frameCount} state={i_State} playerTurn={m_IsPlayerTurn} current={(m_CurrentUnit == null ? "NULL" : m_CurrentUnit.name)}");
		OnStateChange?.Invoke(i_State);
	}

	public void SetMatchMakingItem(MatchMakingItem i_Item)
	{
		CurrentMatchMakingItem = i_Item;
	}
}
