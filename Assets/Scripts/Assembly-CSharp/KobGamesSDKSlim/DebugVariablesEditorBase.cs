using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public abstract class DebugVariablesEditorBase
	{
		[SerializeField]
		private bool m_DebugMode;

		[SerializeField]
		private bool m_GodMode;

		[SerializeField]
		private bool m_EmulateLowFPS;

		public Vector2Int LowFPSValues;

		public GameObject IphoneXDiagramPrefab;

		public GameObject AdsDebugButtonsPrefab;

		[SerializeField]
		private bool m_ShowFPSCounter;

		public GameObject FPSCounterPrefab;

		[SerializeField]
		private bool m_ShowGameLoopButtons;

		public GameObject GameLoopButtonsPrefab;

		[SerializeField]
		private bool m_ShowMediationTester;

		public GameObject MediationTesterPrefab;

		public bool DebugMode => Debug.isDebugBuild && m_DebugMode;

		public bool GodMode => Debug.isDebugBuild && m_DebugMode && m_GodMode;

		public bool EmulateLowFPS => Debug.isDebugBuild && m_DebugMode && m_EmulateLowFPS;

		public bool ShowAdsDebugButtons => Debug.isDebugBuild && m_DebugMode;

		public bool ShowFPSCounter => Debug.isDebugBuild && m_DebugMode && m_ShowFPSCounter;

		public bool ShowGameLoopButtons
		{
			get
			{
				return Debug.isDebugBuild && m_DebugMode && m_ShowGameLoopButtons;
			}
			set
			{
				m_ShowGameLoopButtons = value;
			}
		}

		public bool ShowMediationTester
		{
			get
			{
				return Debug.isDebugBuild && m_DebugMode && m_ShowMediationTester;
			}
			set
			{
				m_ShowMediationTester = value;
			}
		}

		private void OnEmulateLowFPSChange()
		{
			if (!Debug.isDebugBuild || !m_DebugMode || !m_EmulateLowFPS)
				GameManager.Instance.SetDefaultFPS();
		}

		public void SetRefs()
		{
			return;
		}

		public void InitOnGameManagerAwakeEvent()
		{
			if (Debug.isDebugBuild && m_DebugMode && m_ShowFPSCounter)
				if (FPSCounterPrefab != null) UnityEngine.Object.Instantiate(FPSCounterPrefab);
			if (Debug.isDebugBuild && m_DebugMode && m_ShowGameLoopButtons)
				if (GameLoopButtonsPrefab != null) UnityEngine.Object.Instantiate(GameLoopButtonsPrefab);
			if (Debug.isDebugBuild && m_DebugMode && m_ShowMediationTester)
				if (MediationTesterPrefab != null) UnityEngine.Object.Instantiate(MediationTesterPrefab);
			if (Debug.isDebugBuild && m_DebugMode)
				if (AdsDebugButtonsPrefab != null) UnityEngine.Object.Instantiate(AdsDebugButtonsPrefab);
		}
	}
}
