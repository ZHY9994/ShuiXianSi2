using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class MolotovData : FragmentedWeaponData
	{
		[Serializable]
		public class WeaponStats : FragmentStatsBase
		{
			public int ExplosionDamage;

			public float ExplosionRadius;

			public float MolotovDuration;

			public float MolotovDamageInterval;

			public float MolotovDamage;

			public float ExplosiveDamageIncrease => ExplosionDamage * Multiplier;

			public float MolotovDamageIncrease => MolotovDamage * Multiplier;
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Molotov;

		public override ePoolType PoolType => ePoolType.MolotovThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
