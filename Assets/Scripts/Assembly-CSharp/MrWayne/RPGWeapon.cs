using UnityEngine;

namespace MrWayne
{
	public class RPGWeapon : LauncherWeapons
	{
		public override eWeaponType Type => eWeaponType.RPG;

		protected override void scatter(Vector3 i_Direction)
		{
			for (int i = 0; i < m_ActiveFragments.Count; i++)
			{
				LauncherFrag frag = m_ActiveFragments[i];
				if (frag != null)
				{
					frag.LaunchFrag(i_Direction, InitialThrowTime);
					sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Fire);
				}
			}
		}
	}
}
