using System;
using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class ClusterGrenadeWeapon : ThrowableWeapon
	{
		[SerializeField]
		private ExplosiveDetector m_ExplosiveDetector;

		private bool m_HasHit;

		public override eWeaponType Type => eWeaponType.ClusterGrenade;

		private ePoolType FragmentPoolType => ePoolType.ClusterGrenadeFragment;

		private ClusterGrenadeData.WeaponStats m_WeaponParameter => GetStatsType<ClusterGrenadeData.WeaponStats>(Type);

		protected override void hitBodyPart(Collision i_Other)
		{
			fragment(i_Other);
		}

		protected override void hitOtherElements(Collision i_Other)
		{
			fragment(i_Other);
		}

		protected virtual void fragment(Collision i_Other)
		{
			if (m_HasHit)
			{
				return;
			}
			m_HasHit = true;
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.Explosion, transform.position, Vector3.zero);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Explosion);
			shakeScreen();
			freezeframe();
			damageUnit(i_Other);
			scatterFragments(i_Other);
			actionfinished();
			returnToQueue();
		}

		private void damageUnit(Collision i_Other)
		{
			if (m_ExplosiveDetector == null || m_WeaponParameter == null || !m_ExplosiveDetector.HasTargets)
			{
				return;
			}
			freezeframe();
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Explosion);
			float explosionDamage = m_WeaponParameter.ExplosiveDamage;
			if (m_Unit.UnitType == eUnityType.Player)
			{
				explosionDamage = m_WeaponParameter.ExplosiveDamageIncrease;
			}
			m_ExplosiveDetector.Explode(m_WeaponParameter.ExplosionRadius, m_DamageProfile.PushForce, (int)Math.Round(explosionDamage, MidpointRounding.ToEven));
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
		}

		private void scatterFragments(Collision i_Other)
		{
			if (i_Other == null || m_WeaponParameter == null || PoolManager.Instance == null)
			{
				return;
			}
			Vector3 normal = i_Other.GetContact(0).normal;
			int fragmentCount = Mathf.Max(0, Mathf.RoundToInt(m_WeaponParameter.FragCount));
			int spreadCount = m_WeaponParameter.FragSpread == null ? 0 : m_WeaponParameter.FragSpread.Length;
			for (int i = 0; i < fragmentCount; i++)
			{
				GameObject fragmentObject = PoolManager.Instance.Dequeue(FragmentPoolType, transform.position);
				if (fragmentObject == null)
				{
					continue;
				}
				WeaponFragment fragment = WayneStaticFunctions.GetComponentNoAlloc<WeaponFragment>(fragmentObject);
				if (fragment == null)
				{
					continue;
				}
				float spread = i < spreadCount ? m_WeaponParameter.FragSpread[i] : 0f;
				Vector3 randomSpread = getRandomSpread(normal, spread);
				Vector3 direction = randomSpread.magnitude <= 0.00001f ? Vector3.zero : randomSpread.normalized;
				float force = UnityEngine.Random.Range(m_WeaponParameter.MinMaxFragPushForce.x, m_WeaponParameter.MinMaxFragPushForce.y);
				fragment.LaunchFragment(direction * force, Vector3.right * m_WeaponParameter.FragTorque, m_DamageProfile);
			}
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Explosion);
		}

		private Vector3 getRandomSpread(Vector3 i_Normal, float i_SpreadValue)
		{
			if (i_Normal.y != 0f)
			{
				return UnityTransformExtensions.SetPositionZ(i_Normal, UnityEngine.Random.Range(-1f, 1f));
			}
			if (i_Normal.z != 0f)
			{
				return UnityTransformExtensions.SetPositionY(i_Normal, i_SpreadValue);
			}
			return i_Normal;
		}

		protected override void initializeValues()
		{
			base.initializeValues();
			m_HasHit = false;
			if (m_WeaponParameter == null || m_DamageProfile == null || m_ExplosiveDetector == null || m_Unit == null)
			{
				return;
			}
			m_ExplosiveDetector.Initialize(m_WeaponParameter.ExplosionRadius);
			float explosionDamage = m_Unit.UnitType == eUnityType.Player ? m_WeaponParameter.FragmentExplosiveDamageIncrease : m_WeaponParameter.FragmentExplosionDamage;
			m_DamageProfile.ExplosionDamage = (int)Math.Round(explosionDamage, MidpointRounding.ToEven);
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			m_ExplosiveDetector = GetComponentInChildren<ExplosiveDetector>();
		}
	}
}
