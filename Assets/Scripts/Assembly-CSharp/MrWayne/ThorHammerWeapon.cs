using UnityEngine;

namespace MrWayne
{
	public class ThorHammerWeapon : ThrowableWeapon
	{
		[SerializeField]
		private LayerMask m_Layer;

		private float m_DelayTimer;

		private UnitBase m_Target;

		private RaycastHit m_Hit;

		public override eWeaponType Type => eWeaponType.ThorsHammer;

		private ThorHammerData.WeaponStats m_WeaponParmeter => GetStatsType<ThorHammerData.WeaponStats>(Type);

		protected override void Update()
		{
			base.Update();
			if (m_State == eState.Thrown && m_Target != null)
				hammerFlight();
		}

		protected override void hitBodyPart(Collision i_Other)
		{
			if (isFriendlyBodyPart())
			{
				return;
			}
			onHit(i_Other);
		}

		protected override void hitOtherElements(Collision i_Other)
		{
			onHit(i_Other);
		}

		private void onHit(Collision i_Other)
		{
			if (transform != null)
				vFX(WeaponEffectControllers.VFXController.eVFXWeapon.LightningBlast, transform.position);
			shakeScreen();
			if (m_HitBodyPart != null && i_Other != null && m_DamageProfile != null)
			{
				float velocity = getClampedVelocity(i_Other.relativeVelocity.magnitude);
				float damage = DamageUtility.GetDamage(m_DamageProfile.HitDamage, velocity, m_HitBodyPart.DamageablePart, m_Unit);
				ContactPoint contact = i_Other.GetContact(0);
				m_HitBodyPart.PushAllBody(-contact.normal * m_DamageProfile.PushForce, damage, eInteractableType.Weapon);
				freezeframe();
			}
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.WallHit);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
			actionfinished();
		}

		private bool isFriendlyBodyPart()
		{
			if (m_Unit == null || m_HitBodyPart == null || m_HitBodyPart.Physics == null || m_HitBodyPart.Physics.UnitBase == null)
			{
				return false;
			}
			UnitBase hitUnit = m_HitBodyPart.Physics.UnitBase;
			return hitUnit == m_Unit || hitUnit.UnitType == m_Unit.UnitType;
		}

		private void hammerFlight()
		{
			m_DelayTimer += Time.deltaTime;
			if (m_WeaponParmeter == null || m_DelayTimer < m_WeaponParmeter.AttackDelay || m_Target == null || m_RB == null)
				return;
			Vector3 direction = (m_Target.Center.position + Vector3.up - transform.position).normalized;
			if (Physics.Raycast(transform.position, direction, out m_Hit, 10f, m_Layer) && m_Hit.transform.CompareTag("BodyPart"))
				m_RB.velocity = direction * 11f;
		}

		protected override void fire(Vector3 i_Velocity, Vector3 i_Torque)
		{
			m_Target = UnitManager.Instance == null ? null : UnitManager.Instance.GetUnit(m_Unit);
			if (m_RB != null)
				m_RB.transform.rotation = Quaternion.identity;
			base.fire(i_Velocity, i_Torque);
		}

		protected override void initializeValues()
		{
			base.initializeValues();
			m_DelayTimer = 0f;
		}
	}
}
