using System;
using System.Collections.Generic;

namespace MrWayne
{
	[Serializable]
	public class CutterWeaponEditor : WeaponCategoryEditor
	{
		public KnifeData Knife;

		public AxeData Axe;

		public ButcherKnifeData ButcherKnife;

		public ChainSawData ChainSaw;

		public LaserSwordData LaserSword;

		public ScissorsData Scissors;

		public SwordData Sword;

		public override List<WeaponData> All => new List<WeaponData> { Knife, Axe, ButcherKnife, ChainSaw, LaserSword, Scissors, Sword };
	}
}
