using UnityEngine;
using KobGamesSDKSlim;

namespace MrWayne
{
	public class PoisonFlaskWeapon : ThrowableWeapon
	{
		public override eWeaponType Type => eWeaponType.PosionFlask;

		protected override void hitBodyPart(Collision i_Other)
		{
			explode(i_Other);
		}

		protected override void hitOtherElements(Collision i_Other)
		{
			explode(i_Other);
		}

		private void explode(Collision i_Other)
		{
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
			spawnPoisonGas();
			damageUnit(i_Other);
			actionfinished();
			returnToQueue();
		}

		private void damageUnit(Collision i_Other)
		{
			if (m_HitBodyPart == null || i_Other == null || m_DamageProfile == null) return;
			ContactPoint contact = i_Other.GetContact(0);
			float damage = DamageUtility.GetDamage(m_DamageProfile.HitDamage, getClampedVelocity(i_Other.relativeVelocity.magnitude), m_HitBodyPart.DamageablePart, m_Unit);
			m_HitBodyPart.PushAllBody(-contact.normal * m_DamageProfile.PushForce, damage, eInteractableType.Weapon);
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
		}

		private void spawnPoisonGas()
		{
			GameObject gasObject = PoolManager.Instance.Dequeue(ePoolType.PoisonGas, transform.position);
			if (gasObject != null)
				gasObject.GetComponent<PoisonFlaskGas>()?.InitializeGas(m_DamageProfile);
		}
	}
}
