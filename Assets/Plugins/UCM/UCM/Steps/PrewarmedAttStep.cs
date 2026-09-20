using System.Threading.Tasks;
using UnityEngine;

namespace UCM.Steps
{
	[CreateAssetMenu(menuName = "Azur UCM/Steps/Prewarmed ATT step", fileName = "Prewarmed Att step")]
	public class PrewarmedAttStep : AttStep
	{
		private ATTManagerAuthorizationStatus _attConsent;

		public override bool IsStepReadyToDo(UserConsentContext context)
		{
			return context != null && UserConsentManager.IsIosAttVersion() && !context.AttEverAnswered;
		}

		public override async Task DoStep(UserConsentSettings settings, UserConsentLocalization localization, UserConsentContext context)
		{
			if (context == null || context.AttEverAnswered)
				return;

			if (await PrewarmLogic(context))
			{
				if (_attConsent == 0)
					await base.DoStep(settings, localization, context);
				else
					context.SetAttAnswered();
			}
		}

		private async Task<bool> PrewarmLogic(UserConsentContext context)
		{
			_attConsent = 0;
			bool gotConsent = false;
			ATTDialog.CallATTrackingDialog(status =>
			{
				_attConsent = status;
				gotConsent = true;
			});

			UserConsentManager.Log("ATT Step: get att status...");
			while (!gotConsent)
				await Task.Yield();

			UserConsentManager.Log($"ATT Step: status received: {_attConsent}");
			if (_attConsent == ATTManagerAuthorizationStatus.Authorised || _attConsent == ATTManagerAuthorizationStatus.Restricted ||
				(_attConsent == ATTManagerAuthorizationStatus.Denied && context.AttEverAnswered))
				return false;

			UserConsentManager.Log("Fake prewarm");
			return true;
		}
	}
}
