using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class CutterWeapon : MultiHitWeapons
	{
		[SerializeField]
		private Transform m_CutDirectionPointer;

		private CuttingWeaponData.CuttingStatsBase m_WeaponParameter => GetStatsType<CuttingWeaponData.CuttingStatsBase>(Type);

		protected override void onHitBody(Collision i_Other)
		{
			ContactPoint contact = i_Other.GetContact(0);
			Vector3 contactNormal = contact.normal;
			float normalMagnitude = contactNormal.magnitude;
			Vector3 normal = normalMagnitude > 0.00001f
				? new Vector3(0f, contactNormal.y / normalMagnitude, contactNormal.z / normalMagnitude)
				: Vector3.zero;
			Vector3 point = contact.point;
			Vector3 direction = point - transform.position;
			float directionMagnitude = direction.magnitude;
			direction = directionMagnitude > 0.00001f ? direction / directionMagnitude : Vector3.zero;

			bool shouldHide = true;
			if (didObjectHitWhileFacingForward(direction) && i_Other.relativeVelocity.magnitude > m_GeneralParameter.StopVelocity)
			{
				Transform parent = m_HitBodyPart.GetBone().transform;
				vFX(WeaponEffectControllers.VFXController.eVFXWeapon.BloodDripping, point, contactNormal, parent);
				stickWeapon(new Vector3(0f, -normal.y, -normal.z), parent);
				shouldHide = false;
			}

			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.HeadShot, i_HitPart: m_HitBodyPart);
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.BloodSplash, point, i_HitPart: m_HitBodyPart);
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.BloodSplat);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);

			float velocity = getClampedVelocity(i_Other.relativeVelocity.magnitude);
			float damage = !shouldHide && m_WeaponParameter.IsStickable
				? DamageUtility.GetDamageWithStick(m_DamageProfile.HitDamage, velocity, m_HitBodyPart.DamageablePart, m_WeaponParameter.StickMultiplier, m_Unit)
				: DamageUtility.GetDamage(m_DamageProfile.HitDamage, velocity, m_HitBodyPart.DamageablePart, m_Unit);
			m_HitBodyPart.UnpinBodyPart(
				m_WeaponParameter.Unpin,
				new Vector3(0f, -normal.y * m_DamageProfile.PushForce, -normal.z * m_DamageProfile.PushForce),
				point,
				damage,
				eInteractableType.Weapon);
			updateHit();

        if (i_Other.relativeVelocity.magnitude < m_GeneralParameter.StopVelocity)
            actionfinished(shouldHide);
		}

		protected override void onHitElement(Collision i_Other)
		{
			if (m_BounceCount <= 3)
			{
				updateBounceCount();
				vFX(WeaponEffectControllers.VFXController.eVFXWeapon.WallHit, transform.position);
				sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.WallHit);
				haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
			}
		}

		protected virtual bool didObjectHitWhileFacingForward(Vector3 i_Direction)
		{
			if (m_CutDirectionPointer == null)
				return false;
			return Vector3.Dot(i_Direction, m_CutDirectionPointer.forward) > m_GeneralParameter.CuttingWeaponStickValue;
		}

		protected virtual void stickWeapon(Vector3 i_Normal, Transform i_NewParent = null)
		{
			if (m_WeaponParameter == null || !m_WeaponParameter.IsStickable) return;
			disablePhysics();
            transform.SetParent(i_NewParent);
            if (i_Normal.sqrMagnitude >= 0.0000000001f)
				transform.rotation = Quaternion.LookRotation(i_Normal);
		}

		private void OnValidate()
		{
			return;
		}
	}
}
