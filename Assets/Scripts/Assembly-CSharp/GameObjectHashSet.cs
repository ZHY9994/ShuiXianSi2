using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObjectHashSet
{
	public HashSet<GameObject> HashSet = new HashSet<GameObject>();

	public GameObjectHashSet(HashSet<GameObject> i_HashSet)
	{
		HashSet = i_HashSet;
	}
}
