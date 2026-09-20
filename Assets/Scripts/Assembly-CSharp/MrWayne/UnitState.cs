using KobGamesSDKSlim;
using RootMotion.Dynamics;
using UnityEngine;

namespace MrWayne
{
	public class UnitState : State
	{
		[SerializeField]
		protected UnitStateMachine m_UnitStateMachine;

		private RaycastHit[] m_Hits = new RaycastHit[3];

		public virtual eUnitState StateType => eUnitState.Idle;

		protected UnitEditor m_UnitEditor => GameConfig.Instance.GamePlay.UnitData;

		protected GeneralUnitParameters m_GeneralParameters => m_UnitEditor.GeneralUnitParameters;

		protected UnitBase m_Unit => m_UnitStateMachine.Unit;

		protected BehaviourPuppet m_Puppet => m_Unit.Puppet;

		protected bool m_IsUnitTurn => m_Unit.IsUnitTurn;

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			return;
		}

		protected void loopFunction()
		{
			raycastGround();
			updateHeadLook();
			isUnpinned();
		}

		private void raycastGround()
		{
			Vector3 position = m_Unit.Animator.transform.position;
			Vector3 origin = position + Vector3.up * m_GeneralParameters.YOffset;
			float maxDistance = m_GeneralParameters.RayMaxDistance;
			LayerMask groundLayer = m_GeneralParameters.GroundLayer;
			int hitCount = Physics.SphereCastNonAlloc(origin, 0.12f, Vector3.down, m_Hits, maxDistance, groundLayer);
			if (hitCount <= 0)
			{
				if (m_Unit != null && m_Unit.UnitType == eUnityType.Player)
					GameTraceLog.LogError($"[PlayerGroundTrace] miss frame={Time.frameCount} position={position} origin={origin} maxDistance={maxDistance} layerMask={groundLayer.value} puppet={m_Puppet.state}");
				m_UnitStateMachine.ChangeUnitState(eUnitState.Unpinned);
			}
		}

		private void updateHeadLook()
		{
			BehaviourPuppet puppet = m_Puppet;
			float layerWeight = 1f;
			if (puppet.state == BehaviourPuppet.State.Unpinned || puppet.state == BehaviourPuppet.State.GetUp)
			{
				layerWeight = 0f;
			}
			Animator animator = m_Unit.Animator.m_Animator;
			animator.SetLayerWeight(2, layerWeight);
		}

		private void isUnpinned()
		{
			BehaviourPuppet puppet = m_Puppet;
			if (puppet.state == BehaviourPuppet.State.Unpinned || puppet.state == BehaviourPuppet.State.GetUp)
			{
				m_UnitStateMachine.ChangeUnitState(eUnitState.Unpinned);
			}
		}

		public virtual void StartTurn()
		{
			return;
		}

		public void GoToState(eUnitState i_State)
		{
			m_UnitStateMachine.ChangeUnitState(i_State);
		}

		protected override void SetRefs()
		{
			m_UnitStateMachine = GetComponentInParent<UnitStateMachine>();
		}

	}
}
