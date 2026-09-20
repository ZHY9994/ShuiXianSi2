using TMPro;
using UnityEngine;

public class CardStats : MonoBehaviour
{
	[SerializeField]
	private GameObject m_EnableContainer;

	[SerializeField]
	private GameObject m_DisableContainer;

	[SerializeField]
	private TextMeshProUGUI[] m_Titles;

	[SerializeField]
	private TextMeshProUGUI m_StatValue;

	public void InitializeStat(bool i_Enable, string i_Name, string i_Value)
	{
		m_EnableContainer.SetActive(i_Enable);
		m_DisableContainer.SetActive(!i_Enable);
		foreach (TextMeshProUGUI title in m_Titles)
			title.text = i_Name.ToUpper();
		m_StatValue.text = i_Value;
	}
}
