using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class MolotovFragment : WeaponFragment
	{
		[SerializeField]
		private Detector m_Detector;

		[SerializeField]
		private ParticleSystem m_FireVFX;

		private float m_Timer;

		private float m_IntervalTimer;

		protected override eWeaponType Type => eWeaponType.Molotov;

		protected override ePoolType PoolType => ePoolType.MolotovFragment;

		private MolotovData.WeaponStats m_MolotovParameter
		{
			get
			{
				WeaponData weaponData = WeaponShopManager.Instance != null ? WeaponShopManager.Instance.GetWeaponData(Type) : null;
				return weaponData != null ? weaponData.StatsData as MolotovData.WeaponStats : null;
			}
		}

		public override void LaunchFragment(Vector3 i_Velocity, Vector3 i_Torque, WeaponBase.DamageProfile i_Profile)
		{
			m_Timer = 0f;
			m_IntervalTimer = 0f;
			playVfx();
			base.LaunchFragment(i_Velocity, i_Torque, i_Profile);
			if (m_Detector != null && i_Profile != null)
				m_Detector.Initialize(i_Profile.Radius);
		}

		private void Update()
		{
			if (m_State == eState.Finished)
			{
				return;
			}
			MolotovData.WeaponStats parameters = m_MolotovParameter;
			if (parameters == null)
			{
				stopVFX();
				returnToPool();
				return;
			}
			m_Timer += Time.deltaTime;
			giveDamage(parameters);
			if (m_Timer >= parameters.MolotovDuration)
			{
				stopVFX();
				returnToPool();
			}
		}

		private void giveDamage(MolotovData.WeaponStats i_Parameters)
		{
			m_IntervalTimer += Time.deltaTime;
			if (i_Parameters == null || m_Detector == null || m_WeaponDamageProfile == null || m_IntervalTimer < i_Parameters.MolotovDamageInterval)
			{
				return;
			}
			m_IntervalTimer = 0f;
			if (!m_Detector.HasTargets)
			{
				return;
			}
			foreach (UnitPhysics physics in m_Detector.Targets)
			{
				if (physics == null)
					continue;
				float damage = m_WeaponDamageProfile.Type != eUnityType.Player ? i_Parameters.MolotovDamage : i_Parameters.MolotovDamageIncrease;
				physics.TakeDamage(damage, eBodyPart.All, eInteractableType.Weapon);
			}
		}

		private void stopVFX()
		{
			if (m_FireVFX != null)
				m_FireVFX.Stop();
		}

		private void playVfx()
		{
			if (m_FireVFX != null)
				m_FireVFX.Play(true);
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			m_Detector = GetComponentInChildren<Detector>();
			m_FireVFX = GetComponentInChildren<ParticleSystem>();
		}
	}
}
