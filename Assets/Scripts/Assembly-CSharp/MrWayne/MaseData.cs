using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class MaseData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Mase;

		public override ePoolType PoolType => ePoolType.MaseThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
