using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class PipeData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Pipe;

		public override ePoolType PoolType => ePoolType.PipeThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
