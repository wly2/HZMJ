using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
//类型信息
public struct TagGameKind
{
    public ushort wTypeID; //类型号码
    public ushort wJoinID; //挂接索引
    public ushort wSortID; //排序号码
    public ushort wKindID; //名称号码
    public ushort wGameID; //模块索引
    public uint dwOnLineCount; //在线人数
    public uint dwFullCount; //满员人数

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szKindName; //游戏名字

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szProcessName; //进程名字
}

//游戏房间列表结构
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct TagGameServer
{
    public ushort wKindID; //名称索引
    public ushort wNodeID; //节点索引
    public ushort wSortID; //排序索引
    public ushort wServerID; //房间索引
    public ushort wServerType; //房间类型
    public ushort wServerPort; //房间端口
    public long lCellScore; //单元积分
    public long lEnterScore; //进入积分
    public uint dwServerRule; //房间规则
    public uint dwOnLineCount; //在线人数
    public uint dwAndroidCount; //机器人数
    public uint dwFullCount; //满员人数

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szServerAddr; //房间名称

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szServerName; //房间名称
}

//登陆成功
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_GR_LogonSuccess
{
    public uint dwUserRight; //用户权限
    public uint dwMasterRight; //管理权限
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
//登录失败
public struct CMD_GR_LogonFailure
{
    public long lErrorCode; //错误代码

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
    public byte[] szDescribeString; //描述消息
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_ResponseReplayList
{
    public ushort userID; //请求用户ID，用于验证
    public ushort recordNum; //实际的游戏次数，可能小于10
    public uint[,] recordID; //每个游戏录像的ID

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public uint[] roomNum;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public uint[] time;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public ushort[] gameType; //游戏类型 转转-300 杭州-400

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public uint[] gameRule; //游戏规则 创建游戏房间时的规则数据

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
    public uint[] playTotalCount; //游戏局数8？16

    public ushort[,] userIDList; //玩家列表
    public int[,] totalScore; //每次游戏的得分统计，8/16局的总得分
    public byte[,,] userName;
    public byte[,,] userHeadHttp;
    public int[,,] score; //8？16局的玩家得分情况[chair][gamelist]chair座位号0-3 gamelist 第几局0-8？ 0-16
}

//请求失败
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_GR_RequestFailure
{
    public long lErrorCode; //错误代码

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public byte[] szDescribeString; //描述信息
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct TagUserStatus
{
    public ushort wTableID; //桌子索引
    public ushort wChairID; //椅子位置
    public byte cbUserStatus; //用户状态
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_GF_S_UserChat
{
    public ushort wChatLength; //信息长度
    public uint dwChatColor; //信息颜色
    public uint dwSendUserID; //发送用户
    public uint dwTargetUserID; //目标用户

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
    public byte[] szChatString; //聊天信息
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_GR_S_UserExpression
{
    public ushort wItemIndex; //表情索引
    public uint dwSendUserID; //发送用户
    public uint dwTargetUserID; //目标用户
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_CM_SystemMessage
{
    public ushort wType; //消息类型
    public ushort wLength; //消息长度

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
    public byte[] szString; //消息内容
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_GR_C_TableTalk
{
    public byte cbChairID; //座位
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_GR_Match_Wait_Tip
{
    public long lScore; //当前积分
    public ushort wRank; //当前名次
    public ushort wCurTableRank; //本桌名次
    public ushort wUserCount; //当前人数
    public ushort wCurGameCount; //当前局数
    public ushort wGameCount; //总共局数
    public ushort wPlayingTable; //游戏桌数

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szMatchName; //比赛名称
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_GR_MatchResult
{
    public long lGold; //金币奖励
    public uint dwIngot; //元宝奖励
    public uint dwExperience; //经验奖励

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public byte[] szDescribe; //得奖描述
}

//用户信息
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct TagUserInfo
{
    //基本属性
    public uint dwUserID; //用户 I D
    public uint dwGameID; //游戏 I D
    public uint dwGroupID; //社团 I D

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szNickName; //用户昵称

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szGroupName; //社团名字

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szUnderWrite; //个性签名

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szLogonIP; //登录IP

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public byte[] szHeadHttp; //

    //头像信息
    public ushort wFaceID; //头像索引

    public uint dwCustomID; //自定标识

    //用户资料
    public byte cbGender; //用户性别
    public byte cbMemberOrder; //会员等级

    public byte cbMasterOrder; //管理等级

    //用户状态
    public ushort wTableID; //桌子索引

    //public ushort wLastTableID;                      //游戏桌子
    public ushort wChairID; //椅子索引

    public byte cbUserStatus; //用户状态

    //积分信息
    public long lScore; //用户分数
    public long lGrade; //用户成绩

    public long lInsure; //用户银行

    //public long lGameGold;                                //用户元宝
    //游戏信息
    public uint dwWinCount; //胜利盘数
    public uint dwLostCount; //失败盘数
    public uint dwDrawCount; //和局盘数
    public uint dwFleeCount; //逃跑盘数
    public uint dwUserMedal; //用户奖牌
    public uint dwExperience; //用户经验

    public long lLoveLiness; //用户魅力

    //时间信息
    public TagTimeInfo TimerInfo;
}

//时间信息
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct TagTimeInfo
{
    public uint dwEnterTableTimer; //进出桌子时间
    public uint dwLeaveTableTimer; //离开桌子时间
    public uint dwStartGameTimer; //开始游戏时间
    public uint dwEndGameTimer; //离开游戏时间
}

//头像信息
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct TagCustomFaceInfo
{
    public uint dwDataSize; //数据大小

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48 * 48)]
    public uint dwCustomFace; //图片信息
}

//出弹消息结果
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_ChuZengResult
{
    public byte bAllChuZeng; //是否所有用户选择了出增
    public ushort wCurrentUser; //当前选择出增的用户

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] cbZengValue; //当前每个用户的出弹情况，0xff代表无效值，1代表出弹，0代表不出弹
}

//出弹消息 出现这个消息 客户端需要显示是否选择出弹按钮，庄家不需要显示，庄家默认出弹
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_ChuZeng
{
    public ushort lSiceCount; //骰子点数
    public ushort wBankerUser; //庄家用户
    public ushort lLianZhuangCount; //连庄计数

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[] lGameTotalScore; //每个用户总的分数 每场输赢的总分
}

//组合子项
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_WeaveItem
{
    public byte cbWeaveKind; //组合类型
    public byte cbCenterCard; //中心扑克
    public byte cbPublicCard; //公开标志

    public ushort wProvideUser; //供应用户
    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    //public byte[] cbCardData;                                     //扑克数据
}

//空闲状态
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_StatusFree
{
    public long lCellScore; //基础金币
    public ushort wBankerUser; //庄家用户

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] bTrustee; //是否托管

    //public byte bYouDan;                                                      //是否是有弹
    //public byte cbQuanShu;                                                    //几圈局
}

//出增状态
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_StatusChuZeng
{
    public int lCellScore; //基础金币
    public ushort wBankerUser; //庄家用户

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] bTrustee; //是否托管

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] cbChuDan; //每个是否选择出弹  0xff代表还未做出选择 1代表出弹 0代表未出弹

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[] lGameTotalScore; //每个用户总的分数 每场输赢的总分
}

//游戏状态
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_StatusPlay
{
    //游戏变量
    public long lCellScore; //单元积分
    public ushort wBankerUser; //庄家用户

    public ushort wCurrentUser; //当前用户

    //状态变量
    public byte cbActionCard; //动作扑克
    public byte cbActionMask; //动作掩码
    public byte cbLeftCardCount; //剩余数目

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] bTrustee; //是否托管

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public ushort[] wWinOrder;

    //出牌信息
    public ushort wOutCardUser; //出牌用户
    public byte cbOutCardData; //出牌扑克

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] cbDiscardCount; //丢弃数目
                                  //----------------------------------------------------------------------------------------
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public DiscardCard[] cbDiscardCard; //丢弃记录

    //扑克数据
    public byte cbCardCount; //扑克数目

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public byte[] cbCardData; //扑克列表

    public byte cbSendCardData; //发送扑克

    //组合扑克
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] cbWeaveCount; //组合数目
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public Weave[] WeaveItemArray; //组合扑克
    ////财神信息
    //public byte cbMagicIndex;                                                //财神信息
    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    //public byte[] cbChuDan;                   //每个是否选择了出弹  0xff代表还未做出选择 1代表出弹 0代表未出弹
    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    //public int[] lGameTotalScore;              //每个用户总的分数 每场输赢的总分
}
public struct DiscardCard
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
    public byte[] WeaveItem;
}

