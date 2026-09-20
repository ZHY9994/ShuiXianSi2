using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class WeaponBase : MonoBehaviour, IWeapon
	{
		internal enum eState
		{
			Spawn = 0,
			Thrown = 1,
			Hit = 2,
			Bouncing = 3,
			Finished = 4
		}

		[Serializable]
		public class SpawnInfo
		{
			public Vector3 Position;

			public Quaternion Rotation;
		}

		[Serializable]
		public class DamageProfile
		{
			public float HitDamage;

			public float PushForce;

			public float Radius;

			public int ExplosionDamage;

			public eUnityType Type;
		}

		[SerializeField]
		private Transform m_Center;

		[SerializeField]
		private GameObject m_Root;

		[SerializeField]
		protected SpawnInfo m_SpawnInfo;

		protected UnitBase m_Unit;

		internal eState m_State;

		private WeaponEffectControllers m_EffectControllers = new WeaponEffectControllers();

		protected float m_WeaponThrowTime;

		private Tween m_HideTween;

		protected WeaponDataEditor.GeneralWeaponDataEditor m_GeneralParameter => GameConfig.Instance.GamePlay.WeaponData.General;

		protected WeaponData m_WeaponData => WeaponShopManager.Instance.GetWeaponData(Type);

		public virtual eWeaponType Type { get; }

		public ePoolType PoolType => m_WeaponData.PoolType;

		protected DamageProfile m_DamageProfile { get; private set; } = new DamageProfile();

		public bool IsActive => m_State != eState.Spawn && m_State != eState.Finished;

		public float InitialThrowTime { get; protected set; }

		public Transform Center => m_Center == null ? transform : m_Center;

		public static event Action<IWeapon> OnWeaponSpawned;

		public static event Action<IWeapon> OnWeaponFired;

		public static event Action<IWeapon> OnWeaponFinished;

		protected T GetStatsType<T>(eWeaponType i_Type) where T : WeaponData.WeaponStatsBase
		{
			return m_WeaponData.StatsData as T;
		}

		protected virtual void Awake()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			CancelInvoke();
		}

		public virtual void InitializeWeapon(UnitBase i_Unit, Transform i_Spawn)
		{
			m_Unit = i_Unit;
			initializeValues();
			m_EffectControllers.Initialize(Type);
			sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Load);
			OnWeaponSpawned?.Invoke(this);
		}

		public virtual void FireWeapon(Vector3 i_Velocity, Vector3 i_Torque)
		{
			m_WeaponThrowTime = Time.time;
			OnWeaponFired?.Invoke(this);
		}

		public virtual void WeaponHitEnter(Collision i_Other)
		{
			return;
		}

		protected void setParent(Transform i_Parent, bool i_Reset)
		{
            transform.SetParent(i_Parent);
            if (i_Reset)
			{
				transform.localRotation = Quaternion.identity;
			}
		}

		protected void setSpawnPosition()
		{
			transform.localPosition = m_SpawnInfo.Position;
			transform.localRotation = m_SpawnInfo.Rotation;
		}

		protected void sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon i_Type)
		{
			if (m_EffectControllers.Audio != null)
			{
				m_EffectControllers.Audio.PlaySFX(i_Type);
			}
		}

		protected void haptic(WeaponEffectControllers.HapticController.eHapticWeapon i_Haptic)
		{
			if (m_EffectControllers == null || m_EffectControllers.Haptic == null)
				return;
			m_EffectControllers.Haptic.Haptic(i_Haptic);
		}

		protected void vFX(WeaponEffectControllers.VFXController.eVFXWeapon i_VFX, Vector3 i_Position = default(Vector3), Vector3 i_Rotation = default(Vector3), Transform i_Parent = null, UnitBodyPart i_HitPart = null)
		{
			if (m_EffectControllers == null || m_EffectControllers.VFX == null)
				return;
			m_EffectControllers.VFX.PlayVFX(i_VFX, i_Position, i_Rotation, i_Parent, i_HitPart);
		}

		protected void freezeframe()
		{
			WayneFreezeFrame.Instance.FreezeFrame();
		}

		protected void shakeScreen()
		{
			CameraShake.Instance.PlayCamShake();
		}

		internal void setState(eState i_State)
		{
			m_State = i_State;
		}

		protected virtual void actionfinished(bool i_Hide = true)
		{
			m_State = eState.Finished;
			OnWeaponFinished?.Invoke(this);
			if (i_Hide)
			{
				Invoke("hide", m_GeneralParameter.HideDelay);
			}
		}

		private void initializeDamageProfile()
		{
			WeaponData.WeaponStatsBase stats = m_WeaponData.StatsData;
			bool isAI = m_Unit.UnitType != eUnityType.Player;
			m_DamageProfile.HitDamage = isAI ? stats.BaseHitDamage : stats.HitDamageIncrease;
			m_DamageProfile.PushForce = isAI ? stats.PushForce : stats.PushForceIncrease;
			m_DamageProfile.Radius = isAI ? stats.BaseRadius : stats.RadiusIncrease;
			m_DamageProfile.Type = m_Unit.UnitType;
		}

		protected virtual void initializeValues()
		{
			initializeDamageProfile();
			InitialThrowTime = 0f;
		}

		protected float getUnitDamage(float i_Increase, float i_Base)
		{
			return m_Unit.UnitType != eUnityType.Player ? i_Base : i_Increase;
		}

		protected void show()
		{
			if (m_HideTween != null)
			{
				m_HideTween.Kill();
			}
			m_Root.transform.localScale = Vector3.one;
		}

		protected virtual void hide()
		{
			if (m_HideTween != null)
			{
				m_HideTween.Kill();
			}
			m_HideTween = m_Root.transform.DOScale(m_GeneralParameter.m_HideTween.Vector, m_GeneralParameter.m_HideTween.Duration)
				.SetEase(m_GeneralParameter.m_HideTween.Ease)
				.SetDelay(m_GeneralParameter.m_HideTween.Delay)
				.OnComplete(_hideComplete);
		}

		private void _hideComplete()
		{
			PoolManager.Instance.Queue(PoolType, gameObject);
		}

		protected virtual void SetRefs()
		{
			m_Root = KobGamesSDKSlim.ExtensionMethods.FindDeepChild<GameObject>(transform, "Root");
			m_Center = KobGamesSDKSlim.ExtensionMethods.FindDeepChild<Transform>(transform, "Center");
		}

		private void setSpawn()
		{
			m_SpawnInfo.Position = transform.localPosition;
			m_SpawnInfo.Rotation = transform.localRotation;
		}

	}
}
