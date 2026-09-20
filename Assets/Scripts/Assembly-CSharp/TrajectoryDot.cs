using UnityEngine;

public class TrajectoryDot : MonoBehaviour
{
	[SerializeField]
	private Transform m_Root;

	[SerializeField]
	private SpriteRenderer m_Sprite;

	public void Initialize(float i_Scale, Color i_Color)
	{
		m_Root.transform.localScale = Vector3.one * i_Scale;
		m_Sprite.color = i_Color;
	}

	public void UpdateColor(Color i_Color)
	{
		m_Sprite.color = i_Color;
	}

	public void SetOrder(int i_Order = 1)
	{
		m_Sprite.sortingOrder = i_Order;
	}
}
