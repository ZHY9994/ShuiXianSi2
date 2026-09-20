using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	public class Module_AppsFlyer : Module
	{
		[SerializeField]
		private Module_AppsFlyerConfig m_AppsFlyerConfig;

		public sealed override eAnalyticsServices Service => (eAnalyticsServices)0;

		protected sealed override string m_ServiceDefineDirective => string.Empty;

		protected override string[] m_PluginDataPaths => System.Array.Empty<string>();

		public Module_AppsFlyerConfig AppsFlyerConfig => m_AppsFlyerConfig;

		protected sealed override void onServiceAwakeImpl(InitializationDataBase i_InitializationData)
		{
		}

		protected override void onServiceStartImpl()
		{
		}

		private void initAppsFlyer()
		{
		}

		protected virtual void onInitialized()
		{
		}

		protected sealed override void onSendEventImpl(string i_EventName, DynamicDataBase i_DynamicData, GADataContainer i_GaDataContainer = null, List<EventNameObjectPair> i_Details = null)
		{
		}

		public void onConversionDataSuccess(string conversionData)
		{
		}

		public void onConversionDataFail(string i_Error)
		{
		}

		public void onAppOpenAttribution(string i_AttributionData)
		{
		}

		public void onAppOpenAttributionFailure(string i_Error)
		{
		}
	}
}
