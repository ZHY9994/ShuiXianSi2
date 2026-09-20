using KobGamesSDKSlim.GameManagerV1;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-70)]
	public class GameManager : GameManagerBase
	{
		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			StorageManager.Instance.CurrentLevel = StorageManager.Instance.HighScoreLevel;
			AnalyticsManager.Instance.Initialize();
			LoadingManager.Instance.CloseScreenDelayed(1f);
			AmazonAdapterController.Instance.Initialize();
			FirebaseManager.Instance.Initialize();
		}

		public override void OnEnable()
		{
			base.OnEnable();
		}

		public override void OnDisable()
		{
			base.OnDisable();
		}

		public override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
		}

		public override void LevelStarted()
		{
			base.LevelStarted();
		}

		protected override void LevelFailedNoContinue()
		{
			base.LevelFailedNoContinue();
			ADManager.Instance.ShowWhiteAd();
			if (MenuManagerV1.MenuManager.Instance.IsScreenOpened("Screen_LevelFailedwithRevive")) ResetGame();
			else MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_LevelFailed");
		}

		protected override void LevelFailed()
		{
			base.LevelFailed();
			MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_LevelFailedwithRevive");
		}

		public override void LevelCompleted()
		{
			base.LevelCompleted();
			ADManager.Instance.ShowWhiteAd();
			int levelBefore = StorageManager.Instance.CurrentLevel;
			bool shouldDisplayBooster = GameConfig.Instance.GamePlay.BoosterData.ShouldDisplayBoosterReward();
			if (shouldDisplayBooster && levelBefore >= 3) displayBooster();
			else displayLevelCompletion();
			if (levelBefore <= 1 && StorageManager.Instance.WeaponSelectionTutorial)
				StorageManager.Instance.WeaponSelectionTutorial = false;
			StorageManager.Instance.CurrentLevel++;
			global::Ade_Framework.AdeCloudPlayerPrefs.Save();
			AnalyticsManager.Instance.AchievedLevel(StorageManager.Instance.CurrentLevel);
		}

		private void displayBooster()
		{
			BoosterManager.Instance.SetOpenType(BoosterManager.eBoosterOpenType.Level);
			Screen_BoosterReward.GetPackageType = Screen_BoosterReward.eGetPackageType.LevelCompleteReward;
			MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_BoosterReward");
		}

		private void displayLevelCompletion()
		{
			int currentLevel = StorageManager.Instance.CurrentLevel;
			bool itemBeingUnlocked = Wayne.Game.Utils.ShopUtils.IsAnyItemBeingUnlocked(currentLevel);
			MenuManagerV1.MenuManager.Instance.OpenMenuScreen(
				itemBeingUnlocked
					? "Screen_LevelCompletedReward"
					: "Screen_LevelCompletedReward_V2");
		}
	}
}
