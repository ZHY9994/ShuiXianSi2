using System;
using DG.Tweening;
using UnityEngine;

namespace KobGamesSDKSlim.Animation
{
	[Serializable]
	public class EarnObjectUIAnimData
	{
		[Serializable]
		public class LinearAnimData : AnimData
		{
			[Header("Phase 1 - Scale Up and Move")]
			public float InitialSizeDeltaAmount;

			public float InitialSizeDeltaDuration;

			public Ease InitialSizeDeltaEase;

			public Vector3 InitialMoveOffset;

			public Ease InitialMoveEase;

			[Header("Phase 2 - Scale Down")]
			public float FinalSizeDeltaAmount;

			public float FinalSizeDeltaDuration;

			public Ease FinalSizeDeltaEase;

			[Header("Phase 3 - Animate to Final Pos")]
			public TweenData FinalAnimData;
		}

		[Serializable]
		public class BurstAnimData : AnimData
		{
			public float BurstRadius;

			public Vector2 BurstOffset;

			[Range(0f, 1f)]
			public float RadiusThickness;

			public bool IsLimitAngle;

			public float AngleLimitMin;

			public float AngleLimitMax;

			[Header("Phase 1 - Scale Up and move")]
			public float BurstDuration;

			public float BurstSizeDeltaAmount;

			public Ease BurstMoveEase;

			public Ease BurstSizeDeltaEase;

			[Header("Phase 2 - Wait and scale down")]
			public float WaitSizeDeltaAmount;

			public Ease WaitSizeDeltaEase;

			public float WaitSizeDeltaDuration;

			[Header("Phase 3 - Animate to Final Pos")]
			public TweenData FinalAnimData;
		}

		[Serializable]
		public class AnimData
		{
			public int CoinsNumber;

			public float DelayBetweenCoins;

			public AnimationCurve DelayBetweenCoinsCurve;
		}

		public eEarnObjectSpawnMode SpawnMode;

		public LinearAnimData LinearMode;

		public BurstAnimData BurstMode;

		private bool isLinearMode => SpawnMode == eEarnObjectSpawnMode.Linear;

		private bool isBurstCircularMode => SpawnMode == eEarnObjectSpawnMode.Burst;
	}
}
