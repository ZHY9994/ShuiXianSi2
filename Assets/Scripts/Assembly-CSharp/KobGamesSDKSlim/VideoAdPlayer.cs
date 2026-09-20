using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace KobGamesSDKSlim
{
	public class VideoAdPlayer : MonoBehaviour
	{
		public VideoPlayer VideoPlayer;

		public bool PlayWhenReady = true;

		public string VideoAdUrlBase64;

		public string VideoAdUrlMP4;

		private const string k_AdName = "VideoAd.mp4";

		private void Start()
		{
			if (!string.IsNullOrEmpty(VideoAdUrlBase64))
				StartCoroutine(DownloadBase64());
		}

		private IEnumerator DownloadBase64()
		{
			UnityWebRequest req = UnityWebRequest.Get(VideoAdUrlBase64);
			try
			{
				yield return req.SendWebRequest();
				if (!req.isNetworkError && !req.isHttpError)
				{
					string path = Application.persistentDataPath + "/VideoAd.mp4";
					byte[] bytes = Convert.FromBase64String(req.downloadHandler.text);
					File.WriteAllBytes(path, bytes);
					string url = "file://" + Application.persistentDataPath + "/VideoAd.mp4";
					VideoPlayer.url = url;
					VideoPlayer.Prepare();
				}
			}
			finally
			{
				req.Dispose();
			}
		}

		private void Update()
		{
			if (PlayWhenReady && VideoPlayer != null && VideoPlayer.isPrepared && !VideoPlayer.isPlaying)
			{
				if (transform.childCount > 0) transform.GetChild(0).gameObject.SetActive(true);
				VideoPlayer.Play();
			}
		}

		private void Reset()
		{
			if (VideoPlayer == null) VideoPlayer = GetComponentInChildren<VideoPlayer>();
		}
	}
}
