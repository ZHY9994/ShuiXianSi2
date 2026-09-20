using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class DummyWeaponData : WeaponData
	{
		[SerializeField]
		private WeaponStatsBase m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.None;

		public override ePoolType PoolType => ePoolType.CoinUI;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
