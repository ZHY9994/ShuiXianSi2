using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class SyncData : BaseData
	{
		public string SDKPath = "/Users/kobyle/Games/KobGamesSDK_Slim";

		[SerializeField]
		public string DestPath = string.Empty;

		public UnityEngine.Object FileObject;

		[SerializeField]
		public List<string> FilesListPath;


		public override void OnFocus()
		{
			SetPathToThisProject();
		}

		public void SetPathToThisProject()
		{
			DestPath = Application.dataPath.Replace("Assets", "");
		}

		private void addFileToArray()
		{
			return;
		}
	}
}
