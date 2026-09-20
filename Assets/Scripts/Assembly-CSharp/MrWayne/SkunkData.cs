using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class SkunkData : SpecialWeaponData
	{
		[Serializable]
		public class WeaponStats : WeaponStatsBase
		{
			public int ExplosionDamage;

			public float ExplosionRadius;

			public float GasRadius;

			public float GasDamage;

			public float GasDamgeInterval;

			public float GasDuration;

			public float ExplosionDamageIncrease => ExplosionDamage * Multiplier;

			public float GasDamageIncrease => GasDamage * Multiplier;
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Skunk;

		public override ePoolType PoolType => ePoolType.SkunkThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;

		public override float TotalDamage => StatsData == null ? 0f : ((WeaponStats)StatsData).BaseHitDamage * ((WeaponStats)StatsData).Multiplier + ((WeaponStats)StatsData).GasDamage * ((WeaponStats)StatsData).Multiplier * ((WeaponStats)StatsData).GasDuration * 0.8f;
	}
}
