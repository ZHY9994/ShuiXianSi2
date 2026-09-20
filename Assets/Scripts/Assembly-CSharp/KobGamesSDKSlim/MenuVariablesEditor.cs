using System;
using DG.Tweening;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class MenuVariablesEditor : MenuVariablesEditorBase
	{
		[Serializable]
		public class PopUpTextParameter
		{
			public bool UsePopText;

			public bool ShouldRotate;

			public float PunchDuration;

			public float PunchLength;

			public int PunchVibrator;

			[Range(0.1f, 1f)]
			public float PunchElasticity;

			public float ShowDuration;

			public Ease ShowEase;

			public float StayDuration;

			public float HideDuration;

			public Ease HideEase;

			public Color[] PopColors;

			public int[] Rotation;
		}

		[Serializable]
		public class DeckParameter
		{
			public bool IsAnimate;

			public float HideDistance;

			public float ShowDuration;

			public Ease ShowEase;

			public float HideDuration;

			public Ease HideEase;
		}

		[Serializable]
		public class ShopTabParameter
		{
			public Vector2 Size;

			[Range(1f, 20f)]
			public float ScaleFactor;

			public float ShowDuration;

			public Ease ShowEase;

			public float HideDuration;

			public Ease HideEase;
		}

		[Serializable]
		public class ShopDeckParameters
		{
			public DeckParameter UncoverAnim;

			public bool IsAnimateSelection;

			public float SelectionCloseDuration;

			public Ease SelectionCloseEase;

			[Range(0f, 1f)]
			public float FadeInStartValue;

			[Range(0f, 1f)]
			public float FadeInEndValue;

			public float FadeInDuration;

			public Ease FadeInEase;

			public float FadeOutDuration;

			public Ease FadeOutEase;
		}

		public float ScreenFadeInDuration = 0.25f;

		public float ScreenFadeOutDuration = 0.15f;

		public PopUpTextParameter PopUpTextParameters = new PopUpTextParameter
		{
			PunchDuration = 1f,
			PunchLength = 1f,
			PunchVibrator = 10,
			PunchElasticity = 1f,
			ShowDuration = 0.5f,
			ShowEase = (Ease)9,
			StayDuration = 0.5f,
			HideDuration = 0.5f,
			HideEase = (Ease)9
		};

		public DeckParameter DeckParameters = new DeckParameter();

		public ShopDeckParameters ShopDeckParameter = new ShopDeckParameters();

		public DeckParameter TabExpanderParameter = new DeckParameter();

		public ShopTabParameter ShopTabParameters = new ShopTabParameter();
	}
}
