using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPanelTalk : UIWindow
{
    public List<TalkItemData> speakList;
    [SerializeField] ScrollRectList myScrollRect;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] GameObject talkItem;
    public GameObject expression;
    public InputField inputField;

    public void SendButttonClick()
    {
        if (inputField.text != "")
        {
            CMD_C_Chat chat = new CMD_C_Chat();
            string mes = "2|" + inputField.text;
            // chat.szTitle = NetUtil.StringToBytes(mes);
            chat.szTitle = new byte[100];
            byte[] bt = NetUtil.StringToBytes(mes);
            Array.Copy(bt, chat.szTitle, bt.Length);
            SocketSendManager.Instance.ChewTheRag(chat);
        }
    }

    public void ExpressionButtonClick()
    {
        expression.SetActive(true);
    }

    void Start()
    {
        ShowNormalTalk();
    }

    void InitScroll()
    {
        scrollRect.onValueChanged.AddListener(OnValueChange);
        myScrollRect.createitemobject = delegate(int index, UnityAction<GameObject> action)
        {
            if (talkItem != null)
            {
                GameObject cellObj = Instantiate(talkItem.gameObject);
                action(cellObj);
            }
        };
        myScrollRect.updateItem = delegate(ItemCell o, int index)
        {
            o.item.GetComponent<TalkItem>().Init(speakList[index], CloseUI);
        };
        myScrollRect.Init(speakList.Count);
    }

    void InitRecordScroll()
    {
        scrollRect.onValueChanged.AddListener(OnValueChange);
        myScrollRect.createitemobject = delegate(int index, UnityAction<GameObject> action)
        {
            if (talkItem != null)
            {
                GameObject cellObj = Instantiate(talkItem.gameObject);
                action(cellObj);
            }
        };
        myScrollRect.updateItem = delegate (ItemCell o, int index)
        {
            o.item.GetComponent<TalkItem>().InitLocal(speakList[index]);
        };
        myScrollRect.Init(speakList.Count);
    }

    void OnValueChange(Vector2 pos)
    {
        myScrollRect.OnValueChange(pos);
    }

    /// <summary>
    /// 聊天窗口
    /// </summary>
    public void ShowNormalTalk()
    {
        scrollRect.onValueChanged.RemoveAllListeners();
        speakList = TalkDataManager.Instance.List;
        InitScroll();
    }

    /// <summary>
    /// 聊天记录窗口
    /// </summary>
    public void ShowRecordTalk()
    {
        scrollRect.onValueChanged.RemoveAllListeners();
        speakList = TalkDataManager.Instance.RecordList;
        InitRecordScroll();
    }
}