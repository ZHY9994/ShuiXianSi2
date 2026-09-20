using System;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class AnalyticsEditor
	{
		public AnalyticsEditor()
			: base()
		{
		}

		public AnalyticsManager AnalyticsManager
		{
			get
			{
				return Managers.Instance.Analytics;
			}
			set
			{
				Managers.Instance.Analytics = value;
			}
		}
	}
}
