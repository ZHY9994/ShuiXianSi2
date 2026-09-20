using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class ButcherKnifeData : CuttingWeaponData
	{
		[Serializable]
		public class WeaponStats : CuttingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.ButcherKnife;

		public override ePoolType PoolType => ePoolType.ButcherThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
