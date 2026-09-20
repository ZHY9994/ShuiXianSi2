using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class TNTData : ExplosiveWeaponData
	{
		[Serializable]
		public class WeaponStats : ExplosiveStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.TNT;

		public override ePoolType PoolType => ePoolType.TNTThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
