namespace KobGamesSDKSlim.Core
{
	public static class DirectoryUtils
	{
		public static eDirectoriesSearchResult DirectoriesExist(params string[] i_Paths)
		{
			int found = 0;
			for (int i = 0; i < i_Paths.Length; i++)
			{
				if (System.IO.Directory.Exists(i_Paths[i]))
					found++;
			}
			if (found == 0) return eDirectoriesSearchResult.NoFolderFound;
			return found == i_Paths.Length ? eDirectoriesSearchResult.AllFoldersFound : eDirectoriesSearchResult.SomeFoldersFound;
		}

		public static bool DeleteFilesAndDirectoriesRelativePath(params string[] i_Paths)
		{
			if (i_Paths.Length == 0)
			{
				UnityEngine.Debug.LogWarning("Nothing to delete!");
				return false;
			}
			bool deleted = false;
			for (int i = 0; i < i_Paths.Length; i++)
				deleted |= DeleteFileOrDirectoryRelativePath(i_Paths[i]);
			if (!deleted) UnityEngine.Debug.LogWarning("Nothing to delete!");
			return deleted;
		}

		private static bool DeleteFileOrDirectoryRelativePath(string i_Path)
		{
			string path = UnityEngine.Application.dataPath + i_Path;
			if (System.IO.File.Exists(path))
			{
				System.IO.File.Delete(path);
				UnityEngine.Debug.LogError("Deleted " + path);
				return true;
			}
			if (System.IO.Directory.Exists(path))
			{
				System.IO.Directory.Delete(path, true);
				UnityEngine.Debug.LogError("Deleted " + path);
				return true;
			}
			return false;
		}
	}
}
