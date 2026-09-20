using UnityEngine;

public class CollisionCaller : MonoBehaviour
{
	private ICollisionListener _Listener;

	private void OnEnable()
	{
		_Listener = GetComponentInParent<ICollisionListener>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision == null || collision.gameObject.TryGetComponent<CollisionCaller>(out _))
			return;
		_Listener.OnCollisionEnter(collision);
	}
}
