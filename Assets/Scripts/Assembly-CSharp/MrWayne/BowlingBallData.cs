using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class BowlingBallData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.BowlingBall;

		public override ePoolType PoolType => ePoolType.BowlingBallThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
