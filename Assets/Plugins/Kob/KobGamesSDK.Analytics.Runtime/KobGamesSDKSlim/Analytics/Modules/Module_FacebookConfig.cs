using System;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	[Serializable]
	public class Module_FacebookConfig
	{
		[SerializeField]
		private string m_ID;

		[SerializeField]
		[Multiline]
		private string m_IDNotes;

		[SerializeField]
		private string m_GameName;

		public string ID
		{
			get
			{
				return m_ID;
			}
			set
			{ m_ID = value; }
		}

		public string IDNotes
		{
			get
			{
				return m_IDNotes;
			}
			set
			{ m_IDNotes = value; }
		}

		public string GameName
		{
			get
			{
				return m_GameName;
			}
			set
			{ m_GameName = value; }
		}
	}
}
