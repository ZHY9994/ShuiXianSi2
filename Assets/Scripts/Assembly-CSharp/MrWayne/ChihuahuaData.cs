using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class ChihuahuaData : AnimalsWeaponData
	{
		[Serializable]
		public class WeaponStats : AnimalStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Chihuahua;

		public override ePoolType PoolType => ePoolType.ChihuhuaThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
