using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;
using System.Collections.Generic;
using LitJson;

public class PlayerItemScript : MonoBehaviour
{
    //==============单例================//
    public HomePanelScript homepanelScript;
    private List<TalkItemData> _list;
    public Image headerIcon;
    public Image bankerImg;
    public Text nameText;
    public Image readyImg;
    public Text scoreText;
    public string dir;
    public GameObject chatAction;
    public Image offlineImage; //离线图片
    public Text chatMessage;
    public Image emoticons;
    public GameObject chatPaoPao;
    public GameObject HuFlag;
    public AvatarVO avatarvo;
    public List<Sprite> list;
    private int showTime;
    private int showChatTime;
    private Sprite normalIcon;

    private void Awake()
    {
        var str = Resources.Load<TextAsset>("Data/TalkItem").text;
        _list = JsonMapper.ToObject<List<TalkItemData>>(str);
    }

    void Update()
    {
        if (showTime > 0)
        {
            showTime--;
            if (showTime == 0)
            {
                emoticons.gameObject.SetActive(false);
                chatPaoPao.SetActive(false);
            }
        }

        if (showChatTime > 0)
        {
            showChatTime--;
            if (showChatTime == 0)
            {
                chatAction.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
          //  StartCoroutine(LoadImg());
        }
    }

    public void SetExit(int uuid)
    {
        headerIcon.preserveAspect = false;
        if (avatarvo == null)
            return;
        if (avatarvo.account.uuid != uuid)
            return;
        nameText.text = "";
        headerIcon.sprite = normalIcon;
        scoreText.text = "";
        headerIcon.preserveAspect = true;
    }

    public void SetAvatarVo(AvatarVO value)
    {
        if (value != null)
        {
            avatarvo = value;
            readyImg.enabled = avatarvo.isReady;
            nameText.text = avatarvo.account.uuid + "";
            scoreText.text = avatarvo.scores + "";
            if (dir != "B")
            {
                StartCoroutine(LoadImg());
            }
        }
        else
        {
            headerIcon.preserveAspect = false;
            nameText.text = "";
            readyImg.enabled = false;
            bankerImg.enabled = false;
            scoreText.text = "";
            ResourcesLoader.Load<Sprite>("BaseAssets/UI_Online/UI_Public/Icon", (sprite) =>
            {
                headerIcon.overrideSprite = sprite;
            });

            //headerIcon.sprite = Resources.Load("Image/morentouxiang", typeof(Sprite)) as Sprite;
            headerIcon.preserveAspect = true;
        }
    }

    #region 无效的加载路径
    public Sprite tempSp;

    /// <summary>
    /// 加载头像
    /// </summary>
    /// <returns>The image.</returns>
    private IEnumerator LoadImg()
    {
        normalIcon = headerIcon.sprite;
        var str = avatarvo.account.headicon;
        //开始下载图片
        var www = new WWW(avatarvo.account.headicon);
        yield return www;

        //下载完成，保存图片到路径filePath
        if (www != null)
        {
            headerIcon.preserveAspect = false;
            var texture2D = www.texture;
            var bytes = texture2D.EncodeToPNG();
            //将图片赋给场景上的Sprite
            tempSp = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
            headerIcon.sprite = tempSp;
            headerIcon.preserveAspect = true;
        }
        else
        {
            MyDebug.Log("没有加载到图片");
        }
    }
    #endregion

    public void SetbankImgEnable(bool flag)
    {
        bankerImg.enabled = flag;
    }

    public void ShowChatAction()
    {
        showChatTime = 120;
        chatAction.SetActive(true);
    }

    public int GetUuid()
    {
        var result = -1;
        if (avatarvo != null)
        {
            result = avatarvo.account.uuid;
        }

        return result;
    }

    /*设置游戏玩家离线*/
    public void SetPlayerOffline()
    {
        offlineImage.transform.gameObject.SetActive(true);
    }

    /*设置游戏玩家上线*/
    public void SetPlayerOnline()
    {
        offlineImage.transform.gameObject.SetActive(false);
    }

    public void ShowChatMessage(int index)
    {
        showTime = 200;
        index = index - 1001;
        chatMessage.text = TalkDataManager.Instance.List[index].message;
        chatPaoPao.SetActive(true);
    }

    public void ShowChat(string text)
    {
        showTime = 50;
        //var arr = text.Split(new char[1] { '|' });
        var arr = text.Split(new char[1] { '|' });
        if (arr[0] == "0") //表情
        {
            emoticons.overrideSprite = list[int.Parse(arr[1])];
            emoticons.SetNativeSize();
            emoticons.preserveAspect = true;
            emoticons.gameObject.SetActive(true);
            var talkItemData = new TalkItemData
            {
                name = avatarvo.account.nickname,
                userId = avatarvo.account.uuid,
                icon = GlobalDataScript.weChatInformation.headIcon,
                faceSprite = emoticons.overrideSprite
            };
            TalkDataManager.Instance.AddTalkItem(talkItemData);
        }
        else if (arr[0] == "1") //快捷语
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (int.Parse(arr[1]) == _list[i].id)
                {
                    chatMessage.text = _list[i].message;
                    var tid = new TalkItemData
                    {
                        name = avatarvo.account.nickname,
                        userId = avatarvo.account.uuid,
                        icon = GlobalDataScript.weChatInformation.headIcon,
                        message = _list[i].message
                    };
                    TalkDataManager.Instance.AddTalkItem(tid);
                    chatPaoPao.SetActive(true);
                    break;
                }
            }
        }
        else if (arr[0] == "2") //输入文字
        {
            chatMessage.text = arr[1];
            var talkItem = new TalkItemData
            {
                name = avatarvo.account.nickname,
                userId = avatarvo.account.uuid,
                icon = GlobalDataScript.weChatInformation.headIcon,
                message = arr[1]
            };
            TalkDataManager.Instance.AddTalkItem(talkItem);
            chatPaoPao.SetActive(true);
        }
    }

    public void DisplayAvatorIp()
    {
        if (avatarvo == null)
            return;
        UIManager.instance.Show(UIType.UIUserInfo, InitInfo);
    }

    private void InitInfo(GameObject go)
    {
        go.GetComponent<UIPanel_UserInfo>().SetUIData();
    }

    public void SetHuFlagDisplay()
    {
        HuFlag.SetActive(true);
    }

    public void SetHuFlagHidde()
    {
        HuFlag.SetActive(false);
    }

    public void ShowPosition()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        UIManager.instance.Show(UIType.UIPositionMonitoring);
    }
}