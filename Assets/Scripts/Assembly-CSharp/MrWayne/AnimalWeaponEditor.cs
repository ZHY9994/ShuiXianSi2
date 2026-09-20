using System;
using System.Collections.Generic;

namespace MrWayne
{
	[Serializable]
	public class AnimalWeaponEditor : WeaponCategoryEditor
	{
		public ChickenData Chicken;

		public ChihuahuaData Chihuahua;

		public SharkData Shark;

		public override List<WeaponData> All => new List<WeaponData> { Chicken, Chihuahua, Shark };
	}
}
