using System.Runtime.InteropServices;
using AssemblyCSharp;
using LitJson;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Text;

public class SocketLoginEvent : ISocketEvent
{
    private static SocketLoginEvent _instance;

    public static SocketLoginEvent Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SocketLoginEvent();
            }

            return _instance;
        }
    }

    // 处理有效数据
    public bool OnEventTCPSocketRead(int main, int sub, byte[] tmpBuf, int size)
    {
        MyDebug.Log("Main:" + (MainCmd) main);
        switch ((MainCmd) main)
        {
            case MainCmd.MDM_GP_LOGON: //登陆信息
                OnSocketMainLogon(sub, tmpBuf, size);
                break;
            case MainCmd.MDM_GP_SERVER_LIST: //列表信息
                OnSocketMainServerList(sub, tmpBuf, size);
                break;
            case MainCmd.MDM_GP_USER_SERVICE:
                break;
        }

        return true;
    }

    #region  列表信息处理

    // 列表信息处理
    private void OnSocketMainServerList(int sub, byte[] tmpBuf, int size)
    {
        switch ((SUB_GP_LIST) sub)
        {
            case SUB_GP_LIST.SUB_GP_LIST_TYPE:
                break;
            case SUB_GP_LIST.SUB_GP_LIST_KIND:
                OnSocketListKind(tmpBuf, size);
                break;
            case SUB_GP_LIST.SUB_GP_LIST_NODE:
                break;
            case SUB_GP_LIST.SUB_GP_LIST_PAGE:
                break;
            case SUB_GP_LIST.SUB_GP_LIST_SERVER:
                OnSocketListServer(tmpBuf, size);
                break;
            case SUB_GP_LIST.SUB_GP_LIST_MATCH:
                break;
            case SUB_GP_LIST.SUB_GP_VIDEO_OPTION:
                break;
            case SUB_GP_LIST.SUB_GP_LIST_FINISH:
                OnSocketListFinish(tmpBuf, size);
                break;
            case SUB_GP_LIST.SUB_GP_SERVER_FINISH:
                break;
            case SUB_GP_LIST.SUB_GR_KINE_ONLINE:
                break;
            case SUB_GP_LIST.SUB_GR_SERVER_ONLINE:
                break;
            case SUB_GP_LIST.SUB_GR_ONLINE_FINISH:
                break;
            default:
                break;
        }
    }

    // 种类列表
    bool OnSocketListKind(byte[] data, int size)
    {
        ////更新数据
        int itemSize = Marshal.SizeOf(typeof(TagGameKind));
        if (size % itemSize != 0)
            return false;
        int iItemCount = size / itemSize;
        for (int i = 0; i < iItemCount; i++)
        {
            TagGameKind pGameKind =
                (TagGameKind) NetUtil.BytesToStruct(data, typeof(TagGameKind), itemSize, i * itemSize);
            CServerListData.Instance.InsertGameKind(pGameKind);
        }

        return true;
    }

    // 房间列表
    bool OnSocketListServer(byte[] data, int size)
    {
        ////更新数据
        int itemSize = Marshal.SizeOf(typeof(TagGameServer));
        if (size % itemSize != 0)
            return false;
        int iItemCount = size / itemSize;
        for (int i = 0; i < iItemCount; i++)
        {
            TagGameServer pGameServer =
                (TagGameServer) NetUtil.BytesToStruct(data, typeof(TagGameServer), itemSize, i * itemSize);
            CServerListData.Instance.InsertGameServer(pGameServer);
        }

        return true;
    }

    // 列表完成
    bool OnSocketListFinish(byte[] data, int size)
    {
        //登陆完成通知
        return true;
    }

    #endregion

    #region 登陆信息

    // 登陆信息处理
    void OnSocketMainLogon(int sub, byte[] tmpBuf, int size)
    {
        MyDebug.Log("登陆信息 Sub:" + (SUB_GP_LOGON_STATE) sub);
        switch ((SUB_GP_LOGON_STATE) sub)
        {
            case SUB_GP_LOGON_STATE.SUB_GP_LOGON_SUCCESS:
                OnSocketSubLogonSuccess(tmpBuf, size);
                break;
            case SUB_GP_LOGON_STATE.SUB_GP_LOGON_FAILURE:
                OnSocketSubLogonFailure(tmpBuf, size);
                break;
            case SUB_GP_LOGON_STATE.SUB_GP_LOGON_FINISH:
                OnSocketSubLogonFinish(tmpBuf, size);
                break;
            case SUB_GP_LOGON_STATE.SUB_GP_VALIDATE_MBCARD:
                OnSocketSubLogonValidateMBCard(tmpBuf, size);
                break;
            case SUB_GP_LOGON_STATE.SUB_GP_VALIDATE_PASSPORT:
                break;
            case SUB_GP_LOGON_STATE.SUB_GP_VERIFY_RESULT:
                break;
            case SUB_GP_LOGON_STATE.SUB_GP_MATCH_SIGNUPINFO:
                OnSocketSubMacthSignupInfo(tmpBuf, size);
                break;
            case SUB_GP_LOGON_STATE.SUB_GP_GROWLEVEL_CONFIG:
                break;
            case SUB_GP_LOGON_STATE.SUB_GP_UPDATE_NOTIFY:
                OnSocketSubUpdateNotify(tmpBuf, size);
                break;
            default:
                break;
        }
    }

    CMD_GP_LogonAccounts vxAccount;

    // 微信登陆成功
    public void OnWxLoginSucess(WxUserInfo kWxUserInfo)
    {

        LoginData.wxUserInfo = kWxUserInfo;
        vxAccount = new CMD_GP_LogonAccounts();
        vxAccount.dwPlazaVersion = LoginData.PlazaVersion;
        vxAccount.cbValidateFlags = 0x01 | 0x02;
        vxAccount.szMachineID = new byte[33];
        vxAccount.szAccounts = new byte[32];
        vxAccount.szHeadHttp = new byte[256];
        vxAccount.szPassword = new byte[33];

        byte[] bt = NetUtil.StringToBytes("test666");
        Array.Copy(bt, vxAccount.szMachineID, bt.Length);

        bt = NetUtil.StringToBytes("WX" + LoginData.wxUserInfo.openid);
        Array.Copy(bt, vxAccount.szAccounts, bt.Length);
        bt = NetUtil.StringToBytes(LoginData.wxUserInfo.headimgurl);

        Array.Copy(bt, vxAccount.szHeadHttp, bt.Length);
        bt = NetUtil.StringToBytes("WeiXinPassword");
        Array.Copy(bt, vxAccount.szPassword, bt.Length);
        GlobalDataScript.tagUserData = new TagGlobalUserData
        {
            szAccounts = vxAccount.szAccounts,
            szHeadHttp = vxAccount.szHeadHttp,
            szPassword = vxAccount.szPassword

        };
        ISocketEngineSink();
    }

    private void OnSocketSubMacthSignupInfo(byte[] tmpBuf, int size)
    {
    }

    // 登陆完成
    private void OnSocketSubLogonFinish(byte[] tmpBuf, int size)
    {
        SocketPIndividualEveng.Instance.ISocketEngineSink();
        //链接游戏服
    }

    // 登陆提示
    private void OnSocketSubUpdateNotify(byte[] tmpBuf, int size)
    {
    }

    // 登陆失败
    private void OnSocketSubLogonValidateMBCard(byte[] tmpBuf, int size)
    {
    }

    // 登陆失败
    private void OnSocketSubLogonFailure(byte[] tmpBuf, int size)
    {
        DBR_GP_LogonError logonError =
            (DBR_GP_LogonError) NetUtil.BytesToStruct(tmpBuf, typeof(DBR_GP_LogonError), size);
        OnGPLoginFailure(logonError.lErrorCode, NetUtil.GetServerLog(logonError.szErrorDescribe));

    }

    //登陆成功
    private void OnSocketSubLogonSuccess(byte[] tmpBuf, int size)
    {
        MyDebug.Log("登陆成功");
        CMD_GP_LogonSuccess logSucess = NetUtil.BytesToStruct<CMD_GP_LogonSuccess>(tmpBuf);
        MyDebug.Log("登陆成功111");
        ClientResponse response = new ClientResponse
        {
            headCode = APIS.LOGIN_RESPONSE,
            status = 1
        };
        MyDebug.Log("登陆成功  222");
        AvatarVO avatar = new AvatarVO
        {
            account = new Account()
        };
        avatar.account.nickname = LoginData.wxUserInfo.nickname;
        avatar.account.sex = LoginData.wxUserInfo.sex;
        avatar.account.uuid = (int) logSucess.dwUserID;
        avatar.account.headicon = LoginData.wxUserInfo.headimgurl;
        avatar.account.roomcard = (int) logSucess.lUserScore;
        if (GlobalDataScript.weChatInformation == null)
            GlobalDataScript.weChatInformation = new WeChatInformation();
        GlobalDataScript.weChatInformation.weChatName = LoginData.wxUserInfo.nickname;
        GlobalDataScript.weChatInformation.sex = LoginData.wxUserInfo.sex;
        MyDebug.Log("登陆成功  333");
        avatar.IP = "0"; // GlobalDataScript.getInstance().getIpAddress();
        response.message = JsonMapper.ToJson(avatar);
        GlobalDataScript.userData = logSucess;
        MyDebug.Log("登陆成功   444");
        SocketEventHandle.Instance.AddResponse(response);
        MyDebug.Log("登陆成功    555");
    }

    //登陆失败
    void OnGPLoginFailure(uint iErrorCode, string szDescription)
    {
        MyDebug.Log("ErrorCode:" + iErrorCode);

        if (iErrorCode == 1 || iErrorCode == 3) //注册
        {
            SocketSendManager.Instance.RegisterAccount();
        }
        else
        {
            SocketEventHandle.Instance.iscloseLoading = true;
            SocketEngine.Instance.SocketQuit();
            SocketEventHandle.Instance.SetTips("ErrorCode:" + iErrorCode + "/////" + szDescription);
        }
    }

    #endregion

    public void ISocketEngineSink()
    {
        SocketEngine.Instance.SetSocketEvent(this);
        SocketEngine.Instance.InitSocket(APIS.socketUrl, APIS.socketPort);
    }

    public void OnEventTCPSocketLink()
    {
        SocketSendManager.Instance.LoginAccount(vxAccount);
    }

    public void OnEventTCPSocketShut()
    {
    }

    public void OnEventTCPSocketError(int errorCode)
    {
    }

    public bool OnEventTCPHeartTick()
    {
        return true;
    }
}