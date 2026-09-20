using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class AnimalStickable : MonoBehaviour
	{
		[SerializeField]
		protected Animator m_Animator;

		[SerializeField]
		protected Transform m_BloodSpawnPos;

		[SerializeField]
		private FixedJoint m_Joint;

		[SerializeField]
		private ePoolType m_ExplosionType;

		private int m_HitCount;

		private Vector3 m_PushDirection;

		protected UnitBodyPart m_HitPart;

		private float m_DamageTimer;

		protected WeaponBase.DamageProfile m_DamageProfile;

		protected AnimalsWeaponData.AnimalStatsBase m_Parameters;

		protected virtual eWeaponType Type => eWeaponType.Knife;

		protected virtual ePoolType PoolType => ePoolType.ChickenStickable;

		public static event Action OnStickableFinish;

		public virtual void InitializeStickable(UnitBodyPart i_Part, Vector3 i_Direction, WeaponBase.DamageProfile i_Profile, AnimalsWeaponData.AnimalStatsBase i_Parameters)
		{
			m_DamageProfile = i_Profile;
			m_HitCount = 0;
			m_DamageTimer = 0f;
			m_HitPart = i_Part;
			m_PushDirection = i_Direction;
			m_Parameters = i_Parameters ?? getWeaponParameters();
			if (m_Animator != null)
			{
				m_Animator.SetTrigger(StaticString.AP_Attack);
			}
			if (m_Joint != null)
			{
				m_Joint.connectedBody = i_Part != null ? i_Part.RB : null;
			}
		}

		protected virtual void Update()
		{
			if (m_HitPart == null)
			{
				return;
			}
			m_DamageTimer += Time.deltaTime;
			AnimalsWeaponData.AnimalStatsBase parameters = getParameters();
			if (parameters == null)
			{
				return;
			}
			if (m_DamageTimer >= parameters.StickableHitDelay)
			{
				dealDamage();
				m_DamageTimer = 0f;
			}
		}

		private void dealDamage()
		{
			if (m_HitPart == null)
			{
				return;
			}
			playHitAudio();
			AnimalsWeaponData.AnimalStatsBase parameters = getParameters();
			if (parameters == null)
			{
				return;
			}
			int maxHitCount = getMaxHitCount(parameters);
			if (maxHitCount <= m_HitCount)
			{
				handleMaxHit();
			}
			else
			{
				handleHit();
			}
			if (m_BloodSpawnPos != null && PoolManager.Instance != null)
				PoolManager.Instance.Dequeue(ePoolType.BloodExplosion_03, m_BloodSpawnPos.position);
			m_HitCount++;
		}

		private int getMaxHitCount(AnimalsWeaponData.AnimalStatsBase i_Parameters)
		{
			if (i_Parameters == null)
				return 1;
			if (i_Parameters.StickableHitCount > 0)
				return i_Parameters.StickableHitCount;
			return Mathf.Max(1, Mathf.RoundToInt(i_Parameters.StickableMaxHit));
		}

		protected virtual void handleHit()
		{
			if (m_HitPart == null || m_DamageProfile == null)
				return;
			m_HitPart.PushAllBody(m_PushDirection, m_DamageProfile.HitDamage, eInteractableType.Weapon);
			playHaptic(GameConfig.Instance == null ? default : GameConfig.Instance.Haptics.WeaponHit);
		}

		protected virtual void handleMaxHit(bool i_UseHitEvent = true, bool i_ShouldGetUp = true)
		{
			playExplosionAudio();
			playExplosionVFX(m_ExplosionType, transform.position);
			playHaptic(GameConfig.Instance == null ? default : GameConfig.Instance.Haptics.WeaponExplosion);
			AnimalsWeaponData.AnimalStatsBase parameters = getParameters();
			if (m_HitPart == null || m_DamageProfile == null || parameters == null)
			{
				SetNextTurn();
				returnToPool();
				return;
			}
			if (i_UseHitEvent && m_HitPart.Physics != null)
			{
				m_HitPart.Physics.AnimaBitePuppetFix(getHitEvent(parameters), i_ShouldGetUp);
			}
			if (m_PushDirection == Vector3.zero)
			{
				SetNextTurn();
				returnToPool();
				return;
			}
			m_PushDirection = (m_PushDirection + Vector3.up * 0.5f) * m_DamageProfile.PushForce;
			m_HitPart.PushAllBody(m_PushDirection, Mathf.RoundToInt(m_DamageProfile.HitDamage), eInteractableType.Weapon);
			SetNextTurn();
			returnToPool();
		}

		private void SetNextTurn()
		{
			if (GameplayController.Instance != null)
			{
				GameplayController.Instance.SetNextTurnDelay();
			}
		}

		private void playExplosionVFX(ePoolType i_PoolType, Vector3 i_Position)
		{
			if (PoolManager.Instance != null)
			{
				PoolManager.Instance.Dequeue(i_PoolType, i_Position);
			}
		}

		private void playHitAudio()
		{
			AudioDataEditor audioData = GameConfig.Instance == null || GameConfig.Instance.GamePlay == null
				? null
				: GameConfig.Instance.GamePlay.AudioDataEditor;
			WeaponAudioData weaponAudio = audioData == null ? null : audioData.GetWeaponAudio(Type);
			AudioClipData hitSfx = weaponAudio == null ? null : weaponAudio.GetHitSFX();
			if (hitSfx != null)
			{
				if (SoundManager.Instance != null)
				{
					SoundManager.Instance.PlayWeaponSFX(hitSfx.Clip, hitSfx.Volume);
				}
			}
		}

		private void playExplosionAudio()
		{
			AudioDataEditor audioData = GameConfig.Instance == null || GameConfig.Instance.GamePlay == null
				? null
				: GameConfig.Instance.GamePlay.AudioDataEditor;
			WeaponAudioData weaponAudio = audioData == null ? null : audioData.GetWeaponAudio(Type);
			AudioClipData explosiveSfx = weaponAudio == null ? null : weaponAudio.GetExplosiveSFX();
			if (explosiveSfx != null)
			{
				if (SoundManager.Instance != null)
				{
					SoundManager.Instance.PlayExplosionSFX(explosiveSfx.Clip, explosiveSfx.Volume);
				}
			}
		}

		private void playHaptic(MMFeedbackHaptics.HapticTypes i_Haptic)
		{
			if (Managers.Instance != null && Managers.Instance.HapticManager != null)
			{
				Managers.Instance.HapticManager.Haptic(i_Haptic);
			}
		}

		private AnimalsWeaponData.AnimalStatsBase getParameters()
		{
			if (m_Parameters != null)
			{
				return m_Parameters;
			}
			m_Parameters = getWeaponParameters();
			return m_Parameters;
		}

		private AnimalsWeaponData.AnimalStatsBase getWeaponParameters()
		{
			WeaponData weaponData = WeaponShopManager.Instance != null ? WeaponShopManager.Instance.GetWeaponData(Type) : null;
			return weaponData != null ? weaponData.StatsData as AnimalsWeaponData.AnimalStatsBase : null;
		}

		private RootMotion.Dynamics.BehaviourBase.AnimatorEvent[] getHitEvent(AnimalsWeaponData.AnimalStatsBase i_Parameters)
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
				GameConfig.Instance.GamePlay.WeaponData.General != null)
			{
				return GameConfig.Instance.GamePlay.WeaponData.General.DefaultHitEvent;
			}
			return null;
		}

		private void returnToPool()
		{
			if (PoolManager.Instance != null && gameObject.activeSelf)
			{
				PoolManager.Instance.Queue(PoolType, gameObject);
			}
		}

		protected virtual void OnDisable()
		{
			if (m_Joint != null)
			{
				m_Joint.connectedBody = null;
			}
			m_HitPart = null;
			m_DamageProfile = null;
			m_Parameters = null;
			CancelInvoke();
		}

		private void SetRefs()
		{
			m_Animator = GetComponentInChildren<Animator>();
			m_BloodSpawnPos = KobGamesSDKSlim.ExtensionMethods.FindDeepChild<Transform>(transform, "BloodSpawn");
			m_Joint = GetComponent<FixedJoint>();
		}
	}
}
