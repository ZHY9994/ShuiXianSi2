using UnityEngine;
using UnityEngine.Video;

namespace KobGamesSDKSlim
{
	[CreateAssetMenu(fileName = "CrossPromoData", menuName = "Ad Configuration")]
	public class AdConfiguration : ScriptableObject
	{
		public bool IOS;

		public string IOSAppID;

		public bool Android;

		public string AndroidAppID;

		public VideoClip VideoClip;

		public AdConfiguration()
			: base()
		{
		}
	}
}
