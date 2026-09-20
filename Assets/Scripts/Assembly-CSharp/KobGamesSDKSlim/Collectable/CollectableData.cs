using System;
using UnityEngine;

namespace KobGamesSDKSlim.Collectable
{
	[Serializable]
	public class CollectableData
	{
		public int InitialValue;

		public Sprite CollectableSprite;

		public Material ParticlesMaterial;

		public Vector3 ParticlesSize;

		[HideInInspector]
		public eCollectableType Type;

		public int CurrentValue => StorageManager.Instance.GetCollectable(Type);

		public CollectableData()
		{
			ParticlesSize = Vector3.one;
		}
	}
}
