using UnityEngine;

public class LoadingMessage : MonoBehaviour
{
	public enum eMessageType
	{
		Loading = 0,
		NoInternet = 1,
		Loaded = 2
	}

	[SerializeField]
	private GameObject m_Root;

	[SerializeField]
	private GameObject m_Loading;

	[SerializeField]
	private GameObject m_NoInternet;

	private void OnEnable()
	{
		close();
	}

	public void Open()
	{
		m_Root.SetActive(true);
		Show(eMessageType.Loading);
	}

	public void Show(eMessageType i_Message)
	{
		m_Loading.SetActive(i_Message == eMessageType.Loading);
		m_NoInternet.SetActive(i_Message == eMessageType.NoInternet);
		if (i_Message == eMessageType.NoInternet)
			DG.Tweening.DOVirtual.DelayedCall(1f, close);
		else if (i_Message == eMessageType.Loaded)
			close();
	}

	private void close()
	{
		m_Root.SetActive(false);
	}
}
