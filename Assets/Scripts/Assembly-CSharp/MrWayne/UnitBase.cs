using KobGamesSDKSlim;
using RootMotion.Dynamics;
using UnityEngine;

namespace MrWayne
{
	public class UnitBase : MonoBehaviour, IUnit
	{
		[SerializeField]
		private UnitAnimator m_Animator;

		[SerializeField]
		private UnitPhysics m_Physics;

		[SerializeField]
		private UnitVisuals m_Visuals;

		[SerializeField]
		private UnitHealth m_Health;

		[SerializeField]
		private UnitTrajectory m_Trajectory;

		[SerializeField]
		private UnitWeaponHandler m_WeaponHandler;

		[SerializeField]
		private UnitStateMachine m_StateMachine;

		[SerializeField]
		private UnitCollisionIgnore m_CollisionIgnore;

		[SerializeField]
		private UnitPositioner m_UnitPositioner;

		[SerializeField]
		private BehaviourPuppet m_BehaviourPuppet;

		[SerializeField]
		private Transform m_Center;

		[SerializeField]
		private GameObject m_Root;

		private int m_LastPuppetStateTraceFrame = -1;

		public virtual eUnityType UnitType => eUnityType.Player;

		public bool IsUnitDead => m_Health.Health <= 0f;

		public Color BloodColor => m_Visuals.BloodColor;

		public bool IsUnitTurn { get; private set; }

		public UnitAnimator Animator => m_Animator;

		public UnitPhysics Physics => m_Physics;

		public UnitVisuals Visuals => m_Visuals;

		public UnitHealth Health => m_Health;

		public UnitTrajectory Trajectory => m_Trajectory;

		public UnitWeaponHandler WeaponHandler => m_WeaponHandler;

		public UnitCollisionIgnore CollisionIgnore => m_CollisionIgnore;

		public BehaviourPuppet Puppet => m_BehaviourPuppet;

		public UnitStateMachine StateMachine => m_StateMachine;

		public Transform Center => m_Center;

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			poolUnit();
		}

		private void Update()
		{
			if (UnitType == eUnityType.Player && Time.frameCount % 10 == 0)
				LogPuppetState();
		}

		private void LogPuppetState()
		{
			if (m_BehaviourPuppet == null)
				return;
			PuppetMaster puppetMaster = m_BehaviourPuppet.puppetMaster;
			Transform hips = KobGamesSDKSlim.ExtensionMethods.FindDeepChild<Transform>(transform, "mixamorig:Hips");
			Rigidbody hipsBody = hips != null ? hips.GetComponent<Rigidbody>() : null;
			if (puppetMaster == null || hipsBody == null)
				return;
			if (m_LastPuppetStateTraceFrame == Time.frameCount)
				return;
			m_LastPuppetStateTraceFrame = Time.frameCount;
			Vector3 mappedVelocity = Vector3.zero;
			Vector3 mappedAngularVelocity = Vector3.zero;
			Vector3 targetVelocity = Vector3.zero;
			Vector3 positionOffset = Vector3.zero;
			float pinWeight = 0f;
			float pinWeightMlp = 0f;
			Transform targetHips = puppetMaster.targetRoot != null ? KobGamesSDKSlim.ExtensionMethods.FindDeepChild<Transform>(puppetMaster.targetRoot, "mixamorig:Hips") : null;
			if (puppetMaster.muscles != null)
			{
				foreach (Muscle muscle in puppetMaster.muscles)
				{
					if (muscle != null && muscle.rigidbody == hipsBody)
					{
						targetVelocity = muscle.targetVelocity;
						positionOffset = muscle.positionOffset;
						pinWeight = muscle.props != null ? muscle.props.pinWeight : 0f;
						pinWeightMlp = muscle.state.pinWeightMlp;
						mappedVelocity = muscle.mappedVelocity;
						mappedAngularVelocity = muscle.mappedAngularVelocity;
						break;
					}
				}
			}
			//Debug.LogError($"[PuppetState] frame={Time.frameCount} root={transform.position} puppetState={m_BehaviourPuppet.state} masterState={puppetMaster.state} mode={puppetMaster.mode} switching={puppetMaster.isSwitchingMode} hips={hips.position} targetHips={(targetHips != null ? targetHips.position.ToString() : "<null>")} targetScale={(targetHips != null ? targetHips.lossyScale.ToString() : "<null>")} kinematic={hipsBody.isKinematic} gravity={hipsBody.useGravity} constraints={hipsBody.constraints} velocity={hipsBody.velocity} angularVelocity={hipsBody.angularVelocity} targetVelocity={targetVelocity} positionOffset={positionOffset} pinWeight={pinWeight} pinWeightMlp={pinWeightMlp} mappedVelocity={mappedVelocity} mappedAngularVelocity={mappedAngularVelocity}");
		}

