using UnityEngine;
using KobGamesSDKSlim;

public class GlassWallObstacle : Obstacle
{
	protected override void OnObstacleHit(Collision i_Other)
	{
		if (!i_Other.gameObject.CompareTag("Throwable"))
			return;
		SoundManager.Instance.PlayGeneralSFXByType(eSFXType.BrokenGlass);
		PoolManager.Instance.Dequeue(ePoolType.FracturedGlass, transform.position);
		m_IsTriggered = true;
		gameObject.SetActive(false);
	}
}
