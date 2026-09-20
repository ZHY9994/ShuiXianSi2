using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class AIParameter
	{
		public float ThrowDuration = 1f;

		[Range(0f, 1f)]
		public float TrueShotChance = 0.45f;

		public Vector2 RandomAngleRange = new Vector2(110f, 130f);

		public Vector2 RandomForceRange = new Vector2(5f, 8f);

		public Vector2 TutorialRandomAngleRange = new Vector2(110f, 130f);

		public Vector2 TutorialRandomForceRange = new Vector2(4f, 10f);

		public Vector2 ForceMinMax = new Vector2(10f, 20f);

		public Vector2 HForceMinMax;

		public float MaxDistance;

		public AnimationCurve PrecisionCurve;

		public Vector2 PrecisionOffsetMinMax;

		public Vector2 hPrecisionOffsetMinMax;

		public LayerMask GroundLayer;

		public AIVisualData Visuals;

	}
}
