using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class KnifeData : CuttingWeaponData
	{
		[Serializable]
		public class WeaponStats : CuttingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Knife;

		public override ePoolType PoolType => ePoolType.KnifeThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
