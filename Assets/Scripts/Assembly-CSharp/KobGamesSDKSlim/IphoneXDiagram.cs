using UnityEngine;

namespace KobGamesSDKSlim
{
	public class IphoneXDiagram : MonoBehaviour
	{
		private Canvas Canvas;

		private float IphoneXAspectRatio;

		public IphoneXDiagram()
		{
			IphoneXAspectRatio = 2.1653f;
		}

		private void Awake()
		{
			Canvas = GetComponentInChildren<Canvas>();
		}

		private void Update()
		{
			float screenAspectRatio = (float)Screen.height / Screen.width;
			bool shouldEnable = Mathf.Round(screenAspectRatio * 100f) / 100f ==
				Mathf.Round(IphoneXAspectRatio * 100f) / 100f;
			if (Canvas != null && shouldEnable != Canvas.enabled)
				Canvas.enabled = shouldEnable;
		}
	}
}
