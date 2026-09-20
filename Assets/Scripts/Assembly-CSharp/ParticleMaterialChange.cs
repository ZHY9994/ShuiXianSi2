using UnityEngine;

public class ParticleMaterialChange : MonoBehaviour
{
	[SerializeField]
	private Material[] m_Materials;

	[SerializeField]
	private ParticleSystem m_Particle;

	private Renderer m_Renderer;

	private void Awake()
	{
		m_Renderer = m_Particle.GetComponent<Renderer>();
	}

	private void OnEnable()
	{
		setRandomMaterial();
	}

	private void setRandomMaterial()
	{
		if (m_Renderer != null)
		{
			m_Renderer.material = m_Materials[Random.Range(0, m_Materials.Length)];
		}
	}

	private void SetRefs()
	{
		m_Particle = GetComponent<ParticleSystem>();
	}
}
