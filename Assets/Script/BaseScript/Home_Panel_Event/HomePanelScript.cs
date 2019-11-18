using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;
using System;
using DG.Tweening;
using System.Text;
using LitJson;

public class HomePanelScript : MonoBehaviour
{
    public Image headIconImg; //头像路径
    public Text noticeText;
    public Text nickNameText; //昵称
    public Text cardCountText; //房卡剩余数量
    public Text IpText;
    public Text contactInfoContent;
    public GameObject roomCardPanel;
    Texture2D texture2D; //下载的图片
    private string headIcon;
    [HideInInspector]
    public Sprite imgLoad;//保存下载的头像


    /// <summary>
    /// 这个字段是作为消息显示的列表 ，如果要想通过管理后台随时修改通知信息，
    /// 请接收服务器的数据，并重新赋值给这个字段就行了。
    /// </summary>
    private bool startFlag;

    private int showNum;
    private int noticeCount;

    IEnumerator Start()
    {
        HttpManager.instance.SentHttpRequre(HTTP_TYPE.Paomadeng, PaoMaDengText);
        InitUI();
        GlobalDataScript.isonLoginPage = false;
        CheckEnterInRoom();
        AddListener();
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.PlayBGM("BackAudio1");
        SoundManager.Instance.SetMusicV(PlayerPrefs.GetFloat("MusicVolume", 1));
    }

    public void PaoMaDengText(string mes)
    {
        PaoMaDengManager.paoMaDeng = JsonMapper.ToObject<PaoMaDeng>(mes);
        if (PaoMaDengManager.paoMaDeng != null)
        {
            StartCoroutine(PaoMaNotice());
        }
    }

    IEnumerator PaoMaNotice()
    {
        noticeText.transform.parent.gameObject.SetActive(true);
        noticeCount = noticeCount % PaoMaDengManager.paoMaDeng.paomatiao_list.Count;
        noticeText.text = PaoMaDengManager.paoMaDeng.paomatiao_list[noticeCount].id + "." +
                          PaoMaDengManager.paoMaDeng.paomatiao_list[noticeCount].content;
        var time = (noticeText.preferredWidth + 1146) / 129f;
        var tweener = noticeText.transform.DOLocalMoveX(-noticeText.preferredWidth - 1146, time).SetRelative();
        tweener.SetEase(Ease.Linear);
        yield return new WaitForSeconds(time);
        noticeText.transform.localPosition =
            new Vector3(noticeText.transform.localPosition.x + noticeText.preferredWidth + 1146,
                noticeText.transform.localPosition.y, noticeText.transform.localPosition.z);
        noticeText.transform.parent.gameObject.SetActive(false);
        yield return new WaitForSeconds(int.Parse(PaoMaDengManager.paoMaDeng.paomatiao_list[noticeCount].interval));
        noticeCount++;
        StartCoroutine(PaoMaNotice());
    }

    void MoveCompleted()
    {
        showNum++;
        if (showNum == GlobalDataScript.noticeMegs.Count)
        {
            showNum = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Android系统监听返回键，由于只有Android和ios系统所以无需对系统做判断
            MyDebug.Log("Input.GetKey(KeyCode.Escape)");
            if (!GameObject.Find("UIPanel_ExitGame(Clone)"))
            {
                UIManager.instance.Show(UIType.UIExitGame);
            }
        }
    }

    //增加服务器返沪数据监听
    public void AddListener()
    {
        SocketEventHandle.Instance.cardChangeReply += CardChangeReply;
        SocketEventHandle.Instance.contactInfoReply += ContactInfoReply;
        SocketEventHandle.Instance.broadcastNoticeReply += BroadcastNotice;
    }

    public void RemoveListener()
    {
        SocketEventHandle.Instance.cardChangeReply -= CardChangeReply;
        SocketEventHandle.Instance.contactInfoReply -= ContactInfoReply;
        SocketEventHandle.Instance.broadcastNoticeReply -= BroadcastNotice;
    }

