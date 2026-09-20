using System;
using KobGamesSDKSlim;
using RootMotion.Dynamics;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class AnimalWeapon : ThrowableWeapon
	{
		[SerializeField]
		private ExplosiveDetector m_ExplosiveDetector;

		private float m_ExplosionTimer;

		protected bool m_HasHitElement;

		private bool m_HasHitBody;

		private AnimalsWeaponData.AnimalStatsBase m_CachedWeaponParameter;

		private AnimalsWeaponData.AnimalStatsBase m_WeaponParameter
		{
			get
			{
				if (m_CachedWeaponParameter != null)
				{
					return m_CachedWeaponParameter;
				}
				WeaponData weaponData = WeaponShopManager.Instance != null ? WeaponShopManager.Instance.GetWeaponData(Type) : null;
				return weaponData != null ? weaponData.StatsData as AnimalsWeaponData.AnimalStatsBase : null;
			}
		}

		protected virtual ePoolType m_Stickable => ePoolType.CoinUI;

		protected override void Update()
		{
			if (m_State == eState.Thrown)
			{
				AnimalsWeaponData.AnimalStatsBase parameters = m_WeaponParameter;
				if (parameters == null)
				{
					return;
				}
				m_ExplosionTimer += Time.deltaTime;
				if (m_ExplosionTimer >= parameters.ExplosionDelay)
				{
					explode();
				}
			}
		}

		protected override void hitBodyPart(Collision i_Other)
		{
			if (isFriendlyBodyPart(m_HitBodyPart))
			{
				m_HitBodyPart = null;
				return;
			}
			if (!m_HasHitBody)
			{
				damageUnit(i_Other);
				m_HasHitBody = true;
				returnToQueue();
			}
		}

		protected override void hitOtherElements(Collision i_Other)
		{
			m_HasHitElement = true;
			Vector3 normal = i_Other.GetContact(0).normal;
			if (normal.z == 0f)
			{
				return;
			}
			if (normal.sqrMagnitude > 0.0000001f)
			{
				transform.rotation = Quaternion.LookRotation(normal);
			}
		}

		private void damageUnit(Collision i_Other)
		{
			if (m_HitBodyPart == null)
			{
				return;
			}
			if (m_WeaponParameter == null)
			{
				return;
			}
			Vector3 normal = i_Other.GetContact(0).normal;
			Vector3 point = i_Other.GetContact(0).point;
			m_HitBodyPart.Physics.AnimaBitePuppetFix(getHitEvent(m_WeaponParameter), false);
			Vector3 direction = normal.magnitude <= 0.00001f ? Vector3.zero : normal.normalized;
			m_HitBodyPart.PushAllBody(-direction * m_DamageProfile.PushForce, m_DamageProfile.HitDamage, eInteractableType.Weapon);
			haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
			stickObject(point, -normal);
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
		}

		private void stickObject(Vector3 i_HitPoint, Vector3 i_Normal)
		{
			if (PoolManager.Instance == null)
			{
				return;
			}
			GameObject stickableObject = PoolManager.Instance.Dequeue(m_Stickable, i_HitPoint);
			if (stickableObject == null)
			{
				return;
			}
			AnimalStickable stickable = stickableObject.GetComponent<AnimalStickable>();
			if (stickable != null)
			{
				if (i_Normal.sqrMagnitude > 0.0000001f)
				{
					stickable.transform.rotation = Quaternion.LookRotation(i_Normal);
				}
				stickable.InitializeStickable(m_HitBodyPart, i_Normal, m_DamageProfile, m_WeaponParameter);
			}
		}

		private void explode()
		{
			vFX(WeaponEffectControllers.VFXController.eVFXWeapon.Explosion, transform.position, Vector3.zero);
			shakeScreen();
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Explosion);
			bool hasTargets = m_ExplosiveDetector != null && m_ExplosiveDetector.HasTargets;
			if (hasTargets)
			{
				freezeframe();
				haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Explosion);
			}
			if (m_ExplosiveDetector != null)
				m_ExplosiveDetector.Explode(m_DamageProfile.Radius, m_DamageProfile.PushForce, m_DamageProfile.ExplosionDamage);
			actionfinished();
			returnToQueue();
		}

		protected override void initializeValues()
		{
			base.initializeValues();
			m_CachedWeaponParameter = null;
			m_HasHitElement = false;
			m_HasHitBody = false;
			m_ExplosionTimer = 0f;
			m_CachedWeaponParameter = getWeaponParameter();
			AnimalsWeaponData.AnimalStatsBase parameters = m_WeaponParameter;
			if (parameters == null)
			{
				return;
			}
			float explosionDamage = parameters.ExplosionDamage;
			if (m_Unit.UnitType == eUnityType.Player)
			{
				explosionDamage = parameters.ExplosiveDamageIncrease;
			}
			m_DamageProfile.ExplosionDamage = (int)Math.Round(explosionDamage, MidpointRounding.ToEven);
			if (m_ExplosiveDetector != null)
				m_ExplosiveDetector.Initialize(m_DamageProfile.Radius);
		}

		private AnimalsWeaponData.AnimalStatsBase getWeaponParameter()
		{
			WeaponData weaponData = WeaponShopManager.Instance != null ? WeaponShopManager.Instance.GetWeaponData(Type) : null;
			return weaponData != null ? weaponData.StatsData as AnimalsWeaponData.AnimalStatsBase : null;
		}

		private BehaviourBase.AnimatorEvent[] getHitEvent(AnimalsWeaponData.AnimalStatsBase i_Parameters)
		{
			if (i_Parameters != null && i_Parameters.HitEvent != null && i_Parameters.HitEvent.Length > 0)
			{
				return i_Parameters.HitEvent;
			}
			if (i_Parameters != null && i_Parameters.DefaultHitEvent != null && i_Parameters.DefaultHitEvent.Length > 0)
			{
				return i_Parameters.DefaultHitEvent;
			}
			if (GameConfig.Instance != null &&
				GameConfig.Instance.GamePlay != null &&
				GameConfig.Instance.GamePlay.WeaponData != null &&
				GameConfig.Instance.GamePlay.WeaponData.General != null &&
				GameConfig.Instance.GamePlay.WeaponData.General.DefaultHitEvent != null)
			{
				return GameConfig.Instance.GamePlay.WeaponData.General.DefaultHitEvent;
			}
			return null;
		}

		private bool isFriendlyBodyPart(UnitBodyPart i_BodyPart)
		{
			return i_BodyPart != null &&
				i_BodyPart.Physics != null &&
				i_BodyPart.Physics.UnitBase != null &&
				i_BodyPart.Physics.UnitBase.UnitType == m_DamageProfile.Type;
		}
	}
}
