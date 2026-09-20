using System.Collections.Generic;
using KobGamesSDKSlim.GameManagerV1;
using UnityEngine;

namespace KobGamesSDKSlim.MenuManagerV1
{
	[ExecutionOrder(-60)]
	public class MenuManagerBase : Singleton<MenuManager>
	{
		public MenuScreensRefs MenuScreensRefs;

		private readonly List<string> OpenedScreens;

		public override void Start()
		{
			base.Start();
		}

		protected new virtual void OnEnable()
		{
			GameManagerBase.OnGameReset += OnCoreLoop_Reset;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			GameManagerBase.OnGameReset -= OnCoreLoop_Reset;
		}

		public virtual void OnCoreLoop_Reset()
		{
			OpenedScreens.Clear();
			if (MenuScreensRefs?.MenuScreenList == null) return;
			foreach (MenuScreenBase screen in MenuScreensRefs.MenuScreenList.Values)
				if (screen != null) screen.Reset();
		}

		public void OpenMenuScreen(string i_MenuScreen, bool i_Animate)
		{
			if (MenuScreensRefs != null && MenuScreensRefs.MenuScreenList != null &&
				MenuScreensRefs.MenuScreenList.ContainsKey(i_MenuScreen))
				MenuScreensRefs.MenuScreenList[i_MenuScreen].Open(i_Animate);
		}

		public void CloseMenuScreen(string i_MenuScreen, bool i_Animate)
		{
			if (MenuScreensRefs != null && MenuScreensRefs.MenuScreenList != null &&
				MenuScreensRefs.MenuScreenList.ContainsKey(i_MenuScreen))
				MenuScreensRefs.MenuScreenList[i_MenuScreen].Close(i_Animate);
		}

		public void OpenMenuScreen(string i_MenuScreen)
		{
			if (i_MenuScreen == "Screen_WeaponShop" || i_MenuScreen == "Screen_SkinShop")
				GameTraceLog.Log($"[ShopTrace] MenuManagerBase.OpenMenuScreen key={i_MenuScreen} refs={(MenuScreensRefs == null ? "NULL" : "SET")} count={(MenuScreensRefs?.MenuScreenList == null ? -1 : MenuScreensRefs.MenuScreenList.Count)} contains={(MenuScreensRefs?.MenuScreenList != null && MenuScreensRefs.MenuScreenList.ContainsKey(i_MenuScreen))}");
			if (MenuScreensRefs != null && MenuScreensRefs.MenuScreenList != null &&
				MenuScreensRefs.MenuScreenList.ContainsKey(i_MenuScreen))
				MenuScreensRefs.MenuScreenList[i_MenuScreen].Open();
		}

		public void CloseMenuScreen(string i_MenuScreen)
		{
			if (MenuScreensRefs != null && MenuScreensRefs.MenuScreenList != null &&
				MenuScreensRefs.MenuScreenList.ContainsKey(i_MenuScreen))
				MenuScreensRefs.MenuScreenList[i_MenuScreen].Close();
		}

		public void CloseAll()
		{
			if (MenuScreensRefs?.MenuScreenList == null) return;
			foreach (MenuScreenBase screen in MenuScreensRefs.MenuScreenList.Values)
				if (screen != null) screen.Close();
		}

		public T GetMenuScreen<T>() where T : MenuScreenBase
		{
			if (MenuScreensRefs?.MenuScreenList == null) return null;
			foreach (MenuScreenBase screen in MenuScreensRefs.MenuScreenList.Values)
				if (screen is T result) return result;
			return null;
		}

		public bool IsScreenOpened(string i_MenuScreen)
		{
			if (MenuScreensRefs?.MenuScreenList == null) return false;
			return OpenedScreens.Contains(MenuScreensRefs.MenuScreenList[i_MenuScreen].name);
		}

		public virtual void OnMenu_ScreenOpenStart(MenuScreenBase i_MenuScreen)
		{
			return;
		}

		public virtual void OnMenu_ScreenOpenEnd(MenuScreenBase i_MenuScreen)
		{
			if (i_MenuScreen != null) OpenedScreens.Add(i_MenuScreen.name);
		}

		public virtual void OnMenu_ScreenCloseStart(MenuScreenBase i_MenuScreen)
		{
			return;
		}

		public virtual void OnMenu_ScreenCloseEnd(MenuScreenBase i_MenuScreen)
		{
			if (i_MenuScreen != null) OpenedScreens.Remove(i_MenuScreen.name);
		}

		public MenuManagerBase()
		{
			OpenedScreens = new List<string>();
		}
	}
}
