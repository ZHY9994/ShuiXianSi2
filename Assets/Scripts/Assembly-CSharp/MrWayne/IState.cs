using System;

namespace MrWayne
{
	public interface IState
	{
		event Action<IStateMachine, IState> EventOnEnter;

		event Action<IStateMachine, IState> EventOnExit;

		void OnEnter(IStateMachine i_StateMachine);

		void OnExit(IStateMachine i_StateMachine);

		void OnUpdate(IStateMachine i_StateMachine);

		void OnFixedUpdate(IStateMachine i_StateMachine);

		void OnLateUpdate(IStateMachine i_StateMachine);
	}
}
