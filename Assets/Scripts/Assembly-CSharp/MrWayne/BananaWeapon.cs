using UnityEngine;

namespace MrWayne
{
	public class BananaWeapon : FragmentedWeapon
	{
		public override eWeaponType Type => eWeaponType.Banana;

		protected override ePoolType FragmentPoolType => ePoolType.BananaFragment;

		protected override void fragment(Collision i_Other)
		{
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.Explosion, transform.position);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Explosion);
			base.fragment(i_Other);
		}
	}
}
