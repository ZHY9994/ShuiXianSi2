using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class SkunkWeapon : ThrowableWeapon
	{
		[SerializeField]
		private Transform m_Gas;

		[SerializeField]
		private Animator m_SkunkAnimator;

		[SerializeField]
		private ExplosiveDetector m_ExplosiveDetector;

		[SerializeField]
		private LayerMask m_Mask;

		[SerializeField]
		private LayerMask m_RotateMask;

		[SerializeField]
		private float m_RayLength;

		[SerializeField]
		private float m_ForwardRayLength;

		[SerializeField]
		private float m_MoveSpeed = 90f;

		private bool m_FirstHit;

		private RaycastHit[] m_ForwardRay = new RaycastHit[1];

		private RaycastHit[] m_Rayhit = new RaycastHit[1];

		private float m_ExplosionTimer;

		private bool m_HasSpawnedPoison;

		public override eWeaponType Type => eWeaponType.Skunk;

		private SkunkData.WeaponStats m_WeaponParameter
		{
			get
			{
				WeaponData weaponData = WeaponShopManager.Instance != null ? WeaponShopManager.Instance.GetWeaponData(Type) : null;
				return weaponData != null ? weaponData.StatsData as SkunkData.WeaponStats : null;
			}
		}

		protected override void Update()
		{
			if (m_State == eState.Thrown)
			{
				if (!m_FirstHit)
				{
					if (m_RB != null && m_RB.velocity.sqrMagnitude > 0.0000001f)
					{
						m_RB.rotation = Quaternion.LookRotation(m_RB.velocity);
					}
					if (Time.time - InitialThrowTime > m_GeneralParameter.ActiveDuration)
					{
						actionfinished();
						returnToQueue();
					}
					return;
				}
				SkunkData.WeaponStats parameters = m_WeaponParameter;
				if (parameters == null)
				{
					return;
				}
				m_ExplosionTimer += Time.deltaTime;
				if (m_ExplosionTimer > parameters.GasDuration)
				{
					explode();
					return;
				}
			}
			if (m_FirstHit && GroundCheck())
			{
				if (m_RB != null)
				{
					Vector3 forward = transform.forward;
					m_RB.velocity = forward * m_MoveSpeed * Time.deltaTime;
				}
			}
		}

		protected override void hitBodyPart(Collision i_Other)
		{
			SetWalkTrigger();
			ChangeDirection(i_Other);
		}

		protected override void hitOtherElements(Collision i_Other)
		{
			if (i_Other == null)
			{
				return;
			}
			GameObject go = i_Other.gameObject;
			if (go == null)
			{
				return;
			}
			if (go.CompareTag("Obstacle"))
			{
				SetWalkTrigger();
				ChangeDirection(i_Other);
				return;
			}
			if (go.CompareTag("Ground"))
			{
				SetWalkTrigger();
				ChangeDirection(i_Other);
			}
		}

		private void onHit(Collision i_Other)
		{
			SetWalkTrigger();
			ChangeDirection(i_Other);
		}

		private void explode()
		{
			Transform t = transform;
			if (t == null)
			{
				return;
			}
			Vector3 position = t.position;
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.Explosion, position, Vector3.zero);
			shakeScreen();
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Explosion);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Explosion);
			if (m_ExplosiveDetector != null && m_DamageProfile != null)
			{
				SkunkData.WeaponStats parameters = m_WeaponParameter;
				float radius = parameters != null ? parameters.ExplosionRadius : 0f;
				m_ExplosiveDetector.Explode(radius, m_DamageProfile.PushForce, m_DamageProfile.ExplosionDamage);
			}
			actionfinished();
			if (m_Gas != null && m_Gas.childCount > 0)
			{
				Transform child = m_Gas.GetChild(0);
				if (child != null)
				{
					child.SetParent(null);
				}
			}
			returnToQueue();
		}

		private bool GroundCheck()
		{
			Transform t = transform;
			if (t == null)
			{
				return false;
			}
			Vector3 position = t.position;
			Vector3 down = Vector3.down;
			int mask = m_Mask.value;
			return Physics.RaycastNonAlloc(position, down, m_Rayhit, m_RayLength, mask) > 0;
		}

		private void ChangeDirection(Collision i_Other)
		{
			if (i_Other == null)
			{
				return;
			}
			ContactPoint contact = i_Other.GetContact(0);
			Vector3 normal = contact.normal;
			if (!m_FirstHit)
			{
				m_FirstHit = true;
				m_ExplosionTimer = 0f;
				spawnPoisonOnce();
				Transform t = transform;
				Quaternion rotation = t.rotation;
				Vector3 euler = rotation.eulerAngles;
				Quaternion newRotation = Quaternion.Euler(0f, euler.y, 0f);
				t.rotation = newRotation;
			}
			if (GroundCheck())
			{
				if (CheckForward())
				{
					if (m_ForwardRay != null && m_ForwardRay.Length > 0)
					{
						float nz = m_ForwardRay[0].normal.z;
						Transform t = transform;
						Vector3 dir = new Vector3(0f, 0f, nz);
						if (dir.sqrMagnitude > 0.0000001f)
						{
							t.rotation = Quaternion.LookRotation(dir);
						}
					}
				}
			}
		}

		private bool CheckForward()
		{
			Transform t = transform;
			if (t == null)
			{
				return false;
			}
			Vector3 position = t.position;
			Vector3 forward = t.forward;
			int mask = m_RotateMask.value;
			return Physics.RaycastNonAlloc(position, forward, m_ForwardRay, m_ForwardRayLength, mask) > 0;
		}

		private void SetWalkTrigger()
		{
			if (!m_FirstHit)
			{
				if (m_SkunkAnimator != null)
				{
					m_SkunkAnimator.SetTrigger(StaticString.AP_Walk);
				}
			}
		}

		private void spawnPoison()
		{
			if (m_Gas == null || PoolManager.Instance == null)
			{
				return;
			}
			Vector3 position = m_Gas.position;
			GameObject poison = PoolManager.Instance.Dequeue(ePoolType.SkunkPoison, position);
			if (poison == null)
			{
				return;
			}
			SkunkPoison skunkPoison = poison.GetComponent<SkunkPoison>();
			if (skunkPoison != null)
			{
				skunkPoison.transform.SetParent(m_Gas);
				skunkPoison.InitializePoison(m_DamageProfile);
				sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
			}
		}

		private void spawnPoisonOnce()
		{
			if (m_HasSpawnedPoison)
			{
				return;
			}
			m_HasSpawnedPoison = true;
			spawnPoison();
		}

		protected override void fire(Vector3 i_Velocity, Vector3 i_Torque)
		{
			base.fire(i_Velocity, Vector3.zero);
			if (m_SkunkAnimator != null)
			{
				m_SkunkAnimator.SetTrigger(StaticString.AP_Fly);
			}
		}

		protected override void setRBConstraints(RigidbodyConstraints i_Constraint)
		{
			base.setRBConstraints(RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX);
		}

		protected override void initializeValues()
		{
			base.initializeValues();
			m_FirstHit = false;
			m_HasSpawnedPoison = false;
			m_ExplosionTimer = 0f;
			SkunkData.WeaponStats parameters = m_WeaponParameter;
			if (parameters == null)
			{
				return;
			}
			float increase = parameters.ExplosionDamageIncrease;
			float explosionDamage = parameters.ExplosionDamage;
			if (parameters.RadiusIncrease == 0f)
			{
				explosionDamage = increase;
			}
			if (m_DamageProfile != null)
			{
				m_DamageProfile.ExplosionDamage = Mathf.RoundToInt(explosionDamage);
			}
			if (m_ExplosiveDetector != null)
			{
				m_ExplosiveDetector.Initialize(parameters.ExplosionRadius);
			}
		}
	}
}
