using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class LaserSwordData : CuttingWeaponData
	{
		[Serializable]
		public class WeaponStats : CuttingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.LaserSword;

		public override ePoolType PoolType => ePoolType.LaserSwordThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
