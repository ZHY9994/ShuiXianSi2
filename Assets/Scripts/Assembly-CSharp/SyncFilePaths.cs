using System.Collections.Generic;
using UnityEngine;

public class SyncFilePaths : MonoBehaviour
{
	public Object FileObject;

	public List<FilePath> Paths = new List<FilePath>();

	private void DisableAllFilesPath()
	{
		foreach (FilePath path in Paths)
		{
			path.IsEnabled = false;
		}
	}

	private void addFileToArray()
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
	}
}
