using System.IO;

public static class UnityFileExtensions
{
	public static void SaveTo(this string data, string path)
	{
		if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(path))
		{
			FileExtensions.CreateDirectoryIfNotExists(path);
			File.WriteAllText(data, path);
		}
	}

	public static void SaveTo(this byte[] data, string path)
	{
		if (!data.IsNullOrEmpty() && !string.IsNullOrEmpty(path))
		{
			FileExtensions.CreateDirectoryIfNotExists(path);
			File.WriteAllBytes(path, data);
		}
	}

	public static void SaveToPersistentDataPath(this string data, string folderName, string filename)
	{
		if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(filename))
		{
			string basePath = string.IsNullOrEmpty(folderName) ? UnityEngine.Application.persistentDataPath : Path.Combine(UnityEngine.Application.persistentDataPath, folderName);
			data.SaveTo(Path.Combine(basePath, filename));
		}
	}

	public static void SaveToPersistentDataPath(this byte[] data, string folderName, string filename)
	{
		if (!data.IsNullOrEmpty() && !string.IsNullOrEmpty(filename))
		{
			string basePath = string.IsNullOrEmpty(folderName) ? UnityEngine.Application.persistentDataPath : Path.Combine(UnityEngine.Application.persistentDataPath, folderName);
			data.SaveTo(Path.Combine(basePath, filename));
		}
	}

	public static void SaveToDataPath(this string data, string folderName, string filename)
	{
		if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(filename))
		{
			string basePath = string.IsNullOrEmpty(folderName) ? UnityEngine.Application.dataPath : Path.Combine(UnityEngine.Application.dataPath, folderName);
			data.SaveTo(Path.Combine(basePath, filename));
		}
	}

	public static void SaveToDataPath(this byte[] data, string folderName, string filename)
	{
		if (!data.IsNullOrEmpty() && !string.IsNullOrEmpty(filename))
		{
			string basePath = string.IsNullOrEmpty(folderName) ? UnityEngine.Application.dataPath : Path.Combine(UnityEngine.Application.dataPath, folderName);
			data.SaveTo(Path.Combine(basePath, filename));
		}
	}

	public static string LoadFrom_AsString(this string path)
	{
		return string.IsNullOrEmpty(path) || !File.Exists(path) ? null : File.ReadAllText(path);
	}

	public static byte[] LoadFrom_AsBytes(this string path)
	{
		return string.IsNullOrEmpty(path) || !File.Exists(path) ? null : File.ReadAllBytes(path);
	}

	public static string LoadFromPeristantDataPath_AsString(this string filename, string folderName)
	{
		if (string.IsNullOrEmpty(filename)) return null;
		string basePath = string.IsNullOrEmpty(folderName) ? UnityEngine.Application.persistentDataPath : Path.Combine(UnityEngine.Application.persistentDataPath, folderName);
		return Path.Combine(basePath, filename).LoadFrom_AsString();
	}

	public static byte[] LoadFromPeristantDataPath_AsBytes(this string filename, string folderName)
	{
		if (string.IsNullOrEmpty(filename)) return null;
		string basePath = string.IsNullOrEmpty(folderName) ? UnityEngine.Application.persistentDataPath : Path.Combine(UnityEngine.Application.persistentDataPath, folderName);
		return Path.Combine(basePath, filename).LoadFrom_AsBytes();
	}

	public static string LoadFromDataPath_AsString(this string filename, string folderName)
	{
		if (string.IsNullOrEmpty(filename)) return null;
		string basePath = string.IsNullOrEmpty(folderName) ? UnityEngine.Application.dataPath : Path.Combine(UnityEngine.Application.dataPath, folderName);
		return Path.Combine(basePath, filename).LoadFrom_AsString();
	}

	public static byte[] LoadFromDataPath_AsBytes(this string filename, string folderName)
	{
		if (string.IsNullOrEmpty(filename)) return null;
		string basePath = string.IsNullOrEmpty(folderName) ? UnityEngine.Application.dataPath : Path.Combine(UnityEngine.Application.dataPath, folderName);
		return Path.Combine(basePath, filename).LoadFrom_AsBytes();
	}
}
