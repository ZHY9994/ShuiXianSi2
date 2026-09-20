using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public abstract class State : MonoBehaviour, IState
	{
		public bool IsActive { get; private set; }

		public event Action<IStateMachine, IState> EventOnEnter;

		public event Action<IStateMachine, IState> EventOnExit;

		protected virtual void WhenEnter(IStateMachine i_StateMachine)
		{
			return;
		}

		protected virtual void WhenExit(IStateMachine i_StateMachine)
		{
			return;
		}

		protected virtual void WhenUpdate(IStateMachine i_StateMachine)
		{
			return;
		}

		protected virtual void WhenFixedUpdate(IStateMachine i_StateMachine)
		{
			return;
		}

		protected virtual void WhenLateUpdate(IStateMachine i_StateMachine)
		{
			return;
		}

		public void OnEnter(IStateMachine i_StateMachine)
		{
			IsActive = true;
			WhenEnter(i_StateMachine);
			EventOnEnter?.Invoke(i_StateMachine, this);
		}

		public void OnExit(IStateMachine i_StateMachine)
		{
			IsActive = false;
			WhenExit(i_StateMachine);
			EventOnExit?.Invoke(i_StateMachine, this);
		}

		public void OnUpdate(IStateMachine i_StateMachine)
		{
			WhenUpdate(i_StateMachine);
		}

		public void OnFixedUpdate(IStateMachine i_StateMachine)
		{
			WhenFixedUpdate(i_StateMachine);
		}

		public void OnLateUpdate(IStateMachine i_StateMachine)
		{
			WhenLateUpdate(i_StateMachine);
		}

		protected virtual void SetRefs()
		{
			return;
		}
	}
}
