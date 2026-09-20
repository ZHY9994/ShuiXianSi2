using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity.Usercentrics;
using UnityEngine;

namespace UCM.Steps
{
	[CreateAssetMenu(menuName = "Azur UCM/Steps/Settings CMP step", fileName = "Settings CMP step")]
	public class SettingsCmpStep : BaseStep
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public UsercentricsConsentUserResponse userResponse;

			internal void _003CDoStep_003Eb__0(UsercentricsConsentUserResponse response)
			{
				userResponse = response;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CDoStep_003Ed__2 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			private _003C_003Ec__DisplayClass2_0 _003C_003E8__1;

			public SettingsCmpStep _003C_003E4__this;

			public UserConsentContext context;

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

		private static bool isShowing;

		public override bool IsStepReadyToDo(UserConsentContext context)
		{
			return false;
		}

		public override async Task DoStep(UserConsentSettings settings, UserConsentLocalization localization, UserConsentContext context)
		{
			if (isShowing || context == null || Usercentrics.Instance == null) return;
			isShowing = true;
			try
			{
				var display = new _003C_003Ec__DisplayClass2_0();
				Usercentrics.Instance.ShowSecondLayer(null, display._003CDoStep_003Eb__0);
				while (display.userResponse == null) await Task.Yield();
				ApplyConsent(display.userResponse.consents, context);
			}
			finally { isShowing = false; }
		}

		private void ApplyConsent(List<UsercentricsServiceConsent> consents, UserConsentContext context)
		{
			if (consents == null || context == null || context.CmpConsents == null) return;
			context.CmpConsents.AddRange(consents);
			context.SetCmpAnswered();
		}
	}
}
