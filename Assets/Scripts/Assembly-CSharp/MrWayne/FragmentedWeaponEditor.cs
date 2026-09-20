using System;
using System.Collections.Generic;

namespace MrWayne
{
	[Serializable]
	public class FragmentedWeaponEditor : WeaponCategoryEditor
	{
		public HolyGrenadeData HolyGrenade;

		public ShurikenData Shuriken;

		public BananaData Banana;

		public ClusterGrenadeData ClusterGrenade;

		public MolotovData Molotov;

		public override List<WeaponData> All => new List<WeaponData> { HolyGrenade, Shuriken, Banana, ClusterGrenade, Molotov };
	}
}
