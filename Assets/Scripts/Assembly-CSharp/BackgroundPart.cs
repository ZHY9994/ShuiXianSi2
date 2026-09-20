using KobGamesSDKSlim;
using UnityEngine;

public class BackgroundPart : MonoBehaviour
{
	[SerializeField]
	private Background.eBackgroundPart m_BGPart;

	private Vector3 m_LastPosition;

	private float m_CurrentSpeed;

	private Transform m_PlayerPos;

	private float m_LastZ;

	private GamePlayVariablesEditor.BackGroundParameter m_Parameters => GameConfig.Instance.GamePlay.BackgroundParameters;

	public Background.eBackgroundPart Part => m_BGPart;

	private void OnEnable()
	{
		setCurrentSpeed();
	}

	private void OnDisable()
	{
	}

	public void MovePart()
	{
		float lastZ = m_LastPosition.z;
		float playerZ = m_PlayerPos.position.z;
		float currentSpeed = m_CurrentSpeed;
		Transform transform = base.transform;
		Vector3 position = transform.position;
		float smoothDeltaTime = Time.smoothDeltaTime;
		float interpolation = smoothDeltaTime * m_Parameters.LerpSpeed;
		if (interpolation > 1f)
		{
			interpolation = 1f;
		}
		if (interpolation < 0f)
		{
			interpolation = 0f;
		}

		Vector3 newPosition;
		newPosition.x = position.x + (0f - position.x) * interpolation;
		newPosition.z = position.z + (currentSpeed * (lastZ - playerZ) - position.z) * interpolation;
		newPosition.y = position.y + (0f - position.y) * interpolation;
		transform.position = newPosition;
	}

	private void setCurrentSpeed()
	{
		m_CurrentSpeed = m_Parameters.BackgroundData.GetPartSpeedCoefficient(Part);
	}

	public BackgroundPart()
	{
	}
}