//游戏开始
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_GameStart
{
    public ushort wSiceCount; //骰子点数
    public ushort wBankerUser; //庄家用户
    public ushort wCurrentUser; //当前用户
    public byte cbUserAction; //用户动作

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public byte[] cbCardData; //牌列表

    public byte cbLeftCardCount;

    //财神信息
    public byte cbMagicIndex; //财神 索引值
    public ushort wLianZhuangCount; //连庄计数
}

//出牌命令
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_OutCard
{
    public ushort wOutCardUser; //出牌用户
    public byte cbOutCardData; //出牌扑克
}

//发送扑克
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_SendCard
{
    public byte cbCardData; //扑克数据
    public byte cbActionMask; //动作掩码
    public ushort wCurrentUser; //当前用户
    public byte bTail; //末尾发牌
}

//结果信息
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_ResultInfo
{
    public uint paiXingMask; //胡牌牌型掩码
}

//游戏结束
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_GameEnd
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] cbCardCount; //手牌个数

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public CbCard[] cbCardData; //手牌

    public byte cb_hu_card; //胡牌

    //组合扑克
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[] cbWeaveCount; //组合数目

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public Weave[] WeaveItemArray; //组合扑克

    //结束信息
    public uint dwChiHuRight; //胡牌类型
    public ushort wChiHuUser; //赢的玩家

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
    public uint[] dwDuoTaiRight; //多台数相关的掩码

    public ushort lTotalTaiShu; //台数 只有赢的玩家

    //积分信息
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public long[] lGameScore; //游戏积分

    public byte bAllGameEnd; //房间解散
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CbCard
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public byte[] cbCardData;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct Weave
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public CMD_WeaveItem[] WeaveItem;
}

