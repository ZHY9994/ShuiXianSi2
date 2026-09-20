using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Root;

	[SerializeField]
	private Transform m_Target;

	[SerializeField]
	private Image m_ExperienceImage;

	[SerializeField]
	private Image m_TargetExperienceImage;

	[SerializeField]
	private TextMeshProUGUI m_ExperienceLevelText;

	[SerializeField]
	private float m_FillDelay;

	[SerializeField]
	private float m_FillDuration;

	[SerializeField]
	private Ease m_FillEase;

	public Vector3 m_OffsetFromTarget;

	private Tween m_IncreaseExpTween;

	private int m_LastDisplayedExperienceLevel = -1;

	private void Awake()
	{
		onReset();
	}

	private void OnEnable()
	{
		ExperienceManager.OnReset += onExperienceReset;
		LocalizationEventCaller.Register(onReset);
		animateExperienceIncrease();
		updateVisuals();
		LocalizationEventCaller.Register(updateVisuals);
	}

	private void OnDisable()
	{
		ExperienceManager.OnReset -= onExperienceReset;
		LocalizationEventCaller.UnRegister(onReset);
		LocalizationEventCaller.UnRegister(updateVisuals);
	}

	private void Start()
	{
		updateVisuals();
	}

	private void animateExperienceIncrease()
	{
		GameObject root = m_Root;
		ExperienceManager manager = ExperienceManager.Instance;
		if (manager == null)
			return;
		if (root != null)
			root.SetActive(manager.ExperienceLevel > 0 || manager.ExperienceProgression > 0f);
		if (manager.ExperienceProgression >= 1f)
			manager.IsBlock = true;
		float currentScale = m_ExperienceImage.transform.localScale.x;
		float progression = manager.ExperienceProgression;
		m_TargetExperienceImage.transform.localScale = new Vector3(progression, 1f, 1f);
		m_IncreaseExpTween?.Kill();
		m_IncreaseExpTween = m_ExperienceImage.transform.DOScale(new Vector3(progression, 1f, 1f), Mathf.Abs(progression - currentScale) * m_FillDuration)
			.SetDelay(m_FillDelay).SetEase(m_FillEase).OnComplete(() =>
			{
				ExperienceManager.Instance.IsBlock = false;
				if (ExperienceManager.Instance.ExperienceProgression >= 1f)
					KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_ExperienceLevelUp");
			});
	}

	private void onReset()
	{
		m_ExperienceImage.transform.localScale = new Vector3(ExperienceManager.Instance.ExperienceProgression, 1f, 1f);
		m_TargetExperienceImage.transform.localScale = new Vector3(ExperienceManager.Instance.ExperienceProgression, 1f, 1f);
		updateVisuals();
	}

	private void updateVisuals()
	{
		updateVisuals(true);
	}

	private void updateVisualsIfChanged()
	{
		updateVisuals(false);
	}

	private void updateVisuals(bool i_Force)
	{
		ExperienceManager manager = ExperienceManager.Instance;
		if (manager == null || m_ExperienceLevelText == null)
			return;
		if (!i_Force && m_LastDisplayedExperienceLevel == manager.ExperienceLevel)
			return;
		m_ExperienceLevelText.text = string.Format("{0} {1}", LocalizationUtility.GetLocalizedString(eLocalKeyType.Word, "lvl"), manager.ExperienceLevel + 1);
		m_LastDisplayedExperienceLevel = manager.ExperienceLevel;
	}

	private void onExperienceReset()
	{
		onReset();
	}

	private void LateUpdate()
	{
		if (m_Target != null)
			transform.position = m_Target.position + m_OffsetFromTarget;
		updateVisualsIfChanged();
	}
}
