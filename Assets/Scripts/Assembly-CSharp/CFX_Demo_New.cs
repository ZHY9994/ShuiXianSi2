using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CFX_Demo_New : MonoBehaviour
{
	public Renderer groundRenderer;

	public Collider groundCollider;

	[Space]
	[Space]
	public Image slowMoBtn;

	public Text slowMoLabel;

	public Image camRotBtn;

	public Text camRotLabel;

	public Image groundBtn;

	public Text groundLabel;

	[Space]
	public Text EffectLabel;

	public Text EffectIndexLabel;

	private GameObject[] ParticleExamples;

	private int exampleIndex;

	private bool slowMo;

	private Vector3 defaultCamPosition;

	private Quaternion defaultCamRotation;

	private List<GameObject> onScreenParticles = new List<GameObject>();

	private void Awake()
	{
		onScreenParticles = new List<GameObject>();
		List<GameObject> particles = new List<GameObject>();
		for (int i = 0; i < transform.childCount; i++)
			particles.Add(transform.GetChild(i).gameObject);
		particles.Sort((o1, o2) => string.Compare(o1.name, o2.name));
		ParticleExamples = particles.ToArray();
		defaultCamPosition = Camera.main.transform.position;
		defaultCamRotation = Camera.main.transform.rotation;
		StartCoroutine("CheckForDeletedParticles");
		UpdateUI();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			prevParticle();
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			nextParticle();
		else if (Input.GetKeyDown(KeyCode.Delete))
			destroyParticles();
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (groundCollider.Raycast(ray, out hit, 9999f))
			{
				GameObject particle = spawnParticle();
				particle.transform.position = hit.point + particle.transform.position;
			}
		}
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if (scroll != 0f)
			Camera.main.transform.Translate((scroll < 0f ? -1f : 1f) * Vector3.forward, Space.Self);
		if (Input.GetMouseButtonDown(2))
			Camera.main.transform.SetPositionAndRotation(defaultCamPosition, defaultCamRotation);
	}

	public void OnToggleGround()
	{
		groundRenderer.enabled = !groundRenderer.enabled;
		float alpha = groundRenderer.enabled ? 1f : 0.33f;
		groundBtn.color = new Color(1f, 1f, 1f, alpha);
		groundLabel.color = new Color(1f, 1f, 1f, alpha);
	}

	public void OnToggleCamera()
	{
		CFX_Demo_RotateCamera.rotating = !CFX_Demo_RotateCamera.rotating;
		float alpha = CFX_Demo_RotateCamera.rotating ? 1f : 0.33f;
		camRotBtn.color = new Color(1f, 1f, 1f, alpha);
		camRotLabel.color = new Color(1f, 1f, 1f, alpha);
	}

	public void OnToggleSlowMo()
	{
		slowMo = !slowMo;
		Time.timeScale = slowMo ? 0.33f : 1f;
		float alpha = slowMo ? 0.33f : 1f;
		slowMoBtn.color = new Color(1f, 1f, 1f, alpha);
		slowMoLabel.color = new Color(1f, 1f, 1f, alpha);
	}

	public void OnPreviousEffect()
	{
	}

	public void OnNextEffect()
	{
	}

	private void UpdateUI()
	{
		EffectLabel.text = ParticleExamples[exampleIndex].name;
		EffectIndexLabel.text = string.Format("{0}/{1}", (exampleIndex + 1).ToString("00"), ParticleExamples.Length.ToString("00"));
	}

	private GameObject spawnParticle()
	{
		GameObject particle = Instantiate(ParticleExamples[exampleIndex]);
		particle.transform.position = new Vector3(0f, particle.transform.position.y, 0f);
		particle.SetActive(true);
		ParticleSystem ps = particle.GetComponent<ParticleSystem>();
		if (ps != null && ps.main.loop)
		{
			ps.gameObject.AddComponent<CFX_AutoStopLoopedEffect>();
			ps.gameObject.AddComponent<CFX_AutoDestructShuriken>();
		}
		onScreenParticles.Add(particle);
		return particle;
	}

	private IEnumerator CheckForDeletedParticles()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);
			for (int i = onScreenParticles.Count - 1; i >= 0; i--)
			{
				if (onScreenParticles[i] == null)
					onScreenParticles.RemoveAt(i);
			}
		}
	}

	private void prevParticle()
	{
		exampleIndex--;
		if (exampleIndex < 0)
			exampleIndex = ParticleExamples.Length - 1;
		UpdateUI();
	}

	private void nextParticle()
	{
		exampleIndex++;
		if (exampleIndex >= ParticleExamples.Length)
			exampleIndex = 0;
		UpdateUI();
	}

	private void destroyParticles()
	{
		for (int i = onScreenParticles.Count - 1; i >= 0; i--)
		{
			if (onScreenParticles[i] != null)
				Destroy(onScreenParticles[i]);
			onScreenParticles.RemoveAt(i);
		}
	}
}
