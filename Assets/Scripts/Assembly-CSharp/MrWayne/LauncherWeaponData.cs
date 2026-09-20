using System;

namespace MrWayne
{
	public class LauncherWeaponData : WeaponData
	{
		[Serializable]
		public class LauncherStatsBase : WeaponStatsBase
		{
			public int ExplosionDamage;

			public float ExplosionRadius;

			public override float BaseRadius => ExplosionRadius;

			public override float RadiusIncrease => ExplosionRadius * Multiplier;

			public float ExplosiveDamageIncrease => ExplosionDamage * Multiplier;
		}

		public override float TotalDamage => StatsData == null ? 0f : ((LauncherStatsBase)StatsData).ExplosionDamage * ((LauncherStatsBase)StatsData).Multiplier * 0.9f;
	}
}
