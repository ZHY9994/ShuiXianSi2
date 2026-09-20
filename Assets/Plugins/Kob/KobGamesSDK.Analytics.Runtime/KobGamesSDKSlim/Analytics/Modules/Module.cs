using System;
using System.Collections.Generic;
using KobGamesSDKSlim.Analytics.EventsData;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	[Serializable]
	public abstract class Module : ModuleBase
	{
		[Serializable]
		public class Module_Config
		{
			public DisabledEvents m_DisabledEvents;

			[HideInInspector]
			public EventsSO EventsConsts;
		}

		[SerializeField]
		private bool m_IsModuleEnabled;

		[SerializeField]
		protected ePartners m_Partner;

		[SerializeField]
		private Module_Config _ModuleConfig;

		private bool m_IsInitCalled;

		private bool m_IsAppLaunchCalled;

		protected bool m_IsModuleInit;

		protected bool IsModuleEnabled
		{
			get
			{
				return m_IsModuleEnabled;
			}
			set
		{
				m_IsModuleEnabled = value;
			}
		}

		private ePartners m_PartnerGet => m_Partner;

		protected virtual bool PartnerCanBeManuallySet => false;

		public abstract eAnalyticsServices Service { get; }

		protected abstract string m_ServiceDefineDirective { get; }

		protected abstract string[] m_PluginDataPaths { get; }

		public bool ModuleEnabled => m_IsModuleEnabled;

		protected bool m_IsWorking => m_IsModuleEnabled && m_IsModuleInit;

		protected Module_Config m_ModuleConfig => _ModuleConfig;

		protected EventsSO m_EventsConsts => _ModuleConfig?.EventsConsts;

		protected DisabledEvents m_DisabledEvents => _ModuleConfig?.m_DisabledEvents;

		public bool NoFoldersFoundError => false;

		private bool m_SomeFoldersFoundWarning => false;

		private bool showNonEditablePartner => false;

		private void logPluginFolders()
		{
		}

		private bool showEditablePartner()
		{
			return false;
		}

		protected sealed override void OnEnable()
		{
			OnServiceEnable();
		}

		protected sealed override void OnDisable()
		{
			OnServiceDisable();
		}

		protected sealed override void OnApplicationPause(bool pauseStatus)
		{
			OnServiceApplicationPause(pauseStatus);
		}

		public virtual void OnServiceEnable()
		{
		}

		public virtual void OnServiceDisable()
		{
		}

		public virtual void OnServiceApplicationPause(bool i_PauseStatus)
		{
		}

		public void OnServiceAwake(InitializationDataBase i_InitializationData)
		{
			if (m_IsInitCalled) return;
			m_IsInitCalled = true;
			onServiceAwakeImpl(i_InitializationData);
			m_IsModuleInit = true;
		}

		protected abstract void onServiceAwakeImpl(InitializationDataBase i_InitializationData);

		public void OnServiceStart()
		{
			if (!m_IsModuleInit || !m_IsModuleEnabled) return;
			onServiceStartImpl();
		}

		protected virtual void onServiceStartImpl()
		{
		}

		public virtual void OnSendEvent(string i_EventName, DynamicDataBase i_DynamicData, GADataContainer i_GaDataContainer = null, List<EventNameObjectPair> i_Details = null)
		{
			if (canSendEvent(i_EventName)) onSendEventImpl(i_EventName, i_DynamicData, i_GaDataContainer, i_Details);
		}

		protected abstract void onSendEventImpl(string i_EventName, DynamicDataBase i_DynamicData, GADataContainer i_GaDataContainer = null, List<EventNameObjectPair> i_Details = null);

		protected bool canSendEvent(string i_EventName)
		{
			return m_IsModuleEnabled && m_IsModuleInit && !string.IsNullOrEmpty(i_EventName) && (_ModuleConfig?.m_DisabledEvents == null || !_ModuleConfig.m_DisabledEvents.Contains(i_EventName));
		}

		protected bool findDetailValue(string i_Value, List<EventNameObjectPair> i_List)
		{
			if (i_List == null || i_Value == null) return false;
			foreach (EventNameObjectPair pair in i_List)
				if (pair != null && (Equals(pair.OriginalValue, i_Value) || Equals(pair.ConvertedValue, i_Value))) return true;
			return false;
		}

		public void Validate()
		{
		}

		protected void applyChangedDataToPrefab()
		{
		}
	}
}
