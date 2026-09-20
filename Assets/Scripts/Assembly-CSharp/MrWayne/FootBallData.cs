using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class FootBallData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Football;

		public override ePoolType PoolType => ePoolType.FootballThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
