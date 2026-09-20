using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public static class ExtensionMethods
	{
		private static readonly System.Random random;

		private static readonly object syncLock;

		public static void ShowDebugErrorIfNull<T>(this T i_Target, string i_ClassName, string i_FuncName, string i_VariableName)
		{
			if (i_Target == null) Debug.LogError($"{i_ClassName}-{i_FuncName}: {i_VariableName} is null");
		}

		public static bool Contains<T>(this T[] i_Array, T i_Item)
		{
			return i_Array != null && Array.IndexOf(i_Array, i_Item) >= 0;
		}

		public static void ForEach2<T>(this T[] i_Array, Action<T> i_Action)
		{
			if (i_Array == null || i_Action == null) return;
			foreach (var item in i_Array) i_Action(item);
		}

		public static bool IsNull(this UnityEngine.Object obj)
		{
			return obj == null || !obj;
		}

		public static T DeQueue<T>(this IList<T> i_List)
		{
			if (i_List == null || i_List.Count == 0) return default(T);
			T result = i_List[0]; i_List.RemoveAt(0); return result;
		}

		public static T GetRandomItem<T>(this IList<T> i_List)
		{
			return i_List == null || i_List.Count == 0 ? default(T) : i_List[UnityEngine.Random.Range(0, i_List.Count)];
		}

		public static void GetRandomItem(this Type i_List)
		{
			// Kept for binary/API compatibility; the original method has no side effects.
		}

		public static void EnQueue<T>(this IList<T> i_List, T i_Item)
		{
			if (i_List != null) i_List.Add(i_Item);
		}

		public static void Shuffle<T>(this IList<T> i_List)
		{
			if (i_List == null) return;
			for (int i = i_List.Count - 1; i > 0; --i) i_List.Swap(i, UnityEngine.Random.Range(0, i + 1));
		}

		public static void Swap<T>(this IList<T> list, int i, int j)
		{
			T value = list[i]; list[i] = list[j]; list[j] = value;
		}

		public static Sprite ConvertToSprite(this Texture2D i_Texture)
		{
			if (i_Texture == null) throw new ArgumentNullException(nameof(i_Texture));
			return Sprite.Create(i_Texture, new Rect(0f, 0f, i_Texture.width, i_Texture.height), new Vector2(0.5f, 0.5f), 100f);
		}

		public static void Process(this Exception i_Exception, string i_Class, string i_FunctionName)
		{
			if (i_Exception == null) return;
			Debug.LogError(string.Format("{0}-{1} - Error: {2}  Stack: {3}", i_Class, i_FunctionName, i_Exception.Message, i_Exception.StackTrace));
		}

		public static string SetColor(this string i_String, Color i_Color)
		{
			return string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(i_Color), i_String);
		}

		public static bool ToBoolean(this int i_Int)
		{
			return i_Int == 1;
		}

		public static int ToInt(this bool i_Bool)
		{
			return i_Bool ? 1 : 0;
		}

		public static List<T> FindObjectsOfTypeAll<T>()
		{
			return Resources.FindObjectsOfTypeAll<UnityEngine.Object>().OfType<T>().ToList();
		}

		public static void AddListener(this Button i_Button, UnityAction i_Action)
		{
			if (i_Button != null && i_Action != null) i_Button.onClick.AddListener(i_Action);
		}

		public static void RemoveListener(this Button i_Button, UnityAction i_Action)
		{
			if (i_Button != null && i_Action != null) i_Button.onClick.RemoveListener(i_Action);
		}

		public static void Reset(this Transform i_Transform)
		{
			if (i_Transform == null) throw new ArgumentNullException(nameof(i_Transform));
			i_Transform.localPosition = Vector3.zero;
			i_Transform.localRotation = Quaternion.identity;
			i_Transform.localScale = Vector3.one;
		}

		public static void ReferenceCheck<T>(this T i_Item, string i_ClassName = "", string i_MemberName = "")
		{
			if (i_Item == null) Debug.LogError($"{i_ClassName}-{i_MemberName} is null");
		}

		public static void ReferenceCheck<T>(this T[] i_Array, string i_ClassName = "", string i_MemberName = "")
		{
			if (i_Array == null) Debug.LogError($"{i_ClassName}-{i_MemberName} is null");
		}

		public static Vector3 Change(this Vector3 org, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(x ?? org.x, y ?? org.y, z ?? org.z);
		}

		public static Quaternion ToQuaternion(this Vector3 i_Vector3)
		{
			return Quaternion.Euler(i_Vector3);
		}

		public static void RemoveIfNotNull<T>(this SortedList<int, T> i_List, T i_Value)
		{
			if (i_List != null && i_Value != null) i_List.RemoveAt(i_List.IndexOfValue(i_Value));
		}

		public static void AddIfNotNull<T>(this SortedList<int, T> i_List, T i_Value, Type i_Exclude, int i_KeyOrder = 0)
		{
			if (i_List != null && i_Value != null && (i_Exclude == null || i_Value.GetType() != i_Exclude)) i_List.Add(i_KeyOrder, i_Value);
		}

		public static void AddIfNotNull<T>(this ICollection<T> i_List, T i_Value, Type i_Exclude)
		{
			if (i_List != null && i_Value != null && (i_Exclude == null || i_Value.GetType() != i_Exclude)) i_List.Add(i_Value);
		}

		public static void AddIfNotExists<T>(this ICollection<T> i_List, T i_Value)
		{
			if (i_List != null && i_Value != null && !i_List.Contains(i_Value)) i_List.Add(i_Value);
		}

		public static void AddIfNotNull<T>(this ICollection<T> i_List, T i_Value)
		{
			if (i_List != null && i_Value != null) i_List.Add(i_Value);
		}

		public static void RemoveIfNotNull<T>(this ICollection<T> i_List, T i_Value)
		{
			if (i_List != null && i_Value != null) i_List.Remove(i_Value);
		}

		public static bool IsBetween(this float val, float low, float high)
		{
			return val <= high && val >= low;
		}

		public static bool IsBetween(this int val, int low, int high)
		{
			return val >= low && val <= high;
		}

		public static bool IsBetween(this int val, float low, float high)
		{
			return val >= low && val <= high;
		}

		public static void AddX(this Transform i_Transform, float i_X)
		{
			if (i_Transform != null) i_Transform.position = i_Transform.position.Change(x: i_Transform.position.x + i_X);
		}

		public static void AddY(this Transform i_Transform, float i_Y)
		{
			if (i_Transform != null) i_Transform.position = i_Transform.position.Change(y: i_Transform.position.y + i_Y);
		}

		public static void AddLocalY(this Transform i_Transform, float i_Y)
		{
			if (i_Transform != null) i_Transform.localPosition = i_Transform.localPosition.Change(y: i_Transform.localPosition.y + i_Y);
		}

		public static void SetX(this Transform i_Transform, float i_X)
		{
			if (i_Transform != null) i_Transform.position = i_Transform.position.Change(x: i_X);
		}

		public static void SetY(this Transform i_Transform, float i_Y)
		{
			if (i_Transform != null) i_Transform.position = i_Transform.position.Change(y: i_Y);
		}

		public static void SetZ(this Transform i_Transform, float i_Z)
		{
			if (i_Transform != null) i_Transform.position = i_Transform.position.Change(z: i_Z);
		}

		public static void SetLocalX(this Transform i_Transform, float i_X)
		{
			if (i_Transform != null) i_Transform.localPosition = i_Transform.localPosition.Change(x: i_X);
		}

		public static void SetLocalY(this Transform i_Transform, float i_Y)
		{
			if (i_Transform != null) i_Transform.localPosition = i_Transform.localPosition.Change(y: i_Y);
		}

		public static void SetLocalZ(this Transform i_Transform, float i_Z)
		{
			if (i_Transform != null) i_Transform.localPosition = i_Transform.localPosition.Change(z: i_Z);
		}

		public static void SetLocalScaleX(this Transform i_Transform, float i_X)
		{
			if (i_Transform != null) i_Transform.localScale = i_Transform.localScale.Change(x: i_X);
		}

		public static void SetLocalScaleY(this Transform i_Transform, float i_Y)
		{
			if (i_Transform != null) i_Transform.localScale = i_Transform.localScale.Change(y: i_Y);
		}

		public static void SetLocalScaleZ(this Transform i_Transform, float i_Z)
		{
			if (i_Transform != null) i_Transform.localScale = i_Transform.localScale.Change(z: i_Z);
		}

		public static void SetAlpha(this Material i_Material, float i_A)
		{
			if (i_Material != null && i_Material.HasProperty("_Color")) i_Material.color = i_Material.color.SetAlpha(i_A);
		}

		public static Color SetAlpha(this Color i_Color, float i_Alpha)
		{
			i_Color.a = i_Alpha; return i_Color;
		}

		public static Vector3 Clone(this Vector3 i_Vector3)
		{
			return new Vector3(i_Vector3.x, i_Vector3.y, i_Vector3.z);
		}

		public static Vector3 Round(this Vector3 i_Vector3)
		{
			return new Vector3((float)Math.Round(i_Vector3.x), (float)Math.Round(i_Vector3.y), (float)Math.Round(i_Vector3.z));
		}

		public static void SetLayerRecursively(this Transform i_Transform, string i_LayerName)
		{
			if (i_Transform != null) i_Transform.SetLayerRecursively(LayerMask.NameToLayer(i_LayerName));
		}

		public static void SetLayerRecursively(this Transform i_Transform, int i_LayerNumber)
		{
			if (i_Transform == null) return; i_Transform.gameObject.layer = i_LayerNumber; foreach (Transform child in i_Transform) child.SetLayerRecursively(i_LayerNumber);
		}

		public static void SetActiveRecursivelyF(this GameObject i_GameObject, bool i_State, bool i_IncludeSelf = true)
		{
			if (i_GameObject == null) return; if (i_IncludeSelf) i_GameObject.SetActive(i_State); foreach (Transform child in i_GameObject.transform) child.gameObject.SetActiveRecursivelyF(i_State, true);
		}

		public static Transform Search(this Transform i_Target, string i_Name)
		{
			if (i_Target == null) return null; if (i_Target.name == i_Name) return i_Target; foreach (Transform child in i_Target) { var found = child.Search(i_Name); if (found != null) return found; } return null;
		}

		public static T GetSafeComponent<T>(this GameObject obj) where T : MonoBehaviour
		{
			return obj == null ? null : obj.GetComponent<T>();
		}

		public static void InvokeSafe(this Action i_Action)
		{
			i_Action?.Invoke();
		}

		public static void InvokeSafe(this UnityEvent i_UnityEvent)
		{
			i_UnityEvent?.Invoke();
		}

		public static void InvokeSafe<T>(this Action<T> i_Action, T i_Value)
		{
			i_Action?.Invoke(i_Value);
		}

		public static void InvokeSafe<T, L>(this Action<T, L> i_Action, T i_Value1, L i_Value2)
		{
			i_Action?.Invoke(i_Value1, i_Value2);
		}

		public static void InvokeSafe(this UnityAction i_Action)
		{
			i_Action?.Invoke();
		}

		public static void InvokeSafe<T>(this UnityAction<T> i_Action, T i_Value1)
		{
			i_Action?.Invoke(i_Value1);
		}

		public static void InvokeSafe<T, L>(this UnityAction<T, L> i_Action, T i_Value1, L i_Value2)
		{
			i_Action?.Invoke(i_Value1, i_Value2);
		}

		public static void ActivateChildren(this GameObject i_GameObject, bool i_Activate)
		{
			if (i_GameObject == null) return; foreach (Transform child in i_GameObject.transform) child.gameObject.SetActive(i_Activate);
		}

		public static T GetComponentInChildrenNoRoot<T>(this Transform i_Transform) where T : Component
		{
			if (i_Transform == null) return null; foreach (Transform child in i_Transform) { var c = child.GetComponentInChildren<T>(true); if (c != null) return c; } return null;
		}

		public static T GetComponenetByTagSafe<T>(this MonoBehaviour i_GameObject, string i_Tag) where T : Component
		{
			try { var go = i_GameObject == null ? null : GameObject.FindGameObjectWithTag(i_Tag); return go == null ? null : go.GetComponent<T>(); } catch { return null; }
		}

		public static T[] GetComponenetsByTagSafe<T>(this MonoBehaviour i_GameObject, string i_Tag)
		{
			try { return GameObject.FindGameObjectsWithTag(i_Tag).Select(x => x.GetComponent<T>()).Where(x => x != null).ToArray(); } catch { return Array.Empty<T>(); }
		}

		public static T[] GetComponenetsInChildrenByTagSafe<T>(this MonoBehaviour i_GameObject, string i_Tag)
		{
			if (i_GameObject == null) return Array.Empty<T>(); return i_GameObject.GetComponentsInChildren<Transform>(true).Where(x => x.CompareTag(i_Tag)).Select(x => x.GetComponent<T>()).Where(x => x != null).ToArray();
		}

		public static T GetItem<T>(this List<T> i_List, int i_Index) where T : class
		{
			return i_List != null && i_Index >= 0 && i_Index < i_List.Count ? i_List[i_Index] : null;
		}

		public static T FindObject<T>(this GameObject i_Parent, string i_Name, bool i_IncludeInactive = true, bool i_FindGlobal = false) where T : Component
		{
			if (i_FindGlobal) return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(x => x != null && x.name == i_Name);
			return i_Parent == null ? default(T) : i_Parent.transform.FindDeepChild<T>(i_Name);
		}

		public static T FindObjectTag<T>(this GameObject i_Parent, string i_TagName, bool i_IncludeInactive = true, bool i_FindGlobal = false) where T : Component
		{
			if (i_FindGlobal) return Resources.FindObjectsOfTypeAll<T>().FirstOrDefault(x => (x as Component)?.CompareTag(i_TagName) == true);
			return i_Parent == null ? default(T) : i_Parent.GetComponentsInChildren<T>(i_IncludeInactive).FirstOrDefault(x => (x as Component).CompareTag(i_TagName));
		}

		public static Transform FindParentThatHasChild(this Transform i_Parent, string i_Name, int i_Deep = 15)
		{
			if (i_Parent == null || i_Deep < 0) return null; if (i_Parent.Find(i_Name) != null) return i_Parent; return i_Parent.parent.FindParentThatHasChild(i_Name, i_Deep - 1);
		}

		public static Transform FindDeepChild(this Transform aParent, string aName)
		{
			return aParent.Search(aName);
		}

		public static T FindDeepChild<T>(this Transform aParent, string aName)
		{
			var t = aParent.FindDeepChild(aName); return t == null ? default(T) : t.GetComponent<T>();
		}

		public static void SetParent(this MonoBehaviour i_Target, Transform i_Parent)
		{
			if (i_Target != null) i_Target.transform.SetParent(i_Parent, false);
		}

		public static Bounds ComputeTotalBounds(this Transform i_Target)
		{
			var renderers = i_Target == null ? Array.Empty<Renderer>() : i_Target.GetComponentsInChildren<Renderer>(true); if (renderers.Length == 0) return new Bounds(i_Target == null ? Vector3.zero : i_Target.position, Vector3.zero); var b = renderers[0].bounds; for (int i = 1; i < renderers.Length; i++) b.Encapsulate(renderers[i].bounds); return b;
		}

		public static void RegisterOnce(this UnityEvent i_Action, UnityAction i_Callback)
		{
			if (i_Action == null) throw new ArgumentNullException(nameof(i_Action));
			UnityAction tempAction = null;
			tempAction = () =>
			{
				i_Callback?.Invoke();
				i_Action.RemoveListener(tempAction);
			};
			i_Action.AddListener(tempAction);
		}

		public static void SetAlpha(this Text i_Text, float i_Alpha)
		{
			if (i_Text != null) i_Text.color = i_Text.color.SetAlpha(i_Alpha);
		}

		public static string GetUniqueHash()
		{
			return Guid.NewGuid().ToString("N");
		}
	}
}
