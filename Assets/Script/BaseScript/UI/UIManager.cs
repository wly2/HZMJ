using AssemblyCSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoSingleton<UIManager>
{
    readonly Dictionary<UIType, string> UIResources = new Dictionary<UIType, string>();

    Dictionary<UIType, string> UI = new Dictionary<UIType, string>();
    private readonly Stack<UIWindow> windows = new Stack<UIWindow>();                          

    public void Init()
    {
    }
    

    public void Show(UIType _type, UnityAction<GameObject> loadFinishHandler = null, float daly = 0)
    {
        StartCoroutine(ShowUIpanel(_type, loadFinishHandler, daly));
    }        
    public UIManager()
    {        
        UIResources.Add(UIType.UIActivity, "BaseAssets/Prefab/UI/UIPanel_Activity");
        UIResources.Add(UIType.UICourse, "BaseAssets/Prefab/UI/UIPanel_Course");
        UIResources.Add(UIType.UICreateRoom, "BaseAssets/Prefab/UI/UIPanel_CreateRoom");
        UIResources.Add(UIType.UIDissSloveRoom, "BaseAssets/Prefab/UI/UIPanel_DissloveRoom");
        UIResources.Add(UIType.UIJoinRoom, "BaseAssets/Prefab/UI/UIPanel_JoinRoom");
        UIResources.Add(UIType.UIExitGame, "BaseAssets/Prefab/UI/UIPanel_ExitGame");
        UIResources.Add(UIType.UIExitRoom, "BaseAssets/Prefab/UI/UIPanel_ExitRoom");
        UIResources.Add(UIType.UIGameOver, "BaseAssets/Prefab/UI/UIPanel_GameOver");
        UIResources.Add(UIType.UIHelp, "BaseAssets/Prefab/UI/UIPanel_Help");
        UIResources.Add(UIType.UIInvite, "BaseAssets/Prefab/UI/UIPanel_Invite");
        UIResources.Add(UIType.UILoading, "BaseAssets/Prefab/UI/UIPanel_Loading");
        UIResources.Add(UIType.UIPersonalDetails, "BaseAssets/Prefab/UI/UIPanel_PersonalDetails");
        UIResources.Add(UIType.UIPositionMonitoring, "BaseAssets/Prefab/UI/UIPanel_PositionMonitoring");
        UIResources.Add(UIType.UIProtocol, "BaseAssets/Prefab/UI/UIPanel_Protocol");
        UIResources.Add(UIType.UIRank, "BaseAssets/Prefab/UI/UIPanel_Rank");
        UIResources.Add(UIType.UIRealname, "BaseAssets/Prefab/UI/UIPanel_Realname");
        UIResources.Add(UIType.UIRecharge, "BaseAssets/Prefab/UI/UIPanel_Recharge");
        UIResources.Add(UIType.UIReport, "BaseAssets/Prefab/UI/UIPanel_Report");
        UIResources.Add(UIType.UIResultsTheDetails, "BaseAssets/Prefab/UI/UIPanel_ResultsTheDetails");
        UIResources.Add(UIType.UIRoomOver, "BaseAssets/Prefab/UI/UIPanel_RoomOver");
        UIResources.Add(UIType.UIScreenshot, "BaseAssets/Prefab/UI/UIPanel_Screenshot");
        UIResources.Add(UIType.UISetting, "BaseAssets/Prefab/UI/UIPanel_Setting");
        UIResources.Add(UIType.UIShare, "BaseAssets/Prefab/UI/UIPanel_Share");
        UIResources.Add(UIType.UISignIn, "BaseAssets/Prefab/UI/UIPanel_SignIn");
        UIResources.Add(UIType.UITalk, "BaseAssets/Prefab/UI/UIPanel_Talk");
        UIResources.Add(UIType.UITipsDialog, "BaseAssets/Prefab/UI/UIPanel_TipsDialog");
        UIResources.Add(UIType.UIUserInfo, "BaseAssets/Prefab/UI/UIPanel_UserInfo");
        UIResources.Add(UIType.UIWithdrawal, "BaseAssets/Prefab/UI/UIPanel_Withdrawal");
        UIResources.Add(UIType.UIGameRoomSetting, "BaseAssets/Prefab/UI/UIPanel_GameRoomSetting");
        UIResources.Add(UIType.UIRegister, "BaseAssets/Prefab/UI/UIPanel_Register");
    }

    public void CloseUI()
    {
        windows.Pop();
    }

    public int SortingOrder
    {
        get
        {
            if (windows.Count <= 0)
                return 0;
            return windows.Peek().GetComponent<Canvas>().sortingOrder;
        }
    }

    public void ClearUI()
    {
        windows.Clear();
    }         
    public IEnumerator ShowUIpanel(UIType _type, UnityAction<GameObject> loadFinishHandler = null, float daly = 0)
    {
        yield return new WaitForSeconds(daly);
        MyDebug.Log(_type.ToString());
        ResourcesLoader.Load<GameObject>(UIResources[_type], (goo)=> {        
            var go = Instantiate(goo);
            var win = go.GetComponent<UIWindow>();
            win.GetComponent<Canvas>().sortingOrder = SortingOrder + 5;
            windows.Push(win);
            if (loadFinishHandler != null)
            {
                loadFinishHandler(go);
            }

        });   
    }
}

public enum UIType
{
    None = -1,
    UILoading = 0,
    UIGameOver,
    UIUserInfo,
    UISetting,
    UIHelp,
    UISignIn,
    UIService,
    UIMilitary,
    UIShare,
    UICreateRoom,
    UIJoinRoom,
    UIExitRoom,
    UIDissSloveRoom,
    UIExitGame,
    UITalk,
    UIRank,
    UIProtocol,
    UISceneLoading,
    UIInvite,
    UIPay,
    UITutorial,
    UIShiMing,
    UIActivity,
    UICourse,
    UIPersonalDetails,
    UIPositionMonitoring,
    UIRealname,
    UIRecharge,
    UIReport,
    UIResultsTheDetails,
    UIRoomOver,
    UIScreenshot,
    UITipsDialog,
    UIWithdrawal,
    UIGameRoomSetting,
    UIRegister,
}


