using UnityEngine;

namespace MrWayne
{
	public class AxeWeapon : CutterWeapon
	{
		public override eWeaponType Type => eWeaponType.Axe;

		protected override void stickWeapon(Vector3 i_Normal, Transform i_NewParent = null)
		{
			base.stickWeapon(Vector3.zero, i_NewParent);
		}
	}
}
