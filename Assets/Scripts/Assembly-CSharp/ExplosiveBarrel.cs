using KobGamesSDKSlim;
using MrWayne;
using UnityEngine;

public class ExplosiveBarrel : Obstacle
{
	[SerializeField]
	private ExplosiveDetector m_Detector;

	private GamePlayVariablesEditor.ObstacleParameter.ExplosiveBarrelParameters m_Parameters => GameConfig.Instance.GamePlay.ObstacleParameters.ExplosiveBarrel;

	private void OnEnable()
	{
		m_Detector.Initialize(m_Parameters.ExplosionRadius);
	}

	protected override void OnObstacleHit(Collision i_Other)
	{
		if (!i_Other.gameObject.CompareTag("BodyPart"))
			return;
		playEffect();
		SoundManager.Instance.PlayGeneralSFXByType(eSFXType.BigExplosion);
		Vector3 point = i_Other.GetContact(0).point;
		playVFX(point);
		FractureObject(30f, new Vector3(point.y, point.z));
		m_Detector.Explode(m_Parameters.ExplosionRadius, m_Parameters.PushForce, m_Parameters.ExplosionDamage);
		m_IsTriggered = true;
		gameObject.SetActive(false);
	}

	private void FractureObject(float i_Force, Vector3 i_Hitpoint)
	{
		GameObject fracturedObject = PoolManager.Instance.Dequeue(ePoolType.FracturedBarrel, transform.position);
		Fracture fracture = fracturedObject.GetComponent<Fracture>();
		fracture.ResetBodies();
		fracture.Container.rotation = transform.rotation;
		fracture.ObjectFractureRandom(i_Force);
	}

	private void playEffect()
	{
		WayneFreezeFrame.Instance.FreezeFrame();
		WayneTimeManager.Instance.SlowWithDuration();
		CameraShake.Instance.PlayCamShake();
		Managers.Instance.HapticManager.Haptic(GameConfig.Instance.Haptics.WeaponExplosion);
	}

	private void playVFX(Vector3 i_Point)
	{
		GameObject explosion = PoolManager.Instance.Dequeue(ePoolType.Explosions, i_Point);
		explosion.GetComponent<ParticleSystem>().Play();
	}
}
