using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public static class Utils
	{
		private static AudioSource[] m_AudioSources;

		public static int CopiedFiles;

		public static int CopiedFolders;

		public static bool RemoveFileOrDirectoryRelativePath(string i_Path)
		{
			string path = Path.Combine(Application.dataPath, i_Path ?? "");
			if (File.Exists(path)) { File.Delete(path); return true; }
			if (Directory.Exists(path)) { Directory.Delete(path, true); return true; }
			return false;
		}

		public static void MuteAllAudioSources()
		{
			m_AudioSources = UnityEngine.Object.FindObjectsOfType<AudioSource>();
			foreach (var source in m_AudioSources) if (source != null) source.mute = true;
		}

		public static void UnMuteAllAudioSources()
		{
			if (m_AudioSources == null) m_AudioSources = UnityEngine.Object.FindObjectsOfType<AudioSource>();
			foreach (var source in m_AudioSources) if (source != null) source.mute = false;
		}

		public static string GetFuncName([CallerMemberName] string i_FuncName = "")
		{
			return i_FuncName;
		}

		public static void SetVertexColor(Color i_Color, Mesh i_Mesh)
		{
			if (i_Mesh == null) return;
			var colors = new Color[i_Mesh.vertexCount];
			for (int i = 0; i < colors.Length; i++) colors[i] = i_Color;
			i_Mesh.colors = colors;
		}

		public static void ClearConsole()
		{
			#if UNITY_EDITOR
			var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor");
			logEntries?.GetMethod("Clear", BindingFlags.Public | BindingFlags.Static)?.Invoke(null, null);
			#endif
		}

		public static void ToggleBool(ref bool i_Value)
		{
			i_Value = !i_Value;
		}

		public static void ClearNullInList<T>(IList<T> i_List)
		{
			if (i_List == null) return;
			for (int i = i_List.Count - 1; i >= 0; --i) if (i_List[i] == null) i_List.RemoveAt(i);
		}

		public static void ClearNullInDic<M, T>(IDictionary<M, T> i_SortedList)
		{
			if (i_SortedList == null) return;
			var keys = new List<M>(); foreach (var pair in i_SortedList) if (pair.Value == null) keys.Add(pair.Key);
			foreach (var key in keys) i_SortedList.Remove(key);
		}

		public static void InitAndClearDic<T>(ref SortedList<int, T> i_SortedList)
		{
			if (i_SortedList == null) i_SortedList = new SortedList<int, T>(); else i_SortedList.Clear();
		}

		public static void OpenUrlStore(string i_GooglePlayId, string i_AppStoreId)
		{
			Application.OpenURL(GetUrlStore(i_GooglePlayId, i_AppStoreId));
		}

		public static void OpenCrossPromoUrlStore(string i_GooglePlayId, string i_AppStoreId)
		{
			Application.OpenURL(GetUrlStore(i_GooglePlayId, i_AppStoreId));
		}

		public static string GetUrlStore(string i_GooglePlayId, string i_AppStoreId)
		{
			#if UNITY_IOS
			return "itms-apps://itunes.apple.com/app/id" + i_AppStoreId;
			#else
			return "https://play.google.com/store/apps/details?id=" + i_GooglePlayId;
			#endif
		}

		public static string FetchAndroidManifestData(string i_NodeName, string i_AttributeName = "android:value")
		{
			string path = Path.Combine(Application.dataPath, "../Plugins/Android/AndroidManifest.xml");
			if (!File.Exists(path)) return string.Empty;
			var document = new XmlDocument();
			document.Load(path);
			var manager = new XmlNamespaceManager(document.NameTable);
			manager.AddNamespace("android", "http://schemas.android.com/apk/res/android");
			var node = document.DocumentElement?.SelectSingleNode($"descendant::meta-data[@{i_NodeName}]", manager) as XmlElement;
			return node == null ? string.Empty : node.GetAttribute(i_AttributeName);
		}

		public static void SaveAndroidManifestData(string i_Value, string i_NodeName, string i_AttributeName = "android:value")
		{
			string path = Path.Combine(Application.dataPath, "../Plugins/Android/AndroidManifest.xml");
			if (!File.Exists(path)) return;
			var document = new XmlDocument();
			document.Load(path);
			var manager = new XmlNamespaceManager(document.NameTable);
			manager.AddNamespace("android", "http://schemas.android.com/apk/res/android");
			var node = document.DocumentElement?.SelectSingleNode($"descendant::meta-data[@{i_NodeName}]", manager) as XmlElement;
			if (node == null) return;
			if (node.GetAttribute(i_AttributeName) != i_Value) { node.SetAttribute(i_AttributeName, "http://schemas.android.com/apk/res/android", i_Value); document.Save(path); }
		}

		public static bool IsAttributionExists(string i_NodeName)
		{
			return !string.IsNullOrEmpty(FetchAndroidManifestData(i_NodeName));
		}

		public static string GetAndroidManifestPackageName()
		{
			string path = Path.Combine(Application.dataPath, "../Plugins/Android/AndroidManifest.xml");
			if (!File.Exists(path)) return string.Empty;
			var document = new XmlDocument(); document.Load(path);
			return document.DocumentElement?.GetAttribute("package") ?? string.Empty;
		}

		public static void AddManifestNode(string i_AttributeName, string i_AttributeValue)
		{
			if (IsAttributionExists(i_AttributeName)) return;
			string path = Path.Combine(Application.dataPath, "../Plugins/Android/AndroidManifest.xml");
			if (!File.Exists(path)) return;
			var document = new XmlDocument(); document.Load(path);
			var root = document.DocumentElement?.SelectSingleNode("descendant::application") as XmlElement; if (root == null) return;
			var node = document.CreateElement("meta-data");
			node.SetAttribute("name", "http://schemas.android.com/apk/res/android", i_AttributeName);
			node.SetAttribute("value", "http://schemas.android.com/apk/res/android", i_AttributeValue);
			root.AppendChild(node); document.Save(path);
		}

		public static void AddManifestAttributeNew(string i_AttributeName, string i_AttributeValue)
		{
			string path = Path.Combine(Application.dataPath, "../Plugins/Android/AndroidManifest.xml");
			if (!File.Exists(path)) return;
			var document = new XmlDocument(); document.Load(path);
			var application = document.DocumentElement?.SelectSingleNode("descendant::application") as XmlElement; if (application == null) return;
			foreach (XmlAttribute attr in application.Attributes)
				if (attr.LocalName.Contains(i_AttributeName)) return;
			application.SetAttribute(i_AttributeName, "http://schemas.android.com/apk/res/android", i_AttributeValue);
			document.Save(path);
		}

		public static void RemoveManifestAttributeNew(string i_AttributeName)
		{
			string path = Path.Combine(Application.dataPath, "../Plugins/Android/AndroidManifest.xml");
			if (!File.Exists(path)) return;
			var document = new XmlDocument(); document.Load(path);
			var nodes = document.SelectNodes("descendant::application"); if (nodes == null) return;
			bool changed = false;
			foreach (XmlElement application in nodes)
			{
				var remove = new List<XmlAttribute>();
				foreach (XmlAttribute attr in application.Attributes)
					if (attr.LocalName.Contains(i_AttributeName)) remove.Add(attr);
				foreach (var attr in remove) { application.RemoveAttributeNode(attr); changed = true; }
			}
			if (changed) document.Save(path);
		}

		public static void RemoveManifestAttribute(string i_AttributeName)
		{
			string path = Path.Combine(Application.dataPath, "../Plugins/Android/AndroidManifest.xml");
			if (!File.Exists(path)) return;
			var document = new XmlDocument(); document.Load(path);
			var manager = new XmlNamespaceManager(document.NameTable); manager.AddNamespace("android", "http://schemas.android.com/apk/res/android");
			var nodes = document.SelectNodes($"//*[@android:name='{i_AttributeName}']", manager);
			if (nodes == null) return;
			foreach (XmlNode node in nodes) node.ParentNode?.RemoveChild(node);
			document.Save(path);
		}

		public static void UpdateAndroidManifestPackageName(string i_PackageName)
		{
			string path = Path.Combine(Application.dataPath, "../Plugins/Android/AndroidManifest.xml");
			if (!File.Exists(path)) return;
			var document = new XmlDocument(); document.Load(path);
			if (document.DocumentElement == null) return;
			document.DocumentElement.SetAttribute("package", i_PackageName ?? string.Empty); document.Save(path);
		}

		public static int GetEnumLength(Type i_Enum)
		{
			return Enum.GetValues(i_Enum).Length;
		}

		public static void QuitApplication()
		{
			Application.Quit();
		}

		public static Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
		{
			return ScreenToUISpace(parentCanvas, parentCanvas.worldCamera.WorldToScreenPoint(worldPos));
		}

		public static Vector3 ScreenToUISpace(Canvas parentCanvas, Vector3 i_ScreenPos)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.GetComponent<RectTransform>(), i_ScreenPos, parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera, out var point);
			return point;
		}

		public static void CopyFolder(string sourceFolder, string destFolder, bool i_Debug = true, bool i_Overwrite = false)
		{
			if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);
			foreach (var file in Directory.GetFiles(sourceFolder)) CopyFile(file, Path.Combine(destFolder, Path.GetFileName(file)), i_Overwrite);
			foreach (var dir in Directory.GetDirectories(sourceFolder)) { CopyFolder(dir, Path.Combine(destFolder, Path.GetFileName(dir)), i_Debug, i_Overwrite); CopiedFolders++; }
		}

		public static void CopyFile(string sourceFile, string destFile, bool i_Overwrite = false)
		{
			File.Copy(sourceFile, destFile, i_Overwrite);
			CopiedFiles++;
		}

		public static UnityEngine.Object InstantiateResourcesObject(ResourcesObject i_Object)
		{
			if (string.IsNullOrEmpty(i_Object.Path)) return null;
			var asset = Resources.Load(i_Object.Path); return asset == null ? null : UnityEngine.Object.Instantiate(asset);
		}

		public static bool TypeExists(params string[] i_Types)
		{
			return GetTypeByString(i_Types) != null;
		}

		public static Type GetTypeByName(string i_TypeName)
		{
			if (string.IsNullOrEmpty(i_TypeName)) return null;
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var type = assembly.GetType(i_TypeName);
				if (type != null) return type;
			}
			return Type.GetType(i_TypeName);
		}

		public static Type GetTypeByString(params string[] i_Types)
		{
			if (i_Types == null) return null;
			foreach (var name in i_Types) { var type = GetTypeByName(name); if (type != null) return type; }
			return null;
		}

		public static object GetFieldValue(string i_TypeName, string i_FieldName)
		{
			var type = GetTypeByName(i_TypeName);
			var field = type?.GetField(i_FieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			return field?.GetValue(null);
		}
	}
}
