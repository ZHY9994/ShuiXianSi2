using RootMotion.Dynamics;
using UnityEngine;

namespace MrWayne
{
	public class UnitBodyPart : MonoBehaviour
	{
		public eBodyPart BodyPart;

		public eDamageablePart DamageablePart;

		[SerializeField]
		private UnitPhysics m_Physics;

		[SerializeField]
		private MuscleCollisionBroadcaster m_MuscleCollisions;

		[SerializeField]
		private Rigidbody m_RB;

		public Rigidbody RB => m_RB;

		public UnitPhysics Physics => m_Physics;

		public Transform Center => m_Physics.Center;

		public bool IsUnitDead => m_Physics.UnitBase.IsUnitDead;

		public void PushBodyPart(Vector3 i_Velocity)
		{
			m_RB.velocity = Vector3.zero;
			m_RB.AddForce(i_Velocity, ForceMode.Impulse);
		}

		public void PushAllBody(Vector3 i_Velocity, float i_Damage, eInteractableType i_Interactable = eInteractableType.Weapon)
		{
			m_Physics.PushAllBody(i_Velocity, i_Damage, i_Interactable);
		}

		public void UnpinBodyPart(float i_Unpin, Vector3 i_Direction, Vector3 i_HitPoint, float i_Damage, eInteractableType i_Interactable = eInteractableType.Weapon)
		{
			if (i_Unpin > 0f)
			{
				m_MuscleCollisions.Hit(i_Unpin, i_Direction, i_HitPoint);
			}
			if (m_Physics != null)
			{
				m_Physics.TakeDamage(i_Damage, BodyPart, i_Interactable);
			}
		}

		public GameObject GetBone()
		{
			return gameObject;
		}

		public void SetRefs()
		{
			m_Physics = GetComponentInParent<UnitPhysics>();
			m_MuscleCollisions = GetComponent<MuscleCollisionBroadcaster>();
			m_RB = GetComponent<Rigidbody>();
		}
	}
}
