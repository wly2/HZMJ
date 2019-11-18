using System;

public class Url
{
    private static readonly string HOST = "http://hzmj.wedoyx.com/Api/";


    private static readonly string HASH_GAME = "7371de83de78044b6642f9e3389cee72";
    private static readonly string GET_SIGIN = "Game/getUserSign?";
    private static readonly string SEND_SIGIN = "Game/setUserSign?";
    private static readonly string SEND_PAPMADENG = "Game/getPaomatiao?";


    private static readonly string VX_GETTOKEN =
        "https://api.weixin.qq.com/sns/oauth2/access_token?appid=wx9e0ac6315e6937a7&secret=SECRET&code=CODE&grant_type=authorization_code";

    private static readonly string VX_GET_USERINFO =
        " https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID";

    public static string GetUrl(HTTP_TYPE type)
    {

        string urlName = null;
        string hash = null;
        switch (type)
        {
            case HTTP_TYPE.GetSigin:
                urlName = GET_SIGIN;
                hash = HASH_GAME;
                break;
            case HTTP_TYPE.SendSigin:
                urlName = SEND_SIGIN;
                hash = HASH_GAME;
                break;
            case HTTP_TYPE.Paomadeng:
                urlName = SEND_PAPMADENG;
                hash = HASH_GAME;
                break;
            default:
                break;
        }

        return HOST + urlName + "&hash=" + hash + GetCommon();

    }

    #region      微信支付

    public static string SendWxPayUrl(int id)
    {
        return "http://hzmj.wedoyx.com/Api/Pay/recharge_Add?&hash=b97319f083246f02aa99addd2de5036f&paytype=2&goodsid=" +
               id + GetCommon();
        ;
    }

    #endregion

    #region   公用url参数

    private static string GetCommon()
    {
        return "&time=" + GetTimeStamp() + "&pack_id=" + GetPackID() + "&uid=" + GetUid();
    }

    private static int GetTimeStamp()
    {
        return DateTime.Now.Second;
    }

    private static string GetPackID()
    {
        return "111";
    }

    private static int GetUid()
    {
        return GlobalDataScript.loginResponseData.account.uuid;
    }

    #endregion
}

public enum HTTP_TYPE
{
    GetSigin,
    SendSigin,
    Paomadeng,
}