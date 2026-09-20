using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public abstract class InputVariablesEditorBase
	{
		public InputVariablesEditorBase()
		{
			DragSensitivity = Vector2.one;
		}
		[Serializable]
		public class JoystickData
		{
			public JoystickData()
			{
				Radius = 120f;
				HandleRadiusMultiplier = 0.25f;
			}
			public bool IsShowVisuals;

			public bool IsStatic;

			public bool IsResetDirection;

			public float Radius;

			public float HandleRadiusMultiplier;
		}

		public Vector2 DragSensitivity;

		public bool IsUseJoystick;

		public JoystickData Joystick;

		public bool CalculateMultiTouch;
	}
}
