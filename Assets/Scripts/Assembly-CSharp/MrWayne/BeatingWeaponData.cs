using System;

namespace MrWayne
{
	[Serializable]
	public class BeatingWeaponData : WeaponData
	{
		[Serializable]
		public class BeatingStatsBase : WeaponStatsBase
		{
			public float Unpin;

			public float BounceForce;
		}

		public override float TotalDamage => StatsData == null ? 0f : StatsData.BaseHitDamage * StatsData.Multiplier;
	}
}
