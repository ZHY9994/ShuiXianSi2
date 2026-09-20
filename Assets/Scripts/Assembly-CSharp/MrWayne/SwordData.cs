using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class SwordData : CuttingWeaponData
	{
		[Serializable]
		public class WeaponStats : CuttingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Sword;

		public override ePoolType PoolType => ePoolType.SwordThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
