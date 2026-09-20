using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoStopLoopedEffect : MonoBehaviour
{
	public float effectDuration = 2.5f;

	private float d;

	private void OnEnable()
	{
		d = effectDuration;
	}

	private void Update()
	{
		if (d <= 0f)
			return;
		d -= Time.deltaTime;
		if (d > 0f)
			return;
		GetComponent<ParticleSystem>()?.Stop(true);
		CFX_Demo_Translate translate = GetComponent<CFX_Demo_Translate>();
		if (translate != null)
			translate.enabled = false;
	}
}
