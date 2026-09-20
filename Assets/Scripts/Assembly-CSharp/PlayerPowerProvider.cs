using UnityEngine;
using KobGamesSDKSlim;

public class PlayerPowerProvider : MonoBehaviour
{
	private DifficultyConfig m_Config => GameConfig.Instance.GamePlay.Difficulty.Config;

	public float Power => 120f;
}
