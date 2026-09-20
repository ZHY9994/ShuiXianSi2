using System;
using MrWayne;
using UnityEngine;
using UnityEngine.Serialization;
using Wayne.Game.Tutorials;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class GamePlayVariablesEditor : GamePlayVariablesEditorBase
	{
		[Serializable]
		public class ExperienceDataEditor
		{
			[Serializable]
			public class ExperienceLevelData
			{
				public int ExperienceValue;

				public int CashIncreaseValue;

				public int HealthIncreaseValue
				{
					get
					{
						float healthPerLevel = DifficultyManager.Instance.Config.HealthPerLevel;
						return float.IsPositiveInfinity(healthPerLevel) ? int.MinValue : (int)healthPerLevel;
					}
				}
			}

			public int ExperiencePerWin;

			public int ExperiencePerLose;

			public ExperienceLevelData[] ExperienceData;
		}

		[Serializable]
		public class MatchMakingParameter
		{
			public Vector2 MinMaxVelocity;

			public float StartGameDelay;

			public int CharacterNameLimit;

			public string[] m_Names;

			public Sprite[] Flags;
		}

		[Serializable]
		public class ObstacleParameter
		{
			[Serializable]
			public class ExplosiveBarrelParameters
			{
				public float ExplosionRadius;

				public int ExplosionDamage;

				public float PushForce;
			}

			[Serializable]
			public class WoodenCrateParameters
			{
				public float MinImpulseForce;

				public RandomRangeValue MinMaxFragRepulse;

				public float ShankPushForce;

				public RandomRangeValue MinMaxShankDamage;

				public RandomRangeValue MinMaxShankDamagePlayer;

				public RandomRangeValue MinMaxShankHideDuration;
			}

			public bool MiddleObstacleWall;

			public float InvisibleWallOffset;

			public ExplosiveBarrelParameters ExplosiveBarrel;

			public WoodenCrateParameters WoodenCrate;
		}

		[Serializable]
		public class BackGroundParameter
		{
			public bool UseParallaxEffect;

			public float LerpSpeed;

			public BackGroundData BackgroundData;
		}

		[FormerlySerializedAs("UnitEditor")]
		public UnitEditor UnitData;

		[FormerlySerializedAs("WeaponDataEditor")]
		public WeaponDataEditor WeaponData;

		public DeckDataEditor DeckData;

		public TrajectoryDataEditor Trajectory;

		public MatchMakingParameter MatchMakingParameters = new MatchMakingParameter
		{
			MinMaxVelocity = new Vector2(2500f, 5000f),
			StartGameDelay = 1f,
			CharacterNameLimit = 10
		};

		public ObstacleParameter ObstacleParameters = new ObstacleParameter();

		public BackGroundParameter BackgroundParameters = new BackGroundParameter { LerpSpeed = 5f };

		public EffectsDataEditor Effects;

		public BoosterData BoosterData;

		public AudioDataEditor AudioDataEditor;

		public DifficultyDataEditor Difficulty;

		public ExperienceDataEditor Experience;

		public TutorialVariableEditor Tutorials;

		[HideInInspector]
		public TimeManagerData TimeManagerData = new TimeManagerData();
	}
}
