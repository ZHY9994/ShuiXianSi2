using System;
using UnityEngine;

[Serializable]
public class DeckDataEditor
{
	[SerializeField]
	private DeckSlotData[] m_DeckSlots;

	public DeckSlotData[] DeckSlots => m_DeckSlots;

	private void onValuChanged()
	{
		for (int i = 0; i < m_DeckSlots.Length; i++)
		{
			m_DeckSlots[i].Index = i;
		}
	}
}
