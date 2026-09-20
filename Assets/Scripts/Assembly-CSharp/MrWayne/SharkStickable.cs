using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class SharkStickable : AnimalStickable
	{
		protected override eWeaponType Type => eWeaponType.Shark;

		protected override ePoolType PoolType => ePoolType.SharkStickable;

		public override void InitializeStickable(UnitBodyPart i_Part, Vector3 i_Direction, WeaponBase.DamageProfile i_Profile, AnimalsWeaponData.AnimalStatsBase i_Parameters)
		{
			base.InitializeStickable(i_Part, i_Direction, i_Profile, i_Parameters);
			if (i_Part == null || m_BloodSpawnPos == null || PoolManager.Instance == null)
				return;
			Vector3 direction = UnityTransformExtensions.SetPositionX(i_Direction, 0.5f);
			GameObject bloodDrip = PoolManager.Instance.Dequeue(ePoolType.BloodDrip_02, m_BloodSpawnPos.position, Quaternion.LookRotation(-direction));
			if (bloodDrip != null)
				bloodDrip.transform.SetParent(i_Part.transform);
		}
	}
}
