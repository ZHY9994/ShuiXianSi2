using UnityEngine;

namespace MrWayne
{
	public class HolyGrenadeWeapon : FragmentedWeapon
	{
		public override eWeaponType Type => eWeaponType.HolyGrenade;

		protected override ePoolType FragmentPoolType => ePoolType.HolyGrenadeFragment;

		protected override void fragment(Collision i_Other)
		{
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.Explosion, transform.position);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Explosion);
			base.fragment(i_Other);
		}
	}
}
