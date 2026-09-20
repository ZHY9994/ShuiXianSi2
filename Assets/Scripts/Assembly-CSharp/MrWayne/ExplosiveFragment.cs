using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class ExplosiveFragment : WeaponFragment
	{
		[SerializeField]
		protected ExplosiveDetector m_Detector;

		[SerializeField]
		private ePoolType m_ExplosionVFX;

		public override void LaunchFragment(Vector3 i_Velocity, Vector3 i_Torque, WeaponBase.DamageProfile i_Profile)
		{
			base.LaunchFragment(i_Velocity, i_Torque, i_Profile);
			if (m_Detector != null && i_Profile != null)
				m_Detector.Initialize(i_Profile.Radius);
			addTorque(i_Torque);
		}

		private void OnCollisionEnter(Collision i_Other)
		{
			if (m_State == eState.Finished || Time.time - m_LaunchTime < 0.1f)
			{
				return;
			}
			if (i_Other.gameObject.CompareTag("BodyPart") || i_Other.gameObject.CompareTag("EnemyBodyPart") || i_Other.gameObject.CompareTag("Ground") || i_Other.gameObject.CompareTag("Obstacle"))
			{
				executeAction(i_Other);
				m_State = eState.Finished;
				returnToPool();
			}
		}

		protected override void executeAction(Collision i_Other)
		{
			playExplosion();
			SoundManager.Instance.PlayGeneralSFXByType(eSFXType.SmallExplosion);
			CameraShake.Instance.PlayCamShake();
			Managers.Instance.HapticManager.Haptic(GameConfig.Instance.Haptics.WeaponExplosion);
			FragmentedWeaponData.FragmentStatsBase parameters = m_Parameters;
			if (m_Detector == null || m_WeaponDamageProfile == null || parameters == null)
				return;
			m_Detector.Explode(m_WeaponDamageProfile.Radius, parameters.FragImpactForce, m_WeaponDamageProfile.ExplosionDamage);
		}

		private void playExplosion()
		{
			PoolManager.Instance.Dequeue(m_ExplosionVFX, transform.position);
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			m_Detector = GetComponentInChildren<ExplosiveDetector>();
		}
	}
}
