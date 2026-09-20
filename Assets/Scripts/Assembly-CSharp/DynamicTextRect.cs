using TMPro;
using UnityEngine;

public class DynamicTextRect : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_TextField;

	[SerializeField]
	private RandomRangeValue m_MinMaxWidth;

	private void Update()
	{
		updateRect();
	}

	private void updateRect()
	{
		if (m_TextField == null || m_MinMaxWidth == null)
			return;
		float width = Mathf.Clamp(m_TextField.preferredWidth, m_MinMaxWidth.Min, m_MinMaxWidth.Max);
		RectTransform rectTransform = m_TextField.rectTransform;
		Vector2 sizeDelta = rectTransform.sizeDelta;
		sizeDelta.x = width;
		rectTransform.sizeDelta = sizeDelta;
	}
}
