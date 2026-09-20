using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	public bool OnlyDeactivate;

	private void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}

	private IEnumerator CheckIfAlive()
	{
		ParticleSystem ps = GetComponent<ParticleSystem>();
		while (ps != null && ps.IsAlive(true))
		{
			yield return new WaitForSeconds(0.5f);
		}
		if (ps == null)
			yield break;
		if (OnlyDeactivate)
			gameObject.SetActive(false);
		else
			Destroy(gameObject);
	}
}
