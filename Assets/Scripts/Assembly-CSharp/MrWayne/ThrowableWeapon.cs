using System;
using System.Collections;
using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class ThrowableWeapon : WeaponBase
	{
		[SerializeField]
		private WeaponCollision m_WeaponCollision;

		private Vector3 m_InitialThrowPosition;

		protected Rigidbody m_RB;

		protected UnitBodyPart m_HitBodyPart;

		private float m_ActiveTimer;

		protected override void OnDisable()
		{
			disablePhysics();
			setLayer(m_GeneralParameter.NoCollisionLayer);
			base.OnDisable();
		}

		public sealed override void InitializeWeapon(UnitBase i_Unit, Transform i_Spawn)
		{
			show();
			m_HitBodyPart = null;
			m_State = eState.Spawn;
			setLayer(m_GeneralParameter.NoCollisionLayer);
			disablePhysics();
			setParent(i_Spawn, true);
			setSpawnPosition();
			base.InitializeWeapon(i_Unit, i_Spawn);
		}

		protected virtual void Update()
		{
			if (m_State == eState.Thrown)
			{
				m_ActiveTimer += Time.deltaTime;
				if (m_ActiveTimer >= m_GeneralParameter.ActiveDuration)
				{
					actionfinished();
				}
			}
		}

		public sealed override void FireWeapon(Vector3 i_Velocity, Vector3 i_Torque)
		{
			m_InitialThrowPosition = transform.position;
			InitialThrowTime = Time.time;
			m_State = eState.Thrown;
			setParent(null, true);
			enablePhysics();
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Fire);
			fire(i_Velocity, i_Torque);
			StartCoroutine("resetCorotine");
			base.FireWeapon(i_Velocity, i_Torque);
		}

		protected virtual void fire(Vector3 i_Velocity, Vector3 i_Torque)
		{
			if (m_RB != null)
			{
				m_RB.AddForce(i_Velocity, ForceMode.VelocityChange);
			}
			addTorque(i_Torque);
		}

		public sealed override void WeaponHitEnter(Collision i_Other)
		{
			if (i_Other.gameObject.CompareTag("BodyPart"))
			{
				m_HitBodyPart = WayneStaticFunctions.GetComponentNoAlloc<UnitBodyPart>(i_Other.gameObject);
				if (m_HitBodyPart == null)
				{
					return;
				}
				hitBodyPart(i_Other);
			}
			if (i_Other.gameObject.CompareTag("Obstacle") || i_Other.gameObject.CompareTag("Ground"))
			{
				hitOtherElements(i_Other);
				return;
			}
			if (i_Other.gameObject.CompareTag("Throwable"))
			{
				hitOtherElements(i_Other);
			}
		}

		protected virtual void hitBodyPart(Collision i_Other)
		{
			return;
		}

		protected virtual void hitOtherElements(Collision i_Other)
		{
			return;
		}

		private void addForce(Vector3 i_Velocity)
		{
			if (m_RB != null)
			{
				m_RB.AddForce(i_Velocity, ForceMode.VelocityChange);
			}
		}

		private void addTorque(Vector3 i_Torque)
		{
			if (m_RB != null)
			{
				float torqueForce = m_WeaponData.StatsData.TorqueForce;
				m_RB.AddTorque(i_Torque * torqueForce, ForceMode.VelocityChange);
			}
		}

		protected void disablePhysics()
		{
			m_WeaponCollision.EnableColliders(false);
			disableRB();
		}

		private void enablePhysics()
		{
			m_WeaponCollision.EnableColliders(true);
			enableRB();
		}

		private void enableColliders(bool i_Value)
		{
			m_WeaponCollision.EnableColliders(i_Value);
		}

		private void disableRB()
		{
			if (m_RB != null)
			{
				Destroy(m_RB);
			}
		}

		private void enableRB()
		{
			if (m_RB == null)
			{
				m_RB = gameObject.AddComponent<Rigidbody>();
				m_RB.interpolation = RigidbodyInterpolation.Interpolate;
				m_RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
				m_RB.maxAngularVelocity = float.MaxValue;
				m_RB.angularDrag = 0f;
				m_RB.mass = m_WeaponData.StatsData.Mass;
			}
		}

		protected virtual void setRBConstraints(RigidbodyConstraints i_Constraint)
		{
			if (m_RB != null)
			{
				m_RB.constraints = i_Constraint;
			}
		}

		private void setLayer(eLayers i_Layer)
		{
			m_WeaponCollision.ChangeColliderLayer((int)i_Layer);
			gameObject.layer = (int)i_Layer;
		}

		protected override void initializeValues()
		{
			base.initializeValues();
			m_ActiveTimer = 0f;
		}

		protected void returnToQueue()
		{
			PoolManager.Instance.Queue(m_WeaponData.PoolType, gameObject);
		}

		protected override void hide()
		{
			disablePhysics();
			base.hide();
		}

		private IEnumerator resetCorotine()
		{
			while (Vector3.Distance(transform.position, m_InitialThrowPosition) < m_GeneralParameter.ThrowDistanceThreshold)
				yield return null;
			setLayer(m_GeneralParameter.CollisionLayer);
			transform.localPosition = UnityTransformExtensions.SetPositionX(transform.localPosition, 0f);
			setRBConstraints(RigidbodyConstraints.FreezePositionX);
		}

		protected float getClampedVelocity(float i_ImplulseMagnitude)
		{
			if (m_RB == null)
			{
				return 0f;
			}
			float minVelocity = m_GeneralParameter.MinMaxVelocity.x;
			float maxVelocity = m_GeneralParameter.MinMaxVelocity.y;
			if (minVelocity == maxVelocity)
			{
				return 0f;
			}
			return Mathf.Clamp01((i_ImplulseMagnitude - minVelocity) / (maxVelocity - minVelocity));
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			m_WeaponCollision = GetComponent<WeaponCollision>();
		}
	}
}
