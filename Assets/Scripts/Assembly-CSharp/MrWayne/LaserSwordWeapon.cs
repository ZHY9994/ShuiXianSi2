using UnityEngine;

namespace MrWayne
{
	public class LaserSwordWeapon : CutterWeapon
	{
		[SerializeField]
		private ParticleSystem m_FlyVFX;

		public override eWeaponType Type => eWeaponType.LaserSword;

		protected override void fire(Vector3 i_Velocity, Vector3 i_Torque)
		{
			base.fire(i_Velocity, i_Torque);
			if (m_FlyVFX != null) m_FlyVFX.Play();
		}

		protected override void actionfinished(bool i_Hide = true)
		{
			base.actionfinished(i_Hide);
			if (m_FlyVFX != null) m_FlyVFX.Stop();
		}
	}
}
