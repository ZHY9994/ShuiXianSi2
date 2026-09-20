using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class HomingMissileData : LauncherWeaponData
	{
		[Serializable]
		public class WeaponStats : LauncherStatsBase
		{
			public float[] FragSpread;
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.HomingMissle;

		public override ePoolType PoolType => ePoolType.HomingMissileThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;

		public override float TotalDamage => StatsData == null ? 0f : ((WeaponStats)StatsData).ExplosionDamage * ((WeaponStats)StatsData).Multiplier * 0.9f * 4f;
	}
}
