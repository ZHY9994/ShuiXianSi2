using System;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class VideoAdPlayerChunks : MonoBehaviour
{
	public VideoPlayer VideoPlayer;

	private string base64video;

	private void Start()
	{
		DownloadBase64Immediate();
	}

	private void DownloadBase64Immediate()
	{
		string path = Application.persistentDataPath + "/videoad.mp4";
		File.WriteAllBytes(path, Convert.FromBase64String(base64video));
		VideoPlayer.url = "file://" + Application.persistentDataPath + "/videoad.mp4";
		VideoPlayer.Prepare();
	}

	private void Update()
	{
		if (!VideoPlayer.isPrepared)
			return;
		if (!VideoPlayer.isPlaying)
			VideoPlayer.Play();
	}

	public void OpenPersistentPath()
	{
		Application.OpenURL("file://" + Application.persistentDataPath);
	}

	public void CreateBase64Chunks()
	{
		ConvertToStringChunks(base64video, 600000);
	}

	public void ConvertToStringChunks(string i_Base64Video, int i_ChunkSize = 600000)
	{
		string remaining = i_Base64Video;
		if (remaining == null)
			throw new ArgumentNullException(nameof(i_Base64Video));
		int stringLength = remaining.Length;
		for (uint i = 0; i <= 4 && stringLength > 0; i++)
		{
			int length = stringLength >= i_ChunkSize ? i_ChunkSize : stringLength;
			string part = remaining.Substring(0, length);
			remaining = remaining.Substring(length);
			string path = string.Format("{0}/part_{1}.txt", Application.persistentDataPath, i);
			File.WriteAllText(path, part);
			stringLength = remaining.Length;
			if (stringLength < length && stringLength > 0)
				i_ChunkSize = stringLength;
			if (i_ChunkSize > stringLength)
				return;
		}
	}
}
