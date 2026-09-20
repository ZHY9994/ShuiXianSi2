using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public abstract class CameraVariablesEditorBase
	{
		public eCameraZoomType ZoomType;

		public CameraZoomData ZoomDolly;

		public CameraZoomData ZoomOrtho;

		public CameraZoomData ZoomPerspective;

		public CameraVariablesEditorBase()
		{
			ZoomDolly = new CameraZoomData(1f, new Vector2(-5f, 5f));
			ZoomOrtho = new CameraZoomData(1f, new Vector2(2f, 5f));
			ZoomPerspective = new CameraZoomData(20f, new Vector2(25f, 80f));
		}

		private bool m_IsMultiTouchDisabled => GameConfig.Instance == null || !GameConfig.Instance.Input.CalculateMultiTouch;
	}
}
