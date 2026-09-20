using UnityEngine;

namespace KobGamesSDKSlim.ProjectValidator
{
	[ExecutionOrder(-850)]
	public class ForceCanvasRenderCameraSpace : MonoBehaviour
	{
		[SerializeField]
		private Canvas m_Canvas;

		private void Awake()
		{
			if (m_Canvas == null)
				throw new System.NullReferenceException();
			m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
			m_Canvas.worldCamera = CameraManager.Instance.MainCamera;
		}
	}
}
