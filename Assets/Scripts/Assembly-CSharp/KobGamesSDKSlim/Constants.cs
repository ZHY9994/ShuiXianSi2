namespace KobGamesSDKSlim
{
	public class Constants
	{
		public const string k_AndroidManifestPath = "Assets/Plugins/Android/AndroidManifest.xml";

		public const string k_None = "None";

		public static readonly string k_IsDebugLogsEditorPrefKey = UnityEngine.Application.dataPath + "_IsDebugLogs";

		public static readonly string k_ScreenShotsEditorPrefKey = UnityEngine.Application.dataPath + "_SavePath";

		public static readonly string k_BuildSaveEditorPrefKey = UnityEngine.Application.dataPath + "_BuildSavePath";

		public static readonly string k_BuildAndroidEditorPrefKey = UnityEngine.Application.dataPath + "_BuildAndroidPath";

		public static readonly string k_BuildIOSEditorPrefKey = UnityEngine.Application.dataPath + "_BuildIOSPath";

		public static readonly string k_SignAPKEditorPrefKey = UnityEngine.Application.dataPath + "_SignAPK";

		public static readonly string k_KeyStorePathEditorPrefKey = UnityEngine.Application.dataPath + "_KeyStorePath";

		public static readonly string k_SDKFolderName = "_KobGamesSDK_Slim";

		public static readonly string k_ProjectSpecificFolderName = "_Project_Specific_Folder";

		public static string[] EditorPrefKeys => new[]
		{
			k_IsDebugLogsEditorPrefKey, k_ScreenShotsEditorPrefKey, k_BuildSaveEditorPrefKey,
			k_BuildAndroidEditorPrefKey, k_BuildIOSEditorPrefKey, k_SignAPKEditorPrefKey,
			k_KeyStorePathEditorPrefKey
		};
	}
}
