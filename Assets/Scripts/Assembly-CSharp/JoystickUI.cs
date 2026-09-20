using KobGamesSDKSlim;
using UnityEngine;
using UnityEngine.UI;

public class JoystickUI : MonoBehaviour
{
	[SerializeField]
	private Image m_JoystickOutline;

	[SerializeField]
	private Image m_JoystickHandle;

	[SerializeField]
	private Canvas m_Canvas;

	[SerializeField]
	private CanvasScaler m_CanvasScaler;

	private float m_CurrentMatchWidthOrHeight;

	private InputVariablesEditor m_InputVars => GameConfig.Instance.Input;

	public void SetRefs()
	{
		m_JoystickOutline = transform.FindDeepChild<Image>("Joystick Outline");
		m_JoystickHandle = transform.FindDeepChild<Image>("Joystick Handle");
		m_Canvas = GetComponentInParent<Canvas>();
		m_CanvasScaler = GetComponentInParent<CanvasScaler>();
	}

	private void OnEnable()
	{
		InputManager.OnInputDown += onInputDown;
		InputManager.OnInputUp += onInputUp;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnScreenChanged += onScreenChanged;
		if (!m_InputVars.IsUseJoystick || !m_InputVars.Joystick.IsShowVisuals)
			gameObject.SetActive(false);
		m_CurrentMatchWidthOrHeight = m_CanvasScaler.matchWidthOrHeight;
		setSizes();
	}

	private void OnDisable()
	{
		InputManager.OnInputDown -= onInputDown;
		InputManager.OnInputUp -= onInputUp;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnScreenChanged -= onScreenChanged;
	}

	private void onInputDown(Vector2 i_Pos)
	{
		if (m_InputVars.IsUseJoystick && m_InputVars.Joystick.IsShowVisuals)
			m_JoystickOutline.gameObject.SetActive(true);
	}

	private void onInputUp()
	{
		if (m_InputVars.IsUseJoystick && m_InputVars.Joystick.IsShowVisuals)
			m_JoystickOutline.gameObject.SetActive(false);
	}

	private void onScreenChanged(int i_Width, int i_Height)
	{
		setSizes();
	}

	private void Update()
	{
		if (m_CurrentMatchWidthOrHeight != m_CanvasScaler.matchWidthOrHeight)
		{
			m_CurrentMatchWidthOrHeight = m_CanvasScaler.matchWidthOrHeight;
			setSizes();
		}
		if (m_InputVars.IsUseJoystick && m_InputVars.Joystick.IsShowVisuals)
		{
			m_JoystickOutline.rectTransform.anchoredPosition = InputManager.Instance.JoystickPosition / m_Canvas.scaleFactor;
			m_JoystickHandle.rectTransform.anchoredPosition = InputManager.Instance.JoystickHandleLocalPosition * (m_JoystickOutline.rectTransform.sizeDelta.y * 0.5f);
		}
	}

	private void setSizes()
	{
		float t = Mathf.Clamp01(m_CanvasScaler.matchWidthOrHeight);
		float size = Mathf.Lerp(m_CanvasScaler.referenceResolution.x, m_CanvasScaler.referenceResolution.y, t);
		m_JoystickOutline.rectTransform.sizeDelta = Vector2.one * size;
		m_JoystickHandle.rectTransform.sizeDelta = m_JoystickHandle.rectTransform.sizeDelta * m_InputVars.Joystick.HandleRadiusMultiplier;
	}
}
