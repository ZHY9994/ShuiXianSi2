using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace UCM.Steps
{
	[CreateAssetMenu(menuName = "Azur UCM/Steps/Gdpr step", fileName = "GDPR step")]
	public class GdprStep : BaseStep
	{
		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CDoStep_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public UserConsentContext context;

			public GdprStep _003C_003E4__this;

			public UserConsentSettings settings;

			public UserConsentLocalization localization;

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
		private struct _003CShowIosNativeDialog_003Ed__2 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

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

		public override bool IsStepReadyToDo(UserConsentContext context)
		{
			return true;
		}

		public override async Task DoStep(UserConsentSettings settings, UserConsentLocalization localization, UserConsentContext context)
		{
			if (context == null || context.GdprEverAnswered) return;
			bool accepted = false;
			GameObject popup = Object.Instantiate(settings.PopupPrefab);
			GDPRWindowController controller = popup.GetComponent<GDPRWindowController>();
			controller.Initialize(() => { accepted = true; context.GdprConsent = true; context.SetAttAnswered(); }, localization.GetGDPRHeaderLocalized(false), localization.GetGDPRDescriptionLocalized(settings.CompanyName, false), localization.GetGDPRTermsButtonTextLocalized(false), localization.GetGDPRPrivacyButtonTextLocalized(false), settings.TermsLink, settings.PrivacyLink, localization.GetAcceptText(false), localization.GetIsHindi(), localization.IsOverflowRequired(), localization.GetIsThai(), settings.CompanyName, settings.CompanyLogo, settings.SharkLogo, localization.GetCurrentLanguage());
			while (!accepted) await Task.Yield();
			Object.Destroy(popup);
		}

		private async Task ShowIosNativeDialog(UserConsentSettings settings, UserConsentLocalization localization, UserConsentContext context)
		{
			await DoStep(settings, localization, context);
		}
	}
}
