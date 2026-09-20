using UnityEngine;

public class DummyLevel : MonoBehaviour
{
	[SerializeField]
	private Animator m_Animator;

	private void OnEnable()
	{
		randomDance();
	}

	public void SetDummyPosition(Vector3 i_Position)
	{
		m_Animator.transform.position = i_Position;
	}

	private void randomDance()
	{
		if (m_Animator == null)
			return;
		m_Animator.SetTrigger(StaticString.AP_Dance);
		m_Animator.SetInteger(StaticString.AP_DanceType, Random.Range(0, 3));
	}

	private void SetRefs()
	{
		m_Animator = GetComponentInChildren<Animator>();
	}
}
