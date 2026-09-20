using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class BoosterRevealAnimData
{
	public float Duration;

	public Ease Ease;

	public float DelayBeforeSlotReveal;

	public float BoosterRewardCloseDelay = 0.2f;

	public float ShakeDuration;

	public Vector3 ShakeStrength;

	public int Vibrator;

	public float ScreenFadeDuration = 0.5f;

	public Ease ScreenFadeEase = (Ease)3;
}
