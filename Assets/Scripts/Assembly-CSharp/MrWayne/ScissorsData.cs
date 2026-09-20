using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class ScissorsData : CuttingWeaponData
	{
		[Serializable]
		public class WeaponStats : CuttingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Scissors;

		public override ePoolType PoolType => ePoolType.ScissorsThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
