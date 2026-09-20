using UnityEngine;

namespace MrWayne
{
	public class BeatingWeapon : MultiHitWeapons
	{
		private BeatingWeaponData.BeatingStatsBase m_WeaponParameter => GetStatsType<BeatingWeaponData.BeatingStatsBase>(Type);

		protected override void onHitBody(Collision i_Other)
		{
			if (i_Other == null || m_HitBodyPart == null || m_DamageProfile == null) return;
			ContactPoint contact = i_Other.GetContact(0);
			float damage = DamageUtility.GetDamage(m_DamageProfile.HitDamage, getClampedVelocity(i_Other.relativeVelocity.magnitude), m_HitBodyPart.DamageablePart, m_Unit);
			Vector3 push = -contact.normal * m_DamageProfile.PushForce;
			if (m_WeaponParameter != null)
				m_HitBodyPart.UnpinBodyPart(m_WeaponParameter.Unpin, push, contact.point, damage, eInteractableType.Weapon);
			if (damage >= 1f)
				vFX(WeaponEffectControllers.VFXController.eVFXWeapon.HeadShot, i_HitPart:m_HitBodyPart);
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.WallHit, transform.position);
			freezeframe();
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
			updateHit();
		}

		protected override void onHitElement(Collision i_Other)
		{
			if (m_BounceCount <= 3 && i_Other != null)
			{
				updateBounceCount();
				ContactPoint contact = i_Other.GetContact(0);
				reflectWeapon(contact.point - transform.position, contact.normal);
				vFX(WeaponEffectControllers.VFXController.eVFXWeapon.WallHit, transform.position);
				sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.WallHit);
				haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
			}
		}

		private void reflectWeapon(Vector3 i_Direction, Vector3 i_Normal)
		{
			if (m_RB == null || m_WeaponParameter == null) return;
			Vector3 reflected = Vector3.Reflect(i_Direction, i_Normal).normalized * m_WeaponParameter.BounceForce;
			m_RB.AddForce(reflected, ForceMode.Impulse);
		}
	}
}
