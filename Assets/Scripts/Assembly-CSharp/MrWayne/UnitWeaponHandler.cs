using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class UnitWeaponHandler : UnitComponent
	{
		[SerializeField]
		private Transform m_WeaponSpawnPoint;

		private eWeaponType m_WeaponType;

		private ePoolType m_PoolType;

		public WeaponBase ActiveWeapon { get; private set; }

		public eWeaponType m_DummyPlacementType { get; set; }

		public eWeaponType WeaponType => m_WeaponType;

		public WeaponData WeaponData => WeaponShopManager.Instance.GetWeaponData(WeaponType);

		public static event Action<IUnit> OnUnitWeaponThrown;

		protected override void OnEnable()
		{
			ActiveWeapon = null;
			base.OnEnable();
		}

		public virtual void InitializeWeapon(eWeaponType i_Type = eWeaponType.Grenade, Action<eWeaponType> i_SpawnCallback = null)
		{
			if (ActiveWeapon == null)
			{
				setupWeapon(i_Type);
				spawnWeapon();
				i_SpawnCallback?.Invoke(i_Type);
			}
		}

		private void setupWeapon(eWeaponType i_WeaponType)
		{
			m_WeaponType = i_WeaponType;
			m_PoolType = WeaponData.PoolType;
		}

		private void spawnWeapon()
		{
        GameObject weaponObject = PoolManager.Instance.Dequeue(
            m_PoolType,
            m_WeaponSpawnPoint.position,
            null,
            null,
            null,
            true);
			ActiveWeapon = weaponObject.GetComponent<WeaponBase>();
			if (ActiveWeapon != null)
			{
				ActiveWeapon.InitializeWeapon(m_UnitBase, m_WeaponSpawnPoint);
			}
		}

		public void FireWeapon(Vector3 i_PushDirection, Vector3 i_TorqueDirection, Action<eWeaponType> i_Callback = null)
		{
			WeaponBase weapon = ActiveWeapon;
			if (!weapon.gameObject.activeSelf)
			{
				weapon.gameObject.SetActive(true);
			}
			weapon.FireWeapon(i_PushDirection, i_TorqueDirection);
			ActiveWeapon = null;
			i_Callback?.Invoke(m_WeaponType);
			OnUnitWeaponThrown?.Invoke(m_UnitBase);
		}

		private void clearWeapon()
		{
			ActiveWeapon = null;
		}

		private void returnWeapon()
		{
			if (ActiveWeapon != null)
			{
				PoolManager.Instance.Queue(ActiveWeapon.PoolType, ActiveWeapon.gameObject);
				ActiveWeapon = null;
			}
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			m_WeaponSpawnPoint = KobGamesSDKSlim.ExtensionMethods.FindDeepChild<Transform>(transform, "ThrowableSpawn");
		}
	}
}
