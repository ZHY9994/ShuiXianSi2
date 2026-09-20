using System.Threading.Tasks;
using UnityEngine;

namespace UCM.Steps
{
	public abstract class BaseStep : ScriptableObject
	{
		public abstract bool IsStepReadyToDo(UserConsentContext context);

		public abstract Task DoStep(UserConsentSettings settings, UserConsentLocalization localization, UserConsentContext context);
	}
}
