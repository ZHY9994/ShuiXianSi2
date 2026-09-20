using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class UnitStateMachine : UnitComponent, IStateMachine
	{
		[SerializeField]
		private UnitState[] m_States;

		public UnitBase Unit => m_UnitBase;

		protected UnitState Current { get; private set; }

		public event Action<IStateMachine, IState> EventStateEnter;

		public event Action<IStateMachine, IState> EventStateExit;

		protected override void OnDisable()
		{
			base.OnDisable();
			if (Current != null)
			{
				Current.OnExit(this);
			}
			Current = null;
		}

		private void OnEnterCallback(IStateMachine i_StateMachine, IState i_State)
		{
			EventStateEnter?.Invoke(i_StateMachine, i_State);
		}

		private void OnExitCallback(IStateMachine i_StateMachine, IState i_State)
		{
			EventStateExit?.Invoke(i_StateMachine, i_State);
		}

		protected override void onUnitDeath(UnitBase i_Unit)
		{
			if (UnitManager.Instance.AreEnemyUnitsDead)
			{
				if (m_UnitBase.UnitType == eUnityType.Player)
				{
					ChangeUnitState(eUnitState.Win);
					return;
				}
				ChangeUnitState(eUnitState.Dead);
			}
			else if (UnitManager.Instance.ArePlayerUnitsDead)
			{
				if (m_UnitBase.UnitType == eUnityType.AI)
				{
					ChangeUnitState(eUnitState.Win);
				}
				else
				{
					ChangeUnitState(eUnitState.Dead);
				}
			}
		}

		private void Update()
		{
			if (Current != null && Current.IsActive)
			{
				Current.OnUpdate(this);
			}
		}

		private void FixedUpdate()
		{
			if (Current != null && Current.IsActive)
			{
				Current.OnFixedUpdate(this);
			}
		}

		private void LateUpdate()
		{
			if (Current != null && Current.IsActive)
			{
				Current.OnLateUpdate(this);
			}
		}

		public void Initialize()
		{
			if (m_UnitBase == null || m_States == null || m_States.Length == 0)
				SetRefs();
			ChangeUnitState(eUnitState.Idle);
		}

		protected override void OnEnable()
		{
			SetRefs();
			base.OnEnable();
		}

		public void StartTurn()
		{
			foreach (UnitState state in m_States)
			{
				if (state != null)
				{
					state.StartTurn();
				}
			}
		}

		public void ChangeUnitState(eUnitState i_State)
		{
			foreach (UnitState state in m_States)
			{
				if (state != null && state.StateType == i_State)
				{
					Change(state);
					return;
				}
			}
		}

		protected void Change(UnitState i_State)
		{
			if (Current != null)
			{
				Current.OnExit(this);
				Current.EventOnEnter -= OnEnterCallback;
				Current.EventOnExit -= OnExitCallback;
			}
			Current = i_State;
			if (Current != null)
			{
				Current.EventOnEnter += OnEnterCallback;
				Current.EventOnExit += OnExitCallback;
				Current.OnEnter(this);
			}
		}

		protected override void SetRefs()
		{
			m_UnitBase = GetComponentInParent<UnitBase>();
			m_States = GetComponentsInChildren<UnitState>(true);
		}
	}
}
