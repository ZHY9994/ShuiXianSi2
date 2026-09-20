using System.Collections.Generic;
using UnityEngine;

public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField]
	[HideInInspector]
	protected List<TKey> keyData;

	[SerializeField]
	[HideInInspector]
	protected List<TValue> valueData;

	void ISerializationCallbackReceiver.OnAfterDeserialize()
	{
		Clear();
		if (keyData == null || valueData == null)
			return;
		for (int i = 0; i < keyData.Count; i++)
			this[keyData[i]] = valueData[i];
	}

	void ISerializationCallbackReceiver.OnBeforeSerialize()
	{
		if (keyData == null)
			keyData = new List<TKey>();
		if (valueData == null)
			valueData = new List<TValue>();
		keyData.Clear();
		valueData.Clear();
		foreach (KeyValuePair<TKey, TValue> pair in this)
		{
			keyData.Add(pair.Key);
			valueData.Add(pair.Value);
		}
	}
}
