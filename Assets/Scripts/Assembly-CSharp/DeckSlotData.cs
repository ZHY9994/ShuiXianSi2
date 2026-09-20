using System;
using KobGamesSDKSlim;

[Serializable]
public class DeckSlotData
{
	public string Name;

	public int Index;

	public string Id => string.Concat("Deck_", Name, "_");

	public bool IsUnlocked
	{
		get
		{
			return PlayerPrefsBool.GetBool(Id + "IsUnlocked", false);
		}
		set
		{
			PlayerPrefsBool.SetBool(Id + "IsUnlocked", value);
		}
	}

	public eWeaponType WeaponInSlot
	{
		get
		{
			return (eWeaponType)global::Ade_Framework.AdeCloudPlayerPrefs.GetInt(Id + "WeaponInSlot");
		}
		set
		{
			global::Ade_Framework.AdeCloudPlayerPrefs.SetInt(Id + "WeaponInSlot", (int)value);
		}
	}

	public bool HasSavedData => global::Ade_Framework.AdeCloudPlayerPrefs.HasKey(Id + "WeaponInSlot");

	public void Unlock()
	{
		IsUnlocked = true;
	}

	public void UpdateSlot(eWeaponType i_Type)
	{
		WeaponInSlot = i_Type;
	}
}
