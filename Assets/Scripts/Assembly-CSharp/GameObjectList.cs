using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObjectList
{
	public List<GameObject> List = new List<GameObject>();

	public GameObjectList(List<GameObject> i_List)
	{
		List = i_List;
	}
}
