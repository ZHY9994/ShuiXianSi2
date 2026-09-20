using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class HolyGrenadeData : FragmentedWeaponData
	{
		[Serializable]
		public class WeaponStats : FragmentStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.HolyGrenade;

		public override ePoolType PoolType => ePoolType.HolyGrenadeThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