//操作提示
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_OperateNotify
{
    public ushort wResumeUser; //还原用户
    public byte cbActionMask; //动作掩码
    public byte cbActionCard; //动作扑克
}

//操作命令
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_OperateResult
{
    public ushort wOperateUser; //操作用户
    public ushort wProvideUser; //供应用户
    public byte cbOperateCode; //操作代码

    public byte cbOperateCard; //操作扑克
    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    //public int[] lGameTotalScore;           //每个用户总的分数 每场输赢的总分
}

//用户托管
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_Trustee
{
    public byte bTrustee; //是否托管
    public ushort wChairID; //托管用户
}

//用户信息
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_USER_INFO
{
    public ushort dwUserID; //用户ID

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public string[] szLogonIP; //登陆IP

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public string[] szHeadHttp; //头像地址
}

//聊天命令
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_S_Chat
{
    public ushort wChairID; //座位号

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public byte[] szTitle;
}

//出牌命令
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_C_OutCard
{
    public byte cbCardData; //扑克数据
}

//操作命令
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_C_OperateCard
{
    public byte cbOperateCode; //操作代码
    public byte cbOperateCard; //操作扑克
}

//用户托管
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_C_Trustee
{
    public byte bTrustee; //是否托管
}

//聊天命令
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_C_Chat
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public byte[] szTitle; //语音ID
}

//出弹命令
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct CMD_C_ChuZeng
{
    public byte cbChuZengValue; //出弹选择值
}

//私人场房间信息
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
struct CMD_GF_Private_Room_Info
{
    public byte bPlayCoutIdex; //玩家局数0,1
    public byte bGameTypeIdex; //游戏类型
    public uint bGameRuleIdex; //游戏规则
    public byte cb_pay_type;
    public ushort w_player_count;
    public byte bStartGame;
    public uint dwPlayCout; //游戏局数
    public uint dwRoomNum;
    public uint dwCreateUserID;

    public uint dwPlayTotal; //总局数

    //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    //public int[] kWinLoseScore;
    public byte cbRoomType;
}

//查询信息
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
struct CMD_GP_QueryAccountInfo
{
    public uint dwUserID; //用户 I D
}

//操作成功
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
struct CMD_GP_InGameSeverID
{
    public uint LockKindID;
    public uint LockServerID;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
public struct TagGlobalUserData
{
    //基本资料
    public uint dwUserID; //用户 I D
    public uint dwGameID; //游戏 I D
    public uint dwUserMedal; //用户奖牌
    public uint dwExperience; //用户经验
    public uint dwLoveLiness; //用户魅力
    public uint dwSpreaderID; //推广ID

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szAccounts; //登录帐号

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szNickName; //用户昵称

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
    public byte[] szPassword; //登录密码

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
    public byte[] szDynamicPass; //动态密码

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szLogonIP; //登录IP

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szUserChannel; //渠道号

    //用户成绩
    public long lUserScore; //用户游戏币
    public long lUserInsure; //银行游戏币
    public long lUserIngot; //用户元宝

    public double dUserBeans; //用户游戏豆

    //扩展资料
    public byte cbGender; //用户性别
    public byte cbMoorMachine; //锁定机器

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szUnderWrite; //个性签名

    //社团资料
    public uint dwGroupID; //社团索引

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szGroupName; //社团名字

    //会员资料
    public byte cbMemberOrder; //会员等级

    public Systemtime MemberOverDate; //到期时间

    //头像信息
    public ushort wFaceID; //头像索引
    public uint dwCustomID; //自定标识

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
    public byte[] szHeadHttp; //http头像

    //配置信息
    public byte cbInsureEnabled; //银行使能
    public uint dwWinCount; //胜利盘数
    public uint dwLostCount; //失败盘数
    public uint dwDrawCount; //和局盘数
    public uint dwFleeCount; //逃跑盘数
}