using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class RPGData : LauncherWeaponData
	{
		[Serializable]
		public class WeaponStats : LauncherStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.RPG;

		public override ePoolType PoolType => ePoolType.RPGThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
