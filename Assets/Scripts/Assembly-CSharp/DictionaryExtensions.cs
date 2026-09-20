using System.Collections.Generic;
using System.Linq;

public static class DictionaryExtensions
{
	public static bool AddIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
	{
		// [未翻译] IDA has no emitted native symbol for this generic instantiation.
		if (dict.ContainsKey(key))
			return false;
		dict.Add(key, value);
		return true;
	}

	public static bool AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
	{
		dict[key] = value;
		return true;
	}

	public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
	{
		dict[key] = value;
	}

	public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, List<KeyValuePair<TKey, TValue>> kvpList)
	{
		foreach (KeyValuePair<TKey, TValue> kvp in kvpList)
			dict.AddOrReplace(kvp.Key, kvp.Value);
	}

	public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
	{
		return groupings.ToDictionary(group => group.Key, group => group.ToList());
	}
}
