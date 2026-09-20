using Cinemachine;
using KobGamesSDKSlim;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
	public CinemachineImpulseDefinition Impulse;

	public SignalSourceAsset i_Asset;

	public Vector3 Velocity;

	public Vector3 position = Vector3.zero;

	public float ShakeIntensity = 1f;

	public void PlayCamShake()
	{
		Impulse.CreateEvent(position, Velocity * ShakeIntensity);
	}

	public void PlayCamShake(float i_Itensity)
	{
		Impulse.CreateEvent(position, Velocity * i_Itensity);
	}

	public void PlayCamShake(BoosterCameraShakeData i_Data)
	{
		if (i_Data == null)
			return;
		i_Data.CinemachineImpulseDefinition.CreateEvent(position, new Vector3(position.y * i_Data.Velocity.x, position.y * i_Data.Velocity.y, i_Data.Velocity.z * i_Data.ShakeIntensity));
	}
}
