using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using MrWayne;

public class UnitManager : Singleton<UnitManager>
{
	private List<UnitBase> m_ActiveUnits;

	private bool m_IsCompletedCalled;

	private bool m_IsGameEndCalled;

	private Level m_CurrentLevel => LevelManager.Instance.CurrentLevel;

	public List<UnitBase> ActiveUnits => m_ActiveUnits;

	public bool ArePlayerUnitsDead => PlayerUnits.All(x => x.IsUnitDead);

	public bool AreEnemyUnitsDead => EnemyUnits.All(x => x.IsUnitDead);

	public bool IsGameFinished => ArePlayerUnitsDead || AreEnemyUnitsDead;

	public UnitBase AlivePlayerUnit => PlayerUnits.FirstOrDefault(x => !x.IsUnitDead);

	public UnitBase AliveEnemyUnit => EnemyUnits.FirstOrDefault(x => !x.IsUnitDead);

	public UnitBase[] AlivePlayerUnits => PlayerUnits.Where(x => !x.IsUnitDead).ToArray();

	public UnitBase[] AliveEnemyUnits => EnemyUnits.Where(x => !x.IsUnitDead).ToArray();

	public List<UnitBase> PlayerUnits { get; private set; }

	public List<UnitBase> EnemyUnits { get; private set; }

	public static event Action<UnitBase> OnUnitSpawned = delegate { };

	protected override void OnEnable()
	{
		base.OnEnable();
		m_IsCompletedCalled = false;
		Screen_Transition.OnStartGame += onMatchMakingClose;
		UnitHealth.OnUnitDeath += onUnitDeath;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset += onGameReset;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		Screen_Transition.OnStartGame -= onMatchMakingClose;
		UnitHealth.OnUnitDeath -= onUnitDeath;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset -= onGameReset;
	}

	private void onLevelStarted()
	{
		setupUnits();
	}

	private void onMatchMakingClose()
	{
		activateAllUnits();
	}

	private void onUnitDeath(UnitBase i_Unit)
	{
		if (m_IsGameEndCalled)
			return;
		string methodName;
		if (AreEnemyUnitsDead)
		{
			methodName = "GameWin";
		}
		else
		{
			if (!ArePlayerUnitsDead)
				return;
			methodName = "GameLoss";
		}
		m_IsGameEndCalled = true;
		Invoke(methodName, 1f);
	}

	private void onGameReset()
	{
		m_IsGameEndCalled = false;
	}

	private void setupUnits()
	{
		GameTraceLog.LogError($"[PlayerPoolTrace] setupUnits level={(m_CurrentLevel != null ? m_CurrentLevel.name : "<null>")} activeBefore={m_ActiveUnits.Count} playerBefore={PlayerUnits.Count} enemyBefore={EnemyUnits.Count}");
		m_ActiveUnits.Clear();
		PlayerUnits.Clear();
		EnemyUnits.Clear();
		foreach (UnitSpawn spawn in m_CurrentLevel.AllUnitSpawn)
		{
			if (spawn.Type == eUnityType.Player)
			{
				GameTraceLog.LogError($"[PlayerPoolTrace] playerSpawn name={spawn.name} pos={spawn.transform.position} rot={spawn.transform.rotation}");
				PlayerUnits.Add(spawnUnit(ePoolType.Player, spawn, true));
			}
			else if (spawn.Type == eUnityType.AI)
				EnemyUnits.Add(spawnUnit(ePoolType.AI, spawn, true));
		}
	}

	private UnitBase spawnUnit(ePoolType i_PoolType, UnitSpawn i_UnitSpawn, bool i_SpawnVisible)
	{
		GameObject pooled = PoolManager.Instance.Dequeue(i_PoolType, i_UnitSpawn.transform.position);
		UnitBase unit = pooled.GetComponent<UnitBase>();
		if (i_PoolType == ePoolType.Player)
			GameTraceLog.LogError($"[PlayerPoolTrace] spawn dequeued name={pooled.name} id={pooled.GetInstanceID()} rootPos={pooled.transform.position} model={(unit != null && unit.transform.Find("Model") != null ? unit.transform.Find("Model").position.ToString() : "<unresolved>")}");
		unit.InitializeUnit(i_UnitSpawn);
		if (i_PoolType == ePoolType.Player)
			GameTraceLog.LogError($"[PlayerPoolTrace] spawn initialized rootPos={unit.transform.position} visibleRoot={(unit.transform.Find("Model") != null ? unit.transform.Find("Model").gameObject.activeSelf.ToString() : "<unresolved>")}");
		unit.UnitVisibility(i_SpawnVisible);
		m_ActiveUnits.Add(unit);
		OnUnitSpawned?.Invoke(unit);
		return unit;
	}

	private void activateAllUnits()
	{
		foreach (UnitBase unit in m_ActiveUnits)
			unit.SetUnitActive();
	}

	public UnitBase GetUnit(eUnityType i_Type)
	{
		return m_ActiveUnits.FirstOrDefault(x => x.UnitType == i_Type);
	}

	public UnitBase GetUnit(UnitBase i_IgnoredUnit)
	{
		if (i_IgnoredUnit == null)
		{
			return null;
		}
		if (i_IgnoredUnit.UnitType == eUnityType.Player)
		{
			return AliveEnemyUnit;
		}
		if (i_IgnoredUnit.UnitType == eUnityType.AI)
		{
			return AlivePlayerUnit;
		}
		return m_ActiveUnits.FirstOrDefault(x => x != null && x != i_IgnoredUnit && !x.IsUnitDead);
	}

	public void StopAllUnitTurns()
	{
		foreach (UnitBase unit in PlayerUnits)
			unit.StopTurn();
		foreach (UnitBase unit in EnemyUnits)
			unit.StopTurn();
	}

	private void GameWin()
	{
		KobGamesSDKSlim.GameManager.Instance.LevelCompleted();
	}

	private void GameLoss()
	{
		KobGamesSDKSlim.GameManager.Instance.GameOver(true);
	}

	private void killAIUnit()
	{
		foreach (UnitBase unit in AliveEnemyUnits)
			unit.Health.ForceKill();
	}

	public UnitManager()
	{
		m_ActiveUnits = new List<UnitBase>();
		PlayerUnits = new List<UnitBase>();
		EnemyUnits = new List<UnitBase>();
	}
}
