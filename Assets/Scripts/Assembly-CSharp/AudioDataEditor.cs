using System;
using UnityEngine;

[Serializable]
public class AudioDataEditor
{
	public AudioClipData Menu;

	public AudioClipData Combat;

	public float FadeDuration = 1f;

	public float VolumeChangesPerSecond = 10f;

	[SerializeField]
	private GeneralAudioDictionary m_GeneralAudioDictionary = new GeneralAudioDictionary();

	[SerializeField]
	private WeaponAudioDictionary m_WeaponAudioDictionary = new WeaponAudioDictionary();

	[SerializeField]
	private bool m_Debug;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_GeneralVolume = 1f;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_WeaponVolume = 1f;

	public AudioClipData GetGeneralAudio(eSFXType i_Type)
	{
		if (!m_GeneralAudioDictionary.ContainsKey(i_Type))
		{
			return null;
		}
		AudioData data = m_GeneralAudioDictionary[i_Type];
		if (data.AudioClipData.Length == 0)
		{
			return null;
		}
		return data.AudioClipData[UnityEngine.Random.Range(0, data.AudioClipData.Length)];
	}

	public WeaponAudioData GetWeaponAudio(eWeaponType i_Type)
	{
		if (!m_WeaponAudioDictionary.ContainsKey(i_Type))
		{
			return null;
		}
		return m_WeaponAudioDictionary[i_Type];
	}

	private void onValueChangedWeapon()
	{
		foreach (eWeaponType key in m_WeaponAudioDictionary.Keys)
		{
			m_WeaponAudioDictionary[key].SetAllDataVolumes(m_WeaponVolume);
		}
	}

	private void onValueChangedGeneralAudio()
	{
		foreach (eSFXType key in m_GeneralAudioDictionary.Keys)
		{
			m_GeneralAudioDictionary[key].SetVolume(m_GeneralVolume);
		}
	}
}
