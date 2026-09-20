using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MrWayne/LocalizationData", fileName = "LocalizationData")]
public class LocalizationData : ScriptableObject
{
	[SerializeField]
	private List<LocaleData> m_LocaleData = new List<LocaleData>();

	public List<LocaleData> LocaleData => m_LocaleData;

	private void LoadData(TextAsset i_Asset)
	{
		m_LocaleData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocaleData>>(i_Asset.text);
	}
}
