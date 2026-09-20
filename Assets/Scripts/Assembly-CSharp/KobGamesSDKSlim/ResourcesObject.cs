using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public struct ResourcesObject
	{
		[SerializeField]
		[HideInInspector]
		private string m_FullPath;

		[SerializeField]
		[HideInInspector]
		private string m_Path;

		public string Path
		{
			get
			{
				return m_Path;
			}
			set
			{
				m_Path = value;
			}
		}
	}
}
