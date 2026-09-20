using MrWayne;
using UnityEngine;

public class UnitCollisionIgnore : UnitComponent
{
	[SerializeField]
	private Collider[] m_AllUnitColliders;

	public void IgnoreCollision(Collider i_Collider)
	{
		foreach (Collider collider in m_AllUnitColliders)
			Physics.IgnoreCollision(collider, i_Collider, true);
	}

	protected override void SetRefs()
	{
		base.SetRefs();
		m_AllUnitColliders = GetComponentsInChildren<Collider>();
	}
}
