using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	public class GetPackageButton : MonoBehaviour
	{
		[SerializeField]
		private ExtendedButton_RV m_RewardButton;

		private void OnValidate()
		{
			m_RewardButton = GetComponentInChildren<ExtendedButton_RV>();
		}

		private void OnEnable()
		{
			m_RewardButton.Setup(eRewardVideoPlacements.Shop_Get_Package, onRVButtonCallback, rvSuccess, onRVClaimFailed, null);
		}

		private void onRVButtonCallback(eRewardVideoCallResult i_Result)
		{
			return;
		}

		private void onRVClaimFailed()
		{
			return;
		}

		private void onRewardClick()
		{
			return;
		}

		private void rvSuccess()
		{
			onOpenBoosterReward();
			DG.Tweening.DOVirtual.DelayedCall(0.2f, enableButtonInteractivity);
		}

		private void enableButtonInteractivity()
		{
			m_RewardButton.SetInteractable(true);
		}

		private void onOpenBoosterReward()
		{
			BoosterManager.Instance.SetOpenType(BoosterManager.eBoosterOpenType.Shop);
			Screen_BoosterReward.GetPackageType = Screen_BoosterReward.eGetPackageType.Shop;
			MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_BoosterReward");
		}
	}
}
