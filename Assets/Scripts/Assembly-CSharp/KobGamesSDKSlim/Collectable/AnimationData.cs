using System;
using DG.Tweening;
using KobGamesSDKSlim.Animation;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;

namespace KobGamesSDKSlim.Collectable
{
	[Serializable]
	[CreateAssetMenu(menuName = "KobGames/Collectables Data", fileName = "Collectables Data")]
	public class AnimationData : ScriptableObject
	{
		[Serializable]
		public class SendAnimDataDictionary : UnitySerializedDictionary<eCollectableSendAnimType, SendAnimData>
		{
		}

		[Serializable]
		public class ReceiveAnimDataDictionary : UnitySerializedDictionary<eCollectableReceiveAnimType, ReceiveAnimData>
		{
		}

		[Serializable]
		public class EarnMessageAnimDataDictionary : UnitySerializedDictionary<eCollectableEarnMessageAnimType, EarnMessageAnimData>
		{
		}

		[Serializable]
		public class SendAnimData
		{
			[Serializable]
			public class LinearAnimData : BaseAnimData
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
			public class BurstAnimData : BaseAnimData
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
			public class BaseAnimData
			{
				public bool IsSpecificAmount;

				public int SendAmount;

				public int ItemCost;

				public int MaxAmount;

				public float DelayBetween;

				public AnimationCurve DelayCurve;
			}

			public enum eAnimMode
			{
				Linear = 0,
				Burst = 1
			}

			public eAnimMode AnimMode;

			public LinearAnimData LinearAnimMode;

			public BurstAnimData BurstAnimMode;

			private bool isLinearAnimMode => AnimMode == eAnimMode.Linear;

			private bool isBurstAnimMode => AnimMode == eAnimMode.Burst;
		}

		[Serializable]
		public class ReceiveAnimData
		{
			[Serializable]
			public class BurstAnimData : BaseAnimData
			{
				public float BurstRadius;

				public Vector2 BurstOffset;

				[Range(0f, 1f)]
				public float RadiusThickness;

				public bool IsLimitAngle;

				public float AngleLimitMin;

				public float AngleLimitMax;

				[Header("Move")]
				public float MoveDuration;

				public Ease MoveEase;

				[Header("Rotate")]
				public bool IsRotate;

				public float RotateOffsetMax;

				public Ease RotateEase;

				[Header("Scale")]
				public bool IsScale;

				[Range(0f, 1f)]
				public float FinalScale;

				public Ease ScaleEase;

				[Header("Fade")]
				public bool IsFade;

				public float FinalFadeValue;

				public Ease FadeEase;
			}

			[Serializable]
			public class BaseAnimData
			{
				public int ItemCost;

				public int MaxAmount;

				public float DelayBetween;

				public AnimationCurve DelayCurve;
			}

			public enum eAnimMode
			{
				Burst = 0
			}

			public enum eShowMode
			{
				OnIncreaseAmount = 0,
				OnDecreaseAmount = 1,
				Both = 2,
				None = 3
			}

			public eAnimMode AnimMode;

			public eShowMode ShowMode;

			public bool IsPunchIconScale;

			public BurstAnimData IncreaseAnimMode;

			public BurstAnimData DecreaseAnimMode;

			private bool isBurstAnimMode => AnimMode == eAnimMode.Burst;

			private bool isShowDecrease => AnimMode == eAnimMode.Burst && (ShowMode == eShowMode.OnDecreaseAmount || ShowMode == eShowMode.Both);

			private bool isShowIncrease => AnimMode == eAnimMode.Burst && (ShowMode == eShowMode.OnIncreaseAmount || ShowMode == eShowMode.Both);
		}

		[Serializable]
		public class EarnMessageAnimData
		{
			[Serializable]
			public class SimpleTextAnimData : BaseAnimData
			{
				public bool IsShowPanel;

				public bool IsShowText;

				public Color PanelColor;

				public float StartScale;

				public float StartFade;

				public float StartOffset;

				public float AppearDuration;

				public float AppearScale;

				public float AppearFade;

				public float AppearOffset;

				public Ease AppearEase;

				public float MoveDuration;

				public float MoveScale;

				public float MoveFade;

				public float MoveOffset;

				public Ease MoveEase;

				public float DisappearDuration;

				public float DisappearScale;

				public float DisappearFade;

				public float DisappearOffset;

				public Ease DisappearEase;
			}

			[Serializable]
			public class BaseAnimData
			{
			}

			[Serializable]
			public class ParticlesData
			{
				public float ItemCost;

				public float MaxAmount;

				public Vector2 ParticlesLifetime;

				public Vector2 ParticlesSize;

				public Vector2 Speed;

				public float SpawnConeAngle;

				public float SpawnConeRadius;

				public Vector3 SpawnParticlesOffset;

				public float SpawnDelay;
			}

			public enum eAnimMode
			{
				SimpleText = 0
			}

			public eAnimMode AnimMode;

			public SimpleTextAnimData SimpleTextAnimMode;

			public float SizeMultiplier;

			public bool IsSpawnParticles;

			public ParticlesData Particles;

			private bool isSimpleText => AnimMode == eAnimMode.SimpleText;
		}

		[Serializable]
		public class HapticsData
		{
			public bool IsPlayHapticsOnReceive;

			public MMFeedbackHaptics.HapticTypes ReceivedHaptics;

			public MMFeedbackHaptics.HapticTypes CompleteHaptics;

			public float HapticsCooldown;
		}

		public SendAnimDataDictionary SendAnim;

		public ReceiveAnimDataDictionary ReceiveAnim;

		public EarnMessageAnimDataDictionary EarnMessageAnim;

		public HapticsData Haptics;
	}
}
