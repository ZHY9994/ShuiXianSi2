using System;
using RootMotion.Dynamics;

namespace MrWayne
{
	[Serializable]
	public class AnimalsWeaponData : WeaponData
	{
		[Serializable]
		public class AnimalStatsBase : WeaponStatsBase
		{
			public float StickableHitDelay;

			public float StickableMaxHit;

			public int ExplosionDamage;

			public float ExplosionBaseRadius;

			public float ExplosionDelay;

			public BehaviourBase.AnimatorEvent[] HitEvent;

			public override float BaseRadius => ExplosionBaseRadius;

			public override float RadiusIncrease => ExplosionBaseRadius * Multiplier;

		public float ExplosiveDamageIncrease => ExplosionDamage * Multiplier;

		public int StickableHitCount;

		public BehaviourBase.AnimatorEvent[] DefaultHitEvent;
	}

		public override float TotalDamage
		{
			get
			{
				AnimalStatsBase stats = StatsData as AnimalStatsBase;
				return stats == null ? 0f : stats.ExplosionDamage * stats.Multiplier * 0.7f + stats.ExplosionBaseRadius * (stats.BaseHitDamage * stats.Multiplier);
			}
		}
	}
}
