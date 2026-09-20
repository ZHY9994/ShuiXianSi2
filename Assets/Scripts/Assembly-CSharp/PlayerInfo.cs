using DanielLochner.Assets.SimpleScrollSnap;
using KobGamesSDKSlim;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Prefab;

	[SerializeField]
	private SimpleScrollSnap m_Snap;

	[SerializeField]
	private ExtendedButton m_FlagButton;

	[SerializeField]
	private Flag[] m_Flags;

	[SerializeField]
	private TMP_InputField m_PlayerNameText;

	[SerializeField]
	private Image m_MainFlag;

	[SerializeField]
	private ScaleAnimation m_Animation;

	private bool m_Clicked;

	private void OnEnable()
	{
		m_FlagButton.Setup(onClick);
		m_PlayerNameText.onValueChanged.AddListener(onValueChanged);
	}

	private void OnDisable()
	{
		m_PlayerNameText.onValueChanged.RemoveAllListeners();
	}

	private void onClick()
	{
		m_Clicked = !m_Clicked;
		FlagsScrollVisibility(m_Clicked);
	}

	private void onValueChanged(string i_Value)
	{
		if (!System.Text.RegularExpressions.Regex.IsMatch(i_Value, "^[a-zA-Z0-9]*$"))
		{
			m_PlayerNameText.text = System.Text.RegularExpressions.Regex.Replace(i_Value, "[^a-zA-Z0-9]", string.Empty);
		}
		int limit = GameConfig.Instance.GamePlay.MatchMakingParameters.CharacterNameLimit;
		if (m_PlayerNameText.text.Length > limit)
		{
			m_PlayerNameText.text = m_PlayerNameText.text.Substring(0, limit);
		}
		storeName();
	}

	private void FlagsScrollVisibility(bool i_Value)
	{
		if (i_Value)
		{
			m_Animation.StartScale();
		}
		else
		{
			m_Animation.StartScaleOut();
		}
	}

	public void OnCentered()
	{
		Flag flag = m_Snap.Panels[m_Snap.CenteredPanel].GetComponent<Flag>();
		if (flag != null)
		{
			m_MainFlag.sprite = flag.GetIcon;
			storeFlag();
		}
	}

	public void onReset()
	{
		m_Snap.transform.localScale = Vector3.zero;
		m_Clicked = false;
	}

	public void GetFlagAndName()
	{
		m_Snap.GoToPanel(Managers.Instance.Storage.PlayerFlagIndex);
		m_PlayerNameText.text = Managers.Instance.Storage.PlayerName;
	}

	public void StoreInfo()
	{
		storeName();
		storeFlag();
	}

	private void storeName()
	{
		if (m_PlayerNameText.text != null)
		{
			Managers.Instance.Storage.PlayerName = m_PlayerNameText.text;
		}
	}

	private void storeFlag()
	{
		Flag flag = m_Snap.Panels[m_Snap.CenteredPanel].GetComponent<Flag>();
		if (flag != null)
		{
			Managers.Instance.Storage.PlayerFlagIndex = m_Snap.CenteredPanel;
			Managers.Instance.Storage.PlayerFlagSpiteIndex = flag.SpriteIndex;
		}
	}
}
