using System;
using DG.Tweening;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	[CreateAssetMenu(fileName = "Shop Settings", menuName = "KobGames/Shop Settings")]
	public class ShopSettings : ScriptableObject
	{
		[Serializable]
		public class GeneralSettings
		{
			public GeneralSettings()
			{
				IsShowLockedItemSilhouettes = true;
				IsCloseShopOnClickingSelectedItem = true;
			}
			public bool IsShowLockedItemSilhouettes;

			public bool IsCloseShopOnClickingSelectedItem;
		}

		[Serializable]
		public class AppearData
		{
			public AppearData() { }
			public float AppearDuration;

			public Ease AppearEase;

			public ParticleSystem.MinMaxCurve AppearDelay;
		}

		[Serializable]
		public class RandomUnlockData
		{
			public RandomUnlockData() { }
			public int SequenceLength;

			public ParticleSystem.MinMaxCurve Delay;
		}

		[Serializable]
		public class TweensData
		{
			public TweensData() { }
			public PunchTweenParameters PunchOnSelect;

			public PunchTweenParameters PunchOnUnlockSelect;

			public PunchTweenParameters PunchOnUnlock;
		}

		[Serializable]
		public class PunchTweenParameters
		{
			public PunchTweenParameters() { }
			public float Power;

			public float Duration;

			public int Vibration;

			public float Elasticity;
		}

		public GeneralSettings General;

		public AppearData Appear;

		public RandomUnlockData RandomUnlock;

		public TweensData Tweens;

		public ShopSettings()
		{
		}
	}
}
