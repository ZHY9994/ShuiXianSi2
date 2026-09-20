using System;
using KobGamesSDKSlim;
using UnityEngine;

namespace Wayne.Game.Tutorials
{
	[Serializable]
	public abstract class TTutorialData
	{
		[SerializeField]
		private eTutorial m_Type;

		[SerializeField]
		private bool m_IsEnabled = true;

		public bool IsTutorialActive => m_IsTutorialActive && m_IsEnabled;

		public string ID => string.Format("Tutorial_{0}", m_Type);

		public eTutorial Type => m_Type;

		private bool m_IsTutorialActive
		{
			get
		{
				return PlayerPrefsBool.GetBool("m_IsTutorialActive" + ID, true);
			}
			set
			{
				PlayerPrefsBool.SetBool("m_IsTutorialActive" + ID, value);
			}
		}

		public virtual void OnComplete()
		{
			m_IsTutorialActive = false;
			global::Ade_Framework.AdeCloudPlayerPrefs.Save();
		}
	}
}
