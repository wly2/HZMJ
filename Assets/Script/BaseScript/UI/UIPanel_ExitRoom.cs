using System.Runtime.InteropServices;

public class UIPanel_ExitRoom : UIWindow
{
    public void ExitTheRoom()
    {
        SoundManager.Instance.PlaySoundBGM("clickbutton");
        if (GlobalDataScript.loginResponseData.chairID == 0 || UIMaJiangPanel.instance.avatarList.Count >= 4)
        {
            CMD_GR_Dismiss_Private kNetInfo;
            kNetInfo.bDismiss = 1;
            SocketSendManager.Instance.SendData((int) GameServer.MDM_GR_PRIVATE,
                (int) MDM_GR_PRIVATE.SUB_GR_PRIVATE_DISMISS, NetUtil.StructToBytes(kNetInfo), Marshal.SizeOf(kNetInfo));
        }
        else
        {
            SocketSendManager.Instance.SendStandUpPacket(GlobalDataScript.loginResponseData.tableID,
                GlobalDataScript.loginResponseData.chairID);
        }

        gameObject.SetActive(false);
    }

    public void CancelButton()
    {
        CloseUI();
    }
}