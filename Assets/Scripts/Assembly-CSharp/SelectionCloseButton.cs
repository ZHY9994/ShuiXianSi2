using KobGamesSDKSlim;
using UnityEngine;

public class SelectionCloseButton : MonoBehaviour
{
	[SerializeField]
	private ExtendedButton m_SelectionCloseButton;

	private void OnEnable()
	{
		SetRefs();
		m_SelectionCloseButton.Setup(onClick);
	}

	private void onClick()
	{
		DeckManager.Instance.CloseDeck();
	}

	private void SetRefs()
	{
		m_SelectionCloseButton = GetComponent<ExtendedButton>();
	}
}
