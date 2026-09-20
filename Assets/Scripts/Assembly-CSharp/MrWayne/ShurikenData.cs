using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class ShurikenData : FragmentedWeaponData
	{
		[Serializable]
		public class WeaponStats : FragmentStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Shuriken;

		public override ePoolType PoolType => ePoolType.ShurikenThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
