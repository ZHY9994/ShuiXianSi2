using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;

namespace MrWayne
{
	public class Booster : MonoBehaviour
	{
		[SerializeField]
		private Animator m_Animator;

		private void OnEnable()
		{
			Screen_BoosterReward.OnBoosterOpen += onBoosterOpen;
		}

		private void OnDisable()
		{
			Screen_BoosterReward.OnBoosterOpen -= onBoosterOpen;
		}

		private void onBoosterOpen()
		{
			m_Animator.SetTrigger("Open");
			HapticController.Instance.Haptic(eHapticEvent.BoosterOpen);
		}

		public void OnAnimEvent()
		{
			MenuManager.Instance.CloseMenuScreen("Screen_BoosterReward");
			MenuManager.Instance.OpenMenuScreen("Screen_Booster");
		}

		private void SetRefs()
		{
			m_Animator = GetComponent<Animator>();
		}
	}
}
