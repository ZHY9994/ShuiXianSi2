using KobGamesSDKSlim;

namespace Wayne.Game.Tutorials
{
	public class IntroTut : TTutorial
	{
		private int m_PlayerThrownCount;

		public IntroTut()
		{
		}

		protected override eTutorial m_Type => eTutorial.Intro;

		public override void OnActivate()
		{
			if (IsTutorialActive && StorageManager.Instance.CurrentLevel >= 2)
				invokeCompletion();
			MrWayne.PlayerAimingState.OnPlayerThrown += onPlayerThrown;
			MrWayne.PlayerAimingState.OnWeaponSpawned += onPlayerWeaponSpawn;
		}

		public override void OnDeactivate()
		{
			MrWayne.PlayerAimingState.OnPlayerThrown -= onPlayerThrown;
			MrWayne.PlayerAimingState.OnWeaponSpawned -= onPlayerWeaponSpawn;
		}

		public override void OnLevelStart()
		{
			m_PlayerThrownCount = 0;
			reportEvent(TutorialUtils.Tut_LevelStart);
		}

		public override void OnLevelComplete()
		{
			if (IsTutorialActive)
				invokeCompletion();
		}

		private void onPlayerWeaponSpawn(eWeaponType i_Type)
		{
			if (m_PlayerThrownCount <= 2)
			{
				if (i_Type == eWeaponType.Grenade)
					reportEvent(TutorialUtils.Tut_GrenadeTapped);
				else if (i_Type == eWeaponType.Knife)
					reportEvent(TutorialUtils.Tut_KnifeTapped);
			}
		}

		private void onPlayerThrown(eWeaponType i_Type)
		{
			m_PlayerThrownCount++;
			if (StorageManager.Instance.CurrentLevel <= 2)
				AnalyticsManager.Instance.WeaponThrow(i_Type, m_PlayerThrownCount);
			if (m_PlayerThrownCount <= 2)
			{
				if (i_Type == eWeaponType.Grenade)
					reportEvent(TutorialUtils.Tut_GrenadeThrown);
				else if (i_Type == eWeaponType.Knife)
					reportEvent(TutorialUtils.Tut_KnifeThrown);
			}
		}
	}
}
