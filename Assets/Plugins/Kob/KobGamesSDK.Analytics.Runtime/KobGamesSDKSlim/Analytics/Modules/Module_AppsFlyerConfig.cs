using System;
using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	[Serializable]
	public class Module_AppsFlyerConfig
	{
		[SerializeField]
		private string m_ID;

		[SerializeField]
		private bool m_IsIOSATT;

		[SerializeField]
		private string m_AppStoreId;

		[SerializeField]
		private string m_GooglePlayId;

		public string StoreID => IsIOS ? m_AppStoreId : m_GooglePlayId;

		public bool IsIOS => UnityEngine.Application.platform == UnityEngine.RuntimePlatform.IPhonePlayer;

		public string ID
		{
			get
			{
				return m_ID;
			}
			set
			{ m_ID = value; }
		}

		public bool IsIOSATT
		{
			get
			{
				return m_IsIOSATT;
			}
			set
			{ m_IsIOSATT = value; }
		}

		public string AppStoreId
		{
			get
			{
				return m_AppStoreId;
			}
			set
			{ m_AppStoreId = value; }
		}

		public string GooglePlayId
		{
			get
			{
				return m_GooglePlayId;
			}
			set
			{ m_GooglePlayId = value; }
		}
	}
}
