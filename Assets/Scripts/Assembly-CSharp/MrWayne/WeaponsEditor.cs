using System;
using System.Collections.Generic;

namespace MrWayne
{
	[Serializable]
	public class WeaponsEditor
	{
		public CutterWeaponEditor CutterWeapons;

		public BeatingWeaponEditor BeatingWeapons;

		public ExplosiveWeaponEditor ExplosiveWeapons;

		public FragmentedWeaponEditor FragmentedWeapons;

		public AnimalWeaponEditor AnimalWeapons;

		public LauncherWeaponEditor LauncherWeapons;

		public SpecialWeaponEditor SpecialWaapons;

		public Dictionary<eWeaponType, WeaponData> WeaponDictionary;

		public void Initialize()
		{
			WeaponDictionary = new Dictionary<eWeaponType, WeaponData>();
			AddWeaponsToDictionary(CutterWeapons?.All);
			AddWeaponsToDictionary(BeatingWeapons?.All);
			AddWeaponsToDictionary(ExplosiveWeapons?.All);
			AddWeaponsToDictionary(FragmentedWeapons?.All);
			AddWeaponsToDictionary(AnimalWeapons?.All);
			AddWeaponsToDictionary(LauncherWeapons?.All);
			AddWeaponsToDictionary(SpecialWaapons?.All);
		}

		private void AddWeaponsToDictionary(List<WeaponData> i_WeaponList)
		{
			if (i_WeaponList == null) return;
			foreach (WeaponData weapon in i_WeaponList)
			{
				if (weapon != null)
					WeaponDictionary.Add(weapon.Type, weapon);
			}
		}

		public void SetUpNames()
		{
			if (WeaponDictionary == null) return;
			foreach (WeaponData weapon in WeaponDictionary.Values)
			{
				if (weapon?.StatsData != null && string.IsNullOrEmpty(weapon.StatsData.Name))
					weapon.StatsData.Name = weapon.Type.ToString();
			}
		}
	}
}
