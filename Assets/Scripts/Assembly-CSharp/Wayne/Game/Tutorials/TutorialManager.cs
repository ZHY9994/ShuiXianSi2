using System;
using System.Collections.Generic;
using System.Linq;
using KobGamesSDKSlim;
using UnityEngine;
using UnityEngine.UI;
using Wayne.Game.Shops.Weapons;
using KobGamesSDKSlim.GameManagerV1;

namespace Wayne.Game.Tutorials
{
	public class TutorialManager : Singleton<TutorialManager>
	{
		public ScrollRect WeaponShopScrollRect;

		public RectTransform ShopCardEquipButton;

		public RectTransform CardInfoRVUpgradeButton;

		public RectTransform CardInfoContainer;

		public RectTransform CardInfoCloseButton;

		public RectTransform DeckBodyRect;

		public WeaponShopItem[] ShopItems;

		public DeckItem[] DeckItems;

		private ITutorialModule[] m_Modules;

		private readonly Dictionary<Type, ITutorialModule> m_ModulesCache;

		private TutorialVariableEditor m_Editor => GameConfig.Instance.GamePlay.Tutorials;

		public bool IsShopTutorialActive
		{
			get
			{
				TTutorialData tutorialData = m_Editor.GetTutorialData(eTutorial.Shop);
				return tutorialData != null && tutorialData.IsTutorialActive;
			}
		}

		public TutorialManager()
		{
			m_ModulesCache = new Dictionary<Type, ITutorialModule>();
		}

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			setupModules();
			if (m_Modules != null)
			{
				foreach (ITutorialModule module in m_Modules)
					module.OnStartUp();
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			setupModules();
			if (m_Modules != null)
			{
				foreach (ITutorialModule module in m_Modules)
					module.OnActivate();
			}
			KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset += onGameReset;
			KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
			KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompleted += onLevelFinished;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			if (m_Modules != null)
			{
				foreach (ITutorialModule module in m_Modules)
					module.OnDeactivate();
			}
			KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnGameReset -= onGameReset;
			KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
			KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompleted -= onLevelFinished;
		}

		private void onGameReset()
		{
			if (m_Modules != null)
			{
				foreach (ITutorialModule module in m_Modules)
					module.OnGameReset();
			}
		}

		private void onLevelStarted()
		{
			if (m_Modules != null)
			{
				foreach (ITutorialModule module in m_Modules)
					module.OnLevelStart();
			}
		}

		private void onLevelFinished()
		{
			if (m_Modules != null)
			{
				foreach (ITutorialModule module in m_Modules)
					module.OnLevelComplete();
			}
		}

		private void setupModules()
		{
			if (m_Modules == null || m_Modules.Length == 0)
				m_Modules = GetComponentsInChildren<ITutorialModule>();
		}

		public TTutorialData GetData(eTutorial i_Type)
		{
			return getAllData().FirstOrDefault(data => data.Type == i_Type);
		}

		private List<TTutorialData> getAllData()
		{
			return m_Editor.TutorialDatas;
		}

		public void ReportEvent(string i_Text)
		{
			if (!string.IsNullOrEmpty(i_Text))
				AnalyticsManager.Instance.Tutorial(i_Text);
		}
	}
}
