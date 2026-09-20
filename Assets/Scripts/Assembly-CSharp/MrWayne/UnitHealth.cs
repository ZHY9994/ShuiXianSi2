using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;

namespace MrWayne
{
	public class UnitHealth : UnitComponent
	{
		[SerializeField]
		private HealthController m_HealthController;

		protected float m_Health = 1f;

		public bool IsDead => m_Health <= 0f;

		public float TotalHealth { get; private set; }

		public float Health => m_Health;

		public int Level { get; private set; }

		public static event Action<float, UnitBase> OnUnitDamageTaken;

		public static event Action<UnitBase> OnUnitDeath;

		public static event Action OnHealthChanged;

		private void OnValidate()
		{
			m_HealthController = GetComponentInChildren<HealthController>();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			UnitPhysics.OnUnitDamaged += onUnitDamaged;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			UnitPhysics.OnUnitDamaged -= onUnitDamaged;
		}

		private void onUnitDamaged(float i_Damage, eBodyPart i_Part, UnitBase i_Unit, eInteractableType i_Interactable)
		{
			if (i_Unit == m_UnitBase)
			{
				takeDamage(i_Damage, i_Part, i_Interactable);
			}
		}

		public virtual void Initialize(int i_HealthWeight)
		{
			float health;
			if (m_UnitBase.UnitType == eUnityType.Player)
			{
				health = StorageManager.Instance.PlayerHealth;
			}
			else
			{
				health = DifficultyManager.Instance.GetHealth(i_HealthWeight / 10f);
				int playerHealth = StorageManager.Instance.PlayerHealth;
				float healthPerLevel = DifficultyManager.Instance.Config.HealthPerLevel;
				float maxHealth = (playerHealth / healthPerLevel + 5f) * healthPerLevel;
				if (health > maxHealth)
				{
					health = maxHealth;
				}
				if (health < 0f)
				{
					health = 0f;
				}
			}
			m_Health = health;
			Level = Mathf.Max(1, Mathf.CeilToInt(health / DifficultyManager.Instance.Config.HealthPerLevel));
			m_HealthController.Initialize(m_Health, Level);
			TotalHealth = m_Health;
		}

		private void takeDamage(float i_Damage, eBodyPart i_Part = eBodyPart.All, eInteractableType i_Interactable = eInteractableType.None)
		{
			float damage;
			if (m_UnitBase.UnitType != eUnityType.Player)
			{
				damage = m_GeneralParameter.DamageIncreaseLevelsAI * StorageManager.Instance.CurrentLevel + i_Damage;
			}
			else
			{
				float healthRatio = TotalHealth == 0f ? 0f : Mathf.Clamp01(m_Health / TotalHealth);
				float damageIncrease = m_GeneralParameter.DamageIncreaseLevelsPlayer * StorageManager.Instance.CurrentLevel + i_Damage;
				float reduction = Mathf.Lerp(GameConfig.Instance.GamePlay.UnitData.Player.ReceiveDamageReduction.x, GameConfig.Instance.GamePlay.UnitData.Player.ReceiveDamageReduction.y, 1f - healthRatio);
				damage = damageIncrease * reduction;
				if (StorageManager.Instance.CurrentLevel <= 3)
				{
					damage *= 0.5f;
				}
			}
			damage = interactableDamageOverride(i_Interactable, damage);
			if (GameConfig.Instance.Debug.UnlimitedHealth)
			{
				playDamagePopUp(damage);
				return;
			}
			if (m_Health > 0f && damage >= 1f)
			{
				m_Health = Mathf.Max(0f, m_Health - damage);
				playDamagePopUp(damage);
				m_HealthController.DecreaseHealth(damage);
				updateHealth();
				if (m_Health <= 0f)
				{
					death();
				}
			}
		}

		private float interactableDamageOverride(eInteractableType i_Interactable, float i_Damage)
		{
			if (i_Interactable == eInteractableType.Weapon)
			{
				return i_Damage;
			}
			if (i_Interactable == eInteractableType.CrateShank)
			{
				RandomRangeValue minMaxShankDamage = m_UnitBase.UnitType == eUnityType.Player
					? GameConfig.Instance.GamePlay.ObstacleParameters.WoodenCrate.MinMaxShankDamagePlayer
					: GameConfig.Instance.GamePlay.ObstacleParameters.WoodenCrate.MinMaxShankDamage;
				return TotalHealth * minMaxShankDamage.RandomValue();
			}
			return 0f;
		}

		private void updateHealth()
		{
			OnUnitDamageTaken?.Invoke(m_Health / TotalHealth, m_UnitBase);
			OnHealthChanged?.Invoke();
		}

		private void playDamagePopUp(float i_Value)
		{
			if (i_Value <= 0f)
			{
				return;
			}
			GameObject popUp = PoolManager.Instance.Dequeue(ePoolType.DamagePopUp, m_UnitBase.Center.position + Vector3.up * GameConfig.Instance.GamePlay.Effects.DamagePopUp.SpawnOffset);
			PopUpTextDamage text = WayneStaticFunctions.GetComponentNoAlloc<PopUpTextDamage>(popUp);
			if (text != null)
			{
				text.Initialize(i_Value);
			}
		}

		private void death()
		{
			Managers.Instance.HapticManager.Haptic(GameConfig.Instance.Haptics.CharacterDeath);
			OnUnitDeath?.Invoke(m_UnitBase);
		}

		public void ForceKill()
		{
			takeDamage(9000f, eBodyPart.All, eInteractableType.Weapon);
		}
	}
}
