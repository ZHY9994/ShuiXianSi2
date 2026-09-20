using KobGamesSDKSlim.Collectable;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	public class RandomCashButton : MonoBehaviour
	{
		[SerializeField]
		private int m_MinRewardNumber;

		[SerializeField]
		private int m_MaxRewardNumber;

		[SerializeField]
		private ExtendedButton_RV m_RewardButton;

		[SerializeField]
		private CollectableSender m_CollectableSender;

		private void OnValidate()
		{
			if (m_MinRewardNumber > m_MaxRewardNumber)
				m_MaxRewardNumber = m_MinRewardNumber;
		}

		private void OnEnable()
		{
			if (m_RewardButton != null)
				m_RewardButton.Setup(eRewardVideoPlacements.Shop_Get_Cash, onRVButtonCallback, rvSuccess, onRVClaimFailed, null);
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
			m_CollectableSender.Set(UnityEngine.Random.Range(m_MinRewardNumber, m_MaxRewardNumber));
			m_CollectableSender.Send(enableButtonInteractivity);
		}

		private void enableButtonInteractivity()
		{
			m_RewardButton.SetInteractable(true);
		}

		private void onOpenBoosterReward()
		{
			BoosterManager.Instance.SetOpenType(BoosterManager.eBoosterOpenType.Shop);
			Screen_BoosterReward.GetPackageType = (Screen_BoosterReward.eGetPackageType)1;
			MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_BoosterReward");
		}
	}
}
