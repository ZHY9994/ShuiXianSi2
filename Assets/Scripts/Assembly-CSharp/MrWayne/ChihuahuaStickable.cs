using UnityEngine;

namespace MrWayne
{
	public class ChihuahuaStickable : AnimalStickable
	{
		protected override eWeaponType Type => eWeaponType.Chihuahua;

		protected override ePoolType PoolType => ePoolType.ChihuahuaStickable;

		protected override void handleHit()
		{
			if (m_HitPart == null || m_DamageProfile == null) return;
			m_HitPart.UnpinBodyPart(0f, Vector3.zero, Vector3.zero, m_DamageProfile.HitDamage, eInteractableType.Weapon);
		}
	}
}
