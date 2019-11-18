public enum MISSIONTYPE
{
    MISSION_LOGIN_ACCOUNT = 1,
    MISSION_LOGIN_GAMEID = 2,
    MISSION_REGISTER = 3,
    MISSION_UPDATE_INFO = 4,
    MISSION_SERVER_INFO = 5,
    MISSION_LOGIN_VISITOR = 6,
    MISSION_REQUEST_REPLAY_LIST = 7,
    MISSION_REQUEST_REPLAY_DATA = 8,
    MISSION_REQUEST_BUY_FANGKA = 9,
    MISSION_REQUEST_SHARE = 10,
}

public enum SUB_GP_LOGON_STATE
{
    SUB_GP_LOGON_SUCCESS = 100, //登录成功
    SUB_GP_LOGON_FAILURE = 101, //登录失败
    SUB_GP_LOGON_FINISH = 102, //登录完成
    SUB_GP_VALIDATE_MBCARD = 103, //登录失败
    SUB_GP_VALIDATE_PASSPORT = 104, //登录失败
    SUB_GP_VERIFY_RESULT = 105, //验证结果
    SUB_GP_MATCH_SIGNUPINFO = 106, //报名信息
    SUB_GP_GROWLEVEL_CONFIG = 107, //等级配置
    SUB_GP_UPDATE_NOTIFY = 200, //升级提示
}

public enum MainCmd
{
    MDM_GP_LOGON = 1, //广场登录
    MDM_GP_SERVER_LIST = 2, //列表信息
    MDM_GP_USER_SERVICE = 3, //用户服务
}

public enum SUB_GP_LIST
{
    //列表信息
    SUB_GP_LIST_TYPE = 100, //类型列表
    SUB_GP_LIST_KIND = 101, //种类列表
    SUB_GP_LIST_NODE = 102, //节点列表
    SUB_GP_LIST_PAGE = 103, //定制列表
    SUB_GP_LIST_SERVER = 104, //房间列表
    SUB_GP_LIST_MATCH = 105, //比赛列表
    SUB_GP_VIDEO_OPTION = 106, //视频配置

    //完成信息
    SUB_GP_LIST_FINISH = 200, //发送完成
    SUB_GP_SERVER_FINISH = 201, //房间完成

    //在线信息
    SUB_GR_KINE_ONLINE = 300, //类型在线
    SUB_GR_SERVER_ONLINE = 301, //房间在线
    SUB_GR_ONLINE_FINISH = 302, //在线完成
}

public enum MDM_GP_LOGONtt
{
}

public enum GameServer : int
{
    MDM_GR_LOGON = 1, //登录信息
    MDM_GR_CONFIG = 2, //配置信息
    MDM_GR_USER = 3, //用户信息
    MDM_GR_STATUS = 4, //状态信息
    MDM_CM_SYSTEM = 1000, //系统命令
    MDM_GF_GAME = 200, //游戏命令
    MDM_GF_FRAME = 100, //框架命令
    MDM_GR_MATCH = 9, //比赛命令
    MDM_GR_PRIVATE = 10 //比赛命令
}

public enum MDM_GR_LOGON : int
{
    SUB_GR_LOGON_USERID = 1, //ID登录
    SUB_GR_LOGON_MOBILE = 2, //手机登录
    SUB_GR_LOGON_ACCOUNTS = 3, //账户登录
    SUB_GR_LOGON_SUCCESS = 100, //登录成功
    SUB_GR_LOGON_FAILURE = 101, //登录失败
    SUB_GR_LOGON_FINISH = 102, //登录完成
    SUB_GR_UPDATE_NOTIFY = 200, //升级提示
    SUB_RESPONSE_REPLAY_LIST = 412, //响应录像列表
    SUB_RESPONSE_REPLAY_DATA = 413 //响应录像数据
}

public enum MDM_GR_CONFIG : int
{
    SUB_GR_CONFIG_COLUMN = 100, //列表配置
    SUB_GR_CONFIG_SERVER = 101, //房间配置
    SUB_GR_CONFIG_PROPERTY = 102, //道具配置
    SUB_GR_CONFIG_USER_RIGHT = 104, //玩家权限
    SUB_GR_CONFIG_FINISH = 103 //配置完成
}

