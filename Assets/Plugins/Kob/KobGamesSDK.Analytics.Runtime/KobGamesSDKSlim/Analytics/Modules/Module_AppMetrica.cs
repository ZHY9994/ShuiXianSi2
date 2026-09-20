using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	public class Module_AppMetrica : Module
	{
		[CompilerGenerated]
		private sealed class _003CeventOnQueue_003Ed__20 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Module_AppMetrica _003C_003E4__this;

			public string i_EventName;

			public List<EventNameObjectPair> i_Details;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CeventOnQueue_003Ed__20(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[SerializeField]
		private Module_AppMetricaConfig m_AppMetricaConfig;

		private static readonly object s_syncRoot;

		private bool m_CurrentPauseStatus;

		public sealed override eAnalyticsServices Service => (eAnalyticsServices)0;

		protected sealed override string m_ServiceDefineDirective => string.Empty;

		protected override string[] m_PluginDataPaths => Array.Empty<string>();

		public Module_AppMetricaConfig AppMetricaConfig => m_AppMetricaConfig;

		protected sealed override void onServiceAwakeImpl(InitializationDataBase i_InitializationData)
		{
		}

		private void setupAppMetrica()
		{
		}

		protected virtual void onInitialized()
		{
		}

		protected sealed override void onServiceStartImpl()
		{
		}

		public sealed override void OnServiceEnable()
		{
		}

		public sealed override void OnServiceDisable()
		{
		}

		private void handleLog(string condition, string stackTrace, LogType type)
		{
		}

		public sealed override void OnServiceApplicationPause(bool i_PauseStatus)
		{
		}

		protected override void onSendEventImpl(string i_EventName, DynamicDataBase i_DynamicData, GADataContainer i_GaDataContainer = null, List<EventNameObjectPair> i_Details = null)
		{
		}

		[IteratorStateMachine(typeof(_003CeventOnQueue_003Ed__20))]
		private IEnumerator eventOnQueue(string i_EventName, List<EventNameObjectPair> i_Details)
		{
			yield break;
		}
	}
}
