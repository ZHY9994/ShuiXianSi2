using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class BallWithSpikesData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.BallWithSpikes;

		public override ePoolType PoolType => ePoolType.SpikesThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
