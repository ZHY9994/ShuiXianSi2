using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerPool
{
	public string PoolName = string.Empty;

	public GameObject SpawnObj;

	private readonly Stack AvailableObjects = new Stack();

	private readonly List<GameObject> SpawnedObjects = new List<GameObject>();

	public SpawnerPool(string poolName, GameObject objToSpawn)
	{
		PoolName = poolName;
		SpawnObj = objToSpawn;
	}

	public GameObject Spawn(Vector3 position, Quaternion rotation)
	{
		return Spawn(position, rotation, null);
	}

	public GameObject Spawn(Vector3 position, Quaternion rotation, Action<GameObject> spawnedAction)
	{
		GameObject spawned;
		if (AvailableObjects.Count > 0)
		{
			spawned = (GameObject)AvailableObjects.Pop();
			spawned.transform.position = position;
			spawned.transform.rotation = rotation;
		}
		else
		{
			spawned = UnityEngine.Object.Instantiate(SpawnObj, position, rotation);
			spawned.name = string.Format("{0} ({1})", PoolName, SpawnedObjects.Count);
			SpawnedObjects.Add(spawned);
		}
		spawned.SetActive(true);
		spawnedAction?.Invoke(spawned);
		return spawned;
	}

	public void Prespawn(int count)
	{
		List<GameObject> spawned = new List<GameObject>(count);
		for (int i = 0; i < count; i++)
		{
			spawned.Add(Spawn(Vector3.zero, Quaternion.identity, null));
		}
		foreach (GameObject go in spawned)
		{
			UnitySpawningExtensions.Despawn(go);
		}
	}

	public void Despawn(GameObject go)
	{
		if (go == null || AvailableObjects.Contains(go)) return;
		AvailableObjects.Push(go);
		go.SetActive(false);
	}

	public void DespawnList(List<GameObject> goObjList)
	{
		if (goObjList == null || goObjList.Count == 0) return;
		foreach (GameObject go in goObjList)
		{
			Despawn(go);
		}
	}

	public void DespawnAll()
	{
		DespawnList(SpawnedObjects);
	}

	public void ClearPool()
	{
		DespawnAll();
		AvailableObjects.Clear();
		SpawnedObjects.Clear();
	}

	public List<GameObject> GetActiveSpawns()
	{
		return SpawnedObjects.Where(UnityGoExtensions.IsActive).ToList();
	}
}
