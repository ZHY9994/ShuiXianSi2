using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class HammerData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Hammer;

		public override ePoolType PoolType => ePoolType.HammerThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
