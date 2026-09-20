using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class TCFVendor
	{
		public List<IdAndName> features;

		public List<IdAndName> flexiblePurposes;

		public int id;

		public List<IdAndName> legitimateInterestPurposes;

		public string name;

		public string policyUrl;

		public List<IdAndName> purposes;

		public List<TCFVendorRestriction> restrictions;

		public List<IdAndName> specialFeatures;

		public List<IdAndName> specialPurposes;

		public bool showConsentToggle;

		public bool showLegitimateInterestToggle;

		public bool usesNonCookieAccess;

		public string deviceStorageDisclosureUrl;

		public bool usesCookies;

		public bool cookieRefresh;

		public bool dataSharedOutsideEU;

		public DataRetention dataRetention;

		public List<IdAndName> dataCategories;

		public List<VendorUrl> vendorUrls;

		public string _consent;

		public string _legitimateInterestConsent;

		public string _cookieMaxAgeSeconds;

		public bool? consent => StringParser.ToBool(_consent);

		public bool? legitimateInterestConsent => StringParser.ToBool(_legitimateInterestConsent);

		public double? cookieMaxAgeSeconds => StringParser.ToDouble(_cookieMaxAgeSeconds);
	}
}
