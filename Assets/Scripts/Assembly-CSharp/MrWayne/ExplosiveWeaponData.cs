using System;

namespace MrWayne
{
	[Serializable]
	public class ExplosiveWeaponData : WeaponData
	{
		[Serializable]
		public class ExplosiveStatsBase : WeaponStatsBase
		{
			public int ExplosiveDamage;

			public float ExplosionBaseRadius;

			public float ExplosionDelay;

			public override float BaseRadius => ExplosionBaseRadius;

			public override float RadiusIncrease => ExplosionBaseRadius * Multiplier;

			public float ExplosiveDamageIncrease => Multiplier * ExplosiveDamage;
		}

		public override float TotalDamage
		{
			get
			{
				ExplosiveStatsBase stats = StatsData as ExplosiveStatsBase;
				if (stats == null)
				{
					return 0f;
				}
				return stats.ExplosionBaseRadius * stats.Multiplier + stats.Multiplier * stats.ExplosiveDamage * 0.9f;
			}
		}
	}
}
