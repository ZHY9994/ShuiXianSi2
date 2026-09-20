using System;
using System.Collections.Generic;

namespace MrWayne
{
	[Serializable]
	public class ExplosiveWeaponEditor : WeaponCategoryEditor
	{
		public GrenadeData Grenade;

		public TNTData Tnt;

		public WitchBombData WitchBomb;

		public override List<WeaponData> All => new List<WeaponData> { Grenade, Tnt, WitchBomb };
	}
}
