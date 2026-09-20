using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public class RateUsPopUp : MonoBehaviour
	{
		public delegate void RateUsEvent(string i_ShowReason, string i_Result, int i_Stars = 0);

		public Button ButtonLater;

		public Button ButtonNo;

		public Button ButtonYes;

		public Canvas CanvasRoot;

		public RateUsPopupSchema[] Schemas;

		public RateUsPopupSchema SelectedSchema;

		public bool StarsOnByDefault;

		public Image PanelBg;

		public Image TitleBg;

		public TextMeshProUGUI TitleText;

		public Image[] Stars;

		public Button[] StarsButtons;

		public Image YesButtonBg;

		public TextMeshProUGUI YesButtonTextColor;

		[HideInInspector]
		public string Message;

		private bool m_RepaintCanvas;

		private bool m_IsShownRateUs;

		private string m_ShowReason;

		public ePopupMode RateUsPopupMode => GameSettings.Instance.RateUs.RateUsPopupMode;

		public List<int> RateUsLevels => GameSettings.Instance.RateUs.RateUsLevels;

		public string RateUsLevelsBlackList
		{
			get
			{
				return PlayerPrefs.GetString("RateUsLevelsBlackList", string.Empty);
			}
			set
			{
				PlayerPrefs.SetString("RateUsLevelsBlackList", value ?? string.Empty);
			}
		}

		public bool IsAllowedToShow
		{
			get
			{
				return PlayerPrefs.GetInt("IsAllowedToShow", 1) == 1;
			}
			set
			{
				PlayerPrefs.SetInt("IsAllowedToShow", value ? 1 : 0);
			}
		}

		public bool IsRateUsEnabled => GameSettings.Instance.RateUs.IsRateUsEnabled;

		public bool IsFiveStars
		{
			get
			{
				if (Stars == null || Stars.Length == 0 || SelectedSchema == null) return true;
				for (int i = 0; i < Stars.Length; i++)
					if (Stars[i] == null || Stars[i].sprite == SelectedSchema.StarOff) return false;
				return true;
			}
		}

		public int NumberOfStars
		{
			get
			{
				if (Stars == null || SelectedSchema == null) return 0;
				int result = 0;
				for (int i = 0; i < Stars.Length; i++)
					if (Stars[i] != null && Stars[i].sprite != SelectedSchema.StarOff) result++;
				return result;
			}
		}

		public static event RateUsEvent OnRateUsEvent = delegate { };

		public RateUsPopUp()
		{
			Message = "This field is missing a reference.";
		}

		private bool mustNotBeNull(dynamic i_Object)
		{
			return i_Object != null;
		}

		private void OnValidate()
		{
			bool refsValid = PanelBg != null;
			if (refsValid) refsValid = TitleBg != null;
			if (refsValid) refsValid = TitleText != null;
			if (refsValid) refsValid = Stars != null;
			if (refsValid) refsValid = YesButtonBg != null;
			if (refsValid) refsValid = YesButtonTextColor != null;
			_ = refsValid;
		}

		private void onStarsDefaultStateChange()
		{
			if (Managers.Instance != null)
			{
				updateStarsSprites();
				m_RepaintCanvas = true;
			}
		}

		private void OnInspectorGUI()
		{
			if (!m_RepaintCanvas) return;
			if (CanvasRoot == null) throw new NullReferenceException(nameof(CanvasRoot));
			CanvasRoot.gameObject.SetActive(false);
			CanvasRoot.gameObject.SetActive(true);
			m_RepaintCanvas = false;
		}

		public void OnSelectedSchema()
		{
			return;
		}

		private void updateStarsSprites()
		{
			if (Stars == null) throw new NullReferenceException(nameof(Stars));
			if (SelectedSchema == null) throw new NullReferenceException(nameof(SelectedSchema));
			for (int i = 0; i < Stars.Length; i++)
			{
				if (Stars[i] == null) throw new NullReferenceException(nameof(Stars));
				Stars[i].sprite = StarsOnByDefault ? SelectedSchema.StarOn : SelectedSchema.StarOff;
			}
		}

		private void OnEnable()
		{
			CloseRateUs();
		}

		private void OnDisable()
		{
		}

		private void onLevelLoaded()
		{
			if (StorageManager.Instance != null)
				TryShowRateUsOnSpecificLevel(StorageManager.Instance.CurrentLevel, ePopupMode.LevelLoad);
		}

		private void onLevelCompleted()
		{
			if (StorageManager.Instance != null)
				TryShowRateUsOnSpecificLevel(StorageManager.Instance.CurrentLevel, ePopupMode.LevelComplete);
		}

		public void StarButtonOnClick(Button i_Button)
		{
			if (ButtonYes == null || i_Button == null || SelectedSchema == null) throw new NullReferenceException();
			ButtonYes.interactable = true;
			int index = i_Button.transform.GetSiblingIndex();
			for (int i = 0; i <= index && i < Stars.Length; i++)
				if (Stars[i] != null) Stars[i].sprite = SelectedSchema.StarOn;
			for (int i = index + 1; i < Stars.Length; i++)
				if (Stars[i] != null) Stars[i].sprite = SelectedSchema.StarOff;
		}

		public void ButtonLaterOnClick()
		{
			OnRateUsEvent?.Invoke(m_ShowReason, "Later", 0);
			CloseRateUs();
		}

		public void ButtonNoOnClick()
		{
			IsAllowedToShow = false;
			OnRateUsEvent?.Invoke(m_ShowReason, "No", 0);
			CloseRateUs();
		}

		public void ButtonYesOnClick()
		{
			IsAllowedToShow = false;
			OnRateUsEvent?.Invoke(m_ShowReason, "Yes", NumberOfStars);
			if (IsFiveStars && GameSettings.Instance != null && GameSettings.Instance.General != null && GameSettings.Instance.General.Icon != null)
				GameSettings.Instance.General.OpenAppStoreURL();
			CloseRateUs();
		}

		public bool TryShowRateUsOnSpecificLevel(int i_LevelId, ePopupMode i_PopupMode)
		{
			return false;
		}

		private void addRateUsLevelBlackList(int i_LevelId)
		{
			RateUsLevelsBlackList += "_" + i_LevelId + "_";
		}

		private bool isRateUsLevelBlackListed(int i_LevelId)
		{
			return RateUsLevelsBlackList.Contains("_" + i_LevelId + "_");
		}

		public void CloseRateUs()
		{
			if (CanvasRoot != null) CanvasRoot.gameObject.SetActive(false);
		}

		public void ShowRateUs()
		{
			CloseRateUs();
		}
	}
}
