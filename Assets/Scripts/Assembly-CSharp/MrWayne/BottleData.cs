using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class BottleData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Bottle;

		public override ePoolType PoolType => ePoolType.BottleThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
