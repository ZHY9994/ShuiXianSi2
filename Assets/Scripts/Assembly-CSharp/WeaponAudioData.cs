using System;
using UnityEngine;

[Serializable]
public class WeaponAudioData
{
	[SerializeField]
	private AudioClipData[] m_Load;

	[SerializeField]
	private AudioClipData[] m_Throw;

	[SerializeField]
	private AudioClipData[] m_Hit;

	[SerializeField]
	private bool m_IsExplosive;

	[SerializeField]
	private AudioClipData[] m_ExplosionSound;

	public AudioClipData GetLoadSFX()
	{
		return m_Load == null || m_Load.Length == 0 ? null : m_Load[UnityEngine.Random.Range(0, m_Load.Length)];
	}

	public AudioClipData GetThrowSFX()
	{
		return m_Throw == null || m_Throw.Length == 0 ? null : m_Throw[UnityEngine.Random.Range(0, m_Throw.Length)];
	}

	public AudioClipData GetHitSFX()
	{
		return m_Hit == null || m_Hit.Length == 0 ? null : m_Hit[UnityEngine.Random.Range(0, m_Hit.Length)];
	}

	public AudioClipData GetExplosiveSFX()
	{
		return !m_IsExplosive || m_ExplosionSound == null || m_ExplosionSound.Length == 0
			? null
			: m_ExplosionSound[UnityEngine.Random.Range(0, m_ExplosionSound.Length)];
	}

	public void SetAllDataVolumes(float i_Volume)
	{
		if (m_Load != null)
			for (int i = 0; i < m_Load.Length; i++)
				m_Load[i].Volume = i_Volume;
		if (m_Throw != null)
			for (int i = 0; i < m_Throw.Length; i++)
				m_Throw[i].Volume = i_Volume;
		if (m_Hit != null)
			for (int i = 0; i < m_Hit.Length; i++)
				m_Hit[i].Volume = i_Volume;
	}
}
