using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace KobGamesSDKSlim
{
	public class CrossPromoVid : MonoBehaviour
	{
		[SerializeField]
		private Button m_PlayBtn;

		[SerializeField]
		private Button m_CloseBtn;

		[SerializeField]
		private RectTransform m_Parent;

		[SerializeField]
		private TextMeshProUGUI m_Promotext;

		[SerializeField]
		private VideoPlayer m_VideoPlayer;

		[SerializeField]
		private string[] m_PromoTexts;

		[SerializeField]
		private AdConfiguration[] m_AdConfigs;

		private int m_GameOverCounter;

		private int m_GameWonCounter;

		private AdConfiguration m_CurrentAd;

		private GameManager m_GameManager;

		private bool m_StartCounters;

		public int ShowGameOverCountdown = 3;

		public int ShowGameWonCountdown = 3;

		public bool ShowOnFirstLaunch;

		private void Awake()
		{
			m_GameManager = FindObjectOfType<GameManager>();
			m_CloseBtn?.onClick.AddListener(closeWindow);
			GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
			GameManagerV1.GameManagerBase.OnLevelLoaded += onLevelLoaded;
			GameManagerV1.GameManagerBase.OnLevelFailedNoContinue += onLevelFailed;
			GameManagerV1.GameManagerBase.OnLevelCompleted += onLevelCompleted;
		}

		private void Start()
		{
			if (ShowOnFirstLaunch || (Managers.Instance.Storage != null && Managers.Instance.Storage.GameLaunchCount >= 2))
				gameObject.SetActive(true);
			else if (Managers.Instance.Storage != null && Managers.Instance.Storage.GameLaunchCount == 1)
				gameObject.SetActive(false);
		}

		private void onLevelCompleted()
		{
			m_GameWonCounter++;
		}

		private void onLevelFailed()
		{
			m_GameOverCounter++;
		}

		private void onLevelLoaded()
		{
			if (!m_StartCounters && Managers.Instance.Storage != null && Managers.Instance.Storage.GameLaunchCount == 1)
				return;
			if (m_GameOverCounter >= ShowGameOverCountdown || m_GameWonCounter >= ShowGameWonCountdown)
				show();
		}

		private void show()
		{
			m_GameOverCounter = 0;
			m_GameWonCounter = 0;
			m_StartCounters = false;
			gameObject.SetActive(true);
			if (m_Parent != null && !DOTween.IsTweening(m_Parent.GetInstanceID()))
				m_Parent.DOAnchorPosX(-m_Parent.anchoredPosition.x, .25f).From().SetUpdate(true).SetId(m_Parent.GetInstanceID());
		}

		private void onLevelStarted()
		{
			gameObject.SetActive(false);
		}

		private void closeWindow()
		{
			m_GameOverCounter = 0;
			m_GameWonCounter = 0;
			m_StartCounters = true;
			gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			if (m_AdConfigs != null && m_AdConfigs.Length > 0)
				m_CurrentAd = m_AdConfigs[Random.Range(0, m_AdConfigs.Length)];
			if (m_PromoTexts != null && m_PromoTexts.Length > 0 && m_Promotext != null)
				m_Promotext.text = m_PromoTexts[Random.Range(0, m_PromoTexts.Length)];
			if (m_CurrentAd != null && m_VideoPlayer != null)
				m_VideoPlayer.clip = m_CurrentAd.VideoClip;
			m_PlayBtn?.onClick.RemoveAllListeners();
			m_PlayBtn?.onClick.AddListener(onPlay);
			m_VideoPlayer?.GetComponent<Button>()?.onClick.RemoveAllListeners();
			m_VideoPlayer?.GetComponent<Button>()?.onClick.AddListener(onPlay);
		}

		private void onPlay()
		{
			if (m_CurrentAd != null)
				Utils.OpenCrossPromoUrlStore(m_CurrentAd.AndroidAppID, m_CurrentAd.IOSAppID);
		}
	}
}
