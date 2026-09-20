using System;
using KobGamesSDKSlim;
using UnityEngine;
using KobGamesSDKSlim.Collectable;
using KobGamesSDKSlim.GameManagerV1;

public class ExperienceManager : Singleton<ExperienceManager>
{
	public static Action OnReset;

	private GamePlayVariablesEditor.ExperienceDataEditor m_Parameters => GameConfig.Instance.GamePlay.Experience;

	public int Experience
	{
		get
		{
			return PlayerPrefs.GetInt("Experience");
		}
		set
		{
			PlayerPrefs.SetInt("Experience", value);
		}
	}

	public int ExperienceLevel
	{
		get
		{
			return PlayerPrefs.GetInt("ExperienceLevel");
		}
		set
		{
			PlayerPrefs.SetInt("ExperienceLevel", value);
		}
	}

	public GamePlayVariablesEditor.ExperienceDataEditor.ExperienceLevelData CurrentExperienceData
	{
		get
		{
			GamePlayVariablesEditor.ExperienceDataEditor.ExperienceLevelData[] experienceData = m_Parameters?.ExperienceData;
			if (experienceData == null || experienceData.Length == 0)
			{
				return new GamePlayVariablesEditor.ExperienceDataEditor.ExperienceLevelData
				{
					ExperienceValue = 1
				};
			}
			int index = Mathf.Clamp(ExperienceLevel, 0, experienceData.Length - 1);
			return experienceData[index];
		}
	}

	public int TargetExperience => CurrentExperienceData.ExperienceValue;

	public float ExperienceProgression => Mathf.Clamp01((float)Experience / Mathf.Max(1, CurrentExperienceData.ExperienceValue));

	public int HealthIncrease => CurrentExperienceData.HealthIncreaseValue;

	public bool IsBlock { get; set; }

	protected override void OnEnable()
	{
		base.OnEnable();
		GameManagerBase.OnLevelCompleted += onLevelCompleted;
		GameManagerBase.OnLevelFailed += onLevelFail;
		GameManagerBase.OnLevelFailedNoContinue += onLevelFail;
		IsBlock = false;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		GameManagerBase.OnLevelCompleted -= onLevelCompleted;
		GameManagerBase.OnLevelFailed -= onLevelFail;
		GameManagerBase.OnLevelFailedNoContinue -= onLevelFail;
	}

	private void onLevelCompleted()
	{
		Experience += m_Parameters.ExperiencePerWin;
	}

	private void onLevelFail()
	{
		Experience += m_Parameters.ExperiencePerLose;
	}

	public void SetBlock(bool i_IsBlock)
	{
		IsBlock = i_IsBlock;
	}

	public void LevelUp()
	{
		StorageManager.Instance.PlayerHealth += HealthIncrease;
		StorageManager.Instance.AddCollectable(eCollectableType.Coin, CurrentExperienceData.CashIncreaseValue);
		Experience = 0;
		ExperienceLevel++;
		OnReset?.Invoke();
	}
}