public enum MDM_GR_USER : int
{
    SUB_GR_USER_RULE = 1, //用户规则
    SUB_GR_USER_LOOKON = 2, //旁观请求
    SUB_GR_USER_SITDOWN = 3, //坐下请求
    SUB_GR_USER_STANDUP = 4, //起立请求
    SUB_GR_USER_INVITE = 5, //用户邀请
    SUB_GR_USER_INVITE_REQ = 6, //邀请请求
    SUB_GR_USER_REPULSE_SIT = 7, //拒绝玩家坐下
    SUB_GR_USER_KICK_USER = 8, //踢出用户
    SUB_GR_USER_INFO_REQ = 9, //请求用户信息
    SUB_GR_USER_CHAIR_REQ = 10, //请求更换位置
    SUB_GR_USER_CHAIR_INFO_REQ = 11, //请求椅子用户信息
    SUB_GR_SIT_FAILED = 103, //请求失败
    SUB_GR_USER_ENTER = 100, //用户进入
    SUB_GR_USER_SCORE = 101, //用户分数
    SUB_GR_USER_STATUS = 102, //用户状态
    SUB_GR_USER_CHAT = 201, //聊天消息
    SUB_GR_USER_EXPRESSION = 202, //表情消息
    SUB_GR_WISPER_CHAT = 203, //私聊消息
    SUB_GR_WISPER_EXPRESSION = 204, //私聊表情
    SUB_GR_COLLOQUY_CHAT = 205, //会话消息
    SUB_GR_COLLOQUY_EXPRESSION = 206, //会话表情
    SUB_GR_PROPERTY_BUY = 300, //购买道具
    SUB_GR_PROPERTY_SUCCESS = 301, //道具成功
    SUB_GR_PROPERTY_FAILURE = 302, //道具失败
    SUB_GR_PROPERTY_EFFECT = 304, //道具效应
    SUB_GR_PROPERTY_MESSAGE = 303, //道具消息
    SUB_GR_PROPERTY_TRUMPET = 305, //喇叭消息
    SUB_GR_GLAD_MESSAGE = 400 //喜报消息
}

public enum MDM_GR_STATUS : int
{
    SUB_GR_TABLE_INFO = 100, //桌子信息
    SUB_GR_TABLE_STATUS = 101 //桌子状态
}

public enum MDM_CM_SYSTEM : int
{
    SUB_CM_SYSTEM_MESSAGE = 1 //系统消息
}

public enum MDM_GF_FRAME : int
{
    SUB_GF_USER_CHAT = 10, //用户聊天
    SUB_GR_TABLE_TALK = 12, //用户聊天
    SUB_GF_USER_EXPRESSION = 11, //用户表情
    SUB_GF_GAME_STATUS = 100, //游戏状态
    SUB_GF_GAME_SCENE = 101, //游戏场景
    SUB_GF_LOOKON_STATUS = 102, //旁观状态
    SUB_GF_SYSTEM_MESSAGE = 200, //系统消息
    SUB_GF_ACTION_MESSAGE = 201, //动作消息
    SUB_GF_GAME_OPTION = 1, //游戏配置
    SUB_GF_USER_READY = 2, //用户准备
    SUB_GF_LOOKON_CONFIG = 3, //旁观配置
    SUB_GR_MATCH_INFO = 403, //比赛信息
    SUB_GR_MATCH_WAIT_TIP = 404, //等待提示
    SUB_GR_MATCH_RESULT = 405 //游戏结果
}

public enum MDM_GR_MATCH : int
{
    SUB_GR_MATCH_FEE = 400, //报名费用
    SUB_GR_MATCH_NUM = 401, //等待人数
    SUB_GR_LEAVE_MATCH = 402, //退出比赛
    SUB_GR_MATCH_INFO = 403, //比赛信息
    SUB_GR_MATCH_WAIT_TIP = 404, //等待提示
    SUB_GR_MATCH_RESULT = 405, //比赛结果
    SUB_GR_MATCH_STATUS = 406, //比赛状态
    SUB_GR_MATCH_GOLDUPDATE = 409, //金币更新
    SUB_GR_MATCH_ELIMINATE = 410, //比赛淘汰
    SUB_GR_MATCH_JOIN_RESOULT = 411 //加入结果
}

