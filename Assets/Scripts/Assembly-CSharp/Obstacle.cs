using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField]
	protected Rigidbody m_Body;

	protected bool m_IsTriggered;

	private void OnCollisionEnter(Collision i_Other)
	{
		if (!m_IsTriggered)
			OnObstacleHit(i_Other);
	}

	protected virtual void OnObstacleHit(Collision i_Other)
	{
	}

	protected virtual void SetRefs()
	{
		m_Body = GetComponent<Rigidbody>();
	}
}
