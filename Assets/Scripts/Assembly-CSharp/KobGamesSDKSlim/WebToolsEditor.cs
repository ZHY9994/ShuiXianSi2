using System;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class WebToolsEditor
	{
		public bool IsWebToolsEnabled;

		public bool IsFeedbackEnabled;

		public int FeedbackQuestionLimit;

		public bool IsFeedbackRewardEnabled;

		public int FeedbackRewardAmount;

		public string FeedbackRewardCurrency;

		public void ManagersReset()
		{
			Managers.Instance.Reset();
		}

		public WebToolsEditor()
		{
			FeedbackQuestionLimit = 3;
			FeedbackRewardAmount = 100;
			FeedbackRewardCurrency = "GEMS!";
		}
	}
}
