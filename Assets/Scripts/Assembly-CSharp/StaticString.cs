using UnityEngine;

public static class StaticString
{
	public static int AP_WeaponType;

	public static int AP_DanceType;

	public static int AP_Dance;

	public static int AP_Attack;

	public static int AP_Fly;

	public static int AP_Walk;

	public static int AP_OnStateChange;

	public static int AP_State;

	public static int AS_Dance;

	public static int AS_Idle;

	public static int AS_Tap;

	public static int AS_Drag;

	public static int AS_AnimalHit;

	public static string FindingPlayer;

	public static string FoundPlayer;

	public static int SP_BaseTexture;

	public static int SP_OutlineWidth;

	public static int SP_AlbedoColor;

	public static int SP_MainTex;

	public static string Tut_LevelStart;

	public static string Tut_KnifeTapped;

	public static string Tut_KnifeThrown;

	public static string Tut_GrenadeTapped;

	public static string Tut_GrenadeThrow;

	public static string Tut_CardTapped;

	public static string Tut_CardReplaced;

	public static string Tut_ShopClosed;

	public static string Tut_ShopLevelStart;

	static StaticString()
	{
		AP_WeaponType = Animator.StringToHash("WeaponType");
		AP_DanceType = Animator.StringToHash("DanceType");
		AP_Dance = Animator.StringToHash("Dance");
		AP_Attack = Animator.StringToHash("Attack");
		AP_Fly = Animator.StringToHash("Fly");
		AP_Walk = Animator.StringToHash("Walk");
		AP_OnStateChange = Animator.StringToHash("OnStateChange");
		AP_State = Animator.StringToHash("State");
		AS_Dance = Animator.StringToHash("Dance");
		AS_Idle = Animator.StringToHash("Idle");
		AS_Tap = Animator.StringToHash("Tap");
		AS_Drag = Animator.StringToHash("Drag");
		AS_AnimalHit = Animator.StringToHash("Animal Hit");
		FindingPlayer = "FINDING OPPONENT";
		FoundPlayer = "OPPONENT FOUND";
		SP_BaseTexture = Shader.PropertyToID("_BaseMap");
		SP_OutlineWidth = Shader.PropertyToID("_OutlineSize");
		SP_AlbedoColor = Shader.PropertyToID("_AlbedoColor");
		SP_MainTex = Shader.PropertyToID("_BaseMap");
		Tut_LevelStart = "01_level_start";
		Tut_KnifeTapped = "02_knife_tapped";
		Tut_KnifeThrown = "03_knife_thrown";
		Tut_GrenadeTapped = "04_grenade_tapped";
		Tut_GrenadeThrow = "05_grenade_thrown";
		Tut_CardTapped = "06_shop_card_tapped";
		Tut_CardReplaced = "07_shop_card_replaced";
		Tut_ShopClosed = "08_shop_closed";
		Tut_ShopLevelStart = "09_shop_level_start";
	}
}
