using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class ThorHammerData : SpecialWeaponData
	{
		[Serializable]
		public class WeaponStats : WeaponStatsBase
		{
			public float AttackDelay;
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.ThorsHammer;

		public override ePoolType PoolType => ePoolType.ThorHammerThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;

		public override float TotalDamage => StatsData == null ? 0f : StatsData.BaseHitDamage * StatsData.Multiplier * 2f;
	}
}
