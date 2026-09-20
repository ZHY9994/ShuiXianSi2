using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class FragmentedWeaponData : WeaponData
	{
		[Serializable]
		public class FragmentStatsBase : WeaponStatsBase
		{
			public int FragmentExplosionDamage;

			public float FragImpactForce;

			public float FragCount;

			public float FragTorque;

			public float FragExplosionRadius;

			public Vector2 MinMaxFragPushForce;

			public float[] FragSpread;

			public override float BaseRadius => FragExplosionRadius;

			public override float RadiusIncrease => FragExplosionRadius * Multiplier;

			public float FragmentExplosiveDamageIncrease => FragmentExplosionDamage * Multiplier;
		}

		public override float TotalDamage
		{
			get
			{
				FragmentStatsBase stats = StatsData as FragmentStatsBase;
				return stats == null ? 0f : stats.FragExplosionRadius * stats.Multiplier + (stats.FragCount / 3f) * stats.FragmentExplosionDamage * stats.Multiplier;
			}
		}
	}
}
