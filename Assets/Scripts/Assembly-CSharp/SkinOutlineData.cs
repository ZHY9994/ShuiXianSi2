using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SkinOutlineData
{
	[Serializable]
	private class OutlineDatas
	{
		[Serializable]
		private class OutlineData
		{
			public eOutlineType Outline;

			public Material Material;
		}

		[SerializeField]
		private List<OutlineData> m_Outlines;

		public Material GetOutline(eOutlineType i_Oultine)
		{
			return m_Outlines.FirstOrDefault(x => x.Outline == i_Oultine).Material;
		}
	}

	[Serializable]
	private class OutlineDict : UnitySerializedDictionary<eSkinType, OutlineDatas>
	{
	}

	[SerializeField]
	private OutlineDict m_Outline;

	public Material GetOutline(eSkinType i_Type, eOutlineType i_Outline)
	{
		return m_Outline[i_Type].GetOutline(i_Outline);
	}
}
