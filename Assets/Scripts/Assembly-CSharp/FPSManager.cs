using UnityEngine;
using KobGamesSDKSlim;
using KobGamesSDKSlim.GameManagerV1;

public class FPSManager : MonoBehaviour
{
	[SerializeField]
	[Range(1f, 30f)]
	private float m_UpdatesPerSecond = 5f;

	public static int FPSAverageLastUpdate;

	public static int FPSTicksLastUpdate;

	public static int FPSAveragePreviousLevel;

	public static int FPSTickPreviousLevel;

	private float m_LastTime;

	private float m_DeltaTime;

	private float m_DeltaTimeSum;

	private float m_FPSSumPreviousLevel;

	private float m_CurrTime => Time.time;

	private void OnEnable()
	{
		FPSTicksLastUpdate = 0;
		m_LastTime = m_CurrTime;
		GameManagerBase.OnLevelStarted += OnLevelStarted;
	}

	private void OnDisable()
	{
		GameManagerBase.OnLevelStarted -= OnLevelStarted;
	}

	private void OnLevelStarted()
	{
		FPSTicksLastUpdate = 0;
		m_FPSSumPreviousLevel = 0f;
	}

	private void Update()
	{
		m_DeltaTime = m_CurrTime - m_LastTime;
		if (m_DeltaTime <= 0f)
			return;
		m_LastTime = m_CurrTime;
		m_DeltaTimeSum += m_DeltaTime;
		FPSTicksLastUpdate++;
		if (m_DeltaTimeSum <= 1f / m_UpdatesPerSecond)
			return;
		FPSAverageLastUpdate = Mathf.RoundToInt(FPSTicksLastUpdate / m_DeltaTimeSum);
		FPSTicksLastUpdate = 0;
		m_DeltaTimeSum = 0f;
		if (GameManager.Instance.GameState == eGameState.Playing)
		{
			m_FPSSumPreviousLevel += FPSAverageLastUpdate;
			FPSAveragePreviousLevel = Mathf.RoundToInt(m_FPSSumPreviousLevel / ++FPSTickPreviousLevel);
		}
	}
}
