using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	public class Module_GameAnalytics : Module, IGameLoop
	{
		[SerializeField]
		private Module_GameAnalyticsConfig m_GameAnalyticsConfig;

		public sealed override eAnalyticsServices Service => (eAnalyticsServices)0;

		protected sealed override string m_ServiceDefineDirective => string.Empty;

		protected sealed override string[] m_PluginDataPaths => System.Array.Empty<string>();

		public Module_GameAnalyticsConfig GameAnalyticsConfig => m_GameAnalyticsConfig;

		protected sealed override void onServiceAwakeImpl(InitializationDataBase i_InitializationData)
		{
		}

		protected virtual void onInitialized()
		{
		}

		public void OnLevelLoaded(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData)
		{
		}

		public void OnLevelStarted(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData)
		{
		}

		public void OnLevelCompleted(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData)
		{
		}

		public void OnLevelFailed(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData)
		{
		}

		public void OnLevelContinued(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData)
		{
		}

		private void LogGAException(string i_FunctionName, string i_ExMessage, string i_StackTrace)
		{
		}

		protected override void onSendEventImpl(string i_EventName, DynamicDataBase i_DynamicData, GADataContainer i_GaDataContainer = null, List<EventNameObjectPair> i_Details = null)
		{
		}
	}
}
