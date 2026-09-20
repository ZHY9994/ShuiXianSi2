using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public abstract class GameDataRemoteSettingsBase
	{
		[HideInInspector]
		public Action OnValueChangedEditorCallback;

		protected GameDataRemoteSettingsBase()
			: base()
		{
		}

		public string SettingsJsonParsedPretty
		{
			get
			{
				return JsonUtility.ToJson(this, true);
			}
			set
			{
				SetJson(value);
			}
		}

		[HideInInspector]
		public string SettingsJsonParsedNonPretty => JsonUtility.ToJson(this, false);

		public abstract void LoadFromGameConfiguration();

		public virtual void Init(string i_JsonString = null, Action i_OnValueChangedCallback = null)
		{
			OnValueChangedEditorCallback = i_OnValueChangedCallback;
			LoadFromGameConfiguration();
			if (i_JsonString != null) SetJson(i_JsonString);
		}

		public virtual void SetJson(string i_JsonString)
		{
			JsonUtility.FromJsonOverwrite(i_JsonString, this);
		}

		public void OnValueChangedEditor()
		{
			return;
		}
	}
}
