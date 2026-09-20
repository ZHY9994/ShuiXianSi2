using DG.Tweening;
using MrWayne;
using RootMotion.Dynamics;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitPositioner : UnitComponent
{
	[SerializeField]
	private Rigidbody m_Body;

	[FormerlySerializedAs("originalRotation")]
	[SerializeField]
	private Quaternion m_BaseRotation;

	[SerializeField]
	private BehaviourPuppet m_Behaviour;

	[SerializeField]
	private float m_LerpTime;

	[SerializeField]
	private float m_MoveDuration;

	private RigidbodyConstraints GetUpFix;

	private RigidbodyConstraints ResetConstraints;

	private Vector3 m_LastPosition;

	private bool m_Initialized;

	private Tween m_MoveTween;

	protected override void OnDisable()
	{
		base.OnDisable();
		if (m_Behaviour != null && m_Behaviour.onRegainBalance.unityEvent != null)
		{
			m_Behaviour.onRegainBalance.unityEvent.RemoveAllListeners();
		}
		m_Initialized = false;
		m_LastPosition = Vector3.zero;
		if (m_UnitBase != null && m_UnitBase.UnitType == eUnityType.Player)
			GameTraceLog.LogError($"[PlayerPoolTrace] UnitPositioner.OnDisable id={GetInstanceID()} pos={transform.position} local={transform.localPosition}");
	}

	private void onLoseBalance()
	{
		if (m_MoveTween != null)
		{
			m_MoveTween.Kill();
		}
	}

	private void onRegainBalance()
	{
		Vector3 position = transform.position;
		m_LastPosition = new Vector3(0f, position.y, position.z);
		if (m_UnitBase != null && m_UnitBase.UnitType == eUnityType.Player)
			GameTraceLog.LogError($"[PlayerPoolTrace] UnitPositioner.onRegainBalance id={GetInstanceID()} from={position} target={m_LastPosition} root={m_UnitBase.transform.position}");
		if (m_MoveTween != null)
		{
			m_MoveTween.Kill();
		}
		m_MoveTween = transform.DOMove(m_LastPosition, m_MoveDuration).SetEase(Ease.OutSine);
	}

	public void Initialize()
	{
		if (m_UnitBase != null && m_UnitBase.UnitType == eUnityType.Player)
			GameTraceLog.LogError($"[PlayerPoolTrace] UnitPositioner.Initialize begin id={GetInstanceID()} pos={transform.position} local={transform.localPosition} initialized={m_Initialized}");
		transform.localPosition = Vector3.zero;
		m_LastPosition = transform.position;
		m_Initialized = true;
		if (m_Behaviour == null)
		{
			return;
		}
		if (m_Behaviour.onRegainBalance.unityEvent != null)
		{
			m_Behaviour.onRegainBalance.unityEvent.AddListener(onLoseBalance);
		}
		if (m_Behaviour.onRegainBalance.unityEvent != null)
		{
			m_Behaviour.onRegainBalance.unityEvent.AddListener(onRegainBalance);
		}
		if (m_UnitBase != null && m_UnitBase.UnitType == eUnityType.Player)
			GameTraceLog.LogError($"[PlayerPoolTrace] UnitPositioner.Initialize end id={GetInstanceID()} pos={transform.position} local={transform.localPosition} initialized={m_Initialized}");
	}

	private void FixedUpdate()
	{
		if (!m_Initialized)
		{
			return;
		}
		if (m_Behaviour == null)
		{
			return;
		}
		if (m_Behaviour.state == BehaviourPuppet.State.Unpinned || m_Behaviour.state == BehaviourPuppet.State.GetUp)
		{
			m_Body.constraints = GetUpFix;
			return;
		}
		m_Body.constraints = ResetConstraints;
		transform.rotation = Quaternion.Slerp(transform.rotation, m_BaseRotation, Time.deltaTime * m_LerpTime);
	}

	protected override void SetRefs()
	{
		m_UnitBase = GetComponentInParent<UnitBase>();
		m_BaseRotation = transform.rotation;
		m_Body = GetComponent<Rigidbody>();
		m_Behaviour = m_UnitBase.GetComponentInChildren<BehaviourPuppet>();
	}

	public UnitPositioner()
	{
		m_LerpTime = 10f;
		m_MoveDuration = 0.5f;
		GetUpFix = (RigidbodyConstraints)118;
		ResetConstraints = (RigidbodyConstraints)114;
	}
}
