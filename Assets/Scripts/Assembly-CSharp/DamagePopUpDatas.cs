using System;
using UnityEngine;

[Serializable]
public class DamagePopUpDatas
{
	public float SpawnOffset = 0.65f;

	public Color DamageColor;

	public RandomRangeValue MinMaxYOffset;

	public RandomRangeValue MinMaxZOffset;

	public SimpleTweenVector MoveUp;

	public SimpleTweenVector ScaleOut;

	public SimpleTweenVector ScaleIn;

}
