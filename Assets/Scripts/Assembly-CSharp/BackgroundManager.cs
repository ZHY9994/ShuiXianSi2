using KobGamesSDKSlim;
using UnityEngine;

public class BackgroundManager : Singleton<BackgroundManager>
{
	[SerializeField]
	private Background[] m_BackGrounds;

	private bool m_CanMove;

	private int m_ActiveIndex;

	private bool m_CanUseParallaxEffect => GameConfig.Instance.GamePlay.BackgroundParameters.UseParallaxEffect;

	protected override void OnEnable()
	{
		base.OnEnable();
		ResetBG();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset += onGameReset;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset -= onGameReset;
	}

	private void onLevelStarted()
	{
		m_CanMove = true;
	}

	private void onGameReset()
	{
		m_CanMove = false;
	}

	private void Update()
	{
		if (m_CanMove && m_CanUseParallaxEffect)
		{
			moveBGParts();
		}
	}

	private void moveBGParts()
	{
		m_BackGrounds[m_ActiveIndex].MoveParts();
	}

	public void ResetBG()
	{
		resetAllBGPositions();
		hideAllBG();
		showActiveBG();
	}

	private void resetAllBGPositions()
	{
		for (int i = 0; i < m_BackGrounds.Length; i++)
		{
			m_BackGrounds[i].ResetPositions();
		}
	}

	private void showActiveBG()
	{
		m_ActiveIndex = (StorageManager.Instance.CurrentLevel - 1) % m_BackGrounds.Length;
		CameraManager.Instance.MainCamera.backgroundColor = m_BackGrounds[m_ActiveIndex].SkyColor;
		m_BackGrounds[m_ActiveIndex].BGVisibility(true);
		RenderSettings.fog = m_BackGrounds[m_ActiveIndex].UseFog;
	}

	private void hideAllBG()
	{
		for (int i = 0; i < m_BackGrounds.Length; i++)
		{
			m_BackGrounds[i].BGVisibility(false);
		}
	}

	private void SetRefs()
	{
		m_BackGrounds = GetComponentsInChildren<Background>(true);
	}

	private void TestBackGround(int i_Index)
	{
		hideAllBG();
		m_BackGrounds[i_Index].BGVisibility(true);
		CameraManager.Instance.MainCamera.backgroundColor = m_BackGrounds[i_Index].SkyColor;
		RenderSettings.fog = m_BackGrounds[i_Index].UseFog;
	}

	public BackgroundManager()
	{
	}
}
