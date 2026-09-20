using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class ChickenWeapon : AnimalWeapon
	{
		private ParticleSystem m_Feathers;

		public override eWeaponType Type => eWeaponType.Chicken;

		protected override ePoolType m_Stickable => ePoolType.ChickenStickable;

		private void spawnFeathers()
		{
			if (PoolManager.Instance == null)
			{
				return;
			}
			GameObject feathersObject = PoolManager.Instance.Dequeue(ePoolType.FlyFeathers, transform.position);
			if (feathersObject == null)
			{
				return;
			}
			m_Feathers = WayneStaticFunctions.GetComponentNoAlloc<ParticleSystem>(feathersObject);
			if (m_Feathers != null)
			{
				m_Feathers.transform.SetParent(transform);
			}
		}

		protected override void fire(Vector3 i_Velocity, Vector3 i_Torque)
		{
			base.fire(i_Velocity, Vector3.zero);
			if (m_Feathers != null)
			{
				m_Feathers.Play();
			}
		}

		protected override void initializeValues()
		{
			base.initializeValues();
			spawnFeathers();
		}
	}
}
