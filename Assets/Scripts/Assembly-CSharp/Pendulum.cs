using UnityEngine;

public class Pendulum : MonoBehaviour
{
	private float StartTime;

	private Quaternion m_Start;

	private Quaternion m_End;

	private void Start()
	{
		init();
	}

	private void Update()
	{
		StartTime += Time.deltaTime;
		float t = (Mathf.Sin(StartTime + 2f + 1.5708f) + 1f) * 0.5f;
		transform.rotation = Quaternion.Lerp(m_Start, m_End, t);
	}

	private void init()
	{
		m_Start = pendulumSwing(20f);
		m_End = pendulumSwing(-20f);
	}

	private void resetTimer()
	{
		StartTime = 0f;
	}

	private Quaternion pendulumSwing(float i_Angle)
	{
		Vector3 euler = transform.rotation.eulerAngles;
		float x = euler.x + i_Angle;
		if (x > 100f)
		{
			x -= 360f;
		}
		else if (x < -180f)
		{
			x += 360f;
		}
		return Quaternion.Euler(x, euler.y, euler.z);
	}
}
