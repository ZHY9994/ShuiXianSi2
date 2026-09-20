namespace KobGamesSDKSlim
{
	public abstract class GameConfigBase : SingletonScriptableObject<GameConfig>
	{
		public bool IsIOSATT;

		public string ATTPrivacyPolicy;

		public bool MAXControlsLocalizations;

		public AdsMediation AdsMediation;

		public ResolutionsData ResolutionsData;

		protected GameConfigBase()
			: base()
		{
			ATTPrivacyPolicy = "http://change.com";
			AdsMediation = new AdsMediation();
			ResolutionsData = new ResolutionsData { IsDebugEnabled = true };
		}

		public void OnInspectorInit()
		{
			return;
		}

		private void onATTChanged()
		{
			return;
		}
	}
}
