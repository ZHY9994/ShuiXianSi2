using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class ShurikenFragment : WeaponFragment
	{
		protected override eWeaponType Type => eWeaponType.Shuriken;

		protected override ePoolType PoolType => ePoolType.ShurikenFragments;

		private void OnCollisionEnter(Collision i_Other)
		{
			if (i_Other == null || m_State == eState.Finished || Time.time - m_LaunchTime < 0.1f)
			{
				return;
			}
			if (i_Other.gameObject.CompareTag("BodyPart") || i_Other.gameObject.CompareTag("EnemyBodyPart") || i_Other.gameObject.CompareTag("Ground") || i_Other.gameObject.CompareTag("Obstacle"))
			{
				m_State = eState.Finished;
				executeAction(i_Other);
				returnToPool();
			}
		}

		protected override void executeAction(Collision i_Other)
		{
			poolvfx(ePoolType.WallHit, transform.position);
			UnitBodyPart bodyPart = WayneStaticFunctions.GetComponentNoAlloc<UnitBodyPart>(i_Other.gameObject);
			if (bodyPart == null || bodyPart.Physics == null || m_WeaponDamageProfile == null)
			{
				return;
			}
			if (bodyPart.Physics.UnitType == m_WeaponDamageProfile.Type)
			{
				return;
			}
			Vector3 point = i_Other.GetContact(0).point;
			Vector3 normal = i_Other.GetContact(0).normal;
			playShurikenSFX();
			haptic();
			Vector3 direction = normal.magnitude <= 0.00001f ? Vector3.zero : -normal.normalized;
			FragmentedWeaponData.FragmentStatsBase parameters = m_Parameters;
			float impactForce = parameters == null ? 0f : parameters.FragImpactForce;
			float damage = m_WeaponDamageProfile.ExplosionDamage;
			bodyPart.PushAllBody(direction * impactForce, damage, eInteractableType.Weapon);
			Transform bone = bodyPart.GetBone().transform;
			stickShuriken(point, -normal, bone);
			spawnBloodDrip(point, normal, bodyPart.transform);
		}

		private void playShurikenSFX()
		{
			AudioDataEditor audioData = GameConfig.Instance == null ? null : GameConfig.Instance.GamePlay.AudioDataEditor;
			WeaponAudioData weaponAudio = audioData == null ? null : audioData.GetWeaponAudio(eWeaponType.Shuriken);
			AudioClipData explosiveSfx = weaponAudio == null ? null : weaponAudio.GetExplosiveSFX();
			if (explosiveSfx != null)
			{
				if (SoundManager.Instance != null)
				{
					SoundManager.Instance.PlayExplosionSFX(explosiveSfx.Clip, explosiveSfx.Volume);
				}
			}
		}

		private void haptic()
		{
			if (Managers.Instance != null && Managers.Instance.HapticManager != null && GameConfig.Instance != null)
			{
				Managers.Instance.HapticManager.Haptic(GameConfig.Instance.Haptics.WeaponHit);
			}
		}

		private void stickShuriken(Vector3 i_HitPoint, Vector3 i_Normal, Transform i_Parent = null)
		{
			GameObject stickable = poolvfx(ePoolType.ShurikenStickable, i_HitPoint);
			if (stickable == null)
			{
				return;
			}
			Transform transform = stickable.transform;
			transform.SetParent(i_Parent);
			if (i_Normal.sqrMagnitude > 0.0000001f)
			{
				transform.rotation = Quaternion.LookRotation(i_Normal);
			}
		}

		private void spawnBloodDrip(Vector3 i_SpawnPoint, Vector3 i_Normal, Transform i_Parent)
		{
			GameObject bloodDrip = poolvfx(ePoolType.BloodDrip, i_SpawnPoint);
			if (bloodDrip == null)
			{
				return;
			}
			if (i_Normal.sqrMagnitude > 0.0000001f)
			{
				bloodDrip.transform.rotation = Quaternion.LookRotation(i_Normal);
			}
			bloodDrip.transform.SetParent(i_Parent);
		}

		private GameObject poolvfx(ePoolType i_Type, Vector3 i_Position)
		{
			return PoolManager.Instance == null ? null : PoolManager.Instance.Dequeue(i_Type, i_Position);
		}
	}
}
