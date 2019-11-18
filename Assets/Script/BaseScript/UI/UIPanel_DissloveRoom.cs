using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LitJson;
using System.Linq;
/**申请解散房间投票框**/
public class UIPanel_DissloveRoom : UIWindow
{
    public Text schedule;
    public Text sponsorNameText;
    public List<Image> playerList;
    public Button okButton;
    public Button cancleButton;
    private int sponsorName;
    private byte dissolveType;
    private List<int> playerNames;
    private bool isStop;
    private int playerCount;
    List<AvatarVO> avatarList;
    public int agreeCount;
    private int ChoseCount;

    void OnEnable()
    {
        SocketEventHandle.Instance.dissolveRoomReply += OnDissolveRoomReply;
    }

    void OnDisable()
    {
        SocketEventHandle.Instance.dissolveRoomReply -= OnDissolveRoomReply;
    }

    private void OnDissolveRoomReply(ClientResponse response)
    {
        var outRoom = JsonMapper.ToObject<OutRoomResponseVo>(response.message);
        ;
        ExitUI(outRoom.dwDissChairID.ToList(), outRoom.dwNotAgreeChairID.ToList());
    }

    public void ExitUI(List<int> agreeId, List<int> disagreeId, List<AvatarVO> avatarVOList = null)
    {
        if (agreeId.Contains(GlobalDataScript.loginResponseData.chairID) ||
            disagreeId.Contains(GlobalDataScript.loginResponseData.chairID))
        {
            sponsorNameText.gameObject.SetActive(false);
            schedule.gameObject.SetActive(true);
            okButton.transform.gameObject.SetActive(false);
            cancleButton.transform.gameObject.SetActive(false);
        }
        else
        {
            schedule.gameObject.SetActive(false);
            sponsorNameText.text = agreeId[0].ToString();
            sponsorNameText.gameObject.SetActive(true);
        }

        if (agreeId.Count == agreeCount)
        {
            ResourcesLoader.Load<Sprite>("BaseAssets/UI_Online/UI_Public/agree", (sprite) => {
                playerList[ChoseCount].overrideSprite = sprite;
            });

            //playerList[ChoseCount].overrideSprite = Resources.Load("Image/JinDuTiao/agree", typeof(Sprite)) as Sprite;
            playerList[ChoseCount].transform.localEulerAngles = Vector3.zero;
            playerList[ChoseCount].gameObject.SetActive(true);
        }
        else
        {
            if (ChoseCount == 0)
                ResourcesLoader.Load<Sprite>("BaseAssets/UI_Online/UI_Public/JinDuTiao/disagreeleft", (sprite) => {
                    playerList[ChoseCount].overrideSprite = sprite;
                });

            // playerList[ChoseCount].overrideSprite =
            //  Resources.Load("Image/JinDuTiao/disagreeleft", typeof(Sprite)) as Sprite;
            else
                ResourcesLoader.Load<Sprite>("BaseAssets/UI_Online/UI_Public/JinDuTiao/disagree", (sprite) => {
                    playerList[ChoseCount].overrideSprite = sprite;
                });

            //playerList[ChoseCount].overrideSprite =
            //    Resources.Load("Image/JinDuTiao/disagree", typeof(Sprite)) as Sprite;
            playerList[ChoseCount].gameObject.SetActive(true);
        }

        agreeCount = agreeId.Count;
        ChoseCount++;
        if (disagreeId.Count >= 2)
        {
            CloseUI();
        }
    }

    public void ClickOk()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        CMD_GR_Dismiss_Private kNetInfo;
        kNetInfo.bDismiss = 1;
        SocketSendManager.Instance.SendData((int) GameServer.MDM_GR_PRIVATE,
            (int) MDM_GR_PRIVATE.SUB_GR_PRIVATE_DISMISS, NetUtil.StructToBytes(kNetInfo), Marshal.SizeOf(kNetInfo));
        okButton.transform.gameObject.SetActive(false);
        cancleButton.transform.gameObject.SetActive(false);
    }

    public void ClickCancle()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        SoundManager.Instance.SetSoundV(PlayerPrefs.GetFloat("SoundVolume", 1));
        CMD_GR_Dismiss_Private kNetInfo;
        kNetInfo.bDismiss = 0;
        SocketSendManager.Instance.SendData((int) GameServer.MDM_GR_PRIVATE,
            (int) MDM_GR_PRIVATE.SUB_GR_PRIVATE_DISMISS, NetUtil.StructToBytes(kNetInfo), Marshal.SizeOf(kNetInfo));
        okButton.transform.gameObject.SetActive(false);
        cancleButton.transform.gameObject.SetActive(false);
    }
}