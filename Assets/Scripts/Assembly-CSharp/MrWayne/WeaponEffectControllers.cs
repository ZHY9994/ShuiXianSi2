using System;
using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class WeaponEffectControllers
	{
		[Serializable]
		public class AudioController
		{
			public enum eSFXTypeWeapon
			{
				Load = 0,
				Fire = 1,
				Hit = 2,
				Explosion = 3,
				WallHit = 4,
				BloodSpill = 5,
				BloodSplat = 6,
				SmallExplosion = 7
			}

			private eWeaponType Type;

			private AudioDataEditor m_GeneralAudio => GameConfig.Instance == null ? null : GameConfig.Instance.GamePlay.AudioDataEditor;

			private WeaponAudioData m_WeaponAudio => m_GeneralAudio == null ? null : m_GeneralAudio.GetWeaponAudio(Type);

			public void Initialize(eWeaponType i_Type)
			{
				Type = i_Type;
			}

			public void PlaySFX(eSFXTypeWeapon i_Type)
			{
				AudioClipData data = null;
				WeaponAudioData weapon = m_WeaponAudio;
				if (i_Type == eSFXTypeWeapon.Load) data = weapon == null ? null : weapon.GetLoadSFX();
				else if (i_Type == eSFXTypeWeapon.Fire) data = weapon == null ? null : weapon.GetThrowSFX();
				else if (i_Type == eSFXTypeWeapon.Hit) data = weapon == null ? null : weapon.GetHitSFX();
				else if (i_Type == eSFXTypeWeapon.Explosion) data = weapon == null ? null : weapon.GetExplosiveSFX();
				else if (i_Type == eSFXTypeWeapon.WallHit) data = m_GeneralAudio == null ? null : m_GeneralAudio.GetGeneralAudio(eSFXType.WallHit);
				else if (i_Type == eSFXTypeWeapon.BloodSpill) data = m_GeneralAudio == null ? null : m_GeneralAudio.GetGeneralAudio(eSFXType.BloodSpill);
				else if (i_Type == eSFXTypeWeapon.SmallExplosion) data = m_GeneralAudio == null ? null : m_GeneralAudio.GetGeneralAudio(eSFXType.SmallExplosion);
				if (data != null && SoundManager.Instance != null) SoundManager.Instance.PlayWeaponSFX(data.Clip, data.Volume);
			}
		}

		[Serializable]
		public class HapticController
		{
			public enum eHapticWeapon
			{
				Hit = 0,
				Explosion = 1
			}

			public void Initialize()
			{
				return;
			}

			public void Haptic(eHapticWeapon i_Haptic)
			{
				if (Managers.Instance == null || Managers.Instance.HapticManager == null || GameConfig.Instance == null) return;
				if (i_Haptic == eHapticWeapon.Hit)
					Managers.Instance.HapticManager.Haptic(GameConfig.Instance.Haptics.WeaponHit);
				else if (i_Haptic == eHapticWeapon.Explosion)
					Managers.Instance.HapticManager.Haptic(GameConfig.Instance.Haptics.WeaponExplosion);
			}
		}

		[Serializable]
		public class VFXController
		{
			public enum eVFXWeapon
			{
				BloodSplash = 0,
				BloodDripping = 1,
				BloodSplat = 2,
				WallHit = 3,
				HeadShot = 4,
				Explosion = 5,
				LightningBlast = 6
			}

			public void Initialize()
			{
				return;
			}

			public void PlayVFX(eVFXWeapon i_VFX, Vector3 i_Position = default(Vector3), Vector3 i_Rotation = default(Vector3), Transform i_Parent = null, UnitBodyPart i_HitPart = null)
			{
				switch (i_VFX)
				{
					case eVFXWeapon.BloodSplash: bloodSplash(i_Position, i_Rotation, i_Parent, i_HitPart); return;
					case eVFXWeapon.BloodDripping: bloodDripping(i_Position, i_Rotation, i_Parent); return;
					case eVFXWeapon.WallHit: pool(ePoolType.WallHit, i_Position, Quaternion.identity, i_Parent); return;
					case eVFXWeapon.HeadShot: headShot(i_HitPart); return;
					case eVFXWeapon.Explosion: pool(ePoolType.Explosions, i_Position, Quaternion.identity, i_Parent); return;
					case eVFXWeapon.LightningBlast: pool(ePoolType.LightningBlast, i_Position, Quaternion.identity, i_Parent); return;
				}
			}

			private void bloodDripping(Vector3 i_SpawnPoint, Vector3 i_Normal, Transform i_Parent)
			{
				Quaternion rotation = i_Normal.sqrMagnitude > 0.0000001f
					? Quaternion.LookRotation(i_Normal)
					: Quaternion.identity;
				pool(ePoolType.BloodDrip, i_SpawnPoint, rotation, i_Parent);
			}

			private void bloodSplash(Vector3 i_SpawnPoint, Vector3 i_Normal, Transform i_Parent, UnitBodyPart i_HitPart)
			{
				Quaternion rotation = i_Normal.sqrMagnitude > 0.0000001f
					? Quaternion.LookRotation(i_Normal)
					: Quaternion.identity;
				GameObject effect = pool(ePoolType.BloodPop, i_SpawnPoint, rotation, i_Parent);
				if (effect == null || i_HitPart == null) return;
				ParticleSystem particles = effect.GetComponent<ParticleSystem>();
				if (particles == null || i_HitPart.Physics == null || i_HitPart.Physics.UnitBase == null) return;
				ParticleSystem.MainModule main = particles.main;
				main.startColor = i_HitPart.Physics.UnitBase.BloodColor;
				particles.Play();
			}

			private void headShot(UnitBodyPart i_Part)
			{
				if (i_Part == null || i_Part.DamageablePart != eDamageablePart.Head) return;
				Vector3 position = i_Part.transform.position;
				position.y += GameConfig.Instance.GamePlay.Effects.HeadShot.SpawnOffset;
				pool(ePoolType.HeadShotVFX, position, Quaternion.identity);
			}

			private GameObject pool(ePoolType i_Type, Vector3 i_Position = default(Vector3), Quaternion i_Rotation = default(Quaternion), Transform i_Parent = null)
			{
				if (PoolManager.Instance == null) return null;
				GameObject effect = PoolManager.Instance.Dequeue(i_Type);
				if (effect == null) return null;
				Transform t = effect.transform;
				t.position = i_Position;
				t.rotation = i_Rotation;
				t.SetParent(i_Parent, true);
				return effect;
			}
		}

        public AudioController Audio = new AudioController();

        public HapticController Haptic = new HapticController();

        public VFXController VFX = new VFXController();

		public void Initialize(eWeaponType i_Type)
		{
			if (Audio == null || Haptic == null || VFX == null) return;
			Audio.Initialize(i_Type);
		}
	}
}
