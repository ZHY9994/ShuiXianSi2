using UnityEngine;

namespace MrWayne
{
	public class HomingMissileWeapon : LauncherWeapons
	{
		public override eWeaponType Type => eWeaponType.HomingMissle;

		private HomingMissileData.WeaponStats m_HomingParameter => GetStatsType<HomingMissileData.WeaponStats>(Type);

		protected override void scatter(Vector3 i_Direction)
		{
			for (int i = 0; i < m_ActiveFragments.Count; i++)
			{
				HomingMissileData.WeaponStats parameters = m_HomingParameter;
				if (parameters != null)
				{
					float spread = parameters.FragSpread != null && i < parameters.FragSpread.Length
						? parameters.FragSpread[i]
						: 0f;
					Quaternion rot = Quaternion.AngleAxis(spread, Vector3.right);
					Vector3 dir = rot * i_Direction;
					LauncherFrag frag = m_ActiveFragments[i];
					if (frag != null)
					{
						frag.LaunchFrag(dir, InitialThrowTime);
						sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Fire);
					}
				}
			}
		}
	}
}
