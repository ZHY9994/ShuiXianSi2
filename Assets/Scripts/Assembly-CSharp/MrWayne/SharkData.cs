using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class SharkData : AnimalsWeaponData
	{
		[Serializable]
		public class WeaponStats : AnimalStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.Shark;

		public override ePoolType PoolType => ePoolType.SharkThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
