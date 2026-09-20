using System;

[Serializable]
public class FilePath
{
	public string Path;

	public bool IsEnabled = true;

	public FilePath(string i_Path, bool i_IsEnabled)
	{
		Path = i_Path;
		IsEnabled = i_IsEnabled;
	}

}