public enum MDM_GR_PRIVATE : int
{
    SUB_GR_PRIVATE_INFO = 401, //四人场信息
    SUB_GR_CREATE_PRIVATE = 402, //创建四人场
    SUB_GR_CREATE_PRIVATE_SUCESS = 403, //创建四人场成功
    SUB_GR_JOIN_PRIVATE = 404, //加入四人场
    SUB_GF_PRIVATE_ROOM_INFO = 405, //四人场房间信息
    SUB_GF_PRIVATE_END = 407, //四人场结算
    SUB_GR_PRIVATE_DISMISS = 406, //四人场请求解散
    SUB_GR_RIVATE_AGAIN = 408, //重新加入（重连）
    SUB_GR_RIVATE_RECHARGE = 409, //充值
    SUB_GR_RIVATE_RECHARGE_SUCCESS = 410, //充值成功
}

public enum DTP_GR_TABL : int
{
    DTP_GR_TABLE_PASSWORD = 1, //桌子密码

    //用户属性
    DTP_GR_NICK_NAME = 10, //用户昵称
    DTP_GR_GROUP_NAME = 11, //社团名字
    DTP_GR_UNDER_WRITE = 12, //个性签名

    //附加信息
    DTP_GR_USER_NOTE = 20, //用户备注
    DTP_GR_CUSTOM_FACE = 21, //自定头像
}

public enum REQUEST_FAILURE : int
{
    REQUEST_FAILURE_NORMAL = 0, //常规原因
    REQUEST_FAILURE_NOGOLD = 1, //金币不足
    REQUEST_FAILURE_NOSCORE = 2, //积分不足
    REQUEST_FAILURE_PASSWORD = 3, //密码错误
}

public enum MDM_GF : int
{
    MDM_GF_RECORD = 300 //录像回放命令
}

public enum SUB_REQUEST : int
{
    SUB_REQUEST_REPLAY_LIST = 410, //CMD_C_RequestReplayList//请求录像列表
    SUB_REQUEST_REPLAY_DATA = 411, //CMD_C_RequestReplayData请求录像数据
    SUB_RESPONSE_REPLAY_LIST = 412, //CMD_S_ResponseReplayList//响应录像列表
    SUB_RESPONSE_REPLAY_DATA = 413, //CMD_S_ResponseReplayData响应 录像数据
    SUB_REQUEST_RECHARGE = 414, //充值需求
    SUB_RESPONSE_RECHARGE = 415, //充值结果返回
    SUB_REQUEST_WCHAT_SHARE = 416, //微信分享
    SUB_RESPONSE_WCHAT_SHARE = 417, //微信分享结果返回
}

/// <summary>
/// 选择创建房间或加入房间
/// </summary>
public enum ModeType
{
    None,
    Create,
    Join,
}

//服务器命令结构
public enum SUB_S
{
    SUB_S_GAME_START = 100, //游戏开始
    SUB_S_OUT_CARD = 101, //出牌命令
    SUB_S_SEND_CARD = 102, //发送扑克
    SUB_S_OPERATE_NOTIFY = 104, //操作提示
    SUB_S_OPERATE_RESULT = 105, //操作命令
    SUB_S_GAME_END = 106, //游戏结束
    SUB_S_TRUSTEE = 107, //用户托管
    SUB_S_USER_INFO = 110, //用户信息
    SUB_S_CHAT = 112, //聊天命令
    SUB_S_CHU_ZENG = 114, //出弹消息
    SUB_S_CHU_ZENG_RESULT = 113 //出弹消息回应
}

//台州麻将 规则设置
public enum GAME_RULE
{
    GR_WU_DAN = 0,
    GR_YOU_DAN,
    GAME_RULE_NUM
}

//服务定义
public enum CARD
{
    INVALID_VALUE = 0xFF,
    time_start_game = 30, //开始定时器
    TIME_OPERATE_CARD = 15, //操作定时器
    CARD_COLOR_NULL = 0,
    CARD_COLOR_TONG = 1,
    CARD_COLOR_WAN = 2,
    CARD_COLOR_TIAO = 3,
}

