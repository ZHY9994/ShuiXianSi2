using System.Reflection;
using Sirenix.OdinInspector;

namespace KobGamesSDKSlim
{
	public class ShortcutsActionsBase
	{
		public const string LevelComplete = "Level Complete";

		public const string LevelFailedWithRevive = "Level Failed With Revive";

		public const string LevelFailed = "Level Failed";

		public const string PrevLevel = "Previous Level";

		public const string NextLevel = "Next Level";

		public const string ResetLevel = "Reset Level";

		public const string ResetGame = "Reset Game";

		public const string ToggleGameLoopButtons = "Toggle GameLoop Buttons";

		public const string ShowBanner = "Show Banner";

		public const string HideBanner = "Hide Banner";

		public const string EditorPauseToggle = "Editor Pause Toggle";

		public const string OpenGameSettings = "Open Game Settings";

		public const string OpenGameConfig = "Open Game Config";

		public const string SetExtendedHierarchy = "Set Extended Hierarchy";

		public static ValueDropdownList<string> AllFields
		{
			get
			{
				var result = new ValueDropdownList<string>();
				foreach (var field in Fields)
				{
					var value = field.GetValue(null) as string;
					if (value != null)
						result.Add(field.Name, value);
				}
				return result;
			}
		}

		public static ValueDropdownList<string> NotUsedFields
		{
			get
			{
				var result = AllFields;
				var settings = SingletonScriptableObject<GameSettings>.Instance;
				if (settings != null)
				{
					foreach (var field in settings.GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
					{
						var value = field.GetValue(field.IsStatic ? null : settings) as string;
						if (value == null)
							continue;
						for (var i = result.Count - 1; i >= 0; i--)
							if (result[i].Value == value)
								result.RemoveAt(i);
					}
				}
				if (result.Count == 0)
					result.Add("Dummy", "Dummy");
				return result;
			}
		}

		private static FieldInfo[] Fields
		{
			get
			{
				var fields = new System.Collections.Generic.List<FieldInfo>();
				fields.AddRange(typeof(ShortcutsActionsBase).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
				fields.AddRange(typeof(GameSettings).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
				fields.RemoveAll(field => field.FieldType != typeof(string));
				return fields.ToArray();
			}
		}
	}
}
