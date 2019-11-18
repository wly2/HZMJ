using AssemblyCSharp;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class HttpManager : MonoBehaviour
{
    public delegate void MessageHandler(string message);

    public static HttpManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject
                {
                    name = "HttpManager"
                };
                _instance = go.AddComponent<HttpManager>();
            }

            return _instance;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    private static HttpManager _instance;

    public void SentHttpRequre(HTTP_TYPE type, MessageHandler action)
    {
        StartCoroutine(SendGet(Url.GetUrl(type), action));
    }

    private IEnumerator SendGet(string _url, MessageHandler action)
    {
        MyDebug.TestLog("Http URL:" + _url);
        WWW getData = new WWW(_url);
        yield return getData;

        if (getData.error != null)
        {
            MyDebug.TestLog(getData.error);
        }
        else
        {
            MyDebug.TestLog(getData.text);
            action(getData.text);
        }
    }

    public void GetWXReaure(string _url, MessageHandler action)
    {
        StartCoroutine(SendGet(_url, action));
    }

    public void GetWXPay(int goodID, MessageHandler action)
    {
        StartCoroutine(SendGet(Url.SendWxPayUrl(goodID), action));
    }
}