using RootMotion.Dynamics;

namespace MrWayne
{
	public class UnpinnedState : UnitState
	{
		public override eUnitState StateType => eUnitState.Unpinned;

		protected override void WhenEnter(IStateMachine i_StateMachine)
		{
			if (m_Puppet.state != BehaviourPuppet.State.Unpinned && m_Puppet.state != BehaviourPuppet.State.GetUp)
			{
				m_Puppet.SetState(BehaviourPuppet.State.Unpinned);
			}
		}

		protected override void WhenUpdate(IStateMachine i_StateMachine)
		{
			if (m_Puppet.state == BehaviourPuppet.State.Unpinned || m_Puppet.state == BehaviourPuppet.State.GetUp)
			{
				m_UnitStateMachine.Unit.Trajectory.HideTrajectory();
				return;
			}
			eUnitState state = m_Unit.Health.Health <= 0f ? eUnitState.Dead : (m_IsUnitTurn ? eUnitState.Aiming : eUnitState.Idle);
			m_UnitStateMachine.ChangeUnitState(state);
		}
	}
}
