using UnityEngine;

public class SimpleRotatorV2 : MonoBehaviour
{
	public float Speed;

	public Vector3 RotationVector;

	private void Update()
	{
		transform.Rotate(RotationVector * Speed * Time.deltaTime);
	}
}
