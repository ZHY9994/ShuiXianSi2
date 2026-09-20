using UnityEngine;

[RequireComponent(typeof(Light))]
public class CFX_LightIntensityFade : MonoBehaviour
{
	public float duration = 1f;

	public float delay;

	public float finalIntensity;

	private float baseIntensity;

	public bool autodestruct;

	private float p_lifetime;

	private float p_delay;

	private void Start()
	{
		baseIntensity = GetComponent<Light>().intensity;
	}

	private void OnEnable()
	{
		p_lifetime = 0f;
		p_delay = delay;
		if (p_delay > 0f)
			GetComponent<Light>().enabled = false;
	}

	private void Update()
	{
		if (p_delay > 0f)
		{
			p_delay -= Time.deltaTime;
			if (p_delay <= 0f)
				GetComponent<Light>().enabled = true;
			return;
		}
		if (p_lifetime / duration >= 1f)
		{
			if (autodestruct)
				Destroy(gameObject);
			return;
		}
		Light light = GetComponent<Light>();
		float t = Mathf.Clamp01(p_lifetime / duration);
		light.intensity = baseIntensity + (finalIntensity - baseIntensity) * t;
		p_lifetime += Time.deltaTime;
	}
}
