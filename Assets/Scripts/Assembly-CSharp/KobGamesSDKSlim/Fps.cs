using System;
using System.Collections;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class Fps : MonoBehaviour
	{
		public Color textColor = Color.white;

		private string label = string.Empty;

		private float count;

		private GUIStyle style = new GUIStyle();

		private IEnumerator Start()
		{
			style.normal.textColor = textColor;
			style.fontSize = 38;
			GUI.depth = 2;
			while (true)
			{
				if (Time.timeScale == 1f)
				{
					float fps = 1f / Time.deltaTime;
					count = fps;
					label = "FPS :" + Mathf.Round(fps);
					yield return new WaitForSeconds(0.1f);
				}
				else
				{
					label = "Pause";
					yield return new WaitForSeconds(0.5f);
				}
			}
		}

		private void OnGUI()
		{
			GUI.Label(new Rect(5f, 10f, 100f, 25f), label, style);
		}
	}
}
