using System;
using DG.Tweening;

namespace KobGamesSDKSlim.Animation
{
	[Serializable]
	public struct TweenData
	{
		public float Delay;

		public float Duration;

		public Ease Ease;

		public int LoopCount;

		public TweenData(float i_Delay, float i_Duration, Ease i_Ease, int i_LoopCount)
		{
			Delay = i_Delay;
			Duration = i_Duration;
			Ease = i_Ease;
			LoopCount = i_LoopCount;
		}
	}
}
