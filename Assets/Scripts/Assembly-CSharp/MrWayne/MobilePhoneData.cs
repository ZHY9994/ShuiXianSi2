using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class MobilePhoneData : BeatingWeaponData
	{
		[Serializable]
		public class WeaponStats : BeatingStatsBase
		{
		}

		[SerializeField]
		private WeaponStats m_WeaponParameter;

		public override eWeaponType Type => eWeaponType.MobilePhone;

		public override ePoolType PoolType => ePoolType.PhoneThrowable;

		public override WeaponStatsBase StatsData => m_WeaponParameter;
	}
}
