using System;
using UnityEngine;

namespace Crosstales.OnlineCheck.Data
{
	[Serializable]
	[HelpURL("https://www.crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_data_1_1_custom_check.html")]
	[CreateAssetMenu(fileName = "New CustomCheck", menuName = "Online Check PRO/CustomCheck", order = 1000)]
	public class CustomCheck : ScriptableObject
	{
		[Tooltip("Custom URL to perform the Internet availability tests e.g. https://mydomain.com/connect.txt. The host should be https-based and provide an 'Access-Control-Allow-Origin' header.")]
		public string URL;

		[Tooltip("Expected data from the custom URL (as string).")]
		public string ExpectedData;

		[Tooltip("Compares the custom data with 'equals' to the expected data (default: false, false uses 'contains' as match).")]
		public bool DataMustBeEquals;

		[Tooltip("Use only the custom url for Internet availability tests and ignores all built-in checks (default: false).")]
		public bool UseOnlyCustom;

		[Tooltip("Displays all connection errors (default: false).")]
		public bool ShowErrors;

		[Tooltip("Size of the request header (default: 0).")]
		public int HeaderSize;

		public override string ToString()
		{
			return URL;
		}

		public override bool Equals(object obj)
		{
			CustomCheck other = obj as CustomCheck;
			return other != null && URL == other.URL && ExpectedData == other.ExpectedData;
		}

		public override int GetHashCode()
		{
			return (URL ?? string.Empty).GetHashCode();
		}
	}
}