		public virtual void InitializeUnit(UnitSpawn i_UnitSpawn)
		{
			bool tracePlayer = UnitType == eUnityType.Player;
			if (tracePlayer)
				GameTraceLog.LogError($"[PlayerPoolTrace] Initialize begin id={GetInstanceID()} root={transform.position} parent={(transform.parent != null ? transform.parent.name : "<root>")} model={(m_Root != null ? m_Root.transform.position.ToString() : "<null>")} puppetMasterState={(m_BehaviourPuppet != null ? m_BehaviourPuppet.puppetMaster.state.ToString() : "<null>")} behaviourState={(m_BehaviourPuppet != null ? m_BehaviourPuppet.state.ToString() : "<null>")}");
			IsUnitTurn = false;
			transform.SetParent(null);
			PuppetMaster puppetMaster = m_BehaviourPuppet.puppetMaster;
			puppetMaster.Teleport(i_UnitSpawn.transform.position, i_UnitSpawn.transform.rotation, true);
			if (tracePlayer)
				GameTraceLog.LogError($"[PlayerPoolTrace] Initialize afterTeleport root={transform.position} model={(m_Root != null ? m_Root.transform.position.ToString() : "<null>")} spawn={i_UnitSpawn.transform.position}");
			puppetMaster.targetAnimator.Play("Idle");
			puppetMaster.angularLimits = false;
			m_BehaviourPuppet.canGetUp = true;
			m_BehaviourPuppet.onLoseBalance.animations = GameConfig.Instance.GamePlay.UnitData.GeneralUnitParameters.DefaultHitEvent;
			m_StateMachine.ChangeUnitState(eUnitState.Idle);
			m_Health.Initialize(i_UnitSpawn.Weight);
			m_Physics.enableBodyTags();
			m_Visuals.Initialize(i_UnitSpawn);
			m_UnitPositioner.Initialize();
			if (tracePlayer)
				GameTraceLog.LogError($"[PlayerPoolTrace] Initialize end root={transform.position} model={(m_Root != null ? m_Root.transform.position.ToString() : "<null>")} modelActive={(m_Root != null && m_Root.activeSelf)}");
		}

		public void SetUnitActive()
		{
			m_BehaviourPuppet.puppetMaster.mode = PuppetMaster.Mode.Active;
			if (UnitType == eUnityType.Player)
				GameTraceLog.LogError($"[PlayerPoolTrace] SetUnitActive id={GetInstanceID()} root={transform.position} model={(m_Root != null ? m_Root.transform.position.ToString() : "<null>")} puppetMasterState={m_BehaviourPuppet.puppetMaster.state} behaviourState={m_BehaviourPuppet.state} mode={m_BehaviourPuppet.puppetMaster.mode}");
		}

		public void StartTurn()
		{
			IsUnitTurn = true;
			m_StateMachine.StartTurn();
		}

		public void StopTurn()
		{
			IsUnitTurn = false;
		}

		public void ChangeUnitState(eUnitState i_State)
		{
			m_StateMachine.ChangeUnitState(i_State);
		}

		public virtual void UnitVisibility(bool i_Value)
		{
			m_Root.SetActive(i_Value);
		}

		private void poolUnit()
		{
			PuppetMaster puppetMaster = m_BehaviourPuppet.puppetMaster;
			puppetMaster.state = PuppetMaster.State.Alive;
			puppetMaster.mode = PuppetMaster.Mode.Kinematic;
			if (UnitType == eUnityType.Player)
				GameTraceLog.LogError($"[PlayerPoolTrace] poolUnit id={GetInstanceID()} root={transform.position} model={(m_Root != null ? m_Root.transform.position.ToString() : "<null>")} puppetState={puppetMaster.state} mode={puppetMaster.mode}");
		}

		private void SetRefs()
		{
			m_Animator = GetComponentInChildren<UnitAnimator>();
			m_Physics = GetComponent<UnitPhysics>();
			m_Visuals = GetComponent<UnitVisuals>();
			m_Health = GetComponent<UnitHealth>();
			m_Trajectory = GetComponentInChildren<UnitTrajectory>();
			m_WeaponHandler = GetComponent<UnitWeaponHandler>();
			m_StateMachine = GetComponentInChildren<UnitStateMachine>();
			m_CollisionIgnore = GetComponent<UnitCollisionIgnore>();
			m_UnitPositioner = GetComponentInChildren<UnitPositioner>();
			m_BehaviourPuppet = GetComponentInChildren<BehaviourPuppet>();
			m_Center = KobGamesSDKSlim.ExtensionMethods.FindDeepChild<Transform>(transform, "Hips");
			m_Root = KobGamesSDKSlim.ExtensionMethods.FindDeepChild<GameObject>(transform, "Model");
		}
	}
}
