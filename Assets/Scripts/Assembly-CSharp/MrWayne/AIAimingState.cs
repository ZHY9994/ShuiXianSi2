using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class AIAimingState : UnitState
	{
		private Vector3 m_Offset;

		private Vector3 m_PushDirection;

		private Vector3 m_RandomDir;

		private float m_RandomForce;

		private float m_ThrowTimer;

		private AIParameter m_Parameter => m_UnitEditor.AI;

		public override eUnitState StateType => eUnitState.Aiming;

		private WeaponBase m_ActiveWeapon => m_UnitStateMachine.Unit.WeaponHandler.ActiveWeapon;

		protected override void WhenEnter(IStateMachine i_StateMachine)
		{
			reset();
			stasAim();
			m_UnitStateMachine.Unit.WeaponHandler.InitializeWeapon(eWeaponType.Grenade);
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
			if (!m_IsUnitTurn)
			{
				m_UnitStateMachine.ChangeUnitState(eUnitState.Idle);
				return;
			}
			if (m_ActiveWeapon == null)
			{
				return;
			}
			if (!m_IsUnitTurn)
			{
				return;
			}
			m_Unit.Animator.m_Animator.SetLayerWeight(1, 1f);
			m_ThrowTimer += Time.deltaTime;
			showTrajectory();
			m_Unit.Animator.m_LookPosition = m_PushDirection;
			if (m_ThrowTimer < m_UnitEditor.AI.ThrowDuration)
			{
				return;
			}
			m_Unit.Animator.m_LookPosition = Vector3.zero;
			m_Unit.Animator.PlayUnitThrow();
			m_UnitStateMachine.Unit.Trajectory.HideTrajectory();
			m_ThrowTimer = 0f;
			m_UnitStateMachine.Unit.WeaponHandler.FireWeapon(m_PushDirection, -Vector3.right);
			m_UnitStateMachine.Unit.StopTurn();
			m_UnitStateMachine.ChangeUnitState(eUnitState.Idle);
		}

		protected override void WhenExit(IStateMachine i_StateMachine)
		{
			return;
		}

		private void setPushDirection()
		{
			m_PushDirection = m_RandomDir * m_RandomForce;
		}

		private void shouldRandomThrow()
		{
			if (StorageManager.Instance.LevelLoop <= 3)
			{
				m_RandomDir = WayneStaticFunctions.AngleToVector3(Random.Range(m_Parameter.TutorialRandomAngleRange.x, m_Parameter.TutorialRandomAngleRange.y));
				m_RandomForce = Random.Range(m_Parameter.TutorialRandomForceRange.x, m_Parameter.TutorialRandomForceRange.y);
			}
			else
			{
				m_RandomDir = WayneStaticFunctions.AngleToVector3(Random.Range(m_Parameter.RandomAngleRange.x, m_Parameter.RandomAngleRange.y));
				m_RandomForce = Random.Range(m_Parameter.RandomForceRange.x, m_Parameter.RandomForceRange.y);
			}
		}

		private void showTrajectory()
		{
			if (m_ActiveWeapon == null)
			{
				return;
			}
			m_UnitStateMachine.Unit.Trajectory.ShowTrajectory(m_ActiveWeapon.Center.position, m_PushDirection - m_Offset);
		}

		private void stasAim()
		{
			Transform playerCenter = UnitManager.Instance.AlivePlayerUnit.Center;
			Vector3 playerPosition = playerCenter.position;
			Vector3 myPosition = m_UnitStateMachine.Unit.Center.position;
			Vector3 direction = playerPosition - myPosition;
			float distance = direction.magnitude;
			Vector3 normalized;
			if (distance <= 0.00001f)
			{
				normalized = Vector3.zero;
			}
			else
			{
				normalized = direction / distance;
			}
			float distanceRatio = Mathf.Clamp01(distance / m_Parameter.MaxDistance);
			float force = Mathf.Lerp(m_Parameter.ForceMinMax.x, m_Parameter.ForceMinMax.y, distanceRatio);
			float precision = Mathf.Clamp01(m_Parameter.PrecisionCurve.Evaluate(Random.value));
			float horizontalForce = Mathf.Lerp(m_Parameter.PrecisionOffsetMinMax.x, m_Parameter.PrecisionOffsetMinMax.y, precision);
			float hForce = Mathf.Lerp(m_Parameter.HForceMinMax.x, m_Parameter.HForceMinMax.y, distanceRatio);
			float hPrecision = Mathf.Clamp01(m_Parameter.PrecisionCurve.Evaluate(Random.value));
			float verticalForce = Mathf.Lerp(m_Parameter.hPrecisionOffsetMinMax.x, m_Parameter.hPrecisionOffsetMinMax.y, hPrecision);
			m_PushDirection = normalized * (force * horizontalForce) + Vector3.up * (hForce * verticalForce);
		}

		private void reset()
		{
			m_Offset = Vector3.zero;
			m_PushDirection = Vector3.zero;
			m_RandomForce = 0f;
			m_ThrowTimer = 0f;
			m_RandomDir = Vector3.zero;
		}
	}
}
