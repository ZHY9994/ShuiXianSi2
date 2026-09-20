namespace MrWayne
{
	public class WinState : UnitState
	{
		public override eUnitState StateType => eUnitState.Win;

		protected override void WhenEnter(IStateMachine i_StateMachine)
		{
			m_Unit.Animator.m_Animator.CrossFadeInFixedTime(StaticString.AS_Dance, 0.05f);
		}
	}
}
