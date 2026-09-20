using System;

[Serializable]
public class AudioData
{
	public AudioClipData[] AudioClipData;

	public void SetVolume(float i_Volume)
	{
		foreach (AudioClipData audioClipData in AudioClipData)
		{
			audioClipData.Volume = i_Volume;
		}
	}
}
