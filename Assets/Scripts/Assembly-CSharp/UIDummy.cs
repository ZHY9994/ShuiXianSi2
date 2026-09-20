using KobGamesSDKSlim;
using RootMotion.Dynamics;
using UnityEngine;

public class UIDummy : MonoBehaviour, ICollisionListener
{
	private int CollisionCount;

	[SerializeField]
	private Screen_LevelCompletedReward_V2 m_Screen;

	[SerializeField]
	private RectTransform m_CrossHair;

	[SerializeField]
	private ParticleSystem m_AaaaParticles;

	[SerializeField]
	private AudioClip m_LaunchSound;

	[SerializeField]
	private Transform m_RagdollParent;

	[SerializeField]
	private PuppetMaster m_PuppetMaster;

	[SerializeField]
	private Rigidbody[] m_RagdollBodies;

	[SerializeField]
	private Vector3[] m_InitialPositions;

	[SerializeField]
	private float yAdd = 0.15f;

	[SerializeField]
	private float throwForce = 6000f;

	private SoundManager m_SoundManager => SoundManager.Instance;

	private void SetRefs()
	{
		m_RagdollBodies = m_RagdollParent.GetComponentsInChildren<Rigidbody>(true);
		m_InitialPositions = new Vector3[m_RagdollBodies.Length];
		for (int i = 0; i < m_RagdollBodies.Length; i++)
			m_InitialPositions[i] = m_RagdollBodies[i].position;
	}

	private void OnEnable()
	{
		ResetDummy();
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (CollisionCount <= 1)
		{
			m_AaaaParticles.Stop();
			RemoveXVelocity();
			m_Screen.OnRagdollHit();
			CollisionCount++;
		}
	}

	private void ApplyForce(Vector3 velocity)
	{
		for (int i = 0; i < m_RagdollBodies.Length; i++)
			m_RagdollBodies[i].AddForce(velocity);
	}

	private void RemoveXVelocity()
	{
		for (int i = 0; i < m_RagdollBodies.Length; i++)
		{
			Rigidbody body = m_RagdollBodies[i];
			Vector3 v = body.velocity;
			v.x *= 0.15f;
			body.velocity = v;
		}
	}

	public void ResetDummy()
	{
		m_AaaaParticles.Stop();
		DG.Tweening.DOVirtual.DelayedCall(0.5f, () => CollisionCount = 0);
		m_PuppetMaster.state = RootMotion.Dynamics.PuppetMaster.State.Alive;
		for (int i = 0; i < m_RagdollBodies.Length; i++)
			m_RagdollBodies[i].position = m_InitialPositions[i];
	}

	public void ThrowToTarget()
	{
		m_AaaaParticles.Play();
		Vector3 direction = m_CrossHair.TransformPoint(m_CrossHair.position) - transform.position;
		direction = direction.sqrMagnitude <= 1e-10f ? Vector3.zero : direction.normalized;
		direction.y += yAdd;
		direction.x -= 0.05f;
		m_PuppetMaster.state = RootMotion.Dynamics.PuppetMaster.State.Dead;
		m_SoundManager.PlaySFX(m_LaunchSound);
		DG.Tweening.DOVirtual.DelayedCall(0.1f, () => ApplyForce(direction * throwForce));
	}
}
