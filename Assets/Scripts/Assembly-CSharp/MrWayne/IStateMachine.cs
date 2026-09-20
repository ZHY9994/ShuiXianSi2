using System;

namespace MrWayne
{
	public interface IStateMachine
	{
		event Action<IStateMachine, IState> EventStateEnter;

		event Action<IStateMachine, IState> EventStateExit;
	}
}
