using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class RockData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Rock;

		public override ePoolType PoolType => ePoolType.RockThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
