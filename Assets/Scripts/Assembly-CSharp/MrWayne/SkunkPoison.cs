using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class SkunkPoison : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem[] m_Particles;

		[SerializeField]
		private Detector m_Detector;

		private float m_IntervalTimer;

		private bool m_StartAction;

		private UnitBodyPart m_DummyTarget;

		private WeaponBase.DamageProfile m_DamageProfile;

		private SkunkData.WeaponStats m_Parameters => WeaponShopManager.Instance.GetWeaponData(eWeaponType.Skunk).StatsData as SkunkData.WeaponStats;

		public void InitializePoison(WeaponBase.DamageProfile i_DamageProfile)
		{
			CancelInvoke();
			m_IntervalTimer = 0f;
			m_DamageProfile = i_DamageProfile;
			if (m_Parameters == null)
			{
				return;
			}
			if (m_Detector != null)
			{
				m_Detector.Initialize(m_Parameters.GasRadius);
			}
			if (m_Particles != null)
			{
				foreach (ParticleSystem particle in m_Particles)
				{
					particle.Play();
				}
			}
			Invoke("returnToQueue", m_Parameters.GasDuration + 0.2f);
		}

		private void Update()
		{
			giveDamage();
		}

		private void giveDamage()
		{
			m_IntervalTimer += Time.deltaTime;
			if (m_Parameters == null)
			{
				return;
			}
			if (m_DamageProfile == null || m_IntervalTimer < m_Parameters.GasDamgeInterval)
			{
				return;
			}
			m_IntervalTimer = 0f;
			if (m_Detector == null || !m_Detector.HasTargets || m_Detector.Targets == null || m_Detector.Targets.Count < 1)
			{
				return;
			}
			foreach (UnitPhysics physics in m_Detector.Targets)
			{
				if (physics == null)
				{
					continue;
				}
				float damage = m_DamageProfile.Type != eUnityType.Player ? m_Parameters.GasDamage : m_Parameters.GasDamageIncrease;
				physics.TakeDamage(damage, eBodyPart.All, eInteractableType.Weapon);
			}
		}

		private void returnToQueue()
		{
			if (PoolManager.Instance != null && gameObject.activeSelf)
			{
				PoolManager.Instance.Queue(ePoolType.SkunkPoison, gameObject);
			}
		}
	}
}
