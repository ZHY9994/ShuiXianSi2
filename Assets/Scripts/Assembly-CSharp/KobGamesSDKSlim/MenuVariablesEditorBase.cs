using System;
using DG.Tweening;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public abstract class MenuVariablesEditorBase
	{
		public bool UseScreenSettings;

		public bool OpenLevelFailedScreenAfterReviveScreen;

		[Tooltip("Idle time until show No Thanks Buttons")]
		public float TimeToShowNoThanks;

		[Tooltip("No thanks Fade In Duration")]
		public float ShowNoThanksDuration;

		public Ease ShowNoThanksEase;

		[Header("RV")]
		public float RV_NeedleRotationDuration;

		public Ease RV_NeedleRotationEase;

		public float RV_ButtonBlockDuration;

		[Header("Extended Button")]
		public bool ExtendedButtonCanDoScaleAnim;

		public bool ExtendedButtonCanDoHaptics;

		public eButtonScaleAnim ExtendedButtonScaleAnimType;

		public float ExtendedButtonPunchScale;

		public float ExtendedButtonPunchScaleDuration;

		public int ExtendedButtonPunchScaleVibrato;

		public float ExtendedButtonPunchScaleElasticity;

		public Ease ExtendedButtonPunchScaleEaseIn;

		public Ease ExtendedButtonPunchScaleEaseOut;

		public float TimeToAllowRevive;

		public bool LegacyStartGame;

		public bool IsShowGDPR;

		public MenuVariablesEditorBase()
		{
			TimeToShowNoThanks = 2f;
			ShowNoThanksDuration = 1f;
			ShowNoThanksEase = (Ease)7;
			RV_NeedleRotationDuration = 1.3f;
			RV_NeedleRotationEase = (Ease)2;
			RV_ButtonBlockDuration = 3f;
			ExtendedButtonScaleAnimType = (eButtonScaleAnim)1;
			ExtendedButtonPunchScale = 0.2f;
			ExtendedButtonPunchScaleDuration = 0.25f;
			ExtendedButtonPunchScaleVibrato = 7;
			ExtendedButtonPunchScaleElasticity = 0.66f;
			TimeToAllowRevive = 10f;
			ExtendedButtonPunchScaleEaseIn = (Ease)27;
			ExtendedButtonPunchScaleEaseOut = (Ease)9;
		}
	}
}
