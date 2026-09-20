using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wayne.Game.Tutorials
{
	[Serializable]
	public class TutorialVariableEditor
	{
		[Serializable]
		private class TutorialDataWrapper
		{
			[SerializeReference]
			public TTutorialData Data;

			public eTutorial Type => Data.Type;
		}

		[SerializeField]
		private List<TutorialDataWrapper> m_TutorialList = new List<TutorialDataWrapper>();

		public List<TTutorialData> TutorialDatas => m_TutorialList.Select(x => x.Data).ToList();

		public TTutorialData GetTutorialData(eTutorial i_Type)
		{
			TutorialDataWrapper wrapper = m_TutorialList.FirstOrDefault(x => x.Type == i_Type);
			return wrapper.Data;
		}
	}
}
