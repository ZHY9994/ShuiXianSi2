using System;
using UnityEngine;

namespace MrWayne
{
	public class ExplosiveWeapon : MultiHitWeapons
	{
		[SerializeField]
		private ExplosiveDetector m_ExplosiveDetector;

		private float m_ExplosiveTimer;

		private bool m_IsHeadshot;

		private ExplosiveWeaponData.ExplosiveStatsBase m_WeaponParameter => GetStatsType<ExplosiveWeaponData.ExplosiveStatsBase>(Type);

		protected override void fire(Vector3 i_Velocity, Vector3 i_Torque)
		{
			base.fire(i_Velocity, i_Torque);
			m_IsHeadshot = false;
		}

		protected override void Update()
		{
			if (m_State == eState.Thrown)
			{
				m_ExplosiveTimer += Time.deltaTime;
				if (m_WeaponParameter != null && m_ExplosiveTimer >= m_WeaponParameter.ExplosionDelay)
					explode();
			}
		}

		protected override void onHitBody(Collision i_Other)
		{
			if (i_Other == null || m_GeneralParameter == null)
				return;
			float impulse = i_Other.relativeVelocity.magnitude;
			if (impulse < m_GeneralParameter.MinMaxVelocity.x)
				return;
			if (m_DamageProfile == null || m_HitBodyPart == null)
				return;
			float damage = DamageUtility.GetDamage(m_DamageProfile.HitDamage, getClampedVelocity(impulse), m_HitBodyPart.DamageablePart, m_Unit);
			ContactPoint contact = i_Other.GetContact(0);
			Vector3 normal = contact.normal;
			if (normal.sqrMagnitude > 0.0000001f)
				normal.Normalize();
			m_HitBodyPart.UnpinBodyPart(10f, normal * m_DamageProfile.PushForce, contact.point, damage, eInteractableType.Weapon);
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.HeadShot, Vector3.zero, Vector3.zero, null, m_HitBodyPart);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
			updateHit();
		}

		private void explode()
		{
			if (m_ExplosiveDetector == null || m_DamageProfile == null)
				return;
			Vector3 position = transform.position;
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.Explosion, position);
			shakeScreen();
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Explosion);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Explosion);
			if (m_ExplosiveDetector.HasTargets)
				freezeframe();
			m_ExplosiveDetector.Explode(m_DamageProfile.Radius, m_DamageProfile.PushForce, m_DamageProfile.ExplosionDamage);
			actionfinished();
			returnToQueue();
		}

		protected override void initializeValues()
		{
			base.initializeValues();
			m_IsHeadshot = false;
			m_ExplosiveTimer = 0f;
			if (m_DamageProfile == null || m_ExplosiveDetector == null)
				return;
			m_ExplosiveDetector.Initialize(m_DamageProfile.Radius);
			if (m_WeaponParameter == null || m_Unit == null)
				return;
			float damage = m_Unit.UnitType == eUnityType.Player
				? m_WeaponParameter.ExplosiveDamageIncrease
				: m_WeaponParameter.ExplosiveDamage;
			m_DamageProfile.ExplosionDamage = (int)Math.Round(damage, MidpointRounding.ToEven);
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			m_ExplosiveDetector = GetComponentInChildren<ExplosiveDetector>();
		}
	}
}
