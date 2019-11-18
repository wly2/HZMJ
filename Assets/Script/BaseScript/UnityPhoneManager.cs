﻿using AssemblyCSharp;
using LitJson;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnityPhoneManager : MonoBehaviour
{
    public bool isTest = false;

    //导入定义到.m文件中的C函数
    [DllImport("__Internal")]
    private static extern void _SetCopy(string mes);

    [DllImport("__Internal")]
    private static extern void _GetBattery();

    [DllImport("__Internal")]
    private static extern void _CloseBattery();

    [DllImport("__Internal")]
    private static extern void _VXAcces();

    [DllImport("__Internal")]
    private static extern void _ShareSessionText(string mes);

    [DllImport("__Internal")]
    private static extern void _ShareSessionImage(string path);

    [DllImport("__Internal")]
    private static extern void _ShareTimelineText(string mes);

    [DllImport("__Internal")]
    private static extern void _ShareTimelineImage(string path);

    [DllImport("__Internal")]
    private static extern void _SaveLocalPhoto(string path);

    [DllImport("__Internal")]
    private static extern void _WXPay(string mes);

    public static UnityPhoneManager _instance;

    public static UnityPhoneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject temp = new GameObject
                {
                    name = "UnityPhoneManager"
                };
                _instance = temp.AddComponent<UnityPhoneManager>();
            }

            return _instance;
        }
    }

    public void GetBattery()
    {

        if (isTest)
            return;
#if UNITY_IOS && !UNITY_EDITOR
		_GetBattery();
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_GetBattery");
#endif
    }

    public void CloseBattery()
    {
        if (isTest)
            return;
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_CloseBattery");
#elif UNITY_IOS && !UNITY_EDITOR
		_CloseBattery();
#endif
    }

    public void SetCopy(string mes)
    {
        if (isTest)
            return;
#if UNITY_IOS && !UNITY_EDITOR
		_SetCopy(mes);
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		jo.Call("_SetCopy", mes);
#endif
    }

    public void WxDengLu()
    {
        if (isTest)
            return;
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("WxInfo")))
        {
            SetUserInfo(PlayerPrefs.GetString("WxInfo"));
            return;
        }
#if UNITY_IOS && !UNITY_EDITOR
        _VXAcces();
#elif UNITY_ANDROID && !UNITY_EDITOR
        MyDebug.Log("set Sdk");
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_VXAcces");
#endif
    }

    public void ShareSessionText(string mes)
    {
        if (isTest)
            return;
#if UNITY_IOS && !UNITY_EDITOR
        _ShareSessionText(mes);
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_ShareSessionText",mes);
#endif
    }

    //好友
    public void ShareSessionImage(string path)
    {
        if (isTest)
            return;
#if UNITY_IOS && !UNITY_EDITOR
        _ShareSessionImage(path);
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_ShareSessionImage",path);
#endif
    }

    public void ShareTimelineText(string mes)
    {
        if (isTest)
            return;
#if UNITY_IOS && !UNITY_EDITOR
        _ShareTimelineText(mes);
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_ShareTimelineText",mes);
#endif
    }

    //朋友圈
    public void ShareTimelineImage(string path)
    {
        if (isTest)
            return;
#if UNITY_IOS && !UNITY_EDITOR
        _ShareTimelineImage(path);
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_ShareTimelineImage",path);
#endif
    }

    //好友
    public void SaveLocalPhoto(string path)
    {
        if (isTest)
            return;
#if UNITY_IOS && !UNITY_EDITOR
        _SaveLocalPhoto(path);
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_SaveLocalPhoto",path);
#endif
    }

    //好友
    public void WXPay(string mes)
    {
        if (isTest)
            return;
#if UNITY_IOS && !UNITY_EDITOR
        _WXPay(mes);
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("_WXPay",mes);
#endif
    }

    //public void WxSucessful(string ss)
    //{
    //    PlayerPrefs.SetString("WxInfo", ss);
    //    MyDebug.Log("获取信息成功");
    //    WxUserInfo wxInfo = JsonMapper.ToObject<WxUserInfo>(ss);
    //    GlobalDataScript.Instance.wechatOperate.TestLogin(wxInfo);
    //}
    public void SetBattery(string mes)
    {
        if (GameMessageManager.SetBattery != null)
        {
            GameMessageManager.SetBattery(int.Parse(mes));
        }
    }

    //十三道
    private string Appsecret = "15b672e5d099647c3c4641fdca758ac0";

    private string appid = "wxcb036be901577d6a";

    //麻将
    //private string Appsecret = "c93c89ef6cbabb005266b2d5470b9060";
    //private string appid = "wx9e0ac6315e6937a";
    public void SetVXCode(string mes)
    {
        MyDebug.TestLog("SetVXCode:" + mes);
        string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + Appsecret +
                     "&code=" + mes + "&grant_type=authorization_code";
        HttpManager.instance.GetWXReaure(url, SetAccesToken);
    }

    public void SetAccesToken(string mes)
    {
        MyDebug.TestLog("SetAccesToken:" + mes);
        AccesToken token = JsonMapper.ToObject<AccesToken>(mes);
        if (token != null)
        {
            string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + token.access_token + "&openid=" +
                         token.openid;
            HttpManager.instance.GetWXReaure(url, SetUserInfo);
        }
    }

    public void SetUserInfo(string mes)
    {
        MyDebug.TestLog("SetUserInfo:" + mes);
        PlayerPrefs.SetString("WxInfo", mes);
        WxUserInfo wxPersonalInfo = JsonMapper.ToObject<WxUserInfo>(mes);
        GlobalDataScript.Instance.wechatOperate.TestLogin(wxPersonalInfo);
    }

    public void BuySucessfull(string mes)
    {
        GamePayManager.instance.BuyResult();
    }
}

public enum LanType
{
    NETWORN_NONE = 0,

    //wifi连接
    NETWORN_WIFI = 1,

    //手机网络数据连接类型
    NETWORN_2G = 2,
    NETWORN_3G = 3,
    NETWORN_4G = 4,
    NETWORN_MOBILE = 5,
}

public class AccesToken
{
    public string access_token;
    public int expires_in;
    public string refresh_token;
    public string openid;
    public string unionid;
    public string scope;
}

public class WxUserInfo
{
    public string openid;
    public string nickname;
    public int sex;
    public string province;
    public string city;
    public string country;
    public string headimgurl;
    public string unionid;
    public string[] privilege;
}