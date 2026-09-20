using System;
using System.Collections.Generic;
using KobGamesSDKSlim;
using UnityEngine;

[CreateAssetMenu(menuName = "KobGames/Difficulty", fileName = "Difficulty")]
public class DifficultyConfig : ScriptableObject
{
	[Serializable]
	public class EnemyDeckData
	{
		[HideInInspector]
		public int Id;

		public List<eWeaponType> Weapons;
	}

	public Vector2 SpawnRange;

	public float PowerConversion = 0.3f;

	[SerializeField]
	public List<EnemyDeckData> EnemyWeapons;

	public float WinCoef = 0.5f;

	public float LoseCoef = 0.1f;

	public float DifficultyIncreaseOnWin = 1.05f;

	public float DifficultyIncreaseOnWinNew = 5f;

	public AnimationCurve PeriodicDifficultyCurve;

	public float PeriodicDifficultyAmplitude;

	public float PeriodicDifficultyPeriod;

	public float HealthPerLevel => GameConfig.Instance.GamePlay.UnitData.GeneralUnitParameters.HealthPerLevel;

	private void onEnemyWeaponsChanged()
	{
		for (int i = 0; i < EnemyWeapons.Count; i++)
		{
			EnemyWeapons[i].Id = i + 1;
		}
	}
}
