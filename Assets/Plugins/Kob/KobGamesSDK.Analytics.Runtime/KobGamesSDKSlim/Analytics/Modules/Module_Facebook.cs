using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	public class Module_Facebook : Module, IAppLoop
	{
		[SerializeField]
		private Module_FacebookConfig m_FacebookConfig;

		public sealed override eAnalyticsServices Service => (eAnalyticsServices)0;

		protected sealed override string m_ServiceDefineDirective => string.Empty;

		protected override string[] m_PluginDataPaths => System.Array.Empty<string>();

		public Module_FacebookConfig FacebookConfig => m_FacebookConfig;

		protected sealed override void onServiceAwakeImpl(InitializationDataBase i_InitializationData)
		{
		}

		protected virtual void onInitialized()
		{
		}

		public void OnAppPause(bool i_PauseStatus, DynamicDataBase i_DynamicData)
		{
		}

		protected override void onSendEventImpl(string i_EventName, DynamicDataBase i_DynamicData, GADataContainer i_GaDataContainer = null, List<EventNameObjectPair> i_Details = null)
		{
		}

		public static string GetFacebookAppIds()
		{
			return string.Empty;
		}
	}
}
