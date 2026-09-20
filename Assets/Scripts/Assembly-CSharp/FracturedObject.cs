using DG.Tweening;
using KobGamesSDKSlim;
using MrWayne;
using UnityEngine;

public class FracturedObject : MonoBehaviour
{
	private const int DamageLayer = 13;

	[SerializeField]
	private Rigidbody m_Body;

	[SerializeField]
	private Collider m_Collider;

	[SerializeField]
	private Vector3 m_OriginalPositon;

	[SerializeField]
	private Quaternion m_OriginalRotation;

	private Tween m_Tween;

	private bool m_CanDamage;

	private bool m_HasDamaged;

	private UnitBase m_Owner;

	private int m_OriginalLayer;

	private bool m_HasOriginalLayer;

	private void Awake()
	{
		cacheOriginalLayer();
	}

	private void OnDisable()
	{
		m_Tween?.Kill();
	}

	private void OnCollisionEnter(Collision i_Other)
	{
		if (!m_CanDamage || m_HasDamaged || i_Other == null)
		{
			return;
		}
		if (!i_Other.gameObject.CompareTag(eTags.BodyPart.ToString()) && !i_Other.gameObject.CompareTag(eTags.EnemyBodyPart.ToString()))
		{
			return;
		}
		UnitBodyPart bodyPart = WayneStaticFunctions.GetComponentNoAlloc<UnitBodyPart>(i_Other.gameObject);
		if (bodyPart == null || bodyPart.Physics == null || bodyPart.Physics.UnitBase == null)
		{
			return;
		}
		if (m_Owner != null && bodyPart.Physics.UnitBase.UnitType == m_Owner.UnitType)
		{
			return;
		}
		m_HasDamaged = true;
		ContactPoint contact = i_Other.GetContact(0);
		GameObject bloodSplat = PoolManager.Instance.Dequeue(ePoolType.BloodSplat, contact.point);
		if (bloodSplat != null)
		{
			bloodSplat.transform.rotation = Quaternion.LookRotation(contact.normal);
		}
		Vector3 normal = contact.normal;
		float magnitude = normal.magnitude;
		Vector3 direction = magnitude <= 0.00001f ? Vector3.zero : -normal / magnitude * GameConfig.Instance.GamePlay.ObstacleParameters.WoodenCrate.ShankPushForce;
		bodyPart.UnpinBodyPart(5f, direction, contact.point, 10f, eInteractableType.CrateShank);
	}

	public void AddForce(Vector3 i_Velocity, ForceMode i_Mode = ForceMode.Impulse)
	{
		m_Body.AddForce(i_Velocity, i_Mode);
		m_Body.AddTorque(Vector3.right * Random.Range(3, 10));
	}

	public void ExplosionForce(float i_Force, Vector3 i_Postion, float i_Radius, float i_UpWard = 0f, ForceMode i_Mode = ForceMode.Impulse)
	{
		m_Body.AddExplosionForce(i_Force, i_Postion, i_Radius, i_UpWard, i_Mode);
	}

	public void ActivateObject()
	{
		m_Body.isKinematic = false;
	}

	public void ResetObject()
	{
		cacheOriginalLayer();
		m_CanDamage = false;
		m_HasDamaged = false;
		m_Owner = null;
		gameObject.layer = m_OriginalLayer;
		m_Body.isKinematic = true;
		m_Body.velocity = Vector3.zero;
		m_Body.angularVelocity = Vector3.zero;
		transform.localPosition = m_OriginalPositon;
		transform.localRotation = m_OriginalRotation;
		transform.localScale = Vector3.one;
	}

	public void Hide(float i_Duration, Ease i_Ease)
	{
		m_Tween = transform.DOScale(Vector3.zero, i_Duration).SetEase(i_Ease);
	}

	public void EnableShardDamage(UnitBase i_Owner)
	{
		m_Owner = i_Owner;
		m_HasDamaged = false;
		m_CanDamage = true;
		gameObject.layer = DamageLayer;
	}

	public void SetRefs()
	{
		m_Body = GetComponent<Rigidbody>();
		m_Collider = GetComponent<Collider>();
		cacheOriginalLayer();
		m_OriginalPositon = transform.localPosition;
		m_OriginalRotation = transform.localRotation;
		m_Body.isKinematic = true;
	}

	private void cacheOriginalLayer()
	{
		if (m_HasOriginalLayer)
		{
			return;
		}
		m_OriginalLayer = gameObject.layer;
		m_HasOriginalLayer = true;
	}
}
