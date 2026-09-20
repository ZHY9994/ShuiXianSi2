using UnityEngine;

namespace MrWayne
{
	public class UnitSpawn : MonoBehaviour
	{
		public eUnityType Type;

		[Range(1f, 10f)]
		public int Weight = 1;

		public AISkinData AISkin;

		public void RandomizeWeight()
		{
			Weight = Random.Range(1, 11);
		}
	}
}
