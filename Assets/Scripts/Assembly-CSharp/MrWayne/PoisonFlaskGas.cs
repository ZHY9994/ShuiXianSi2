using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class PoisonFlaskGas : MonoBehaviour
	{
		[SerializeField]
		private Detector m_Detector;

		private float m_IntervalTimer;

		private UnitBodyPart m_DummyTarget;

		private WeaponBase.DamageProfile m_DamageProfile;

		private PoisonFlaskData.WeaponStats m_Parameters => WeaponShopManager.Instance.GetWeaponData(eWeaponType.PosionFlask).StatsData as PoisonFlaskData.WeaponStats;

		public void InitializeGas(WeaponBase.DamageProfile i_DamageProfile)
		{
			CancelInvoke();
			m_DamageProfile = i_DamageProfile;
			m_IntervalTimer = 0f;
			if (m_DamageProfile == null || m_Detector == null || m_Parameters == null)
			{
				return;
			}
			m_Detector.Initialize(m_Parameters.GasRadius);
			Invoke("returnToQueue", m_Parameters.GasDuration);
		}

		private void Update()
		{
			if (m_DamageProfile == null || m_Detector == null || m_Parameters == null)
			{
				return;
			}
			m_IntervalTimer += Time.deltaTime;
			if (m_IntervalTimer >= m_Parameters.GasDamageInterval)
			{
				giveDamage();
				m_IntervalTimer = 0f;
			}
		}

		private void giveDamage()
		{
			if (!m_Detector.HasTargets)
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
				PoolManager.Instance.Queue(ePoolType.PoisonGas, gameObject);
			}
		}
	}
}
