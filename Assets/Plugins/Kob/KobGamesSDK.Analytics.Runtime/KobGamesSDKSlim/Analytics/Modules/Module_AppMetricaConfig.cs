using System;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	[Serializable]
	public class Module_AppMetricaConfig
	{
		[SerializeField]
		private string m_ID;

		[SerializeField]
		private bool m_ExceptionsReporting;

		[SerializeField]
		private uint m_SessionTimeoutSec;

		[SerializeField]
		private bool m_LocationTracking;

		[SerializeField]
		private bool m_Logs;

		[SerializeField]
		private bool m_HandleFirstActivationAsUpdate;

		[SerializeField]
		private bool m_StatisticsSending;

		[SerializeField]
		private bool m_RevenueTrackingEnabled;

		public const string VERSION = "4.3.0";

		public string ID
		{
			get
			{
				return m_ID;
			}
			set
			{ m_ID = value; }
		}

		public bool ExceptionsReporting => m_ExceptionsReporting;

		public uint SessionTimeoutSec => m_SessionTimeoutSec;

		public bool LocationTracking => m_LocationTracking;

		public bool Logs => m_Logs;

		public bool HandleFirstActivationAsUpdate => m_HandleFirstActivationAsUpdate;

		public bool StatisticsSending => m_StatisticsSending;

		public bool RevenueTrackingEnabled => m_RevenueTrackingEnabled;
	}
}
