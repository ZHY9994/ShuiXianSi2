using System.Threading.Tasks;
using UnityEngine;

namespace UCM.Steps
{
	[CreateAssetMenu(menuName = "Azur UCM/Steps/Att step", fileName = "Att step")]
	public class AttStep : BaseStep
	{
		[Tooltip("If true, then ATT step will be skipped. You can call att step yourself later via UserConsentManager.Instance.ShowAtt()")]
		[SerializeField]
		private bool callAttOnlyOnDemand;

		public override bool IsStepReadyToDo(UserConsentContext context)
		{
			return !callAttOnlyOnDemand && UserConsentManager.IsIosAttVersion();
		}

		public override async Task DoStep(UserConsentSettings settings, UserConsentLocalization localization, UserConsentContext context)
		{
			if (!UserConsentManager.IsIosAttVersion())
			{
				await Task.Yield();
				UserConsentManager.Log($"ATT dialog is ignored on {Application.platform}");
				return;
			}

			await Task.Yield();
			ATTDialog.CallATTrackingDialog(null);
		}
	}
}
