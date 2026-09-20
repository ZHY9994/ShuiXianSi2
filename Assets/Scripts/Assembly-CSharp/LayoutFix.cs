using UnityEngine;
using UnityEngine.UI;

public class LayoutFix : MonoBehaviour
{
	[SerializeField]
	private VerticalLayoutGroup m_Layout;

	private bool m_FirstLaunch = true;

	private void OnEnable()
	{
		Invoke(nameof(resetLayout), 0.03f);
	}

	private void resetLayout()
	{
		m_Layout.enabled = false;
		m_Layout.enabled = true;
	}
}
