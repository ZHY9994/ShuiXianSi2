using KobGamesSDKSlim.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public class HUDManager : Singleton<HUDManager>
	{
		[SerializeField]
		private GameObject m_CoinsCounter;

		[SerializeField]
		private ExtendedButton m_InputButton;

		[SerializeField]
		private TextMeshProUGUI m_LevelText;

		[SerializeField]
		private CoinUpdater m_CoinUpdater;

		[SerializeField]
		private ProgressBar m_ProgressBar;

		[SerializeField]
		private Canvas m_Canvas;

		[SerializeField]
		private CanvasGroup m_CanvasGroup;

		[SerializeField]
		private ScreenFade m_ScreenFade;

		private Vector2 m_DummyVector2;

		public RectTransform DefaultCoinsTarget => m_CoinUpdater != null ? m_CoinUpdater.CoinMoveTarget : null;

		private HUDVariablesEditor m_HUDVars => GameConfig.Instance.HUD;

		private MenuVariablesEditor m_MenuVars => GameConfig.Instance.Menus;

		private InputVariablesEditor m_InputVars => GameConfig.Instance.Input;

		private new void OnEnable()
		{
			SetRefs();
			m_InputButton.OnDownEvent += InputDown;
			m_InputButton.OnUpEvent += InputUp;
			GameManagerV1.GameManagerBase.OnGameReset += OnReset;
			GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
			GameManagerV1.GameManagerBase.OnLevelCompleted += OnLevelFinished;
			GameManagerV1.GameManagerBase.OnLevelFailed += OnLevelFinished;
		}

		public override void OnDisable()
		{
			m_InputButton.OnDownEvent -= InputDown;
			m_InputButton.OnUpEvent -= InputUp;
			GameManagerV1.GameManagerBase.OnGameReset -= OnReset;
			GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
			GameManagerV1.GameManagerBase.OnLevelCompleted -= OnLevelFinished;
			GameManagerV1.GameManagerBase.OnLevelFailed -= OnLevelFinished;
			base.OnDisable();
		}

		private void OnReset()
		{
			UpdateCurrentLevelText();
		}

		private void onLevelStarted()
		{
			Open();
			UpdateCurrentLevelText();
		}

		private void OnLevelFinished()
		{
			CloseProgressBar();
		}

		private void Init()
		{
			SetRefs();
			UpdateCurrentLevelText();
		}

		public void SetLevelTextVisibility(bool i_IsVisible)
		{
			if (m_LevelText != null) m_LevelText.gameObject.SetActive(i_IsVisible);
		}

		private void UpdateCurrentLevelText()
		{
			if (m_LevelText != null && GameManagerV1.GameManagerBase.Instance != null)
				m_LevelText.text = StorageManager.Instance.CurrentLevel.ToString();
		}

		public void Open(bool i_Animate = true)
		{
			if (m_CanvasGroup == null) return;
			m_CanvasGroup.alpha = 1f;
			m_CanvasGroup.blocksRaycasts = true;
		}

		public void Close(bool i_Animate = true)
		{
			if (m_CanvasGroup == null) return;
			m_CanvasGroup.alpha = 0f;
			m_CanvasGroup.blocksRaycasts = false;
		}

		public void InputDown(PointerEventData i_PointerEventData)
		{
			InputManager.Instance?.InputDown();
		}

		public void InputUp(PointerEventData i_PointerEventData)
		{
			InputManager.Instance?.InputUp();
		}

		public Image GetFadeImage()
		{
			return m_ScreenFade != null ? m_ScreenFade.FadeImage : null;
		}

		public void OpenProgressBar()
		{
			if (m_ProgressBar != null) m_ProgressBar.Open();
		}

		public void CloseProgressBar()
		{
			if (m_ProgressBar != null) m_ProgressBar.Close();
		}

		public void SetProgressBarProgress(float i_Progress)
		{
			if (m_ProgressBar != null) m_ProgressBar.SetProgress(i_Progress);
		}

		public void SetRefs()
		{
			Transform t = transform;
			m_InputButton = t.FindDeepChild<ExtendedButton>("Input");
			m_LevelText = t.FindDeepChild<TextMeshProUGUI>("Level ID");
			m_ProgressBar = t.FindDeepChild<ProgressBar>("ProgressBar");
			m_Canvas = GetComponent<Canvas>();
			m_CanvasGroup = GetComponentInChildren<CanvasGroup>();
			m_ScreenFade = GetComponentInChildren<ScreenFade>();
		}

		private void OnValidate()
		{
			SetRefs();
		}
	}
}
