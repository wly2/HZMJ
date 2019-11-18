using UnityEngine;
using AssemblyCSharp;
/**
 * 微信操作
 */
public class WechatOperateScript : MonoBehaviour
{
    /*
	 * 登录，提供给button使用
	 */
    public void Login(string stri)
    {
        MyDebug.Log("TEST Wei xin");
        var wx = new WxUserInfo();
        var str = "test" + stri;
        wx.openid = str;
        wx.nickname = str;
        wx.headimgurl =
            "http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0";
        SocketLoginEvent.Instance.OnWxLoginSucess(wx);
    }

    public void InviteFriend()
    {
        if (GlobalDataScript.roomVo != null)
        {
            var roomvo = GlobalDataScript.roomVo;
            GlobalDataScript.totalTimes = roomvo.roundNumber;
            GlobalDataScript.surplusTimes = roomvo.roundNumber;
            var str = "";
            if (roomvo.hong)
            {
                str += LocalizationManager.GetInstance.GetValue("KEY.10005") + ",";
            }
            else
            {
                if (roomvo.roomType == 1)
                {
                    str += LocalizationManager.GetInstance.GetValue("KEY.10002") + ",";
                }
                else if (roomvo.roomType == 2)
                {
                    str += LocalizationManager.GetInstance.GetValue("KEY.10003") + ",";
                }
                else if (roomvo.roomType == 3)
                {
                    str += LocalizationManager.GetInstance.GetValue("KEY.10004") + ",";
                }
            }

            str += string.Format(LocalizationManager.GetInstance.GetValue("KEY.11026"), roomvo.roundNumber) + ",";
            if (roomvo.ziMo == 1)
            {
                str += LocalizationManager.GetInstance.GetValue("KEY.11016") + ",";
            }
            else
            {
                str += LocalizationManager.GetInstance.GetValue("KEY.11017") + ",";
            }

            if (roomvo.addWordCard)
            {
                str += LocalizationManager.GetInstance.GetValue("KEY.11019") + ",";
            }

            if (roomvo.xiaYu > 0)
            {
                str += string.Format(LocalizationManager.GetInstance.GetValue("KEY.11027"), roomvo.xiaYu) + ",";
            }

            if (roomvo.ma > 0)
            {
                str += string.Format(LocalizationManager.GetInstance.GetValue("KEY.11028"), roomvo.ma) + ",";
            }

            if (roomvo.magnification > 0)
            {
                str += LocalizationManager.GetInstance.GetValue("KEY.11022") + roomvo.magnification;
            }

            str += LocalizationManager.GetInstance.GetValue("KEY.11029");
        }
    }

    public void TestLogin(WxUserInfo wx)
    {
        SocketLoginEvent.Instance.OnWxLoginSucess(wx);
    }
}