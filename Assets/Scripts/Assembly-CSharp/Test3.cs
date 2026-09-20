using MrWayne;
using UnityEngine;

public class Test3 : MonoBehaviour
{
	private enum eChange
	{
		TransformPoint = 0,
		TransformVector = 1,
		TransformDirection = 2,
		InverseTransformPoint = 3,
		InverseTransformVector = 4,
		InverseTransformDirection = 5
	}

	[SerializeField]
	private Transform m_Player;

	[SerializeField]
	private WeaponBase m_Weapon;

	[SerializeField]
	private Transform m_Target;

	[SerializeField]
	private Vector3 m_Offset;

	[SerializeField]
	private eChange m_Change;

	private void Update()
	{
		run();
	}

	private void run()
	{
		if (m_Player == null || m_Target == null || m_Weapon == null) return;

		Vector3 position = m_Weapon.Center.position;
		switch (m_Change)
		{
			case eChange.TransformPoint:
				m_Offset = m_Player.TransformPoint(position);
				break;
			case eChange.TransformVector:
				m_Offset = m_Player.TransformVector(position);
				break;
			case eChange.TransformDirection:
				m_Offset = m_Player.TransformDirection(position);
				break;
			case eChange.InverseTransformPoint:
				m_Offset = m_Player.InverseTransformPoint(position);
				break;
			case eChange.InverseTransformVector:
				m_Offset = m_Player.InverseTransformVector(position);
				break;
			case eChange.InverseTransformDirection:
				m_Offset = m_Player.InverseTransformDirection(position);
				break;
		}

		m_Target.position = m_Offset;
	}
}
