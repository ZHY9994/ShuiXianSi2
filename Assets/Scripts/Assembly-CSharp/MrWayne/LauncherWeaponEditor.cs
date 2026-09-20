using System;
using System.Collections.Generic;

namespace MrWayne
{
	[Serializable]
	public class LauncherWeaponEditor : WeaponCategoryEditor
	{
		public RPGData RPG;

		public HomingMissileData HomingMissile;

		public override List<WeaponData> All => new List<WeaponData> { RPG, HomingMissile };
	}
}
