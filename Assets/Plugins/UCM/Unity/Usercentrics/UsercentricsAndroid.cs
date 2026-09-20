using System;
using UnityEngine;
namespace Unity.Usercentrics
{
	internal class UsercentricsAndroid : IUsercentricsPlatform
	{
		private const string UC_UNITY_JAVA_NAME="com.usercentrics.sdk.unity.UsercentricsUnity";
		private const string UC_BANNER_JAVA_NAME="com.usercentrics.sdk.unity.UsercentricsUnityBanner";
		private const string UNITY_PLAYER_NAME="com.unity3d.player.UnityPlayer";
		private readonly Lazy<AndroidJavaClass> _usercentricsUnityClass=new Lazy<AndroidJavaClass>(()=>new AndroidJavaClass(UC_UNITY_JAVA_NAME));
		private readonly Lazy<AndroidJavaClass> _usercentricsBannerClass=new Lazy<AndroidJavaClass>(()=>new AndroidJavaClass(UC_BANNER_JAVA_NAME));
		private readonly Lazy<AndroidJavaObject> _currentActivity=new Lazy<AndroidJavaObject>(()=>new AndroidJavaClass(UNITY_PLAYER_NAME).GetStatic<AndroidJavaObject>("currentActivity"));
		public void Initialize(string json){_usercentricsUnityClass.Value.Call("initialize",_currentActivity.Value,json);}
		public void ShowFirstLayer(string json){_usercentricsBannerClass.Value.Call("showFirstLayer",_currentActivity.Value,json);}
		public void ShowSecondLayer(string json){_usercentricsBannerClass.Value.Call("showSecondLayer",_currentActivity.Value,json);}
		public string GetControllerID(){return _usercentricsUnityClass.Value.Call<string>("getControllerId");}
		public void GetTCFData(){_usercentricsUnityClass.Value.Call("getTCFData");}
		public string GetUSPData(){return _usercentricsUnityClass.Value.Call<string>("getUSPData");}
		public void RestoreUserSession(string id){_usercentricsUnityClass.Value.Call("restoreUserSession",id);}
		public void SubscribeOnConsentUpdated(){_usercentricsUnityClass.Value.Call("subscribeOnConsentUpdated");}
		public void DisposeOnConsentUpdatedSubscription(){_usercentricsUnityClass.Value.Call("disposeOnConsentUpdatedSubscription");}
		public void SubscribeOnConsentMediation(){_usercentricsUnityClass.Value.Call("subscribeOnConsentMediation");}
		public void DisposeOnConsentMediationSubscription(){_usercentricsUnityClass.Value.Call("disposeOnConsentMediationSubscription");}
		public string GetFirstLayerSettings(){return _usercentricsUnityClass.Value.Call<string>("getFirstLayerSettings");}
		public void AcceptAll(){_usercentricsBannerClass.Value.Call("acceptAll");}
		public void DenyAll(){_usercentricsBannerClass.Value.Call("denyAll");}
		public void Track(int type){_usercentricsUnityClass.Value.Call("track",type);}
		public void SetCmpId(int id){_usercentricsUnityClass.Value.Call("setCmpId",id);}
		public string GetCmpData(){return _usercentricsUnityClass.Value.Call<string>("getCmpData");}
		public void SetABTestingVariant(string variant){_usercentricsUnityClass.Value.Call("setABTestingVariant",variant);}
		public string GetABTestingVariant(){return _usercentricsUnityClass.Value.Call<string>("getABTestingVariant");}
		public string GetAdditionalConsentModeData(){return _usercentricsUnityClass.Value.Call<string>("getAdditionalConsentModeData");}
		public string GetConsents(){return _usercentricsUnityClass.Value.Call<string>("getConsents");}
		public void ClearUserSession(){_usercentricsUnityClass.Value.Call("clearUserSession");}
	}
}
