using KobGamesSDKSlim;
using MrWayne;
using System.Linq;
using UnityEngine;

public class Level : LevelBase
{
	[SerializeField]
	private UnitSpawn[] m_AllUnitSpawns;

	public UnitSpawn[] AllUnitSpawn => m_AllUnitSpawns;
	public UnitSpawn GetSpawn(eUnityType i_Type)
	{
		return m_AllUnitSpawns.FirstOrDefault(x => x.Type == i_Type);
	}

	protected override void OnValidate()
	{
		base.OnValidate();
		SetRefs();
	}

	private void SetRefs()
	{
		m_AllUnitSpawns = GetComponentsInChildren<UnitSpawn>();
	}

	public void RandomizeWeights()
	{
		foreach (UnitSpawn spawn in m_AllUnitSpawns) spawn.RandomizeWeight();
	}
}
