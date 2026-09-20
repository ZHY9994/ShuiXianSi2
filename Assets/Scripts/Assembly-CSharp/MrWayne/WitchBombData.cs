using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class WitchBombData : ExplosiveWeaponData
	{
		[Serializable]
		public class WeaponStats : ExplosiveStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.WitchBomb;

		public override ePoolType PoolType => ePoolType.WitchBombThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
