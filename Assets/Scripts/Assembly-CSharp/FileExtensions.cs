using System.IO;

public static class FileExtensions
{
	public static void CreateDirectoryIfNotExists(this string folder)
	{
		if (!string.IsNullOrEmpty(folder))
		{
			string directoryName = Path.GetDirectoryName(folder);
			if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
				Directory.CreateDirectory(directoryName);
		}
	}
}
