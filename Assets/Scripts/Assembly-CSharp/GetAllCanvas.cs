using UnityEngine;
using UnityEngine.UI;

public class GetAllCanvas : MonoBehaviour
{
	[SerializeField]
	private Canvas[] m_Canvas;

	[SerializeField]
	private GraphicRaycaster[] m_RayCasters;

	private void run()
	{
		m_Canvas = GetComponentsInChildren<Canvas>(true);
		m_RayCasters = GetComponentsInChildren<GraphicRaycaster>(true);
	}
}
