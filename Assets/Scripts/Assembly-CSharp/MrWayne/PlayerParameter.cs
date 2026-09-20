using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class PlayerParameter
	{
		public Vector2 DragDistanceMinMax = new Vector2(20f, 300f);

		public float MinimumDrag;

		public Vector2 KickDamageRange = new Vector2(2f, 7f);

		public float KickPushForce = 26.75f;

		public Vector2 ReceiveDamageReduction;

	}
}
