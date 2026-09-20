using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
	public HealthController()
	{
	}

	[SerializeField]
	private Transform m_Target;

	[SerializeField]
	private GameObject m_HealthRoot;

	[SerializeField]
	private Image m_HealthFill;

	[SerializeField]
	private Image m_DamageFill;

	[SerializeField]
	private TextMeshProUGUI m_LevelText;

	[SerializeField]
	private HorizontalLayoutGroup m_HorisontalGroup;

	[SerializeField]
	private float m_DamageFillDelay;

	[SerializeField]
	private float m_DamageFillDuration;

	[SerializeField]
	private Ease m_DamageFillEase;

	[SerializeField]
	private GameObject[] m_Lines;

	public Vector3 m_OffsetFromTarget;

	private int m_LinesAmount;

	private float m_InitialHealth;

	public bool IsDead => Health <= 0f;

	public float Health { get; private set; }

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void Initialize(float i_Health, int i_Level)
	{
		m_LevelText.text = i_Level.ToString();
		m_InitialHealth = i_Health;
		Health = i_Health;
		updateVisuals();
		changeDamageImage();
		setLines(i_Level);
		m_HorisontalGroup.enabled = true;
		Invoke(nameof(deactivateGroup), 1f);
	}

	private void deactivateGroup()
	{
		m_HorisontalGroup.enabled = false;
	}

	private void setLines(int i_LinesAmount)
	{
		deactivateAllLines();
		m_LinesAmount = i_LinesAmount;
		for (int i = 0; i < m_LinesAmount && i < m_Lines.Length; i++)
			m_Lines[i].SetActive(true);
	}

	private void updateLines()
	{
		int healthPerLine = (int)(m_InitialHealth / m_LinesAmount);
		int activeLines = (int)Health / healthPerLine;
		for (int i = 0; i < m_LinesAmount && i < m_Lines.Length; i++)
			m_Lines[i].SetActive(i <= activeLines);
	}

	private void deactivateAllLines()
	{
		for (int i = 0; i < m_Lines.Length; i++)
			m_Lines[i].SetActive(false);
	}

	private void updateVisuals()
	{
		m_HealthRoot.SetActive(Health > 0f);
		float fillRatio = m_InitialHealth > 0f ? Health / m_InitialHealth : 0f;
		m_HealthFill.transform.localScale = new Vector3(fillRatio, 1f, 1f);
		updateLines();
	}

	private void LateUpdate()
	{
		if (Health > 0f)
			transform.position = m_Target.position + m_OffsetFromTarget;
	}

	public void DecreaseHealth(float i_Value)
	{
		Health -= i_Value;
		updateVisuals();
		changeDamageImage();
	}

	public void IncreaseHealth(float i_Value)
	{
		Health += i_Value;
		updateVisuals();
		changeDamageImage();
	}

	private void changeHealth(float i_HealthChange)
	{
		Health += i_HealthChange;
		updateVisuals();
		changeDamageImage();
	}

	private void changeDamageImage()
	{
		m_DamageFill.transform.DOKill();
		m_DamageFill.transform.DOScale(m_HealthFill.transform.localScale, m_DamageFillDuration).SetDelay(m_DamageFillDelay).SetEase(m_DamageFillEase);
	}
}
