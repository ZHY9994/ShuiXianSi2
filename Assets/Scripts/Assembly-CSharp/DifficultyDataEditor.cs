using System;

[Serializable]
public class DifficultyDataEditor
{
	public DifficultyConfig Config;

	public int CurrentPower => (int)DifficultyManager.Instance.Power;

	public int MinHealth => (int)DifficultyManager.Instance.MinHealth;

	public int MaxHealth => (int)DifficultyManager.Instance.MaxHealth;

	public int CurrentHealth => (int)DifficultyManager.Instance.Health;

	public int TargetHealth => (int)DifficultyManager.Instance.TargetHealth;

	public float PeriodicDifficulty => DifficultyManager.Instance.PeriodicDifficulty;
}
