namespace MrWayne
{
	public class AIIdleState : UnitState
	{
		public override eUnitState StateType => eUnitState.Idle;

		protected override void WhenEnter(IStateMachine i_StateMachine)
		{
			m_Unit.Animator.m_Animator.SetLayerWeight(1, 0f);
		}

		protected override void WhenUpdate(IStateMachine i_StateMachine)
		{
			loopFunction();
			if (m_IsUnitTurn)
			{
				m_UnitStateMachine.ChangeUnitState(eUnitState.Aiming);
			}
		}

		protected override void WhenExit(IStateMachine i_StateMachine)
		{
			return;
		}
	}
}
