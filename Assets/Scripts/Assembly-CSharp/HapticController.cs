using KobGamesSDKSlim;
using KobGamesSDKSlim.GameManagerV1;
using MoreMountains.FeedbacksForThirdParty;

public class HapticController : Singleton<HapticController>
{
	private float m_Timer;

	private float m_Duration;

	private MMFeedbackHaptics.HapticTypes m_HapticType;

	private bool m_IsVibrating;

	private new void OnEnable()
	{
		GameManagerBase.OnGameReset += onLevelFinished;
		GameManagerBase.OnLevelFailedNoContinue += onLevelFinished;
		GameManagerBase.OnLevelCompleted += onLevelFinished;
	}

	public override void OnDisable()
	{
		GameManagerBase.OnGameReset -= onLevelFinished;
		GameManagerBase.OnLevelFailedNoContinue -= onLevelFinished;
		GameManagerBase.OnLevelCompleted -= onLevelFinished;
		base.OnDisable();
	}

	private void onLevelFinished()
	{
	}

	private void Update()
	{
		if (m_IsVibrating)
		{
			m_Timer += UnityEngine.Time.deltaTime;
			if (m_Timer < m_Duration)
				Managers.Instance.HapticManager.Haptic(m_HapticType);
			if (m_Timer > m_Duration)
				stopVibration();
		}
	}

	private void Haptic(HapticData i_Data)
	{
		if (i_Data == null)
			return;
		if (m_IsVibrating)
		{
			if (!i_Data.Override)
				return;
			stopVibration();
		}
		if (i_Data.IsWork)
		{
			m_Timer = 0f;
			m_Duration = i_Data.Duration;
			m_HapticType = i_Data.HapticType;
			m_IsVibrating = true;
		}
	}

	public void Haptic(eHapticEvent i_Event)
	{
		Haptic(GameConfig.Instance.Haptics.GetHapticData(i_Event));
	}

	private void stopVibration()
	{
		m_Timer = 0f;
		m_Duration = 0f;
		m_IsVibrating = false;
	}
}
