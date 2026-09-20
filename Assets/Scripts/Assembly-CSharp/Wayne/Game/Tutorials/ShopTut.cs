using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using Wayne.Game.Shops;
using KobGamesSDKSlim.MenuManagerV1;

namespace Wayne.Game.Tutorials
{
	public class ShopTut : TTutorial
	{
		private eStep m_CurrentStep;

		private bool m_ClosedShopAfterTutorial;

		public ShopTut()
		{
		}

		protected override eTutorial m_Type => eTutorial.Shop;

		public static event Action<eStep> OnStepChanged = delegate { };

		public override void OnActivate()
		{
			if (StorageManager.Instance.CurrentLevel >= 4 && IsTutorialActive)
				invokeCompletion();
			if (DeckManager.Instance.IsWeaponInDeck(eWeaponType.RPG))
				invokeCompletion();
			WeaponShopTabController.OnTabInitialized += onTabInitialized;
			CardInfoManager.OnCardInfoOpen += onCardInfoOpened;
			DeckManager.OnDeckOpen += onDeckOpened;
			DeckManager.OnDeckUpdate += onDeckUpdate;
			DeckManager.OnDeckClose += onDeckClosed;
		}

		public override void OnDeactivate()
		{
			WeaponShopTabController.OnTabInitialized -= onTabInitialized;
			CardInfoManager.OnCardInfoOpen -= onCardInfoOpened;
			DeckManager.OnDeckOpen -= onDeckOpened;
			DeckManager.OnDeckUpdate -= onDeckUpdate;
			DeckManager.OnDeckClose -= onDeckClosed;
		}

		private void initialize()
		{
			if (IsTutorialActive)
			{
				MenuManager.Instance.OpenMenuScreen("Screen_ShopTutorial");
				setStep(eStep.Step_1);
			}
		}

		public override void OnLevelStart()
		{
			if (m_ClosedShopAfterTutorial)
			{
				AnalyticsManager.Instance.Tutorial(TutorialUtils.Tut_ShopLevelStart);
				m_ClosedShopAfterTutorial = false;
			}
		}

		private void onTabInitialized()
		{
			initialize();
		}

		private void onCardInfoOpened()
		{
			setStep(eStep.Step_2);
			reportEvent(TutorialUtils.Tut_CardTapped);
		}

		private void onDeckOpened()
		{
			setStep(eStep.Step_3);
		}

		private void onDeckUpdate()
		{
			if (IsTutorialActive)
			{
				MenuManager.Instance.CloseMenuScreen("Screen_ShopTutorial");
				reportEvent(TutorialUtils.Tut_CardReplaced);
				reportEvent(TutorialUtils.Tut_ShopClosed);
				m_ClosedShopAfterTutorial = true;
				invokeCompletion();
			}
		}

		private void onDeckClosed()
		{
		}

		private void setStep(eStep i_Step)
		{
			if (!IsTutorialActive)
				return;
			m_CurrentStep = i_Step;
			OnStepChanged?.Invoke(m_CurrentStep);
		}
	}
}
