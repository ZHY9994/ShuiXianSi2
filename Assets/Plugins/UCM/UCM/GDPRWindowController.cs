using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UCM
{
	public class GDPRWindowController : MonoBehaviour
	{
		[SerializeField]
		private Text _headerTextUnity;

		[SerializeField]
		private Text _gdprTextUnity;

		[SerializeField]
		private Text _termsTextUnity;

		[SerializeField]
		private Text _privacyTextUnity;

		[SerializeField]
		private Text _acceptTextUnity;

		[SerializeField]
		private Button _acceptButton;

		[SerializeField]
		private Button _termsButton;

		[SerializeField]
		private Button _privacyButton;

		[SerializeField]
		private EventSystem _eventSystem;

		[SerializeField]
		private Font thaiFont;

		[SerializeField]
		private Font hindiFontChankaya;

		[SerializeField]
		private Font defaultFont;

		[SerializeField]
		private HindiBlockController hindiBlockController;

		[SerializeField]
		private Image logoImage;

		[SerializeField]
		private Image sharkImage;

		[FormerlySerializedAs("versionIosText")]
		[SerializeField]
		private Text isoIosText;

		public void Initialize(UnityAction onAccept, string headerText, string mainText, string termsButton, string privacyButton, string linkTerms, string linkPrivacy, string accept, bool isHindi, bool goesOverflow, bool isThai, string companyName, Sprite companyLogo, Sprite sharkLogo, string iso)
		{
			if (_headerTextUnity != null) _headerTextUnity.text = headerText;
			if (_gdprTextUnity != null) _gdprTextUnity.text = mainText;
			if (_termsTextUnity != null) _termsTextUnity.text = termsButton;
			if (_privacyTextUnity != null) _privacyTextUnity.text = privacyButton;
			if (_acceptTextUnity != null) _acceptTextUnity.text = accept;
			if (_termsButton != null) _termsButton.GetComponentInChildren<Text>().text = termsButton;
			if (_privacyButton != null) _privacyButton.GetComponentInChildren<Text>().text = privacyButton;
			if (_acceptButton != null) _acceptButton.GetComponentInChildren<Text>().text = accept;
			if (_termsButton != null) _termsButton.onClick.RemoveAllListeners();
			if (_privacyButton != null) _privacyButton.onClick.RemoveAllListeners();
			if (_acceptButton != null)
			{
				_acceptButton.onClick.RemoveAllListeners();
				if (onAccept != null) _acceptButton.onClick.AddListener(onAccept);
			}
			if (logoImage != null) logoImage.sprite = companyLogo;
			if (sharkImage != null) sharkImage.sprite = sharkLogo;
			if (isoIosText != null) isoIosText.text = iso;
			if (hindiBlockController != null && isHindi) hindiBlockController.SetCompanyName(companyName);
			ActivateEventSystem();
		}

		private void ActivateEventSystem()
		{
			if (_eventSystem != null) _eventSystem.gameObject.SetActive(true);
		}

		public void AddOnClickListener(UnityAction onConfirmed)
		{
			if (_acceptButton != null && onConfirmed != null) _acceptButton.onClick.AddListener(onConfirmed);
		}
	}
}
