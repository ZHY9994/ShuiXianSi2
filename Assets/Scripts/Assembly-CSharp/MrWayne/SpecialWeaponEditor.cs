using System;
using System.Collections.Generic;

namespace MrWayne
{
	[Serializable]
	public class SpecialWeaponEditor : WeaponCategoryEditor
	{
		public ThorHammerData ThorHammer;

		public PoisonFlaskData PoisonFlask;

		public SkunkData Skunk;

		public DummyWeaponData DummyWeapon;

		public override List<WeaponData> All => new List<WeaponData> { ThorHammer, PoisonFlask, Skunk, DummyWeapon };
	}
}