    //房卡变化处理
    private void CardChangeReply(ClientResponse response)
    {
        cardCountText.text = response.message;
        GlobalDataScript.loginResponseData.account.roomcard = int.Parse(response.message);
    }

    private void ContactInfoReply(ClientResponse response)
    {
        contactInfoContent.text = response.message;
        roomCardPanel.SetActive(true);
    }

    private void BroadcastNotice(ClientResponse response)
    {
    }

    /*
     *初始化显示界面
	 */
    private void InitUI()
    {
        if (GlobalDataScript.loginResponseData != null)
        {
            headIcon = GlobalDataScript.loginResponseData.account.headicon;
            var nickName = GlobalDataScript.loginResponseData.account.nickname;
            var roomCardcount = GlobalDataScript.loginResponseData.account.roomcard;
            cardCountText.text = roomCardcount + "";
            nickNameText.text = nickName;
            IpText.text = "ID:" + GlobalDataScript.loginResponseData.account.uuid;
            GlobalDataScript.weChatInformation.id = GlobalDataScript.loginResponseData.account.uuid;
            GlobalDataScript.loginResponseData.account.roomcard = roomCardcount;
        }

        if (imgLoad != null)
        {
            headIconImg.sprite = imgLoad;
            headIconImg.preserveAspect = true;
        }
        else
            StartCoroutine(LoadImg());
    }

    /*
      * 判断进入房间
      */
    private void CheckEnterInRoom()
    {
        if (GlobalDataScript.roomVo != null && GlobalDataScript.roomVo.roomId != 0)
        {

        }
    }

    public IEnumerator LoadImg()
    {
        //开始下载图片
        if (headIcon != null && headIcon != "")
        {
            headIconImg.preserveAspect = false;
            var www = new WWW(Encoding.ASCII.GetString(NetUtil.StringToBytes(headIcon)));
            yield return www;
            //下载完成，保存图片到路径filePath
            try
            {
                texture2D = www.texture;
                //将图片赋给场景上的Sprite
                GlobalDataScript.weChatInformation.headIcon = Sprite.Create(texture2D,
                    new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
                imgLoad = GlobalDataScript.weChatInformation.headIcon;
                headIconImg.sprite = imgLoad;
                headIconImg.preserveAspect = true;
            }
            catch (Exception e)
            {
                MyDebug.Log("LoadImg" + e.Message);
            }
        }
    }

    public void ShowAuthentication()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIRealname);
    }

    public void ShowUserInfo()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIUserInfo, InitInfo);
    }

    private void InitInfo(GameObject go)
    {
        go.GetComponent<UIPanel_UserInfo>().SetUIData();
    }

    public void ShowRank()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIRank);
    }

    public void ShowSetting()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UISetting);
    }

    public void ShowInvite()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIInvite);
    }

    public void ShowPay()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIRecharge);
    }

    public void ShowGameTutorial()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UICourse);
    }

    public void ShowHelp()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIHelp);
    }

    public void ShowRedBao()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIWithdrawal);
    }

    public void ShowSignIn()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        if (SiginDataManager.siginData == null)
        {
            HttpManager.instance.SentHttpRequre(HTTP_TYPE.GetSigin, GetSignInCallBack);
        }
        else 
        {
            UIManager.instance.Show(UIType.UISignIn);
        }
      
    }

    private void GetSignInCallBack(string msg)
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SiginDataManager.siginData = JsonMapper.ToObject<SiginData>(msg);
        UIManager.instance.Show(UIType.UISignIn);
    }

    public void ShowService()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIActivity);
    }

    public void ShowMilitary()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIReport);
    }

    public void ShowShare()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIShare);
    }

    public void ShowCreateRoom()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UICreateRoom);
        MyDebug.Log("调出创建房间界面");
    }

    public void ShowJoinRoom()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        UIManager.instance.Show(UIType.UIJoinRoom);
        MyDebug.Log("调出加入房间界面");
    }
}