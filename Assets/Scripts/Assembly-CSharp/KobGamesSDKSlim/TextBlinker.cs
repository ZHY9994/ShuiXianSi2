using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace KobGamesSDKSlim
{
	public class TextBlinker : AnimatorBase
	{
		public Color Initial;

		public Color Final;

		public Text Text;

		public TextMeshProUGUI TextTMP;

		protected override void Awake()
		{
			if (RandomStartTime)
				StartTime = Random.Range(0f, 1f);
			ResetValues();
		}

		public override void StartAnimation()
		{
			if (Tween != null)
				Tween.Kill();
			m_IsPlaying = true;
			if (Text != null)
				Tween = Text.DOColor(Final, Duration);
			else if (TextTMP != null)
				Tween = TextTMP.DOColor(Final, Duration);
			if (Tween != null)
			{
				Tween = Tween.SetEase(Ease).SetLoops(LoopCount, LoopType);
				SetId(Tween);
				if (StartTime > 0f)
					Tween.Goto(Mathf.Clamp01(StartTime) * Duration, true);
			}
		}

		public override void ResetValues()
		{
			StopAnimation();
			if (Text != null)
				Text.color = Initial;
			else if (TextTMP != null)
				TextTMP.color = Initial;
		}
	}
}
