using KobGamesSDKSlim;
using MrWayne;
using UnityEngine;

public class WoodenBox : MonoBehaviour
{
	[SerializeField]
	private Transform m_SolidModel;

	[SerializeField]
	private Rigidbody m_SolidRigidbody;

	[SerializeField]
	private BoxCollider m_SolidCollider;

	[SerializeField]
	private Transform m_FragmentsContainer;

	[SerializeField]
	private WoodenCrateFragment[] m_Fragments;

	[SerializeField]
	private Vector3[] m_DefaultPositions;

	private GamePlayVariablesEditor.ObstacleParameter.WoodenCrateParameters m_Parameters => GameConfig.Instance.GamePlay.ObstacleParameters.WoodenCrate;

	protected void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != eTags.Obstacle.ToString())
		{
			return;
		}
		Vector3 impulse = collision.impulse;
		if (impulse.magnitude > m_Parameters.MinImpulseForce)
		{
			fragment(collision.impulse);
		}
	}

	private void disableSolidBox()
	{
		m_SolidModel.gameObject.SetActive(false);
		m_SolidRigidbody.isKinematic = true;
		m_SolidCollider.enabled = false;
	}

	private void fragment()
	{
		fragment(m_SolidRigidbody.velocity);
	}

	private void fragment(Vector3 pushVelocity)
	{
		disableSolidBox();
		SoundManager.Instance.PlayGeneralSFXByType(eSFXType.WoodenBoxBreaking);
		m_FragmentsContainer.gameObject.SetActive(true);
		for (int i = 0; i < m_Fragments.Length; i++)
		{
			Vector3 solidPosition = m_SolidModel.localPosition;
			Transform fragmentTransform = m_Fragments[i].transform;
			Vector3 fragmentPosition = fragmentTransform.localPosition;
			float repulse = m_Parameters.MinMaxFragRepulse.RandomValue();
			Vector3 velocity = pushVelocity + new Vector3(
				(solidPosition.x - fragmentPosition.x) * repulse,
				(solidPosition.y - fragmentPosition.y) * repulse,
				(solidPosition.z - fragmentPosition.z) * repulse);
			Vector3 torque = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
			m_Fragments[i].LaunchFragment(velocity, torque, default(WeaponBase.DamageProfile));
		}
	}

	public void Fragment(Vector3 pushVelocity)
	{
		fragment(pushVelocity);
	}

	private void resetFragments()
	{
		for (int i = 0; i < m_Fragments.Length; i++)
		{
			m_Fragments[i].transform.localPosition = m_DefaultPositions[i];
			m_Fragments[i].transform.localEulerAngles = Vector3.zero;
		}
	}

	private void setRefs()
	{
		m_SolidModel = transform.GetChild(0);
		m_SolidRigidbody = GetComponent<Rigidbody>();
		m_SolidCollider = GetComponent<BoxCollider>();
		m_FragmentsContainer = transform.GetChild(1);
		m_Fragments = m_FragmentsContainer.GetComponentsInChildren<WoodenCrateFragment>();
		m_DefaultPositions = new Vector3[m_Fragments.Length];
		for (int i = 0; i < m_Fragments.Length; i++)
		{
			m_DefaultPositions[i] = m_Fragments[i].transform.localPosition;
		}
	}
}
