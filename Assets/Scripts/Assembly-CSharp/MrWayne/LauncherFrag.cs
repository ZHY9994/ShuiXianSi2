using System;
using System.Collections;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class LauncherFrag : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem m_ParticleVFX;

		[SerializeField]
		private Collider m_Collider;

		[SerializeField]
		private ExplosiveDetector m_Detector;

		private Rigidbody m_RB;

		private float m_ThrowTime;

		private bool m_Thrown;

		private bool m_Triggered;

		private WeaponBase.DamageProfile m_DamageProfile;

		public static event Action<LauncherFrag> OnFragHit;

		private void OnEnable()
		{
			setLayer(GameConfig.Instance.GamePlay.WeaponData.General.NoCollisionLayer);
		}

		private void OnDisable()
		{
			disablePhysics();
		}

		private void Update()
		{
			if (!m_Thrown || m_RB == null) return;
			if (Time.time - m_ThrowTime >= GameConfig.Instance.GamePlay.WeaponData.General.DamageDelay)
			{
				if (m_RB.velocity.sqrMagnitude > 0.0000001f)
				{
					m_RB.rotation = Quaternion.LookRotation(m_RB.velocity);
				}
			}
		}

		private void OnCollisionEnter(Collision i_Other)
		{
			if (m_Triggered || !m_Thrown || i_Other == null) return;
			if (Time.time - m_ThrowTime < GameConfig.Instance.GamePlay.WeaponData.General.DamageDelay) return;
			GameObject other = i_Other.gameObject;
			if (other.CompareTag("BodyPart") || other.CompareTag("Obstacle") || other.CompareTag("Ground") || other.CompareTag("Throwable"))
			{
				explode(i_Other);
				m_Triggered = true;
			}
		}

		public void Initialize(Transform i_Parent, WeaponBase.DamageProfile i_Profile)
		{
			m_DamageProfile = i_Profile;
			m_Thrown = false;
			m_Triggered = false;
			if (m_ParticleVFX != null)
			{
				m_ParticleVFX.gameObject.SetActive(false);
			}
			transform.SetParent(i_Parent);
			if (m_Detector != null && m_DamageProfile != null)
			{
				m_Detector.Initialize(m_DamageProfile.Radius);
			}
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			disablePhysics();
		}

		public void LaunchFrag(Vector3 i_Velocity, float i_Throwtime)
		{
			m_ThrowTime = i_Throwtime;
			if (m_ParticleVFX != null)
			{
				m_ParticleVFX.gameObject.SetActive(true);
			}
			transform.SetParent(null);
			enablePhysics();
			if (m_RB == null) return;
			m_RB.AddForce(i_Velocity, ForceMode.Impulse);
			m_Thrown = true;
			StartCoroutine(resetCorotine());
		}

		private void enablePhysics()
		{
			if (m_Collider != null)
			{
				m_Collider.enabled = true;
			}
			if (m_RB == null)
				m_RB = gameObject.AddComponent<Rigidbody>();
			m_RB.interpolation = RigidbodyInterpolation.Interpolate;
			m_RB.maxAngularVelocity = float.MaxValue;
			m_RB.angularDrag = 0f;
			setLayer(GameConfig.Instance.GamePlay.WeaponData.General.CollisionLayer);
		}

		private void disablePhysics()
		{
			if (m_Collider != null)
			{
				m_Collider.enabled = false;
			}
			if (m_RB != null)
			{
				Destroy(m_RB);
			}
		}

		private void explode(Collision i_Other)
		{
			if (m_Detector == null || m_DamageProfile == null) return;
			if (PoolManager.Instance != null)
			{
				PoolManager.Instance.Dequeue(ePoolType.Explosions, transform.position);
			}
			if (CameraShake.Instance != null)
			{
				CameraShake.Instance.PlayCamShake();
			}
			playExplosionSfx();
			if (Managers.Instance != null && Managers.Instance.HapticManager != null && GameConfig.Instance != null)
			{
				Managers.Instance.HapticManager.Haptic(GameConfig.Instance.Haptics.WeaponExplosion);
			}
			directHit(i_Other);
			m_Detector.Explode(m_DamageProfile.Radius, m_DamageProfile.PushForce, m_DamageProfile.ExplosionDamage);
			m_Thrown = false;
			OnFragHit?.Invoke(this);
			if (PoolManager.Instance != null && gameObject.activeSelf)
			{
				PoolManager.Instance.Queue(ePoolType.LauncherFragment, gameObject);
			}
		}

		public void CancelFrag()
		{
			m_Thrown = false;
			m_Triggered = true;
			StopAllCoroutines();
			if (m_ParticleVFX != null)
			{
				m_ParticleVFX.gameObject.SetActive(false);
			}
			if (PoolManager.Instance != null && gameObject.activeSelf)
			{
				PoolManager.Instance.Queue(ePoolType.LauncherFragment, gameObject);
			}
		}

		private void directHit(Collision i_Collider)
		{
			if (i_Collider == null || m_DamageProfile == null) return;
			UnitBodyPart part = i_Collider.gameObject.GetComponent<UnitBodyPart>();
			if (part == null) return;
			ContactPoint contact = i_Collider.GetContact(0);
			part.PushAllBody(-contact.normal * m_DamageProfile.PushForce, 0f, eInteractableType.Weapon);
		}

		private void playExplosionSfx()
		{
			AudioDataEditor audioData = GameConfig.Instance == null ? null : GameConfig.Instance.GamePlay.AudioDataEditor;
			WeaponAudioData weaponAudio = audioData == null ? null : audioData.GetWeaponAudio(eWeaponType.HomingMissle);
			AudioClipData explosiveSfx = weaponAudio == null ? null : weaponAudio.GetExplosiveSFX();
			if (explosiveSfx != null && SoundManager.Instance != null)
				SoundManager.Instance.PlayWeaponSFX(explosiveSfx.Clip, explosiveSfx.Volume);
		}

		private void setLayer(eLayers i_Layer)
		{
			gameObject.layer = (int)i_Layer;
		}

		private IEnumerator resetCorotine()
		{
			yield return new WaitForSeconds(0.17f);
			transform.localPosition = UnityTransformExtensions.SetPositionX(transform.localPosition, 0f);
			if (m_RB != null)
				m_RB.constraints = RigidbodyConstraints.FreezePositionX;
		}
	}
}
