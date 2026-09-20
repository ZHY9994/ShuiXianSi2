using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class ChickenData : AnimalsWeaponData
	{
		[Serializable]
		public class WeaponStats : AnimalStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Chicken;

		public override ePoolType PoolType => ePoolType.ChickenThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
