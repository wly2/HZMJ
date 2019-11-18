using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using LitJson;
using System.Collections;

public class UIPanelLogin : MonoBehaviour
{
    public Toggle agreeProtocol;
    public static int TestId;

    private void Awake()
    {

#if UNITY_STANDALONE_WIN
        Screen.SetResolution(854, 480, false);
#endif
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SoundManager.Instance.Init();
        MySceneManager.instance.Init();
        yield return new WaitForEndOfFrame();
        SoundManager.Instance.PlayBGM("backMusic");
        SoundManager.Instance.SetMusicV(PlayerPrefs.GetFloat("MusicVolume", 1));
        GlobalDataScript.isonLoginPage = true;
        SocketEventHandle.Instance.loginReply += OnLoginReply;
        SocketEventHandle.Instance.backRoomReply += OnBackRoomReply;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DoLogin();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            //Android系统监听返回键，由于只有Android和ios系统所以无需对系统做判断
            if (!GameObject.Find("UIPanel_ExitGame(Clone)"))
            {
                UIManager.instance.Show(UIType.UIExitGame);
            }
        }
    }

    public void Login()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");

        if (agreeProtocol.isOn)
        {
            DoLogin();
            MyDebug.Log("微信登录");
        }
        else
        {
            MyDebug.Log("请先同意用户使用协议");
            TipsManagerScript.getInstance.setTips(LocalizationManager.GetInstance.GetValue("KEY.20012"));
            
        }
    }

    public void DoLogin()
    {
        ///随机登录
        if (UnityPhoneManager.Instance.isTest)
        {
            GlobalDataScript.Instance.wechatOperate.Login("" + Random.Range(100, 999));//
            return;
        }
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        GlobalDataScript.Instance.wechatOperate.Login("" + Random.Range(100, 999));// 
#else
        UnityPhoneManager.Instance.WxDengLu();    ///微信登录  
#endif
    }

    public void OnLoginReply(ClientResponse response)
    {
        MyDebug.Log("OnLoginReply !!!");
        if (response.status == 1)
        {
            if (GlobalDataScript.homePanel != null)
            {
                GlobalDataScript.homePanel.GetComponent<HomePanelScript>().RemoveListener();
                Destroy(GlobalDataScript.homePanel);
            }

            if (GlobalDataScript.gamePlayPanel != null)
            {
                GlobalDataScript.gamePlayPanel.GetComponent<UIMaJiangPanel>().ExitOrDissoliveRoom();
            }

            GlobalDataScript.loginResponseData = JsonMapper.ToObject<AvatarVO>(response.message);
            RemoveListener();
        }
    }

    public void RemoveListener()
    {
        SocketEventHandle.Instance.loginReply -= OnLoginReply;
        SocketEventHandle.Instance.backRoomReply -= OnBackRoomReply;
    }

    private void OnBackRoomReply(ClientResponse response)
    {
        if (GlobalDataScript.homePanel != null)
        {
            GlobalDataScript.homePanel.GetComponent<HomePanelScript>().RemoveListener();
            Destroy(GlobalDataScript.homePanel);
        }

        if (GlobalDataScript.gamePlayPanel != null)
        {
            GlobalDataScript.gamePlayPanel.GetComponent<UIMaJiangPanel>().ExitOrDissoliveRoom();
        }

        GlobalDataScript.reEnterRoomData = JsonMapper.ToObject<RoomJoinResponseVo>(response.message);
        for (int i = 0; i < GlobalDataScript.reEnterRoomData.playerList.Count; i++)
        {
            var itemData = GlobalDataScript.reEnterRoomData.playerList[i];
            if (itemData.account.openid == GlobalDataScript.loginResponseData.account.openid)
            {
                GlobalDataScript.loginResponseData.account.uuid = itemData.account.uuid;
                ChatSocket.GetInstance.SendMsg(new LoginChatRequest(GlobalDataScript.loginResponseData.account.uuid));
                break;
            }
        }

        MyDebug.Log("OnBackRoomReply  To Game");
        MySceneManager.instance.SceneToMaJiang();
        RemoveListener();
    }

    public void ShowXieYi()
    {
        ResourcesLoader.instance.testC();
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        UIManager.instance.Show(UIType.UIProtocol);
    }

    private string filePath;

    IEnumerator LoadAnouncementText()
    {
        var wwwObject = new WWW(filePath); //利用www类加载
        MyDebug.Log(wwwObject.url);
        yield return wwwObject;
        var mainBundle = wwwObject.assetBundle; //获得AssetBundle
        var abr = mainBundle.LoadAssetAsync("UIPanel_Protocol", typeof(GameObject)); //异步加载GameObject类型
        yield return abr;
        var go = Instantiate(abr.asset) as GameObject;
        yield return null;
        mainBundle.Unload(false); //卸载所有包含在bundle中的对象。已经加载的才会卸载
        wwwObject.Dispose(); //中断www
    }

    public void RegisterBtn()
    {
        GlobalDataScript.Instance.wechatOperate.Login("" + Random.Range(100, 999));
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
    }
}