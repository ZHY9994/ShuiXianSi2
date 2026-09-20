using System.Collections.Generic;
using MrWayne;
using UnityEngine;
using KobGamesSDKSlim;

public class FightVFX : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem m_Particle;

	[SerializeField]
	private float m_HitTime = 1.5f;

	private List<UnitAnimator> m_Controllers = new List<UnitAnimator>();

	private float m_HitTimer;

	private void OnEnable()
	{
		m_HitTimer = 0f;
		Invoke(nameof(returnToPool), m_Particle.main.duration - 1f);
	}

	private void OnTriggerEnter(Collider i_Other)
	{
		if (!i_Other.gameObject.CompareTag("Player"))
			return;
		UnitAnimator controller = i_Other.GetComponent<UnitAnimator>();
		if (!m_Controllers.Contains(controller))
			m_Controllers.Add(controller);
	}

	private void OnTriggerStay(Collider i_Other)
	{
		if (!i_Other.gameObject.CompareTag("Player"))
			return;
		m_HitTimer += Time.deltaTime;
		if (m_HitTimer > m_HitTime)
		{
			UnitAnimator controller = i_Other.GetComponent<UnitAnimator>();
			if (controller != null && m_Controllers.Contains(controller))
			{
				controller.UnitBase.Physics.TakeDamage(10f, eBodyPart.All, eInteractableType.Weapon);
				m_HitTimer = 0f;
			}
		}
	}

	private void OnTriggerExit(Collider i_Other)
	{
		if (!i_Other.gameObject.CompareTag("Player"))
			return;
		UnitAnimator controller = i_Other.GetComponent<UnitAnimator>();
		if (m_Controllers.Contains(controller))
			m_Controllers.Remove(controller);
	}

	private void returnToPool()
	{
		PoolManager.Instance.Queue(ePoolType.FightVFX, gameObject);
		GameplayController.Instance.SetNextTurnDelay();
	}
}
