using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

public static class LocalizationUtility
{
	public static string LocalizationTableName => "Who_Dies_First_Table";

	public static string WORD => "word_";

	public static string WEAPONNAME => "weapon_name_";

	public static string TUTORIALSTEP => "tutorial_step";

	private const string LocalizationResourcesPath = "Localization/Tables";
	private const string PreferredLocalePrefsKey = "Localization.LocaleCode";
	private const string ForcedLocaleCode = "zh-Hans";

	private static SharedTableData s_SharedTableData;
	private static readonly Dictionary<string, StringTable> s_TableByLocaleCode =
		new Dictionary<string, StringTable>(StringComparer.OrdinalIgnoreCase);
	private static bool s_IsInitialized;
	private static string s_SelectedLocaleCode;

	public static void Initialize()
	{
		ensureInitialized();
	}

	public static eLocaleType GetLocalType()
	{
		string locale = GetSelectedLocaleCode();
		if (locale == "ja") return eLocaleType.JA;
		if (locale == "ko") return eLocaleType.KO;
		if (locale.StartsWith("zh", StringComparison.OrdinalIgnoreCase)) return eLocaleType.ZH;
		return eLocaleType.EN;
	}

	public static string GetWordLocalizedString(string i_String)
	{
		return GetLocalizedString(eLocalKeyType.Word, i_String);
	}

	public static string GetWeaponLocalizedString(string i_String)
	{
		return GetLocalizedString(eLocalKeyType.WeaponName, i_String.Replace(" ", "_").ToLowerInvariant());
	}

	public static string GetLocalizedString(LocalizedString i_LocalizedString, IList<UnityEngine.Object> i_Arguments = null)
	{
		ensureInitialized();

		if (i_LocalizedString == null || i_LocalizedString.IsEmpty)
			return string.Empty;

		string key = resolveEntryKey(i_LocalizedString.TableEntryReference);
		if (string.IsNullOrEmpty(key))
			return string.Empty;

		return GetLocalizedString(key, i_Arguments);
	}

	public static string GetLocalizedString(eLocalKeyType i_Type, string i_String)
	{
		return GetLocalizedString(getLocaleKeyString(i_Type) + i_String);
	}

	public static string GetLocalizedString(string i_Key, IList<UnityEngine.Object> i_Arguments)
	{
		string localized = GetLocalizedString(i_Key);
		if (string.IsNullOrEmpty(localized) || i_Arguments == null || i_Arguments.Count == 0)
			return localized;

		try
		{
			return string.Format(localized, i_Arguments.ToArray());
		}
		catch (FormatException)
		{
			return localized;
		}
	}

	public static string GetLocalizedString(string i_Key)
	{
		ensureInitialized();

		StringTable table = GetActiveTable();
		if (table == null)
			return i_Key;

		StringTableEntry entry = table.GetEntry(i_Key);
		if (entry == null)
			return i_Key;

		string localized = entry.LocalizedValue;
		return localized ?? string.Empty;
	}

	public static string GetSelectedLocaleCode()
	{
		ensureInitialized();
		return s_SelectedLocaleCode ?? "en";
	}

	private static void ensureInitialized()
	{
		if (s_IsInitialized)
			return;

		s_IsInitialized = true;
		loadTables();
		selectLocale();
	}

	private static void loadTables()
	{
		s_SharedTableData = Resources.Load<SharedTableData>($"{LocalizationResourcesPath}/{LocalizationTableName} Shared Data");
		s_TableByLocaleCode.Clear();

		foreach (StringTable table in Resources.LoadAll<StringTable>(LocalizationResourcesPath))
		{
			if (table == null)
				continue;

			string code = getLocaleCodeFromTableName(table.name);
			if (string.IsNullOrEmpty(code))
				continue;

			s_TableByLocaleCode[code] = table;
		}

		if (s_SharedTableData == null)
		{
			StringTable sampleTable = s_TableByLocaleCode.Values.FirstOrDefault();
			if (sampleTable != null)
				s_SharedTableData = sampleTable.SharedData;
		}
	}

	private static void selectLocale()
	{
		if (isSupportedLocale(ForcedLocaleCode))
		{
			s_SelectedLocaleCode = ForcedLocaleCode;
			return;
		}

		if (isSupportedLocale("zh-Hant"))
		{
			s_SelectedLocaleCode = "zh-Hant";
			return;
		}

		string preferredCode = normalizeLocaleCode(PlayerPrefs.GetString(PreferredLocalePrefsKey, string.Empty));
		if (isSupportedLocale(preferredCode))
		{
			s_SelectedLocaleCode = preferredCode;
			return;
		}

		string systemCode = getSystemLocaleCode();
		if (isSupportedLocale(systemCode))
		{
			s_SelectedLocaleCode = systemCode;
			return;
		}

		if (systemCode != null && systemCode.StartsWith("zh", StringComparison.OrdinalIgnoreCase))
		{
			if (s_TableByLocaleCode.ContainsKey("zh-Hans"))
			{
				s_SelectedLocaleCode = "zh-Hans";
				return;
			}

			if (s_TableByLocaleCode.ContainsKey("zh-Hant"))
			{
				s_SelectedLocaleCode = "zh-Hant";
				return;
			}
		}

		if (s_TableByLocaleCode.ContainsKey("en"))
		{
			s_SelectedLocaleCode = "en";
			return;
		}

		s_SelectedLocaleCode = s_TableByLocaleCode.Keys.FirstOrDefault();
	}

	private static bool isSupportedLocale(string code)
	{
		return !string.IsNullOrEmpty(code) && s_TableByLocaleCode.ContainsKey(code);
	}

	private static StringTable GetActiveTable()
	{
		ensureInitialized();

		if (!string.IsNullOrEmpty(s_SelectedLocaleCode)
			&& s_TableByLocaleCode.TryGetValue(s_SelectedLocaleCode, out StringTable table))
		{
			return table;
		}

		if (s_TableByLocaleCode.TryGetValue("en", out table))
			return table;

		return s_TableByLocaleCode.Values.FirstOrDefault();
	}

	private static string getLocaleCodeFromTableName(string tableName)
	{
		if (string.IsNullOrEmpty(tableName))
			return null;

		string prefix = LocalizationTableName + "_";
		if (!tableName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			return null;

		return normalizeLocaleCode(tableName.Substring(prefix.Length));
	}

	private static string getSystemLocaleCode()
	{
		switch (Application.systemLanguage)
		{
			case SystemLanguage.Japanese:
				return "ja";
			case SystemLanguage.Korean:
				return "ko";
			case SystemLanguage.ChineseSimplified:
				return "zh-Hans";
			case SystemLanguage.ChineseTraditional:
				return "zh-Hant";
			default:
				return "en";
		}
	}

	private static string normalizeLocaleCode(string code)
	{
		if (string.IsNullOrWhiteSpace(code))
			return null;

		return code.Trim();
	}

	private static string resolveEntryKey(TableEntryReference entryReference)
	{
		if (entryReference.ReferenceType == TableEntryReference.Type.Name)
			return entryReference.Key;

		if (entryReference.ReferenceType == TableEntryReference.Type.Id && s_SharedTableData != null)
			return entryReference.ResolveKeyName(s_SharedTableData);

		return null;
	}

	private static string getLocaleKeyString(eLocalKeyType i_Type)
	{
		if (i_Type == eLocalKeyType.TutorialStep) return TUTORIALSTEP;
		if (i_Type == eLocalKeyType.WeaponName) return WEAPONNAME;
		return i_Type == eLocalKeyType.Word ? WORD : string.Empty;
	}
}
