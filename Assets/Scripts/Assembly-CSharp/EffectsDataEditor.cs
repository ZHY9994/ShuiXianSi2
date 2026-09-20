using System;
using UnityEngine;

[Serializable]
public class EffectsDataEditor
{
	[Serializable]
	public class HeadShotParameters
	{
		public float SpawnOffset = 1.2f;

		public Vector2 RotationMinMax = new Vector2(5f, 15f);

		public SimpleTweenVector MoveUp;

		public SimpleTweenVector ScaleOut;

		public SimpleTweenVector ScaleScaleIn;

	}

	public DamagePopUpDatas DamagePopUp;

	public HeadShotParameters HeadShot;
}
