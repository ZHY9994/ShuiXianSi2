using DG.Tweening;
using KobGamesSDKSlim;
using TMPro;
using UnityEngine;

public class PopText : MonoBehaviour
{
	[SerializeField]
	private Transform m_Root;

	[SerializeField]
	private TextMeshProUGUI m_Text;

	[SerializeField]
	private Vector3 m_OriginalScale;

	[SerializeField]
	private Quaternion m_OriginalRotation;

	private MenuVariablesEditor.PopUpTextParameter m_Parameter => GameConfig.Instance.Menus.PopUpTextParameters;

	private void OnEnable()
	{
		hide();
		GameplayController.OnStateChange += onStateChange;
	}

	private void OnDisable()
	{
		GameplayController.OnStateChange -= onStateChange;
	}

	private void onStateChange(eTurnState i_State)
	{
		// IDA confirms this callback is an empty implementation.
	}

	private void playPopText()
	{
		if (m_Parameter.UsePopText)
		{
			resetPopText();
			randomRotation();
			show();
			OnPunchStarted().OnComplete(OnPunchFinished);
		}
	}

	private void resetPopText()
	{
		hide();
		m_Root.localScale = m_OriginalScale;
		m_Root.rotation = m_OriginalRotation;
	}

	private void randomRotation()
	{
		if (m_Parameter.ShouldRotate)
		{
			m_Root.rotation = Quaternion.Euler(0f, 0f, m_Parameter.Rotation[Random.Range(0, m_Parameter.Rotation.Length)]);
		}
	}

	private Tween OnPunchStarted()
	{
		return m_Root.DOScale(m_OriginalScale, m_Parameter.ShowDuration).From(0f).SetEase(m_Parameter.ShowEase);
	}

	private void OnPunchFinished()
	{
		m_Root.DOScale(Vector3.zero, m_Parameter.HideDuration)
			.SetDelay(m_Parameter.StayDuration)
			.SetEase(m_Parameter.HideEase)
			.OnComplete(resetPopText);
	}

	private void hide()
	{
		m_Root.gameObject.SetActive(false);
	}

	private void show()
	{
		m_Root.gameObject.SetActive(true);
	}

	private void SetRefs()
	{
		m_Text = GetComponentInChildren<TextMeshProUGUI>();
		m_Root = transform.FindDeepChild("Root");
		m_OriginalScale = m_Root.localScale;
		m_OriginalRotation = m_Root.rotation;
	}
}
