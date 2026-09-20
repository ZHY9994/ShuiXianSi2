using System;
using System.Collections;
using UnityEngine;

public class CFX2_Demo : MonoBehaviour
{
	public bool orderedSpawns = true;

	public float step = 1f;

	public float range = 5f;

	private float order = -5f;

	public Material groundMat;

	public Material waterMat;

	public GameObject[] ParticleExamples;

	private int exampleIndex;

	private string randomSpawnsDelay = "0.5";

	private bool randomSpawns;

	private bool slowMo;

	private void OnMouseDown()
	{
		Collider collider = GetComponent<Collider>();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (collider.Raycast(ray, out hit, 9999f))
		{
			GameObject particle = spawnParticle();
			particle.transform.position = hit.point + particle.transform.position;
		}
	}

	private GameObject spawnParticle()
	{
		GameObject particle = Instantiate(ParticleExamples[exampleIndex]);
		particle.transform.position = new Vector3(0f, particle.transform.position.y, 0f);
		particle.SetActive(true);
		for (int i = 0; i < particle.transform.childCount; i++)
			particle.transform.GetChild(i).gameObject.SetActive(true);
		return particle;
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(5f, 20f, Screen.width - 10f, 60f));
		GUILayout.BeginHorizontal();
		GUILayout.Label("Effect");
		if (GUILayout.Button("<"))
			prevParticle();
		GUILayout.Label(ParticleExamples[exampleIndex].name, GUILayout.Width(210f));
		if (GUILayout.Button(">"))
			nextParticle();
		GUILayout.Label("Click on the ground to spawn selected particles", GUILayout.Width(150f));
		if (GUILayout.Button(CFX_Demo_RotateCamera.rotating ? "Stop Camera" : "Rotate Camera"))
			CFX_Demo_RotateCamera.rotating = !CFX_Demo_RotateCamera.rotating;
		if (GUILayout.Button(randomSpawns ? "Stop Random Spawns" : "Random Spawns", GUILayout.Width(140f)))
		{
			randomSpawns = !randomSpawns;
			if (randomSpawns)
				StopCoroutine("RandomSpawnsCoroutine");
			else
				StartCoroutine("RandomSpawnsCoroutine");
		}
		randomSpawnsDelay = GUILayout.TextField(randomSpawnsDelay, 10, GUILayout.Width(42f));
		randomSpawnsDelay = System.Text.RegularExpressions.Regex.Replace(randomSpawnsDelay, "[^0-9.]", string.Empty);
		Renderer renderer = GetComponent<Renderer>();
		if (GUILayout.Button(renderer.enabled ? "Hide Ground" : "Show Ground", GUILayout.Width(90f)))
			renderer.enabled = !renderer.enabled;
		if (GUILayout.Button(slowMo ? "Normal Time" : "Slow Motion", GUILayout.Width(100f)))
		{
			slowMo = !slowMo;
			Time.timeScale = slowMo ? 0.33f : 1f;
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	private IEnumerator RandomSpawnsCoroutine()
	{
		while (true)
		{
			GameObject particle = spawnParticle();
			if (orderedSpawns)
			{
				particle.transform.position = transform.position + new Vector3(order, particle.transform.position.y, 0f);
				order -= step;
				if (order < -range)
					order = range;
			}
			else
			{
				Vector3 position = transform.position;
				particle.transform.position = new Vector3(position.x + UnityEngine.Random.Range(-range, range), position.y + particle.transform.position.y, position.z + UnityEngine.Random.Range(-range, range));
			}
			yield return new WaitForSeconds(float.Parse(randomSpawnsDelay));
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
			prevParticle();
		else if (Input.GetKeyDown(KeyCode.RightArrow))
			nextParticle();
	}

	private void prevParticle()
	{
		exampleIndex--;
		if (exampleIndex < 0)
			exampleIndex = ParticleExamples.Length - 1;
		string name = ParticleExamples[exampleIndex].name;
		GetComponent<Renderer>().material = name.Contains("Splash") || ParticleExamples[exampleIndex].name.Contains("Skim") ? waterMat : groundMat;
	}

	private void nextParticle()
	{
		exampleIndex++;
		if (exampleIndex >= ParticleExamples.Length)
			exampleIndex = 0;
		string name = ParticleExamples[exampleIndex].name;
		GetComponent<Renderer>().material = name.Contains("Splash") || name.Contains("Skim") ? waterMat : groundMat;
	}

}
