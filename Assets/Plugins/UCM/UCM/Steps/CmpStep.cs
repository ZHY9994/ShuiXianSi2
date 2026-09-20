using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity.Usercentrics;
using UnityEngine;

namespace UCM.Steps
{
	[CreateAssetMenu(menuName = "Azur UCM/Steps/CMP step", fileName = "Cmp step")]
	public class CmpStep : BaseStep
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_0
		{
			public UsercentricsConsentUserResponse userResponse;

			internal void _003CDoStep_003Eb__0(UsercentricsConsentUserResponse response)
			{
				userResponse = response;
			}
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CDoStep_003Ed__3 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public CmpStep _003C_003E4__this;

			public UserConsentContext context;

			public UserConsentSettings settings;

			private _003C_003Ec__DisplayClass3_0 _003C_003E8__1;

			private bool _003CskipByTimeout_003E5__2;

			private YieldAwaitable.YieldAwaiter _003C_003Eu__1;

			private TaskAwaiter _003C_003Eu__2;

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

		private const float MaxTimeWaitingCmp = 20f;

		private float _timeWaitingCmpStep;

		public override bool IsStepReadyToDo(UserConsentContext context)
		{
			return true;
		}

		public override async Task DoStep(UserConsentSettings settings, UserConsentLocalization localization, UserConsentContext context)
		{
			if (context == null || Usercentrics.Instance == null) return;
			var display = new _003C_003Ec__DisplayClass3_0();
			_timeWaitingCmpStep = 0f;
			Usercentrics.Instance.ShowFirstLayer(display._003CDoStep_003Eb__0);
			while (display.userResponse == null && _timeWaitingCmpStep < MaxTimeWaitingCmp)
			{
				_timeWaitingCmpStep += 0.1f;
				await Task.Delay(100);
			}
			if (display.userResponse != null) ApplyConsent(display.userResponse.consents, context);
		}

		private void ApplyConsent(List<UsercentricsServiceConsent> consents, UserConsentContext context)
		{
			if (consents == null || context == null || context.CmpConsents == null) return;
			context.CmpConsents.AddRange(consents);
			context.SetCmpAnswered();
		}
	}
}
