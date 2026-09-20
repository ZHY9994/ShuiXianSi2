using System;
using Cinemachine;
using UnityEngine;

[Serializable]
public class BoosterCameraShakeData
{
	public CinemachineImpulseDefinition CinemachineImpulseDefinition;

	public Vector3 Velocity;

	public float ShakeIntensity;
}
