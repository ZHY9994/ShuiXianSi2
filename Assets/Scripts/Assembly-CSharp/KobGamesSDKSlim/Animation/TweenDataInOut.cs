using System;
using DG.Tweening;

namespace KobGamesSDKSlim.Animation
{
	[Serializable]
	public struct TweenDataInOut
	{
		public float DurationIn;

		public float DurationOut;

		public Ease EaseIn;

		public Ease EaseOut;
	}
}
