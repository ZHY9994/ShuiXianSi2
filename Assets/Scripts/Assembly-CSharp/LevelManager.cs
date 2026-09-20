using DG.Tweening;
using KobGamesSDKSlim;
using UnityEngine;
using MrWayne;

public class LevelManager : LevelManagerBase
{
	[SerializeField]
	private DummyLevel m_DummyLevel;

	private Level m_CurrentLevelStructure;

	private int m_DummyLevelProgress;

	private int m_LevelCounter;

	private LevelNumbers m_CurrentLevelNumber;

	private bool m_FirstLaunch = true;

	private Sequence m_LevelCompleteSequence;

	private bool m_EndCalled;

	protected override int m_CurrentLevelProgress => m_DummyLevelProgress;

	public override Level CurrentLevel => m_CurrentLevelStructure;

	private LevelsVariablesEditor m_LevelParameters => GameConfig.Instance.Levels;

	protected override void onEnableImpl()
	{
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompleted += onLevelCompleted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelFailedNoContinue += onLevelFailed;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset += onGameReset;
	}

	protected override void onDisableImpl()
	{
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompleted -= onLevelCompleted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelFailedNoContinue -= onLevelFailed;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset -= onGameReset;
	}

	private void onLevelStarted()
	{
		disableDummy();
		m_CurrentLevelStructure.gameObject.SetActive(true);
		m_EndCalled = false;
		m_DummyLevelProgress = 100;
	}

	private void onGameReset()
	{
		if (!m_FirstLaunch) onResetSequence();
		m_EndCalled = false;
	}

	private void onLevelCompleted()
	{
		m_LevelCounter = 0;
		GameConfig.Instance.Levels.SetLoopedLevel();
	}

	private void onLevelFailed()
	{
	}

	protected override void setNextLevelAndDataImpl()
	{
		if (m_CurrentLevelStructure != null) Destroy(m_CurrentLevelStructure.gameObject);
		m_CurrentLevelStructure = Instantiate(m_LevelParameters.GetLevel());
        m_CurrentLevelStructure.transform.SetParent(transform.GetChild(0));
		m_CurrentLevelStructure.gameObject.SetActive(false);
		Vector3 position = m_CurrentLevelStructure.GetSpawn(eUnityType.Player).transform.position;
		position.y = 0.488f;
		m_DummyLevel.SetDummyPosition(position);
		CurrentLevelData.SetOptionalData(i_LevelRandom: false, i_LevelLoop: StorageManager.Instance.LevelLoop);
	}

	protected override void loadLevelImpl()
	{
		if (m_FirstLaunch)
		{
			KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenWelcomeScreen();
			enableDummy();
			m_FirstLaunch = false;
		}
	}

	private void onResetSequence()
	{
		m_LevelCompleteSequence?.Kill();
		m_LevelCompleteSequence = DOTween.Sequence().SetId(-90);
		m_LevelCompleteSequence.Append(HUDManager.Instance.GetFadeImage().DOFade(1f, 0.3f));
		m_LevelCompleteSequence.AppendInterval(0.3f);
		m_LevelCompleteSequence.AppendCallback(() => KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenWelcomeScreen());
		m_LevelCompleteSequence.AppendCallback(enableDummy);
		m_LevelCompleteSequence.Append(HUDManager.Instance.GetFadeImage().DOFade(0f, 0.5f));
	}

	private void disableDummy()
	{
		m_DummyLevel.gameObject.SetActive(false);
	}

	private void enableDummy()
	{
		m_DummyLevel.gameObject.SetActive(true);
	}

	private void SetRefs()
	{
	}
}
