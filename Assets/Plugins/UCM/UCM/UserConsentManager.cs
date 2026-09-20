using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UCM.Steps;
using UnityEngine;

namespace UCM
{
	public class UserConsentManager
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public bool gdprConfirmed;

			public bool fromSettings;

			public UserConsentManager _003C_003E4__this;

			public GDPRWindowController popupWindowController;

			internal void _003CShowGdprUnityUI_003Eb__0()
			{
				gdprConfirmed = true;
				if (_003C_003E4__this != null && _003C_003E4__this._userConsentContext != null)
				{
					_003C_003E4__this._userConsentContext.GdprConsent = true;
					_003C_003E4__this._userConsentContext.SetAttAnswered();
				}
				gdprConfirmed = true;
				_003C_003E4__this._userConsentContext.GdprConsent = true;
				if (popupWindowController != null)
					UnityEngine.Object.Destroy(popupWindowController.gameObject);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CShowAtt_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public UserConsentManager _003C_003E4__this;

			private TaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CShowCmpFromSettings_003Ed__21 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public UserConsentManager _003C_003E4__this;

			private TaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CShowGdprUnityUI_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public bool fromSettings;

			public UserConsentManager _003C_003E4__this;

			private _003C_003Ec__DisplayClass20_0 _003C_003E8__1;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CStartFlowInternal_003Ed__24 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public UserConsentManager _003C_003E4__this;

			public Action flowFinished;

			private TaskAwaiter _003C_003Eu__1;

			private List<BaseStep>.Enumerator _003C_003E7__wrap1;

			private void MoveNext()
			{
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CTryShowCmpIfNeverShown_003Ed__22 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public UserConsentManager _003C_003E4__this;

			private TaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private const string GdprSettingsFileName = "UserConsentSettings";

		private const string GdprLocalizationFileName = "UserConsentLocalization";

		private static readonly Version ManagerVersion;

		private static UserConsentManager instance;

		private UserConsentSettings _userConsentSettings;

		private UserConsentLocalization _userConsentLocalization;

		private UserConsentContext _userConsentContext;

		private GdprStep _gdprUcmStep;

		private CmpStep _cmpUcmStep;

		public static UserConsentManager Instance
		{
			get
			{
				if (instance == null) instance = new UserConsentManager();
				return instance;
			}
		}

		public bool GdprConsent => _userConsentContext.GdprConsent;

		public bool CmpIsReadyButNotShown
		{
			get
			{
				if (_userConsentContext == null || _userConsentContext.UsercentricsReadyStatus == null || _userConsentContext.CmpInitializationError) return false;
				if (_userConsentContext.UsercentricsReadyStatus.consents == null || _userConsentContext.UsercentricsReadyStatus.consents.Count < 1) return false;
				return !_userConsentContext.CmpEverShown && _userConsentSettings.IsCmpCountry(_userConsentContext.UsercentricsReadyStatus.location.countryCode);
			}
		}

		private UserConsentManager()
		{
			_userConsentSettings = Resources.Load<UserConsentSettings>(GdprSettingsFileName);
			_userConsentLocalization = Resources.Load<UserConsentLocalization>(GdprLocalizationFileName);
			_userConsentContext = new UserConsentContext();
			if (_userConsentSettings != null) CheckSteps();
		}

		private void CheckSteps()
		{
			_gdprUcmStep = _userConsentSettings.Steps.Find(step => step is GdprStep) as GdprStep;
			_cmpUcmStep = _userConsentSettings.Steps.Find(step => step is CmpStep) as CmpStep;
		}

		public void StartFlow(Action onFlowFinished)
		{
			StartFlowInternal(onFlowFinished);
		}

		public void CheckATTStatus(Action<ATTManagerAuthorizationStatus> statusCallback)
		{
			ATTDialog.GetAttStatus(statusCallback);
		}

		public async Task ShowAtt()
		{
			if (_userConsentSettings?.PrewarmedAttUcmStep != null) await _userConsentSettings.PrewarmedAttUcmStep.DoStep(_userConsentSettings, _userConsentLocalization, _userConsentContext);
		}

		public async Task ShowGdprUnityUI(bool fromSettings)
		{
			if (_gdprUcmStep != null) await _gdprUcmStep.DoStep(_userConsentSettings, _userConsentLocalization, _userConsentContext);
		}

		public async Task ShowCmpFromSettings()
		{
			if (_userConsentSettings?.SettingsCmpStep != null) await _userConsentSettings.SettingsCmpStep.DoStep(_userConsentSettings, _userConsentLocalization, _userConsentContext);
		}

		public async Task TryShowCmpIfNeverShown()
		{
			if (NeedShowCmpButton() && _cmpUcmStep != null) await _cmpUcmStep.DoStep(_userConsentSettings, _userConsentLocalization, _userConsentContext);
		}

		public bool NeedShowCmpButton()
		{
			return CmpIsReadyButNotShown;
		}

		private async void StartFlowInternal(Action flowFinished)
		{
			if (_userConsentSettings == null || _userConsentContext == null) { flowFinished?.Invoke(); return; }
			foreach (BaseStep step in _userConsentSettings.Steps)
				if (step != null && step.IsStepReadyToDo(_userConsentContext)) await step.DoStep(_userConsentSettings, _userConsentLocalization, _userConsentContext);
			flowFinished?.Invoke();
		}

		public static bool IsIosAttVersion()
		{
			return false;
		}

		public void RemoveGdprConsent()
		{
			_userConsentContext.GdprConsent = false;
		}

		public static void Log(string message)
		{
			UnityEngine.Debug.Log("[UCM] " + message);
		}

		public static void LogWarning(string message)
		{
			UnityEngine.Debug.LogWarning("[UCM] " + message);
		}

		public static void LogError(string message)
		{
			UnityEngine.Debug.LogError("[UCM] " + message);
		}
	}
}
