using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public enum UsercentricsAnalyticsEventType
	{
		CmpShown = 0,
		AcceptAllFirstLayer = 1,
		DenyAllFirstLayer = 2,
		SaveFirstLayer = 3,
		AcceptAllSecondLayer = 4,
		DenyAllSecondLayer = 5,
		SaveSecondLayer = 6,
		ImprintLink = 7,
		MoreInformationLink = 8,
		PrivacyPolicyLink = 9,
		CcpaTogglesOn = 10,
		CcpaTogglesOff = 11
	}
}
