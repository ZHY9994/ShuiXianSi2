using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.MenuManagerV1
{
	[ExecutionOrder(-55)]
	public class MenuScreensRefs : MonoBehaviour
	{
		public List<MenuScreenBase> ScreensList = new List<MenuScreenBase>();

		private MenuScreenBase m_Screen;

		public Dictionary<string, MenuScreenBase> MenuScreenList = new Dictionary<string, MenuScreenBase>();

		private void HideAllScreens()
		{
			if (ScreensList == null) return;
			ScreensList.ForEach(x => x.gameObject.SetActive(false));
		}

		private void reloadScreensList()
		{
			if (ScreensList == null) ScreensList = new List<MenuScreenBase>();
			ScreensList.Clear();
			foreach (MenuScreenBase screen in GetComponentsInChildren<MenuScreenBase>(true))
				if (screen != null) ScreensList.Add(screen);
		}

		public void Awake()
		{
			GameTraceLog.Log($"[ShopTrace] MenuScreensRefs.Awake begin go={name} active={gameObject.activeInHierarchy}");
			// IDA 0x022304C8: register this component into MenuManagerBase at runtime.
			// The scene field is intentionally {fileID: 0}; it is an internal runtime bridge.
			MenuManager.Instance.MenuScreensRefs = this;
			MenuScreenBase[] screens = GetComponentsInChildren<MenuScreenBase>(true);
			foreach (MenuScreenBase screen in screens)
				if (screen != null) AddScreen(screen.GetType().Name, screen);
			GameTraceLog.Log($"[ShopTrace] MenuScreensRefs.Awake complete refs={(MenuManager.Instance.MenuScreensRefs == this)} count={MenuScreenList.Count} weapon={MenuScreenList.ContainsKey("Screen_WeaponShop")}");
		}

		public void AddScreen(string i_Name, MenuScreenBase i_MenuScreenBase)
		{
			if (MenuScreenList.ContainsKey(i_Name)) return;
			MenuScreenList.Add(i_Name, i_MenuScreenBase);
		}
	}
}
