using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class AxeData : CuttingWeaponData
	{
		[Serializable]
		public class WeaponStats : CuttingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Axe;

		public override ePoolType PoolType => ePoolType.AxeThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
