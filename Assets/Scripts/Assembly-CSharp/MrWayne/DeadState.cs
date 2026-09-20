namespace MrWayne
{
	public class DeadState : UnitState
	{
		public override eUnitState StateType => eUnitState.Dead;

		protected override void WhenEnter(IStateMachine i_StateMachine)
		{
			m_UnitStateMachine.Unit.Trajectory.HideTrajectory();
			m_Puppet.puppetMaster.Kill();
		}
	}
}
