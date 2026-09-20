using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class GrenadeData : ExplosiveWeaponData
	{
		[Serializable]
		public class WeaponStats : ExplosiveStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Grenade;

		public override ePoolType PoolType => ePoolType.GrenadeThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
