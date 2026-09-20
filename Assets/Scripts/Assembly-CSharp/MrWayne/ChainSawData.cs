using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class ChainSawData : CuttingWeaponData
	{
		[Serializable]
		public class WeaponStats : CuttingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.ChainSaw;

		public override ePoolType PoolType => ePoolType.ChainSawThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
