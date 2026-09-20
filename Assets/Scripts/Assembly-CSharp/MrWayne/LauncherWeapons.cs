using System;
using System.Collections.Generic;
using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class LauncherWeapons : WeaponBase
	{
		[SerializeField]
		protected Transform[] m_Points;

		[SerializeField]
		private Collider m_Collider;

		protected List<LauncherFrag> m_ActiveFragments = new List<LauncherFrag>();

		private Rigidbody m_RB;

		private LauncherWeaponData.LauncherStatsBase m_LauncherParameter => GetStatsType<LauncherWeaponData.LauncherStatsBase>(Type);


		private void OnEnable()
		{
			LauncherFrag.OnFragHit += onFragmentHit;
		}

		protected override void OnDisable()
		{
			LauncherFrag.OnFragHit -= onFragmentHit;
			disablePhysics();
			CancelInvoke();
		}

		private void onFragmentHit(LauncherFrag i_Fragment)
		{
			if (i_Fragment == null || !m_ActiveFragments.Contains(i_Fragment))
			{
				return;
			}
			if (m_State != eState.Finished)
			{
				for (int i = 0; i < m_ActiveFragments.Count; i++)
				{
					LauncherFrag activeFragment = m_ActiveFragments[i];
					if (activeFragment != null && activeFragment != i_Fragment)
					{
						activeFragment.CancelFrag();
					}
				}
				m_ActiveFragments.Clear();
				actionfinished();
			}
		}

		private void setRocketsFragments()
		{
			if (m_Points == null || PoolManager.Instance == null)
			{
				return;
			}
			for (int i = 0; i < m_Points.Length; i++)
			{
				if (m_Points[i] == null)
				{
					continue;
				}
				GameObject frag = PoolManager.Instance.Dequeue(ePoolType.LauncherFragment);
				if (frag == null)
				{
					continue;
				}
				LauncherFrag launcherFrag = frag.GetComponent<LauncherFrag>();
				if (launcherFrag != null)
				{
					launcherFrag.Initialize(m_Points[i], m_DamageProfile);
					m_ActiveFragments.Add(launcherFrag);
				}
			}
		}

		protected virtual void scatter(Vector3 i_Direction)
		{
			return;
		}

		private void enablePhysics()
		{
			if (m_Collider == null)
			{
				return;
			}
			m_Collider.enabled = true;
			if (m_RB == null)
			{
				m_RB = gameObject.AddComponent<Rigidbody>();
			}
			m_RB.AddForce(-transform.forward * UnityEngine.Random.Range(5, 10), ForceMode.Impulse);
			m_RB.AddTorque(transform.forward * UnityEngine.Random.Range(5, 10), ForceMode.Impulse);
		}

		private void disablePhysics()
		{
			if (m_Collider != null)
			{
				m_Collider.enabled = false;
			}
			if (m_RB != null)
			{
				Destroy(m_RB);
			}
		}

		private void vfxspark()
		{
			if (m_Points == null || m_Points.Length == 0 || m_Points[0] == null || PoolManager.Instance == null)
			{
				return;
			}
			GameObject fx = PoolManager.Instance.Dequeue(ePoolType.RocketFX, m_Points[0].position);
			if (fx == null)
			{
				return;
			}
			ParticleSystem ps = fx.GetComponent<ParticleSystem>();
			if (ps != null)
			{
				ps.transform.rotation = Quaternion.LookRotation(-transform.right);
				ps.Play();
			}
		}

		public override void InitializeWeapon(UnitBase i_Unit, Transform i_Spawn)
		{
			m_ActiveFragments.Clear();
			show();
			disablePhysics();
			m_State = eState.Spawn;
			setParent(i_Spawn, true);
			setSpawnPosition();
			base.InitializeWeapon(i_Unit, i_Spawn);
			LauncherWeaponData.LauncherStatsBase parameters = m_LauncherParameter;
			if (parameters != null)
			{
				float explosionDamage = parameters.ExplosionDamage;
				if (parameters.RadiusIncrease == 0f)
				{
					explosionDamage = parameters.ExplosiveDamageIncrease;
				}
			m_DamageProfile.ExplosionDamage = (int)Math.Round(explosionDamage, MidpointRounding.ToEven);
			}
			setRocketsFragments();
		}

		public override void FireWeapon(Vector3 i_Velocity, Vector3 i_Torque)
		{
			InitialThrowTime = Time.time;
			m_State = eState.Thrown;
			setParent(null, false);
			vfxspark();
			scatter(i_Velocity);
			enablePhysics();
			base.FireWeapon(i_Velocity, i_Torque);
		}

		private void fire(Vector3 i_Velocity, Vector3 i_Torque)
		{
			vfxspark();
			scatter(i_Velocity);
		}

		protected override void hide()
		{
			disablePhysics();
			base.hide();
		}
	}
}
