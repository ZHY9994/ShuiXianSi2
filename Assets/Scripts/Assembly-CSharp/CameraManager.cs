using Cinemachine;
using KobGamesSDKSlim;
using KobGamesSDKSlim.GameManagerV1;
using MrWayne;
using UnityEngine;

[ExecutionOrder(101)]
public class CameraManager : Singleton<CameraManager>
{
	public enum eCameraState
	{
		MainMenu = 0,
		GamePlay = 1,
		Win = 2
	}

	[SerializeField]
	private Transform m_Rig;

	[SerializeField]
	private Transform m_Gimbal;

	[SerializeField]
	private Transform m_Dolly;

	[SerializeField]
	private Camera m_MainCamera;

	[SerializeField]
	private Camera m_UICamera;

	[SerializeField]
	private CinemachineStateDrivenCamera m_StateController;

	[SerializeField]
	private CinemachineVirtualCamera m_WinCamera;

	[SerializeField]
	private CinemachineVirtualCamera m_GameplayCamera;

	private eCameraState m_State;

	public Camera MainCamera => m_MainCamera;

	public Camera UICamera => m_UICamera;

	private CameraVariablesEditor m_CameraVars => GameConfig.Instance.Camera;

	private new void OnEnable()
	{
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelLoaded += onLevelLoaded;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompleted += onLevelCompleted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelFailedNoContinue += onLevelCompleted;
		MrWayne.UnitHealth.OnUnitDeath += onUnitDeath;
	}

	public override void OnDisable()
	{
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelLoaded -= onLevelLoaded;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompleted -= onLevelCompleted;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelFailedNoContinue -= onLevelCompleted;
		MrWayne.UnitHealth.OnUnitDeath -= onUnitDeath;
	}

	private void onLevelLoaded()
	{
		setState(eCameraState.MainMenu);
	}

	private void onLevelStarted()
	{
		setState(eCameraState.GamePlay);
	}

	private void onUnitDeath(UnitBase i_Unit)
	{
		if (UnitManager.Instance == null)
			return;
		if (UnitManager.Instance.AreEnemyUnitsDead)
		{
			setWinPlayerTarget();
			setState(eCameraState.Win);
		}
		else if (UnitManager.Instance.ArePlayerUnitsDead)
		{
			setWinEnemyTarget();
			setState(eCameraState.Win);
		}
	}

	private void onLevelCompleted()
	{
		setState(eCameraState.Win);
	}

	private void setWinPlayerTarget()
	{
		UnitBase unit = UnitManager.Instance != null ? UnitManager.Instance.AlivePlayerUnit : null;
		setWinTarget(unit);
	}

	private void setWinEnemyTarget()
	{
		UnitBase unit = UnitManager.Instance != null ? UnitManager.Instance.AliveEnemyUnit : null;
		setWinTarget(unit);
	}

	private void setWinTarget(UnitBase i_Unit)
	{
		if (m_WinCamera == null || i_Unit == null)
			return;
		m_WinCamera.Follow = i_Unit.transform;
	}

	public void ForceStateChange(eCameraState i_State)
	{
		setState(i_State);
	}

	private void setState(eCameraState i_State)
	{
		Animator animatedTarget = m_StateController.m_AnimatedTarget;
		animatedTarget.SetInteger(StaticString.AP_State, (int)i_State);
		animatedTarget.SetTrigger(StaticString.AP_OnStateChange);
		m_State = i_State;
	}

	public void SetGameplayCamera(Transform i_FollowPoint, float i_Distance)
	{
		m_GameplayCamera.Follow = i_FollowPoint;
		m_GameplayCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = i_Distance;
	}

	private void SetRef()
	{
		m_Rig = transform;
		m_Gimbal = transform.FindDeepChild("Gimbal");
		m_Dolly = transform.FindDeepChild("Dolly");
		m_MainCamera = Camera.main;
		m_UICamera = GameObject.Find("Camera UI").GetComponent<Camera>();
		m_StateController = GetComponentInChildren<CinemachineStateDrivenCamera>();
		m_WinCamera = transform.FindDeepChild<CinemachineVirtualCamera>("Win Cam");
	}
}
