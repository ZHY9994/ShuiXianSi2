using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class ClusterGrenadeData : FragmentedWeaponData
	{
		[Serializable]
		public class WeaponStats : FragmentStatsBase
		{
			public int ExplosiveDamage;

			public float ExplosionRadius;

			public float ExplosiveDamageIncrease => ExplosiveDamage * Multiplier;
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.ClusterGrenade;

		public override ePoolType PoolType => ePoolType.ClusterGrenadeThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
