using DG.Tweening;
using System.Linq;
using KobGamesSDKSlim;
using MrWayne;
using TMPro;
using UnityEngine;

public class TopUiHealthController : MonoBehaviour
{
	[SerializeField]
	private ProgressBar m_PlayerHealthBar;

	[SerializeField]
	private ProgressBar m_PlayerHealthBarDamage;

	[SerializeField]
	private ProgressBar m_EnemyHealthBar;

	[SerializeField]
	private ProgressBar m_EnemyHealthBarDamage;

	[SerializeField]
	private TextMeshProUGUI m_PlayerUnitCounter;

	[SerializeField]
	private TextMeshProUGUI m_EnemyUnitCounter;

	[SerializeField]
	private float m_DamageFillDelay;

	[SerializeField]
	private float m_DamageFillDuration;

	[SerializeField]
	private Ease m_DamageFillEase;

	private float m_PlayerTotalHealth;

	private float m_EnemyTotalHealth;

	private Tween m_DamageTween;

	private void OnEnable()
	{
		UnitHealth.OnHealthChanged += onHealthChanged;
		UnitHealth.OnUnitDeath += onUnitDeath;
		init();
	}

	private void OnDisable()
	{
		UnitHealth.OnHealthChanged -= onHealthChanged;
		UnitHealth.OnUnitDeath -= onUnitDeath;
	}

	private void init()
	{
		m_PlayerTotalHealth = UnitManager.Instance.PlayerUnits.Sum(x => x.Health.TotalHealth);
		m_EnemyTotalHealth = UnitManager.Instance.EnemyUnits.Sum(x => x.Health.TotalHealth);
		onHealthChanged();
		updateUnitCounters();
	}

	private void onHealthChanged()
	{
		float playerHealth = UnitManager.Instance.AlivePlayerUnits.Sum(x => x.Health.Health);
		float enemyHealth = UnitManager.Instance.AliveEnemyUnits.Sum(x => x.Health.Health);
		m_PlayerHealthBar.SetProgress(playerHealth / m_PlayerTotalHealth);
		m_EnemyHealthBar.SetProgress(enemyHealth / m_EnemyTotalHealth);
		float playerDamageStartValue = m_PlayerHealthBarDamage.Value;
		float enemyDamageStartValue = m_EnemyHealthBarDamage.Value;
		if (m_DamageTween != null)
		{
			m_DamageTween.Kill();
		}
		m_DamageTween = DOVirtual.Float(0f, 1f, m_DamageFillDuration, value =>
		{
			float t = Mathf.Clamp01(value);
			m_PlayerHealthBarDamage.SetProgress(Mathf.Lerp(playerDamageStartValue, m_PlayerHealthBar.Value, t));
			m_EnemyHealthBarDamage.SetProgress(Mathf.Lerp(enemyDamageStartValue, m_EnemyHealthBar.Value, t));
		})
			.SetDelay(m_DamageFillDelay)
			.SetEase(m_DamageFillEase);
	}

	private void onUnitDeath(UnitBase i_Unit)
	{
		updateUnitCounters();
	}

	private void updateUnitCounters()
	{
		m_PlayerUnitCounter.text = UnitManager.Instance.AlivePlayerUnits.Length.ToString();
		m_EnemyUnitCounter.text = UnitManager.Instance.AliveEnemyUnits.Length.ToString();
	}
}
