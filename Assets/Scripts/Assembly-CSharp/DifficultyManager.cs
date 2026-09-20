using KobGamesSDKSlim;
using UnityEngine;

public class DifficultyManager : Singleton<DifficultyManager>
{
	[SerializeField]
	private PlayerPowerProvider m_PowerProvider;

	private const string k_HealthID = "difficulty_health";

	private float m_TargetHealth;

	public float[] Weights;

	private DifficultyConfig m_Config => GameConfig.Instance.GamePlay.Difficulty.Config;

	public DifficultyConfig Config => m_Config;

	public float Power => m_PowerProvider.Power;

	public float Health => m_Health;

	public float TargetHealth => m_TargetHealth;

	private float m_Health
	{
		get
		{
			return PlayerPrefs.GetFloat(k_HealthID, -1f);
		}
		set
		{
			PlayerPrefs.SetFloat(k_HealthID, value);
		}
	}

	public float MinHealth => m_Health - m_Config.SpawnRange.x * m_Health;

	public float MaxHealth => m_Health + m_Config.SpawnRange.y * m_Health;

	public float PeriodicDifficulty
	{
		get
		{
			float normalized = 0f;
			if (m_Config.PeriodicDifficultyPeriod != 0f)
			{
				normalized = Mathf.Clamp01(Mathf.Repeat(StorageManager.Instance.CurrentLevel - 1, m_Config.PeriodicDifficultyPeriod) / m_Config.PeriodicDifficultyPeriod);
			}
			return m_Config.PeriodicDifficultyAmplitude * m_Config.PeriodicDifficultyCurve.Evaluate(normalized);
		}
	}

	public float GetHealth()
	{
		return GetHealth(getCurrentLevelWeight());
	}

	public float GetHealth(float i_Weight)
	{
		float minHealth = MinHealth;
		float maxHealth = MaxHealth;
		return minHealth + Mathf.Clamp01(i_Weight) * (maxHealth - minHealth);
	}

	public float GetHealthPercent(float i_Health)
	{
		float maxHealth = Mathf.Max(MaxHealth, MinHealth + 15f);
		if (MinHealth == maxHealth)
		{
			return 0f;
		}
		return Mathf.Clamp01((i_Health - MinHealth) / (maxHealth - MinHealth));
	}

	private void OnValidate()
	{
		m_PowerProvider = GetComponentInChildren<PlayerPowerProvider>();
	}

	public override void OnAwakeEvent()
	{
		base.OnAwakeEvent();
		if (m_Health == -1f)
		{
			calculateTargetHealth();
			m_Health = m_TargetHealth;
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelLoaded += onLevelLoaded;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStart;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompleted += onLevelComplete;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelFailed += onLevelFail;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelLoaded -= onLevelLoaded;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStart;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompleted -= onLevelComplete;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelFailed -= onLevelFail;
	}

	private void onLevelLoaded()
	{
		calculateTargetHealth();
	}

	private void onLevelStart()
	{
		calculateTargetHealth();
	}

	private void onLevelComplete()
	{
		calculateTargetHealth();
	}

	private void onLevelFail()
	{
		calculateTargetHealth();
	}

	private void calculateTargetHealth()
	{
		m_TargetHealth = m_Config.PowerConversion * Power + m_Config.DifficultyIncreaseOnWinNew * StorageManager.Instance.CurrentLevel;
		m_Health = m_TargetHealth;
	}

	public void InitWeights()
	{
		Weights = new float[100];
		for (int i = 0; i < Weights.Length; i++)
		{
			Weights[i] = Random.value;
		}
	}

	private float getWeight(int i_Id)
	{
		return Weights[i_Id % Weights.Length];
	}

	private float getCurrentLevelWeight()
	{
		return getWeight(StorageManager.Instance.CurrentLevel);
	}

	public int GetEnemyHealth()
	{
		return Mathf.RoundToInt(GetHealth(getCurrentLevelWeight()));
	}
}
