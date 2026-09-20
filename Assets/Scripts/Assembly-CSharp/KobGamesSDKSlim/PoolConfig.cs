using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class PoolConfig
	{
		public bool UseList;

		public GameObject Prefab;

		public List<GameObject> PrefabList;

		public int InitialQuantity;

		public PoolConfig()
		{
		}
	}
}
