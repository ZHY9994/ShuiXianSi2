using System;

namespace MrWayne
{
	[Serializable]
	public class CuttingWeaponData : WeaponData
	{
		[Serializable]
		public class CuttingStatsBase : WeaponStatsBase
		{
			public float Unpin;

			public bool IsStickable;

			public float StickMultiplier;
		}

		public override float TotalDamage => StatsData == null ? 0f : StatsData.BaseHitDamage * StatsData.Multiplier * 1.15f;
	}
}
