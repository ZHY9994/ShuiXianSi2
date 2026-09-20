using System;
using System.Collections.Generic;
using KobGamesSDKSlim.Analytics.EventsData;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	public static class ModuleUtils
	{
		private static readonly Color m_DebugColor;

		private static readonly string m_DebugAnalyticsTitle;

		public static void DebugModuleInitialization(eAnalyticsServices i_Service, List<EventNameObjectPair> i_Details = null)
		{
			Debug.Log(addColorToLog($"{m_DebugAnalyticsTitle ?? "Analytics"}: initialized {i_Service}. {paramsToLog(i_Details)}"));
		}

		public static void DebugAnalyticsEvent(eAnalyticsServices i_Service, string i_EventName, List<EventNameObjectPair> i_Details)
		{
			Debug.Log(addColorToLog($"{m_DebugAnalyticsTitle ?? "Analytics"}: {i_Service}/{i_EventName}. {paramsToLog(i_Details)}"));
		}

		private static string addColorToLog(string i_Log)
		{
			return $"<color=#{ColorUtility.ToHtmlStringRGB(m_DebugColor)}>{i_Log}</color>";
		}

		public static string paramsToLog(List<EventNameObjectPair> i_Elements)
		{
			if (i_Elements == null || i_Elements.Count == 0) return string.Empty;
			return string.Join(", ", i_Elements.FindAll(x => x != null).ConvertAll(x => $"{x.OriginalName}={x.OriginalValue}"));
		}

		private static string paramsToLog(params Tuple<string, object>[] i_KeyPairs)
		{
			if (i_KeyPairs == null || i_KeyPairs.Length == 0) return string.Empty;
			return string.Join(", ", Array.ConvertAll(i_KeyPairs, x => $"{x.Item1}={x.Item2}"));
		}

		public static Dictionary<string, string> StringObjectPairToDictionaryStringString(List<EventNameObjectPair> i_KeyPairs)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			if (i_KeyPairs == null) return result;
			foreach (EventNameObjectPair pair in i_KeyPairs)
				if (pair != null && !string.IsNullOrEmpty(pair.ConvertedName)) result[pair.ConvertedName] = pair.ConvertedValue?.ToString();
			return result;
		}

		public static Dictionary<string, object> StringObjectPairToDictionaryStringObject(List<EventNameObjectPair> i_KeyPairs, bool i_FromGA = false)
		{
			Dictionary<string, object> result = new Dictionary<string, object>();
			if (i_KeyPairs == null) return result;
			foreach (EventNameObjectPair pair in i_KeyPairs)
				if (pair != null && !string.IsNullOrEmpty(pair.ConvertedName)) result[pair.ConvertedName] = pair.ConvertedValue;
			return result;
		}

		public static List<EventNameObjectPair> TupleToStringObjectPair(Tuple<string, object>[] i_Tuples)
		{
			List<EventNameObjectPair> result = new List<EventNameObjectPair>();
			if (i_Tuples == null) return result;
			foreach (Tuple<string, object> tuple in i_Tuples)
				if (tuple != null) result.Add(EventNameObjectPair.CreatePooled(tuple.Item1, tuple.Item2));
			return result;
		}

		public static void ConvertEventData(ref string i_EventName, ref List<EventNameObjectPair> i_Details, in EventsSO i_EventsConsts, eAnalyticsServices i_Service, ePartners i_Partner)
		{
			if (i_EventsConsts == null) throw new System.ArgumentNullException(nameof(i_EventsConsts));
			i_EventName = i_EventsConsts.ConvertEntryName(i_EventName, i_Service, i_Partner);
			if (i_Details == null) return;
			foreach (EventNameObjectPair pair in i_Details)
				if (pair != null) i_EventsConsts.ConvertStringObjectPair(pair, i_Service, i_Partner);
		}
	}
}
