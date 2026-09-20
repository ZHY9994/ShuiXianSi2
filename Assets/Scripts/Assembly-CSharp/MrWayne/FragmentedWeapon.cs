using System;
using System.Collections.Generic;
using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class FragmentedWeapon : ThrowableWeapon
	{
		private bool m_HasHit;

		protected List<WeaponFragment> m_Fragments = new List<WeaponFragment>();

		private FragmentedWeaponData.FragmentStatsBase m_WeaponParameter => WeaponShopManager.Instance.GetWeaponData(Type).StatsData as FragmentedWeaponData.FragmentStatsBase;

		protected virtual ePoolType FragmentPoolType => ePoolType.Explosions;

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
			damageUnit(i_Other);
			shakeScreen();
			scatterFragments(i_Other);
			freezeframe();
			actionfinished();
			returnToQueue();
		}

		protected void damageUnit(Collision i_Other)
		{
			if (m_HitBodyPart == null)
			{
				return;
			}
			float clampedVelocity = getClampedVelocity(i_Other.relativeVelocity.magnitude);
			float damage = DamageUtility.GetDamage(m_DamageProfile.HitDamage, clampedVelocity, m_HitBodyPart.DamageablePart, m_Unit);
			bool isPlayer = m_HitBodyPart.Physics.UnitType == eUnityType.Player;
			bool shouldApplyDirectDamage = Type == eWeaponType.Shuriken || (!isPlayer && Type == eWeaponType.HolyGrenade);
			float pushDamage = shouldApplyDirectDamage ? damage : 0f;
			Vector3 normal = i_Other.GetContact(0).normal;
			m_HitBodyPart.PushAllBody(-normal * m_DamageProfile.PushForce, pushDamage, eInteractableType.Weapon);
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
		}

		protected virtual void scatterFragments(Collision i_Other)
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
			if (m_WeaponParameter == null || m_Unit == null)
			{
				m_Fragments.Clear();
				m_HasHit = false;
				return;
			}
			float explosionDamage = m_WeaponParameter.FragmentExplosionDamage;
			if (m_Unit.UnitType == eUnityType.Player)
			{
				explosionDamage = m_WeaponParameter.FragmentExplosiveDamageIncrease;
			}
			m_DamageProfile.ExplosionDamage = (int)Math.Round(explosionDamage, MidpointRounding.ToEven);
			m_Fragments.Clear();
			m_HasHit = false;
		}
	}
}
