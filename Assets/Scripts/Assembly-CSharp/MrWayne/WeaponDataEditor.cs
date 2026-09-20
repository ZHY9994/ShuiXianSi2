using System;
using RootMotion.Dynamics;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class WeaponDataEditor
	{
		[Serializable]
		public class GeneralWeaponDataEditor
		{
			public float HeadShotMultiplierPlayer;

			public float HeadShotMultiplierAI;

			public float BodyShotMultiplier;

			public float LimbShotMultiplier;

			public AnimationCurve ExplosiveCurve;

			public AnimationCurve VelocityCurve;

			public Vector2 MinMaxVelocity;

			public float StopVelocity;

			public float ActiveDuration;

			public float DamageDelay;

			public float ThrowDistanceThreshold;

			public float PushBodyYOffset;

			public eLayers CollisionLayer;

			public eLayers NoCollisionLayer;

			public float HitDelay;

			public BehaviourBase.AnimatorEvent[] DefaultHitEvent;

			public float HideDelay;

			public SimpleTweenVector m_HideTween;

			public float CuttingWeaponStickValue;
		}

		public GeneralWeaponDataEditor General;

		public WeaponsEditor Weapons;

		public UpgradeDataEditor Upgrade;
	}
}
