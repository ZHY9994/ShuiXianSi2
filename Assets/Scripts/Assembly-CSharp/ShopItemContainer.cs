using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemContainer : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_ValueText;

	[SerializeField]
	private bool m_HasFiller;

	[SerializeField]
	private Image m_Filler;

	public virtual void Visibility(bool i_Value)
	{
		gameObject.SetActive(i_Value);
	}

	public virtual void SetValue(string i_Value)
	{
		if (m_ValueText != null)
			m_ValueText.text = i_Value;
	}

	public virtual void UpdateFiller(float i_Value)
	{
		m_Filler.fillAmount = i_Value;
	}
}
