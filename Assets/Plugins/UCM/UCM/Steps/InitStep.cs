using System.Threading.Tasks;
using Unity.Usercentrics;
using UnityEngine;

namespace UCM.Steps
{
	[CreateAssetMenu(menuName = "Azur UCM/Steps/Init step", fileName = "Init step")]
	public class InitStep : BaseStep
	{
		public override bool IsStepReadyToDo(UserConsentContext context)
		{
			return context != null && context.UsercentricsReadyStatus == null;
		}

		public override async Task DoStep(UserConsentSettings settings, UserConsentLocalization localization, UserConsentContext context)
		{
			if (context == null || Usercentrics.Instance == null)
				return;

			if (settings == null || string.IsNullOrEmpty(settings.UsercentricsId))
			{
				UserConsentManager.LogError("Usercentrics settings ID is not available in User consent settings!");
				return;
			}

			Usercentrics.Instance.SettingsID = settings.UsercentricsId;
			await Task.Yield();
			bool completed = false;
			Usercentrics.Instance.Initialize(
				status => { context.UsercentricsReadyStatus = status; completed = true; },
				error => { UserConsentManager.LogWarning("Usercentrics initialization error: " + error); context.CmpInitializationError = true; completed = true; });
			while (!completed)
				await Task.Yield();
		}
	}
}
