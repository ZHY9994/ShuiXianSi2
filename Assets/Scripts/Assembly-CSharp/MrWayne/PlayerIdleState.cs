using Wayne.Game.Shops;

namespace MrWayne
{
	public class PlayerIdleState : UnitState
	{
		public override eUnitState StateType => eUnitState.Idle;

		protected override void OnEnable()
		{
			WeaponSelectionManager.PlayerSetWeapon += onSetWeapon;
		}

		protected override void OnDisable()
		{
			WeaponSelectionManager.PlayerSetWeapon -= onSetWeapon;
		}

		private void onSetWeapon(WeaponItemData i_Item)
		{
			m_UnitStateMachine.Unit.WeaponHandler.m_DummyPlacementType = i_Item.WeaponType;
		}

		protected override void WhenEnter(IStateMachine i_StateMachine)
		{
			return;
		}

		protected override void WhenUpdate(IStateMachine i_StateMachine)
		{
			loopFunction();
		}

		protected override void WhenExit(IStateMachine i_StateMachine)
		{
			return;
		}

		public override void StartTurn()
		{
			m_UnitStateMachine.ChangeUnitState(eUnitState.Aiming);
		}
	}
}
