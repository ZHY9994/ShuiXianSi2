using System;
using UnityEngine;

namespace UCM
{
	[Serializable]
	public class UserConsentLanguage
	{
		private const string PlaceholderCompany = "<companyname>";

		private const string ArabicIso = "ar";

		private const string ThaiIso = "th";

		private const string HebrewIso = "iw-IL";

		private const string HindiIso = "hi";

		public string languageIso;

		[SerializeField]
		private string GDPRDescription;

		[SerializeField]
		private string GDPRAcceptButton;

		[SerializeField]
		private string GDPRHeader;

		[SerializeField]
		private string GDPRTermsButton;

		[SerializeField]
		private string GDPRPrivacyButton;

		public bool GoesOverflow()
		{
			return IsArabic() || IsHebrew();
		}

		public string Description(string companyName, bool fromSettings)
		{
			if (IsArabic())
			{
				string text = ArabicFixer.Fix(GDPRDescription, false, true);
				return text.Replace(PlaceholderCompany, companyName ?? string.Empty).Trim();
			}
			if (IsHebrew())
				return "ינא ,וז היצקילפאב שומיש ידי לע\nשומישה יאנתל יתמכסה עיבמ\nרשאמו Azur Games לש\n.תויטרפה תוינידמ תא יתארקש".Replace("Azur Games", companyName ?? string.Empty);
			return (GDPRDescription ?? string.Empty).Replace(PlaceholderCompany, companyName ?? string.Empty).Trim();
		}

		public string Accept(bool fromSettings)
		{
			if (IsArabic()) return ArabicFixer.Fix(GDPRAcceptButton, false, true);
			if (IsHebrew()) return TextRtlConverter.SwitchRTL(GDPRAcceptButton);
			return (GDPRAcceptButton ?? string.Empty).Trim();
		}

		public string Header(bool fromSettings)
		{
			if (IsArabic()) return ArabicFixer.Fix(GDPRHeader, false, true);
			if (IsHebrew()) return TextRtlConverter.SwitchRTL(GDPRHeader);
			return (GDPRHeader ?? string.Empty).Trim();
		}

		public string Terms(bool fromSettings)
		{
			if (IsArabic()) return ArabicFixer.Fix(GDPRTermsButton, false, true);
			if (IsHebrew()) return TextRtlConverter.SwitchRTL(GDPRTermsButton);
			return (GDPRTermsButton ?? string.Empty).Trim();
		}

		public string Privacy(bool fromSettings)
		{
			if (IsArabic()) return ArabicFixer.Fix(GDPRPrivacyButton, false, true);
			if (IsHebrew()) return TextRtlConverter.SwitchRTL(GDPRPrivacyButton);
			return (GDPRPrivacyButton ?? string.Empty).Trim();
		}

		public bool FallbackIso(string iso)
		{
			if (string.IsNullOrEmpty(iso) || string.IsNullOrEmpty(languageIso)) return false;
			string[] requested = iso.Split('-');
			string[] current = languageIso.Split('-');
			return requested.Length > 0 && current.Length > 0 && requested[0] == current[0];
		}

		public bool IsArabic()
		{
			return languageIso == ArabicIso;
		}

		public bool IsHindi()
		{
			return languageIso == HindiIso;
		}

		public bool IsHebrew()
		{
			return languageIso == HebrewIso;
		}

		public bool IsThai()
		{
			return languageIso == ThaiIso;
		}
	}
}
