using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class PlayerAimingState : UnitState
	{
		private bool m_IsMouseDown;

		private Vector2 m_InitialMousePos;

		private Vector2 m_CurrentMousePos;

		private Vector2 m_DragDireciton;

		private Vector3 m_PushDirection;

		private float m_Distance;

		private float m_TapTime;

		public override eUnitState StateType => eUnitState.Aiming;

		private Vector3 MousePosition => InputManager.Instance.MousePosition;

		private WeaponBase m_ActiveWeapon => m_UnitStateMachine.Unit.WeaponHandler.ActiveWeapon;

		private UnitTrajectory m_Trajectory => m_UnitStateMachine.Unit.Trajectory;

		public static event Action<eWeaponType> OnPlayerThrown;

		public static event Action<eWeaponType> OnWeaponSpawned;

		protected override void OnEnable()
		{
			InputManager.OnInputDown += onInputDown;
			InputManager.OnInputUp += onInputUp;
		}

		protected override void OnDisable()
		{
			InputManager.OnInputDown -= onInputDown;
			InputManager.OnInputUp -= onInputUp;
		}

		private void onInputDown(Vector2 i_Position)
		{
			if (!IsActive || m_Unit.Health.Health <= 0f || !m_IsUnitTurn)
			{
				return;
			}
			m_IsMouseDown = true;
			m_InitialMousePos = MousePosition;
			EventManager.OnPlayerStartAimEvent.Trigger();
		}

		private void onInputUp()
		{
			if (!IsActive || m_Unit.Health.Health <= 0f || !m_IsUnitTurn || !m_IsMouseDown)
			{
				return;
			}
			if (Vector3.Distance(m_InitialMousePos, Input.mousePosition) < m_UnitEditor.Player.MinimumDrag)
			{
				resetPush();
				return;
			}
			m_IsMouseDown = false;
			m_Unit.Animator.m_LookPosition = Vector3.zero;
			m_Unit.Animator.PlayUnitThrow();
			m_Unit.WeaponHandler.FireWeapon(m_PushDirection, Vector3.right, OnPlayerThrown);
			m_Trajectory.HideTrajectory();
			TrajectoryManager.Instance.HideTutorial();
			m_Unit.StopTurn();
			m_UnitStateMachine.ChangeUnitState(eUnitState.Idle);
			EventManager.OnPlayerEndAimEvent.Trigger();
		}

		protected override void WhenEnter(IStateMachine i_StateMachine)
		{
			resetParameters();
			m_Unit.WeaponHandler.InitializeWeapon(m_Unit.WeaponHandler.m_DummyPlacementType, OnWeaponSpawned);
			m_Unit.Animator.m_Animator.SetLayerWeight(1, 1f);
			// Temporary runtime validation: the exported Head layer has no AvatarMask
			// and otherwise overrides the RightHand.Throw Idle pose.
			m_Unit.Animator.m_Animator.SetLayerWeight(2, 0f);
		}

		protected override void WhenUpdate(IStateMachine i_StateMachine)
		{
			loopFunction();
			// UnitState.updateHeadLook restores layer 2 every frame while Puppet is active.
			// Keep it disabled for this validation until a proper Head AvatarMask is restored.
			m_Unit.Animator.m_Animator.SetLayerWeight(2, 0f);
			if (m_IsMouseDown)
			{
				updateDragPosition();
				setTrajectory();
			}
		}

		protected override void WhenExit(IStateMachine i_StateMachine)
		{
			return;
		}

		private void updateDragPosition()
		{
			if (m_InitialMousePos == Vector2.zero && !m_IsMouseDown)
			{
				m_InitialMousePos = MousePosition + Vector3.left;
			}
			m_CurrentMousePos = MousePosition;
			m_DragDireciton = m_InitialMousePos - m_CurrentMousePos;
			m_Unit.Animator.m_LookPosition = new Vector3(0f, -m_DragDireciton.y, -m_DragDireciton.x);
			m_Distance = Vector2.Distance(m_InitialMousePos, m_CurrentMousePos);
		}

		private void setTrajectory()
		{
			float min = m_UnitEditor.Player.DragDistanceMinMax.x;
			float max = m_UnitEditor.Player.DragDistanceMinMax.y;
			float force = WayneStaticFunctions.RemapClamped(min, max, 0f, 1f, m_Distance);
			updatePushDirection(force);
			displayTrajecotry();
		}

		private void updatePushDirection(float i_Force)
		{
			Vector2 drag = m_DragDireciton;
			Vector3 direction;
			if (drag.magnitude <= 0.00001f)
			{
				direction = Vector3.zero;
			}
			else
			{
				direction = new Vector3(0f, drag.y / drag.magnitude, drag.x / drag.magnitude);
			}
			float force = m_Unit.WeaponHandler.WeaponData.StatsData.ThrowForce * i_Force;
			m_PushDirection = new Vector3(-direction.x * force, -direction.y * force, -direction.z * force);
		}

		private void resetPush()
		{
			m_IsMouseDown = false;
			m_Unit.Animator.m_LookPosition = Vector3.zero;
			m_DragDireciton = Vector2.zero;
			m_PushDirection = Vector3.zero;
			m_Trajectory.HideTrajectory();
		}

		private void displayTrajecotry()
		{
			if (StorageManager.Instance.CurrentLevel == 1 && StorageManager.Instance.WeaponSelectionTutorial && StorageManager.Instance.IsFirstLaunch)
			{
				TrajectoryManager.Instance.ShowPreview(m_ActiveWeapon.Center.position);
				if (TrajectoryManager.Instance.UpdateCorrectness(m_ActiveWeapon.Center.position, m_PushDirection))
				{
					m_Trajectory.HideTrajectory();
					return;
				}
			}
			m_Trajectory.ShowTrajectory(m_ActiveWeapon.Center.position, m_PushDirection);
		}

		private void resetParameters()
		{
			m_IsMouseDown = false;
			m_InitialMousePos = Vector2.zero;
			m_CurrentMousePos = Vector2.zero;
			m_DragDireciton = Vector2.zero;
			m_Distance = 0f;
			m_PushDirection = Vector3.zero;
		}
	}
}
