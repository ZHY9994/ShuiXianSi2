using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialDisableEnableGO : MonoBehaviour
{
	[SerializeField]
	private GameObject[] m_Enable;

	[SerializeField]
	private GameObject[] m_Disable;

	[SerializeField]
	private Image[] m_MaskedImages;

	private void OnEnable()
	{
		for (int i = 0; i < m_Enable.Length; i++)
		{
			m_Enable[i].SetActive(false);
		}
		for (int i = 0; i < m_Disable.Length; i++)
		{
			m_Disable[i].SetActive(false);
		}
	}

	public void ActivateTut(bool i_Enable)
	{
		if (i_Enable)
		{
			showData();
		}
	}

	public void DisableTut()
	{
		for (int i = 0; i < m_Enable.Length; i++)
		{
			m_Enable[i].SetActive(false);
		}
		for (int i = 0; i < m_Disable.Length; i++)
		{
			m_Disable[i].SetActive(true);
		}
	}

	public void EnableMaskedImage(bool i_Enable)
	{
		if (i_Enable)
		{
			for (int i = 0; i < m_MaskedImages.Length; i++)
			{
				m_MaskedImages[i].raycastTarget = false;
				m_MaskedImages[i].DOFade(1f, 0f);
			}
		}
	}

	public void DisableMaskedImages()
	{
		for (int i = 0; i < m_MaskedImages.Length; i++)
		{
			m_MaskedImages[i].raycastTarget = true;
			m_MaskedImages[i].DOFade(0f, 0f);
		}
	}

	private void initializeOnTutorial()
	{
		hideData();
		for (int i = 0; i < m_Enable.Length; i++)
		{
			m_Enable[i].SetActive(true);
		}
	}

	private void hideData()
	{
		for (int i = 0; i < m_Enable.Length; i++)
		{
			m_Enable[i].SetActive(false);
		}
		for (int i = 0; i < m_Disable.Length; i++)
		{
			m_Disable[i].SetActive(false);
		}
	}

	private void showData()
	{
		for (int i = 0; i < m_Enable.Length; i++)
		{
			m_Enable[i].SetActive(true);
		}
		for (int i = 0; i < m_Disable.Length; i++)
		{
			m_Disable[i].SetActive(false);
		}
	}
}
