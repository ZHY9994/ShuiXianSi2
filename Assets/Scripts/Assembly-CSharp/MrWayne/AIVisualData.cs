using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class AIVisualData : VisualData<string, AIVisualData.AISkinData>
	{
		[Serializable]
		public class AISkinData : SkinDataBase
		{
			public Color SkinColor;
		}

		public Color DeathColor;
	}
}
