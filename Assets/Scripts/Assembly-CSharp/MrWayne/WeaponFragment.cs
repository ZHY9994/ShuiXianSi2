using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class WeaponFragment : MonoBehaviour
	{
		protected enum eState
		{
			Launched = 0,
			Finished = 1
		}

		[SerializeField]
		private Rigidbody m_RB;

		protected eState m_State;

		protected WeaponBase.DamageProfile m_WeaponDamageProfile;

		protected float m_LaunchTime;

		protected virtual eWeaponType Type => eWeaponType.Knife;

		protected virtual ePoolType PoolType => ePoolType.HolyGrenadeFragment;

		protected FragmentedWeaponData.FragmentStatsBase m_Parameters
		{
			get
			{
				WeaponData weaponData = WeaponShopManager.Instance != null ? WeaponShopManager.Instance.GetWeaponData(Type) : null;
				return weaponData != null ? weaponData.StatsData as FragmentedWeaponData.FragmentStatsBase : null;
			}
		}

		public Rigidbody RB => m_RB;

		public virtual void LaunchFragment(Vector3 i_Velocity, Vector3 i_Torque, WeaponBase.DamageProfile i_Profile)
		{
			m_WeaponDamageProfile = i_Profile;
			m_State = eState.Launched;
			m_LaunchTime = Time.time;
			resetRB();
			addForce(i_Velocity);
		}

		protected void addForce(Vector3 i_Velocity)
		{
			if (m_RB != null)
			{
				m_RB.AddForce(i_Velocity, ForceMode.VelocityChange);
			}
		}

		protected void addTorque(Vector3 i_Torque)
		{
			if (m_RB != null)
			{
				m_RB.AddTorque(i_Torque, ForceMode.VelocityChange);
			}
		}

		private void resetRB()
		{
			if (m_RB == null)
			{
				return;
			}
			m_RB.velocity = Vector3.zero;
			m_RB.angularVelocity = Vector3.zero;
		}

		protected virtual void executeAction(Collision i_Other)
		{
			return;
		}

		protected void returnToPool()
		{
			if (PoolManager.Instance != null && gameObject.activeSelf)
			{
				PoolManager.Instance.Queue(PoolType, gameObject);
			}
		}

		protected void setState(eState i_State)
		{
			m_State = i_State;
		}

		protected virtual void SetRefs()
		{
			m_RB = GetComponent<Rigidbody>();
		}
	}
}
