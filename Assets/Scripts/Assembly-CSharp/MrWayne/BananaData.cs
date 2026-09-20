using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class BananaData : FragmentedWeaponData
	{
		[Serializable]
		public class WeaponStats : FragmentStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Banana;

		public override ePoolType PoolType => ePoolType.BananaThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
