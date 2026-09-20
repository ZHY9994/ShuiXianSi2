using System;
using System.Runtime.CompilerServices;
using DanielLochner.Assets.SimpleScrollSnap;
using KobGamesSDKSlim;
using TMPro;
using UnityEngine;

public class MatchMaking : MonoBehaviour
{
	[SerializeField]
	private Transform m_Container;

	[SerializeField]
	private SimpleScrollSnap m_Scroll;

	[SerializeField]
	private TextMeshProUGUI m_FindPlayerText;

	[SerializeField]
	private MatchMakingItem[] m_MatchMakingItems;

	private GamePlayVariablesEditor.MatchMakingParameter m_Parameter => GameConfig.Instance.GamePlay.MatchMakingParameters;

	public MatchMakingItem CurrentItem { get; private set; }

	public static event Action OnMatchMakingOpen = delegate { };

	public static event Action<MatchMakingItem> OnMatchMakingItemSelected = delegate { };

	public static event Action OnMatchMakingClose = delegate { };

	private void OnEnable()
	{
		randomizeMatchMakingItems();
		startMatchMaking();
	}

	private void startMatchMaking()
	{
		m_FindPlayerText.text = StaticString.FindingPlayer;
		m_Container.gameObject.SetActive(true);
		Spin();
		OnMatchMakingOpen?.Invoke();
	}

	private void randomizeMatchMakingItems()
	{
		foreach (MatchMakingItem item in m_MatchMakingItems) item.RandomizeCharacter();
	}

	private void Spin()
	{
		m_Scroll.Velocity = Vector2.right * UnityEngine.Random.Range(m_Parameter.MinMaxVelocity.x, m_Parameter.MinMaxVelocity.y);
	}

	public void GetSelectedPanel()
	{
		for (int i = 0; i < m_MatchMakingItems.Length; i++)
			if (m_Scroll.Panels[m_Scroll.CenteredPanel].gameObject == m_MatchMakingItems[i].gameObject)
			{
				CurrentItem = m_MatchMakingItems[i];
				GameplayController.Instance.SetMatchMakingItem(CurrentItem);
				break;
			}
		m_FindPlayerText.text = StaticString.FoundPlayer;
		DG.Tweening.DOVirtual.DelayedCall(m_Parameter.StartGameDelay, StartGame);
	}

	private void ChangeFindPlayerText(string i_Text)
	{
		m_FindPlayerText.text = i_Text;
	}

	private void StartGame()
	{
		m_Container.gameObject.SetActive(false);
		SoundManager.Instance.PlayGeneralSFXByType(eSFXType.LevelStart);
		OnMatchMakingClose?.Invoke();
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_MatchMaking");
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_GamePlay");
	}

	private void SetRefs()
	{
		m_Container = transform.GetChild(0);
		m_Scroll = GetComponentInChildren<SimpleScrollSnap>();
		m_FindPlayerText = transform.FindDeepChild<TextMeshProUGUI>("Finding Text");
		m_MatchMakingItems = GetComponentsInChildren<MatchMakingItem>();
	}
}
