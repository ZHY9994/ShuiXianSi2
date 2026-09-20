using KobGamesSDKSlim;
using MrWayne;
using System.Linq;
using UnityEngine;

public class EnemyInvisibleWall : MonoBehaviour
{
	[SerializeField]
	private BoxCollider m_BoxCollider;

	private GameConfig m_GameConfig => GameConfig.Instance;

	private void OnEnable()
	{
		m_BoxCollider.enabled = m_GameConfig.GamePlay.ObstacleParameters.MiddleObstacleWall;
		UnitManager.OnUnitSpawned += onUnitSpawn;
	}

	private void OnDisable()
	{
		UnitManager.OnUnitSpawned -= onUnitSpawn;
	}

	private void onUnitSpawn(UnitBase i_Base)
	{
		if (i_Base != null && i_Base.UnitType == eUnityType.AI)
			i_Base.CollisionIgnore.IgnoreCollision(m_BoxCollider);
	}

	private void Update()
	{
		if (UnitManager.Instance == null || UnitManager.Instance.AliveEnemyUnit == null)
			return;
		float nearestZ = UnitManager.Instance.AliveEnemyUnits.Min(x => x.Center.position.z);
		transform.position = Vector3.forward * (nearestZ - m_GameConfig.GamePlay.ObstacleParameters.InvisibleWallOffset);
	}
}
