using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using System;
using LitJson;

public class UIPanel_CreateRoom : UIWindow
{
    public Toggle shiFengTog;
    public Toggle haoGangTog;
    private int wanFa;
    private int juShu;
    private int huFa;
    private int beishu;
    private int payFor;
    private GameObject gameSence;
    private RoomCreateVo sendVo; //创建房间的信息

    void Start()
    {
        SocketEventHandle.Instance.createRoomReply += OnCreateRoomReply;
    }

    public void CloseDialog()
    {
        MyDebug.Log("closeDialog");
        SocketEventHandle.Instance.createRoomReply -= OnCreateRoomReply;
        Destroy(this);
        Destroy(gameObject);
    }

    /*
	 * 创建转转麻将房间
	 */
    public void CreateZhuanzhuanRoom()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        const int roundNumber = 4; //房卡数量
        const bool isZimo = false; //自摸
        const bool hasHong = false; //红中赖子
        const bool isSevenDoube = false; //七小对
        const int maCount = 0;
        sendVo = new RoomCreateVo
        {
            ma = maCount,
            roundNumber = roundNumber,
            ziMo = isZimo ? 1 : 0,
            hong = hasHong,
            sevenDouble = isSevenDoube
        };

        MyDebug.Log(sendVo.roomType);
        var sendmsgstr = JsonMapper.ToJson(sendVo);
        if (GlobalDataScript.loginResponseData.account.roomcard > 0)
        {
            var haogang = haoGangTog.isOn ? Define.GAME_TYPE_HAOGANG : 0;
            var shifang = shiFengTog.isOn ? Define.GAME_TYPE_10FENG : 0;
            var rule = huFa | haogang | beishu | shifang;

            //var hufa = huFa;
            //Debug.LogError(huFa);
            //var haogang1 = haogang;
            //Debug.LogError(haogang1);
            //var beishu1 = beishu;
            //Debug.LogError(beishu1);
            //var shifang1 = shifang;
            //Debug.LogError(shifang1);

            //Debug.LogError(rule);
            SocketSendManager.Instance.CreateRoom(rule, payFor, juShu);

            MyDebug.Log("创建房间成功!");
            
        }
        else
        {
            TipsManagerScript.getInstance.setTips(LocalizationManager.GetInstance.GetValue("KEY.11044"));
        }
    }

    public void Setjushu(int dex)
    {
        juShu = dex;
    }

    public void Sethufa(int dex)
    {
        huFa = dex == 1 ? Define.GAME_TYPE_DANLAOQI : Define.GAME_TYPE_ERLAOQI;
    }

    public void SetBeishu(int dex)
    {
        beishu = dex == 10 ? Define.GAME_TYPE_NOTOP : dex == 20 ? Define.GAME_TYPE_64TOP : Define.GAME_TYPE_128TOP;
       
    }

    public void SetPayFo(int dex)
    {
        payFor = dex;
    }

    public void OnCreateRoomReply(ClientResponse response)
    {
        MyDebug.Log(response.message);
        if (response.status == 1)
        {
            var roomid = Int32.Parse(response.message);
            sendVo.roomId = roomid;

            MyDebug.LogError(sendVo.roomId);

            GlobalDataScript.roomVo = sendVo;
            GlobalDataScript.loginResponseData.roomId = roomid;
            GlobalDataScript.loginResponseData.isOnLine = true;
            GlobalDataScript.type = ModeType.Create;
            if (GlobalDataScript.homePanel != null)
                GlobalDataScript.homePanel.SetActive(false);
            CloseDialog();
        }
        else
        {
            TipsManagerScript.getInstance.setTips(response.message);
        }
    }
}