using System;

[Serializable]
public class LocaleData
{
	public string Key;

	public string Comment;

	public string EN;

	public string JA;

	public string KO;

	public string GetLocalString(eLocaleType i_Type)
	{
		if (i_Type == eLocaleType.KO) return KO;
		if (i_Type == eLocaleType.JA) return JA;
		return i_Type == eLocaleType.EN ? EN : string.Empty;
	}

	private void Add()
	{
	}
}
