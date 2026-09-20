using System;
using System.Collections;
using UnityEngine;

public class CFX_ShurikenThreadFix : MonoBehaviour
{
	private ParticleSystem[] systems;

	private void OnEnable()
	{
		systems = GetComponentsInChildren<ParticleSystem>();
		if (systems != null)
		{
			foreach (ParticleSystem system in systems)
			{
				system.Stop(true);
				system.Clear(true);
			}
		}
		StartCoroutine("WaitFrame");
	}

	private IEnumerator WaitFrame()
	{
		yield return null;
		if (systems != null)
			foreach (ParticleSystem system in systems)
				system.Play(true);
	}
}
