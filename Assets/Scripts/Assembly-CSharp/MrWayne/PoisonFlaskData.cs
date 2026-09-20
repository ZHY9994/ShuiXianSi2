using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class PoisonFlaskData : SpecialWeaponData
	{
		[Serializable]
		public class WeaponStats : WeaponStatsBase
		{
			public float GasDamage;

			public float GasDuration;

			public float GasRadius;

			public float GasDamageInterval;

			public float GasDamageIncrease => GasDamage * Multiplier;
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.PosionFlask;

		public override ePoolType PoolType => ePoolType.PoisonFlaskThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;

		public override float TotalDamage => StatsData == null ? 0f : ((WeaponStats)StatsData).GasDamage * ((WeaponStats)StatsData).Multiplier * (1f + ((WeaponStats)StatsData).GasDuration);
	}
}
