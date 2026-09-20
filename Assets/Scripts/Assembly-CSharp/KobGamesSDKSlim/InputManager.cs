using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-75)]
	public class InputManager : Singleton<InputManager>
	{
		private ScreenData m_ScreenData;

		[SerializeField]
		private bool LogScreenData;

		private bool m_IsInputDown;

		private Vector3 m_InputDownPosNormalized;

		private Vector3 m_LastInputPosNormalized;

		private Vector2 m_DeltaDrag;

		private Vector2 m_Drag;

		public static Action<Vector2> OnInputDown;

		public static Action OnInputUp;

		public static Action<Vector2> OnDragDelta;

		public static Action<Vector2> OnDrag;

		private List<Touch> m_InitTouchList = new List<Touch>();

		private List<Touch> m_LastTouchList = new List<Touch>();

		private List<Touch> m_CurrTouchList = new List<Touch>();

		private float m_TouchZoom;

		private float m_TouchZoomDelta;

		public static Action<float> OnTouchZoomDelta;

		public static Action<float> OnTouchZoom;

		private Vector2 m_JoystickDirection = Vector2.zero;

		private Vector2 m_JoystickPosition = Vector2.zero;

		private Vector2 m_JoystickHandleLocalPosition = Vector2.zero;

		public ScreenData ScreenData => m_ScreenData;

		public bool IsInputDown => m_IsInputDown;

		private Vector3 m_MousePosNormalized => new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0f);

		public Vector2 DeltaDrag => m_DeltaDrag;

		public Vector2 Drag => m_Drag;

		public Vector3 MousePosition => Input.mousePosition;

		public float TouchZoomDelta => m_TouchZoomDelta;

		public float TouchZoom => m_TouchZoom;

		private Vector2 m_JoystickPositionNormalized => m_JoystickPosition / new Vector2(Screen.width, Screen.height);

		private Vector2 m_JoystickPositionFromMousePosAndDirection => new Vector2(m_MousePosNormalized.x, m_MousePosNormalized.y) + m_JoystickDirection;

		public Vector2 JoystickDirection => m_JoystickDirection;

		public Vector2 JoystickPosition => m_JoystickPosition;

		public Vector2 JoystickHandleLocalPosition => m_JoystickHandleLocalPosition;

		private InputVariablesEditor m_InputVars => GameConfig.Instance != null ? GameConfig.Instance.Input : null;

		private InputVariablesEditorBase.JoystickData m_JoystickVars => m_InputVars?.Joystick;

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			m_ScreenData = new ScreenData();
			m_ScreenData.CalculateData(LogScreenData);
			resetTouchLists();
		}

		private new void OnEnable()
		{
			GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
			GameManagerV1.GameManagerBase.OnScreenChanged += onScreenChanged;
		}

		public override void OnDisable()
		{
			GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
			GameManagerV1.GameManagerBase.OnScreenChanged -= onScreenChanged;
		}

		private void onLevelStarted()
		{
			m_JoystickDirection = Vector2.zero;
			m_JoystickPosition = Vector2.zero;
			m_JoystickHandleLocalPosition = Vector2.zero;
		}

		private void onScreenChanged(int i_width, int i_height)
		{
			if (m_ScreenData == null) throw new NullReferenceException(nameof(m_ScreenData));
			m_ScreenData.CalculateData(false);
		}

		private void ResetValues()
		{
			m_DeltaDrag = Vector2.zero;
			m_Drag = Vector2.zero;
			m_TouchZoom = 0f;
			m_TouchZoomDelta = 0f;
			m_InitTouchList?.Clear();
			m_LastTouchList?.Clear();
		}

		private void Update()
		{
			if (m_InputVars != null && m_InputVars.CalculateMultiTouch) calculateMultiTouch();
		}

		private void FixedUpdate()
		{
			if (!m_IsInputDown) return;
			Vector3 current = m_MousePosNormalized;
			m_Drag = new Vector2(current.x - m_InputDownPosNormalized.x, current.y - m_InputDownPosNormalized.y);
			m_DeltaDrag = new Vector2(current.x - m_LastInputPosNormalized.x, current.y - m_LastInputPosNormalized.y);
			if (m_Drag.sqrMagnitude > 0.0000001f) OnDrag?.Invoke(m_Drag);
			if (m_DeltaDrag.sqrMagnitude > 0.0000001f) OnDragDelta?.Invoke(m_DeltaDrag);
			m_LastInputPosNormalized = current;
			if (m_InputVars != null && m_InputVars.IsUseJoystick) computeJoystick();
		}

		public void InputDown()
		{
			m_IsInputDown = true;
			m_InputDownPosNormalized = m_MousePosNormalized;
			m_LastInputPosNormalized = m_InputDownPosNormalized;
			ResetValues();
			resetJoystick();
			OnInputDown?.Invoke(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
		}

		public void InputUp()
		{
			m_IsInputDown = false;
			ResetValues();
			OnInputUp?.Invoke();
		}

		private void calculateMultiTouch()
		{
			calculateCurrTouchList();
			if (m_CurrTouchList == null || m_InitTouchList == null) return;
			if (m_CurrTouchList.Count != m_InitTouchList.Count) { resetTouchLists(); return; }
			if (m_CurrTouchList.Count < 2) return;
			var a = m_CurrTouchList[0].position;
			var b = m_CurrTouchList[1].position;
			var ia = m_InitTouchList[0].position;
			var ib = m_InitTouchList[1].position;
			m_TouchZoom = Vector2.Distance(a, b) - Vector2.Distance(ia, ib);
			if (m_LastTouchList != null && m_LastTouchList.Count >= 2)
				m_TouchZoomDelta = Vector2.Distance(a, b) - Vector2.Distance(m_LastTouchList[0].position, m_LastTouchList[1].position);
			cloneTouchList(ref m_LastTouchList, m_CurrTouchList);
			if (m_ScreenData != null)
			{
				OnTouchZoom?.Invoke(m_TouchZoom / m_ScreenData.FinalDPI);
				OnTouchZoomDelta?.Invoke(m_TouchZoomDelta / m_ScreenData.FinalDPI);
			}
		}

		private void calculateCurrTouchList()
		{
			if (m_CurrTouchList == null) m_CurrTouchList = new List<Touch>();
			m_CurrTouchList.Clear();
			for (int i = 0; i < Input.touchCount; i++) m_CurrTouchList.Add(Input.GetTouch(i));
			if (m_InitTouchList != null && m_InitTouchList.Count == 0)
				m_InitTouchList.AddRange(m_CurrTouchList);
		}

		private void resetTouchLists()
		{
			m_InitTouchList = new List<Touch>();
			m_LastTouchList = new List<Touch>();
			m_CurrTouchList = new List<Touch>();
		}

		private void cloneTouchList(ref List<Touch> i_TouchesRef, List<Touch> i_ListToCopyFrom)
		{
			if (i_TouchesRef == null) i_TouchesRef = new List<Touch>();
			i_TouchesRef.Clear();
			if (i_ListToCopyFrom != null) i_TouchesRef.AddRange(i_ListToCopyFrom);
		}

		private void resetJoystick()
		{
			if (m_InputVars == null || !m_InputVars.IsUseJoystick || m_JoystickVars == null) return;
			if (m_JoystickVars.IsResetDirection)
			{
				if (m_JoystickDirection.sqrMagnitude < 0.0000001f)
				{
					m_JoystickDirection = Vector2.zero;
					m_JoystickPosition = Input.mousePosition;
					m_JoystickHandleLocalPosition = m_JoystickPosition;
				}
				else
				{
					m_JoystickDirection = m_JoystickDirection.normalized * 0.1f;
					m_JoystickPosition = (Vector2)Input.mousePosition - m_JoystickDirection;
				}
			}
			else
			{
				m_JoystickPosition = m_JoystickPositionFromMousePosAndDirection;
				m_JoystickHandleLocalPosition = m_JoystickDirection;
			}
			computeJoystick();
		}

		private void computeJoystick()
		{
			if (m_ScreenData == null || m_JoystickVars == null) return;
			m_JoystickDirection = (new Vector2(m_MousePosNormalized.x, m_MousePosNormalized.y) - m_JoystickPosition / m_ScreenData.FinalDPI) / m_JoystickVars.Radius;
			if (m_JoystickDirection.magnitude > 1f)
			{
				m_JoystickDirection = m_JoystickDirection.normalized;
				if (!m_JoystickVars.IsStatic) m_JoystickPosition = m_JoystickPositionFromMousePosAndDirection;
			}
			m_JoystickHandleLocalPosition = m_JoystickDirection;
		}
	}
}
