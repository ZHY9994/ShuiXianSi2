using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class WeaponCollision : MonoBehaviour
	{
		[SerializeField]
		private WeaponBase m_Weapon;

		[SerializeField]
		private Collider[] m_Colliders;

		private bool m_IsActive => m_Weapon != null && (m_Weapon.m_State & ~WeaponBase.eState.Finished) != 0;

		private void OnCollisionEnter(Collision i_Other)
		{
			if (m_IsActive && !IsDamageDelayNotPassed())
			{
				onCollisonEnter(i_Other);
			}
		}

		private void OnCollisionStay(Collision i_Other)
		{
			if (m_IsActive && !IsDamageDelayNotPassed())
			{
				onCollisionStay(i_Other);
			}
		}

		private void OnCollisionExit(Collision i_Other)
		{
			if (m_IsActive && !IsDamageDelayNotPassed())
			{
				onCollisionExit(i_Other);
			}
		}

		protected virtual void onCollisonEnter(Collision i_Other)
		{
			m_Weapon.WeaponHitEnter(i_Other);
		}

		protected virtual void onCollisionStay(Collision i_Other)
		{
			return;
		}

		protected virtual void onCollisionExit(Collision i_Other)
		{
			return;
		}

		public void EnableColliders(bool i_Enable)
		{
			if (m_Colliders == null)
			{
				return;
			}
			foreach (Collider collider in m_Colliders)
			{
				if (collider != null)
				{
					collider.enabled = i_Enable;
				}
			}
		}

		public void ChangeColliderLayer(int i_Layer)
		{
			if (m_Colliders == null)
			{
				return;
			}
			foreach (Collider collider in m_Colliders)
			{
				if (collider != null)
				{
					collider.gameObject.layer = i_Layer;
				}
			}
		}

		private bool IsDamageDelayNotPassed()
		{
			if (m_Weapon == null || GameConfig.Instance == null)
			{
				return true;
			}
			return Time.time - m_Weapon.InitialThrowTime < GameConfig.Instance.GamePlay.WeaponData.General.DamageDelay;
		}

		private void SetRefs()
		{
			m_Weapon = GetComponent<WeaponBase>();
			m_Colliders = GetComponentsInChildren<Collider>();
		}
	}
}
