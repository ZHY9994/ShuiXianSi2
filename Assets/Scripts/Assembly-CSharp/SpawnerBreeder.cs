using System.Collections.Generic;
using UnityEngine;

public class SpawnerBreeder : MonoSingletonExt<SpawnerBreeder>
{
	private static readonly Dictionary<string, SpawnerPool> SpawnPoolDict = new Dictionary<string, SpawnerPool>();

	public static SpawnerPool Create(string poolName, GameObject obj)
	{
		SpawnerPool pool = new SpawnerPool(poolName, obj);
		SpawnPoolDict.Add(poolName, pool);
		return pool;
	}

	public static void Remove(string poolName)
	{
		if (SpawnPoolDict.ContainsKey(poolName))
		{
			SpawnPoolDict.Remove(poolName);
		}
	}

	public static void RemoveAll()
	{
		SpawnPoolDict.Clear();
	}

	public static SpawnerPool Get(string poolName)
	{
		return SpawnPoolDict.ContainsKey(poolName) ? SpawnPoolDict[poolName] : null;
	}
}
