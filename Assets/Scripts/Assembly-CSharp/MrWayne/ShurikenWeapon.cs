using UnityEngine;

namespace MrWayne
{
	public class ShurikenWeapon : FragmentedWeapon
	{
		public override eWeaponType Type => eWeaponType.Shuriken;

		protected override ePoolType FragmentPoolType => ePoolType.ShurikenFragments;

		protected override void fragment(Collision i_Other)
		{
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
			base.fragment(i_Other);
		}
	}
}
