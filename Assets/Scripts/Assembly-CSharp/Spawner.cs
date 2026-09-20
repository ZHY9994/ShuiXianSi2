using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoSingletonExt<Spawner>
{
	private static readonly Dictionary<string, SpawnerPool> PoolList = new Dictionary<string, SpawnerPool>();

	public static GameObject CreateSpawn(GameObject go, Vector3 position, Quaternion rotation)
	{
		return CreateSpawn(go, position, rotation, null);
	}

	public static GameObject CreateSpawn(GameObject go, Vector3 position, Quaternion rotation, Action<GameObject> spawnedAction)
	{
		if (go == null) return null;
		string poolName = go.name;
		SpawnerPool pool;
		if (PoolList.ContainsKey(poolName))
		{
			pool = PoolList[poolName];
		}
		else
		{
			pool = SpawnerBreeder.Get(poolName) ?? SpawnerBreeder.Create(poolName, go);
			PoolList.Add(poolName, pool);
		}
		return pool.Spawn(position, rotation, spawnedAction);
	}

	public static void Despawn(GameObject go)
	{
		if (go == null) return;
		string name = go.name;
		int openParen = name.LastIndexOf('(');
		string poolName = name.Substring(0, openParen - 1);
		if (PoolList.ContainsKey(poolName))
		{
			PoolList[poolName].Despawn(go);
		}
		else
		{
			UnityEngine.Object.Destroy(go);
		}
	}

	public static void Prespawn(GameObject go, int count)
	{
		if (go == null) return;
		string poolName = go.name;
		SpawnerPool pool;
		if (PoolList.ContainsKey(poolName))
		{
			pool = PoolList[poolName];
		}
		else
		{
			pool = SpawnerBreeder.Get(poolName) ?? SpawnerBreeder.Create(poolName, go);
			PoolList.Add(poolName, pool);
		}
		pool.Prespawn(count);
	}
}
