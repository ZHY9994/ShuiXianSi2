using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public class AdsScreensDummy : MonoBehaviour
	{
		public GameObject InterstitialScreen;

		public GameObject RewardVideoScreen;

		public Button InterstitialScreenCloseButton;

		public Button RewardVideoScreenCloseButton;

		public Button RewardVideoScreenFailButton;

		private void Awake()
		{
			hideScreens();
		}

		private void OnDestroy()
		{
		}

		private void hideScreens()
		{
			if (InterstitialScreen != null) InterstitialScreen.SetActive(false);
			if (RewardVideoScreen != null) RewardVideoScreen.SetActive(false);
		}

		private void unRegisterEvents()
		{
			InterstitialScreenCloseButton?.onClick.RemoveListener(interstitialClosedButtonClick);
			RewardVideoScreenCloseButton?.onClick.RemoveListener(rewardVideoClosedButtonClick);
			RewardVideoScreenFailButton?.onClick.RemoveListener(rewardVideoClosedFailClick);
		}

		private void OnInterstitialOpened(string i_Placement)
		{
			return;
		}

		private void interstitialClosedButtonClick()
		{
			hideScreens();
		}

		private void OnRewardVideoOpened(string i_Placement)
		{
			return;
		}

		private void rewardVideoClosedButtonClick()
		{
			hideScreens();
		}

		private void rewardVideoClosedFailClick()
		{
			hideScreens();
		}
	}
}
