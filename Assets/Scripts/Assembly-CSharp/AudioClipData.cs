using System;
using UnityEngine;

[Serializable]
public class AudioClipData
{
	public AudioClip Clip;

	[Range(0f, 1f)]
	public float Volume = 1f;

}
