using System.Collections.Generic;
using UnityEngine;

public class CFX_SpawnSystem : MonoBehaviour
{
	private static CFX_SpawnSystem instance;

	public GameObject[] objectsToPreload = new GameObject[0];

	public int[] objectsToPreloadTimes = new int[0];

	public bool hideObjectsInHierarchy;

	public bool spawnAsChildren = true;

	public bool onlyGetInactiveObjects;

	public bool instantiateIfNeeded;

	private bool allObjectsLoaded;

	private Dictionary<int, List<GameObject>> instantiatedObjects = new Dictionary<int, List<GameObject>>();

	private Dictionary<int, int> poolCursors = new Dictionary<int, int>();

	public static bool AllObjectsLoaded => instance.allObjectsLoaded;

	public static GameObject GetNextObject(GameObject sourceObj, bool activateObject = true)
	{
		if (sourceObj == null || instance == null)
		{
			return null;
		}
		int uniqueId = sourceObj.GetInstanceID();
		if (!instance.instantiatedObjects.ContainsKey(uniqueId))
		{
			return null;
		}
		List<GameObject> objects = instance.instantiatedObjects[uniqueId];
		int cursor = instance.poolCursors[uniqueId];
		GameObject result = null;
		if (!instance.onlyGetInactiveObjects)
		{
			result = objects[cursor];
			instance.increasePoolCursor(uniqueId);
		}
		else
		{
			int start = cursor;
			do
			{
				result = objects[cursor];
				instance.increasePoolCursor(uniqueId);
				cursor = instance.poolCursors[uniqueId];
				if (result != null && !result.activeSelf)
				{
					break;
				}
				result = null;
			}
			while (cursor != start);
			if (result == null && instance.instantiateIfNeeded)
			{
				PreloadObject(sourceObj, 1);
				result = instance.instantiatedObjects[uniqueId][instance.instantiatedObjects[uniqueId].Count - 1];
			}
		}
		if (activateObject && result != null)
		{
			result.SetActive(true);
		}
		return result;
	}

	public static void PreloadObject(GameObject sourceObj, int poolSize = 1)
	{
		instance.addObjectToPool(sourceObj, poolSize);
	}

	public static void UnloadObjects(GameObject sourceObj)
	{
		instance.removeObjectsFromPool(sourceObj);
	}

	private void addObjectToPool(GameObject sourceObject, int number)
	{
		if (sourceObject == null)
		{
			return;
		}
		int uniqueId = sourceObject.GetInstanceID();
		if (!instantiatedObjects.ContainsKey(uniqueId))
		{
			instantiatedObjects.Add(uniqueId, new List<GameObject>());
			poolCursors.Add(uniqueId, 0);
		}
		for (int i = 0; i < number; i++)
		{
			GameObject clone = Instantiate(sourceObject);
			clone.SetActive(false);
			foreach (CFX_AutoDestructShuriken component in clone.GetComponentsInChildren<CFX_AutoDestructShuriken>(true))
			{
				component.OnlyDeactivate = true;
			}
			foreach (CFX_LightIntensityFade component in clone.GetComponentsInChildren<CFX_LightIntensityFade>(true))
			{
				component.autodestruct = false;
			}
			instantiatedObjects[uniqueId].Add(clone);
			if (hideObjectsInHierarchy)
			{
				clone.hideFlags = HideFlags.HideInHierarchy;
			}
			if (spawnAsChildren)
			{
				clone.transform.SetParent(transform);
			}
		}
	}

	private void removeObjectsFromPool(GameObject sourceObject)
	{
		if (sourceObject == null)
		{
			return;
		}
		int uniqueId = sourceObject.GetInstanceID();
		if (!instantiatedObjects.ContainsKey(uniqueId))
		{
			return;
		}
		List<GameObject> objects = instantiatedObjects[uniqueId];
		for (int i = objects.Count - 1; i >= 0; i--)
		{
			Destroy(objects[i]);
			objects.RemoveAt(i);
		}
		instantiatedObjects.Remove(uniqueId);
		poolCursors.Remove(uniqueId);
	}

	private void increasePoolCursor(int uniqueId)
	{
		poolCursors[uniqueId]++;
		if (poolCursors[uniqueId] >= instantiatedObjects[uniqueId].Count)
		{
			poolCursors[uniqueId] = 0;
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		allObjectsLoaded = false;
		for (int i = 0; i < objectsToPreload.Length; i++)
		{
			PreloadObject(objectsToPreload[i], objectsToPreloadTimes[i]);
		}
		allObjectsLoaded = true;
	}
}
