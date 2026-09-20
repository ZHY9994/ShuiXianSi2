using System.Collections.Generic;
using UnityEngine;

namespace UCM
{
	[CreateAssetMenu(fileName = "UTUDLocalizationSettings", menuName = "Azur UCM/User Tracking Usage Description Localization")]
	public class UTUDLocalizationSettings : ScriptableObject
	{
		[SerializeField]
		private List<UTUDLocalization> _localizations;

		public List<UTUDLocalization> Localization => _localizations;
	}
}
