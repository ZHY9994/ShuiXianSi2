using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;

public class DOTWeenIDsBase
{
	public const int RateUsRays = -100;

	public const int NoKillOnReset = -90;

	public const int Default = -80;

	public static object[] IdsBase = { RateUsRays, NoKillOnReset };

	private static object[] m_IdsToExcludeFromGameReset;

	public static object[] IdsToExcludeFromGameReset
	{
		get
		{
			if (m_IdsToExcludeFromGameReset == null)
			{
				List<object> ids = new List<object>(IdsBase);
				if (DOTweenIDs.Ids != null)
				{
					ids.AddRange(DOTweenIDs.Ids);
				}
				m_IdsToExcludeFromGameReset = ids.ToArray();
			}
			return m_IdsToExcludeFromGameReset;
		}
	}

	public static ValueDropdownList<int> ValuesDropdownData
	{
		get
		{
			ValueDropdownList<int> result = new ValueDropdownList<int>();
			foreach (FieldInfo field in DotweenIDsFields)
			{
				result.Add(field.Name, (int)field.GetValue(null));
			}
			return result;
		}
	}

	private static FieldInfo[] DotweenIDsFields
	{
		get
		{
			List<FieldInfo> fields = new List<FieldInfo>();
			fields.AddRange(typeof(DOTweenIDs).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly));
			fields.AddRange(typeof(DOTWeenIDsBase).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly));
			fields.RemoveAll(x => x.FieldType != typeof(int));
			return fields.ToArray();
		}
	}
}