//游戏属性
public enum GAME
{
    KIND_ID = 391, //游戏ID
}

//组件属性
public enum GAME_PLAYER
{
    GAME_PLAYER = 4, //游戏人数
}

//状态定义
public enum GAME_SCENE
{
    GAME_SCENE_FREE = 0, //等待开始
    GAME_SCENE_PLAY = 101, //游戏状态
    GAME_SCENE_CHU_ZENG = 102, //出弹状态
}

//常量定义
public enum CONSTANTS
{
    MAX_WEAVE = 4, //最大组合
    MAX_INDEX = 34, //最大索引
    MAX_COUNT = 14, //最大数目
    MAX_REPERTORY = 136, //最大库存
    ZI_PAI_START_INDEX = 27, //东风起始索引
    BAI_BAN_INDEX = 33, //白板索引
    LIU_JU_COUNT = 16, //流局剩余的牌数
    MAX_RIGHT_COUNT = 1, //最大权位DWORD个数
    LEN_ACCOUNTS = 32, //IP
    LEN_USER_NOTE = 256, //用户头像
    PRO_VERSION = 2, //程序版本号
}

//客户端命令结构
public enum SUB_C
{
    SUB_C_OUT_CARD = 1, //出牌命令
    SUB_C_OPERATE_CARD = 3, //操作扑克
    SUB_C_TRUSTEE = 4, //用户托管
    SUB_C_CHAT = 7, //聊天
    //SUB_C_CHU_ZENG = 7,                                    //出增回复
}

public enum MJ_PAI
{
    INVALID_PAI = 0x00,
    YI_TONG = 0x01,
    ER_TONG,
    SAN_TONG,
    SI_TONG,
    WU_TONG,
    LIU_TONG,
    QI_TONG,
    BA_TONG,
    JIU_TONG,
    YI_WAN = 0x11,
    ER_WAN,
    SAN_WAN,
    SI_WAN,
    WU_WAN,
    LIU_WAN,
    QI_WAN,
    BA_WAN,
    JIU_WAN,
    YI_SUO = 0x21,
    ER_SUO,
    SAN_SUO,
    SI_SUO,
    WU_SUO,
    LIU_SUO,
    QI_SUO,
    BA_SUO,
    JIU_SUO,
    DONG_FENG = 0x31,
    NAN_FENG,
    XI_FENG,
    BEI_FENG,
    HONG_ZHONG,
    FA_CAI,
    BAI_BAN
}

public enum WIK : byte
{
    WIK_NULL = 0x00, //没有类型
    WIK_LEFT = 0x01, //左吃类型
    WIK_CENTER = 0x02, //中吃类型
    WIK_RIGHT = 0x04, //右吃类型
    WIK_PENG = 0x08, //碰牌类型
    WIK_GANG = 0x10, //杠牌类型
    WIK_CHI_HU = 0x20, //吃胡类型
}

public enum MAIN_CMD
{
    MDM_GP_LOGON = 1,
    MDM_GP_SERVER_LIST = 2, //列表信息
    MDM_GP_USER_SERVICE = 3, //用户服务
    MDM_GP_REMOTE_SERVICE = 4, //远程服务
    MDM_MB_LOGON = 100, //广场登陆
    MDM_MB_SERVER_LIST = 101, //列表信息
}

public enum MDM_SERVICE
{
    //个人资料
    SUB_GP_USER_INDIVIDUAL = 301, //个人资料
    SUB_GP_QUERY_INDIVIDUAL = 302, //查询信息
    SUB_GP_MODIFY_INDIVIDUAL = 303, //修改资料
    SUB_GP_QUERY_ACCOUNTINFO = 304, //个人信息
    SUB_GP_QUERY_INGAME_SEVERID = 305, //游戏状态

    //设置推荐人结果
    SUB_GP_SPREADER_RESOULT = 520, //设置推荐人结果

    //操作结果
    SUB_GP_OPERATE_SUCCESS = 900, //操作成功
    SUB_GP_OPERATE_FAILURE = 901, //操作失败
}